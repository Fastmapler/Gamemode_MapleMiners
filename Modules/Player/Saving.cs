$MM::SaveLocation = "config/server/MM/";

function GameConnection::MM_SaveData(%client)
{
    if (!%client.hasSpawnedOnce)
        return;
        
    %saveList[%saveLists++] = "MM_PickaxeLevel\tMM_BatteryCharge\tMM_SpareBatteries\tMM_MaxSpareBatteries\tMM_MaxInvSlots";
    %saveList[%saveLists++] = "MM_DeathCount";

    %file = new FileObject();
    if(%file.openForWrite($MM::SaveLocation @ %client.bl_id @ ".txt"))
    {
        //Save most player data
        for (%i = 0; %i <= %saveLists; %i++)
        {
            for (%j = 0; %j < getFieldCount(%saveList[%i]); %j++)
            {
                %varName = getField(%saveList[%i], %j);
                %varData = get_var_obj(%client, %varName);

                if (%varData !$= "")
                    %file.writeLine(%varName TAB %varData);
            }
        }

        //Save materials
        for (%i = 0; %i < MatterData.getCount(); %i++)
        {
            %matter = MatterData.getObject(%i);
            %count = %client.getMaterial(%matter.name);
            if (%count > 0)
                %file.writeLine("MM_Materials" @ %matter.name TAB %count);
        }

        if (isObject(%player = %client.player))
        {
            //Save Tools
            for (%i = 0; %i < %client.GetMaxInvSlots(); %i++)
                if (isObject(%tool = %player.tool[%i]))
                    %file.writeLine("TOOL" TAB %i TAB %tool.getName());

            //Save Health
            %file.writeLine("DAMAGE" TAB %player.getDamageLevel());

            //Save Rads
            %file.writeLine("RADS" TAB (%player.MM_RadLevel + 0));
        }
        else
        {
            //Save Tools
            for (%i = 0; %i < %client.saveTools; %i++)
                if (isObject(%tool = %client.saveTool[%i]))
                    %file.writeLine("TOOL" TAB %i TAB %tool.getName());
        }
    }
    %file.close();
    %file.delete();
}

function GameConnection::MM_LoadData(%client)
{
    %file = new FileObject();
	%file.openForRead($MM::SaveLocation @ %client.bl_id @ ".txt");
	while(!%file.isEOF())
	{
		%line = %file.readLine();
        switch$ (getField(%line, 0))
        {
            case "DAMAGE":
                %client.playerDamage = getField(%line, 1);
            case "RADS":
                %client.playerRads = getField(%line, 1);
            case "TOOL":
                %client.saveTools = getField(%line, 1) + 1;
                %client.saveTool[getField(%line, 1)] = getField(%line, 2);
            default:
                set_var_obj(%client, getField(%line, 0), getFields(%line, 1, 256));
        }
	}

	%file.close();
	%file.delete();
}

package MM_SavingLoading
{
    function GameConnection::onClientLeaveGame(%client)
	{
        if (%client.hasSpawnedOnce)
            %client.MM_SaveData();

		parent::onClientLeaveGame(%client);
	}
    function GameConnection::createPlayer(%client, %trans)
	{
        if (!%client.hasSpawnedOnce)
        {
            %client.InitPlayerStats();
            schedule(2000, %client, "commandToClient", %client,'messageBoxOk',"WARNING!", "Death fees have been added! Talk to the 'Grim Reaper' at spawn to see the fee for dying.");
        }   
        
		Parent::createPlayer(%client, %trans);

        if (isObject(%player = %client.player))
        {
            %client.schedule(100, restoreTools);
            if (%client.playerDamage !$= "")
            {
                %player.setDamageLevel(%client.playerDamage);
                %client.playerDamage = "";
            }
            if (%client.playerRads !$= "")
            {
                %player.MM_RadLevel = %client.playerRads;
                %client.playerRads = "";
            }

            commandToClient(%client, 'PlayGui_CreateToolHud', %client.GetMaxInvSlots());
        }
        
           
    }
};
activatePackage("MM_SavingLoading");

function MM_Autosaver()
{
	cancel($MM::Autosaver);

	for (%i = 0; %i < ClientGroup.getCount(); %i++)
		ClientGroup.getObject(%i).MM_SaveData();

	$MM::Autosaver = schedule(60 * 1000, ClientGroup, "MM_Autosaver");
}
schedule(10, 0, "MM_Autosaver");