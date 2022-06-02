registerOutputEvent("fxDTSBrick", "SellOres", "", true);
function fxDTSBrick::SellOres(%this, %client) { %client.SellOres(); }

function GameConnection::SellOres(%client)
{
    %sum = 0;
    for (%i = 0; %i < MatterData.getCount(); %i++)
    {
        %matter = MatterData.getObject(%i);

        if (%matter.value > 0 && !%matter.unsellable)
        {
            %count = %client.MM_Materials[%matter.name];
            %sum += %count * %matter.value;
            %client.MM_Materials[%matter.name] = 0;
        }
    }

    %client.chatMessage("\c6You sold all your valued materials for" SPC %sum @ "cr!");
    %client.MM_Materials["Credits"] += %sum;
}

function GameConnection::GetOreValueSum(%client)
{
    %sum = 0;
    for (%i = 0; %i < MatterData.getCount(); %i++)
    {
        %matter = MatterData.getObject(%i);
        if (%matter.value > 0 && !%matter.unsellable)
        {
            %count = %client.MM_Materials[%matter.name];
            %sum += %count * %matter.value;
        }
    }

    return %sum;
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
    %client.MM_Materials["Credits"] += %sum;
}

function GameConnection::GetPickUpgradeCost(%client)
{
    return PickaxeUpgradeCost(%client.MM_PickaxeLevel);
}

function PickaxeUpgradeCost(%val)
{
    return mFloor(4 * %val) + mFloor(0.25 * ((%val - 1) + 300 * mPow(1.2, (%val - 1) / 24))) - 23;
}

function GameConnection::UpgradePickaxe(%client)
{
    %cost = %client.GetPickUpgradeCost();
    if (%client.MM_Materials["Credits"] >= %cost)
    {
        %client.MM_Materials["Credits"] -= %cost;
        %client.MM_PickaxeLevel++;
    }
    else
    {
        %client.chatMessage("\c6You need atleast\c3" SPC (%cost - %client.MM_Materials["Credits"]) @ "\c6 more credits to upgrade to the next level!");
    }
}

datablock fxDTSBrickData(brickMMWorkbenchData)
{
	brickFile = "Add-Ons/Gamemode_MapleMiners/Modules/Environment/Bricks/Workbench.blb";
	category = "Special";
	subCategory = "Maple Miners";
	uiName = "Workbench";
	iconName = "Add-Ons/Gamemode_MapleMiners/Modules/Environment/Bricks/BrickGeneric";
};

function GameConnection::PurchaseItem(%client, %item)
{
    %costData = $MM::ItemCost[%item.getName()];
    
    if (%costData !$= "")
    {
        for (%i = 0; %i < getFieldCount(%costData); %i += 2)
            %text = %text @ %client.MM_Materials[getField(%costData, %i + 1)] @ "/" @ getField(%costData, %i) SPC getField(%costData, %i + 1) @ "<br>";

        %client.selectedPurchaseItem = %item;
        commandToClient(%client,'messageBoxYesNo',"Purchasing", "[" @ %item.uiName @ "]<br>Purchase cost:<br>---<br>" @ %text @ "---<br>Purchase this item?", 'PurchaseItemAccept','PurchaseItemCancel');
    }
}