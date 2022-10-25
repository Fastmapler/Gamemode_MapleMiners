registerOutputEvent("GameConnection", "OpenBank", "", true);
function GameConnection::OpenBank(%client)
{
    if (!isObject(%player = %client.player))
        return;

    %bsm = new ScriptObject()
    {
        superClass = "BSMObject";
        class = "MM_bsmBank";
        
        title = "Loading...";
        format = "arial:24" TAB "\c2" TAB "\c6" TAB "\c2" TAB "\c7";

        entryCount = 0;

        hideOnDeath = true;
        deleteOnFinish = true;

        shopPosition = %player.getPosition();
    };

    if (!%client.MM_WarnBankControls)
    {
        %client.MM_WarnBankControls = true;
        %client.chatMessage("\c6Use [\c3Brick Shift Away/Towards\c6] keys to scroll though your Bank.");
        %client.chatMessage("\c6Use [\c3Plant Brick\c6] to select/take out an item.");
        %client.chatMessage("\c6Use [\c3Drop Item\c6] to insert your held item into the Bank.");
    }

    MissionCleanup.add(%bsm);

    %client.brickShiftMenuEnd();
    %client.brickShiftMenuStart(%bsm);

    %client.SOICheckDistance();
    %client.BankUpdateInterface();
}

function GameConnection::BankUpdateInterface(%client)
{
    if (!isObject(%bsm = %client.brickShiftMenu) || %bsm.class !$= "MM_bsmBank")
        return;

    %client.MM_MaxToolStorage = %client.MM_MaxToolStorage + 0;

    %client.ReorganizeBank();

    for (%i = 0; %i < %bsm.entryCount; %i++)
        %bsm.entry[%i] = "";

    %bsm.entryCount = 0;
    %bsm.entry[%bsm.entryCount] = "[Finish]" TAB "closeMenu"; %bsm.entryCount++;
    %bsm.entry[%bsm.entryCount] = "[Upgrade]" TAB "upgrade"; %bsm.entryCount++;
    

    %itemCount = 0;

    for (%i = 0; %i < %client.MM_MaxToolStorage; %i++)
	{
		%item = %client.MM_ToolStorage[%i];

        if (isObject(%item))
        {
            %bsm.entry[%bsm.entryCount] = %item.uiName TAB %i;
            %itemCount++;
        }
        else
            %bsm.entry[%bsm.entryCount] = "---" TAB "";

		%bsm.entryCount++;
	}

    %bsm.title = "<font:tahoma:16>\c3Tool Storage (" @ %itemCount @ "/" @ %client.MM_MaxToolStorage @ ")";
}

function MM_bsmBank::onUserMove(%obj, %client, %id, %move, %val)
{
	if(%move == $BSM::PLT && %id !$= "closeMenu" && isObject(%player = %client.player))
	{
        if (%id $= "upgrade")
        {
            %client.PurchaseUpgrade("Tool Storage");
        }
        else
        {
            %data = %client.MM_ToolStorage[%id];
            if (isObject(%data))
            {
                %item = new Item()
                {
                    datablock = %data.getID();
                    static    = "0";
                    position  = vectorAdd(%player.getPosition(), "0 0 1");
                };
                MissionCleanup.add(%item);

                %client.MM_ToolStorage[%id] = 0;
                %client.BankUpdateInterface();
            }
        }
        return;
	}

	Parent::onUserMove(%obj, %client, %id, %move, %val);
}

function GameConnection::ReorganizeBank(%client)
{
    %tempItemCount = 0;

    for (%i = 0; %i < %client.MM_MaxToolStorage; %i++)
	{
		%item = %client.MM_ToolStorage[%i];

        if (isObject(%item))
            %tempItems[%tempItemCount++] = %item.getName();
	}

    for (%i = 0; %i < %client.MM_MaxToolStorage; %i++)
		%client.MM_ToolStorage[%i] = "";

    for (%i = 1; %i <= %tempItemCount; %i++)
        %client.MM_ToolStorage[%i - 1] = %tempItems[%i];
}