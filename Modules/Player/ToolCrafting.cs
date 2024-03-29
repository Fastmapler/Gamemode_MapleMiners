registerOutputEvent("fxDTSBrick", "CheckToolCrafting", "", true);
function fxDTSBrick::CheckToolCrafting(%this, %client)
{
    if (!isObject(%player = %client.player))
        return;

    if (!%client.MM_WarnAnvilControls)
	{
		%client.MM_WarnAnvilControls = true;
		%client.chatMessage("\c6Use [\c3Drop Tool\c6] key \c4on the Anvil\c6 to input your equipped tool's inventory slot into the anvil.");
		%client.chatMessage("\c6Use [\c3Primary Fire\c6] key to craft the output item, if there is one.");
	}

    %slot0 = "---";
    if (getField(%player.MM_ToolCraftItems, 0) !$= "")
        %slot0 = %player.tool[getField(%player.MM_ToolCraftItems, 0)].uiName @ " (Slot " @ getField(%player.MM_ToolCraftItems, 0) @ ")";

    %slot1 = "---";
    if (getField(%player.MM_ToolCraftItems, 1) !$= "")
        %slot1 = %player.tool[getField(%player.MM_ToolCraftItems, 1)].uiName @ " (Slot " @ getField(%player.MM_ToolCraftItems, 1) @ ")";

    %output = "???";
    if (isObject(%tool = %player.GetToolCraftingOutput()))
        %output = %tool.uiName;

    %client.MM_CenterPrint(%slot0 @ "<br>\c6+<br>\c6" @ %slot1 @ "<br>\c6=<br>\c6" @ %output, 3);
    %client.LastToolCraftingCheck = getSimTime();
}

registerOutputEvent("fxDTSBrick", "AttemptToolCrafting", "", true);
function fxDTSBrick::AttemptToolCrafting(%this, %client)
{
    if (!isObject(%player = %client.player))
        return;

    if (getSimTime() - %client.LastToolCraftingCheck > 3000)
    {
        %client.MM_CenterPrint("Touch the anvil first to see what you are making!", 3);
        return;
    }

    if (!isObject(%item = %player.GetToolCraftingOutput()))
    {
        %client.chatMessage("No recipe found!");
        return;
    }

    if ($MM::ItemDisc[%item.getName()] !$= "")
            %disc = "<br>" @ $MM::ItemDisc[%item.getName()];

    commandToClient(%client,'messageBoxYesNo',"Tool Crafting", "[" @ %item.uiName @ "]" @ %disc @ "<br>Craft this item?", 'CraftItemAccept','');
}

function ServerCmdCraftItemAccept(%client)
{
    if (!isObject(%player = %client.player))
        return;
        
    if (!isObject(%item = %player.GetToolCraftingOutput()))
    {
        %client.chatMessage("No recipe found!");
        return;
    }

	%player.RemoveTool(getField(%player.MM_ToolCraftItems, 0));
    %player.RemoveTool(getField(%player.MM_ToolCraftItems, 1));

    %item = new Item()
    {
        datablock = %item;
        static    = "0";
        position  = vectorAdd(%player.getPosition(), "0 0 1");
        craftedItem = true;
    };
}

function Player::GetToolCraftingOutput(%player)
{
    %tool0 = "";
    if (isObject(%obj0 = %player.tool[getField(%player.MM_ToolCraftItems, 0)]))
        %tool0 = %obj0.getName();

    %tool1 = "";
    if (isObject(%obj1 = %player.tool[getField(%player.MM_ToolCraftItems, 1)]))
        %tool1 = %obj1.getName();

    if ($MM::ToolCraftingRecipe[%tool0, %tool1] !$= "")
        return $MM::ToolCraftingRecipe[%tool0, %tool1];
    if ($MM::ToolCraftingRecipe[%tool1, %tool0] !$= "")
        return $MM::ToolCraftingRecipe[%tool1, %tool0];

    return "";
}

package MM_ItemCrafting
{
    function ServerCmdDropTool(%client, %position)
    {
        if (isObject(%player = %client.player))
        {
            %eye = %player.getEyePoint();
            %dir = %player.getEyeVector();
            %for = %player.getForwardVector();
            %face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
            %mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
            %ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, 5)), %mask, %obj);
            if(isObject(%hit = firstWord(%ray))&& isObject(%item = %player.tool[%position]))
            {
                %hitName = %hit.getDataBlock().getName();
                if (%hitName $= "brickMMAnvilData")
                {
                    if (getFieldCount(%player.MM_ToolCraftItems) >= 2 || hasField(%player.MM_ToolCraftItems, %position))
                    %player.MM_ToolCraftItems = "";

                    %player.MM_ToolCraftItems = trim(%player.MM_ToolCraftItems TAB %position);
                    %hit.CheckToolCrafting(%client);
                    return;
                }
                if (%hitName $= "brickMMToolStorageData")
                {
                    %client.MM_MaxToolStorage = %client.MM_MaxToolStorage + 0;

                    for (%i = 0; %i < %client.MM_MaxToolStorage; %i++)
                    {
                        %item = %client.MM_ToolStorage[%i];

                        if (!isObject(%item))
                        {
                            %client.MM_ToolStorage[%i] = %player.tool[%position].getName();
                            %client.chatMessage("\c6You stored your \c3" @ %player.tool[%position].uiName @ "\c6!");

                            %player.tool[%position] = 0;
                            %player.weaponCount--;
                            messageClient(%client, 'MsgItemPickup', '', %position,0);
                            serverCmdUnUseTool(%client);

                            %client.BankUpdateInterface();

                            %success = true;
                            break;
                        }
                    }

                    if (!%success)
                        %client.chatMessage("\c0You ran out of storage in your tool storage! To upgrade your storage, open your storage and use [Upgrade].");

                    return;
                }
                else if (%hitName $= "brickMMRecyclerData")
                {
                    if (%item.recycleLoot !$= "")
                    {
                        for (%i = 0; %i < getFieldCount(%item.recycleLoot); %i += 2)
                        {
                            %amount = getField(%item.recycleLoot, %i);
                            %name = getField(%item.recycleLoot, %i + 1);
                            %client.chatMessage("\c6+" @ %amount SPC %name);
                            %client.AddMaterial(%amount, %name);
                        }
                        %client.chatMessage("\c2You recycled the " @ %item.uiName @ "!");

                        %player.tool[%position] = 0;
                        %player.weaponCount--;
                        messageClient(%client, 'MsgItemPickup', '', %position,0);
                        serverCmdUnUseTool(%client);
                    }
                    else
                        %client.chatMessage("\c6The " @ %item.uiName @ " can't be recycled!");

                    return;
                }
            }
        }

        Parent::ServerCmdDropTool(%client, %position);
    }
};
activatePackage("MM_ItemCrafting");