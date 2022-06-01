registerOutputEvent("fxDTSBrick", "SellOres", "", true);
function fxDTSBrick::SellOres(%this, %client) { %client.SellOres(); }

function GameConnection::SellOres(%client)
{
    %sum = 0;
    for (%i = 0; %i < MatterData.getCount(); %i++)
    {
        %matter = MatterData.getObject(%i);
        if (%matter.value > 0)
        {
            %count = %client.MM_Materials[%matter.name];
            %sum += %count * %matter.value;
            %client.MM_Materials[%matter.name] = 0;
        }
    }

    %client.chatMessage("\c6You sold all your valued materials for" SPC %sum @ "cr!");
    %client.MM_Credits += %sum;
}

registerOutputEvent("fxDTSBrick", "CheckPickUpgradeCost", "", true);
function fxDTSBrick::CheckPickUpgradeCost(%this, %client)
{
    %client.MM_CenterPrint("Pickaxe level upgrade cost:<br>\c3" @ %client.GetPickUpgradeCost() @ "\c6cr<br>\c6Click the workbench to upgrade!", 3);
    %client.LastPickPriceCheck = getSimTime();
}

registerOutputEvent("fxDTSBrick", "AttemptPickUpgrade", "", true);
function fxDTSBrick::AttemptPickUpgrade(%this, %client)
{
    if (getSimTime() - %client.LastPickPriceCheck > 3000)
    {
        %client.MM_CenterPrint("Touch the workbench first to see the upgrade cost!", 3);
        return;
    }
    %client.UpgradePickaxe();
}

function GameConnection::SellOres(%client)
{
    %sum = 0;
    for (%i = 0; %i < MatterData.getCount(); %i++)
    {
        %matter = MatterData.getObject(%i);
        if (%matter.value > 0)
        {
            %count = %client.MM_Materials[%matter.name];
            %sum += %count * %matter.value;
            %client.MM_Materials[%matter.name] = 0;
        }
    }

    %client.chatMessage("\c6You sold all your valued materials for" SPC %sum @ "cr!");
    %client.MM_Credits += %sum;
}

function GameConnection::GetPickUpgradeCost(%client)
{
    %money = mFloor(0.25 * ((%client.MM_PickaxeLevel - 1) + 300 * mPow(1.2, (%client.MM_PickaxeLevel - 1) / 24)));
    return %money;
}

function GameConnection::UpgradePickaxe(%client)
{
    %cost = %client.GetPickUpgradeCost();
    if (%client.MM_Credits >= %cost)
    {
        %client.MM_Credits -= %cost;
        %client.MM_PickaxeLevel++;
    }
    else
    {
        %client.chatMessage("\c6You need atleast\c3" SPC (%cost - %client.MM_Credits) @ "\c6 more credits to upgrade to the next level!");
    }
}

datablock fxDTSBrickData(brickMMBrickBoxData)
{
	brickFile = "Add-Ons/Gamemode_MapleMiners/Modules/Environment/Bricks/Workbench.blb";
	category = "Special";
	subCategory = "Maple Miners";
	uiName = "Workbench";
	iconName = "Add-Ons/Gamemode_MapleMiners/Modules/Environment/Bricks/BrickGeneric";
};