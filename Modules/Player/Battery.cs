$MM::MaxBatteryCharge = 10000;

function GameConnection::ChangeBatteryEnergy(%client, %change)
{
    %change = mRound(%change);

    if (%change < 0)
    {
        if (%client.MM_BatteryCharge <= 0 && %client.MM_SpareBatteries <= 0)
            return false;

        while (%change < 0)
        {
            %chargeDiff = getMax(%client.MM_BatteryCharge * -1, %change);
            %client.MM_BatteryCharge += %chargeDiff;
            %change -= %chargeDiff;
            
            if (%client.MM_BatteryCharge <= 0)
            {
                if (%client.MM_SpareBatteries > 0)
                {
                    %client.MM_BatteryCharge += $MM::MaxBatteryCharge;
                    %client.MM_SpareBatteries--;
                }
                else
                {
                    %client.MM_BatteryCharge = 0;
                    break;
                }
            }
        }

        return true;
    }
    else if (%change > 0)
    {
        if (%client.MM_BatteryCharge >= $MM::MaxBatteryCharge && %client.MM_SpareBatteries >= %client.MM_MaxSpareBatteries)
            return false;

        while (%change > 0)
        {
            %chargeDiff = getMin($MM::MaxBatteryCharge - %client.MM_BatteryCharge, %change);
            %client.MM_BatteryCharge += %chargeDiff;
            %change -= %chargeDiff;

            if (%client.MM_BatteryCharge >= $MM::MaxBatteryCharge)
            {
                if (%client.MM_SpareBatteries < %client.MM_MaxSpareBatteries && %change > 0)
                {
                    %client.MM_BatteryCharge -= $MM::MaxBatteryCharge;
                    %client.MM_SpareBatteries++;
                }
                else
                {
                    %client.MM_BatteryCharge = $MM::MaxBatteryCharge;
                    break;
                }
            }
        }

        return true;
    }
}

//Would just use an equation but that would be too much with the different material costs.
$MM::UpgradeCost["Battery", 0] = "250\tCredits";
$MM::UpgradeCost["Battery", 1] = "1000\tCredits\t5\tGraphite\t5\tLithium";
$MM::UpgradeCost["Battery", 2] = "4000\tCredits\t10\tGraphite\t10\tLithium\t5\tLead\t5\tSilver";
$MM::UpgradeCost["Battery", 3] = "16000\tCredits\t5\tNeodymium\t5\tUranium";
$MM::UpgradeCost["Battery", 4] = "64000\tCredits\t10\tNeodymium\t10\tUranium\t5\tPalladium\t5\tThorium";

$MM::UpgradeCost["Inventory", 5] = "500\tCredits";
$MM::UpgradeCost["Inventory", 6] = "2000\tCredits\t";

function GameConnection::GetUpgradeLevel(%client, %upgrade)
{
    switch$ (%upgrade)
    {
        case "Battery": return %client.MM_MaxSpareBatteries;
        case "Inventory": return %client.GetMaxInvSlots();
    }

    return -1;
}

registerOutputEvent("GameConnection", "PurchaseUpgrade", "string 200 156");
function GameConnection::PurchaseUpgrade(%client, %upgrade)
{
    %costData = $MM::UpgradeCost[%upgrade, %client.GetUpgradeLevel(%upgrade)];
    
    if (%costData !$= "")
    {
        for (%i = 0; %i < getFieldCount(%costData); %i += 2)
            %text = %text @ %client.MM_Materials[getField(%costData, %i + 1)] @ "/" @ getField(%costData, %i) SPC getField(%costData, %i + 1) @ "<br>";

        %client.selectedUpgradeItem = %upgrade;
        commandToClient(%client,'messageBoxYesNo',"Upgrade", "[" @ %upgrade @ " Upgrade]<br>Upgrade cost:<br>---<br>" @ %text @ "---<br>Upgrade this ability?", 'UpgradeItemAccept','UpgradeItemCancel');
    }
    else
    {
        %client.chatMessage("\c6You maxed out your" SPC %upgrade SPC "ability!");
    }
}

function ServerCmdUpgradeItemAccept(%client)
{
    %upgrade = %client.selectedUpgradeItem;
    %costData = $MM::UpgradeCost[%upgrade, %client.GetUpgradeLevel(%upgrade)];

    if (!isObject(%player = %client.player) || %costData $= "")
        return;

    for (%i = 0; %i < getFieldCount(%costData); %i += 2)
    {
        if (%client.MM_Materials[getField(%costData, %i + 1)] < getField(%costData, %i))
        {
            %client.chatMessage("You need more " @ getField(%costData, %i + 1) @ "!");
            ServerCmdUpgradeItemCancel(%client);
            return;
        }
    }

    for (%i = 0; %i < getFieldCount(%costData); %i += 2)
        %client.MM_Materials[getField(%costData, %i + 1)] -= getField(%costData, %i);

    switch$ (%upgrade)
    {
        case "Battery":
            %client.MM_MaxSpareBatteries++;
            %client.MM_SpareBatteries++;
            %client.chatMessage("You got a new battery! You now have " @ %client.MM_MaxSpareBatteries @ " spare batteries.");
        case "Inventory":
            %slots = %client.GetMaxInvSlots();
            %client.SetMaxInvSlots(%slots + 1);
            %client.chatMessage("You got an extra inventory slot! You now have " @ (%slots + 1) @ " total slots.");
    }

    ServerCmdUpgradeItemCancel(%client);
}

function ServerCmdUpgradeItemCancel(%client)
{
    %client.selectedUpgradeItem = "";
}

//Battery Pickups
function MM_BatteryPickup(%data, %player)
{
    if (!isObject(%client = %player.client) || %data.rechargeValue <= 0)
        return;

    return %client.ChangeBatteryEnergy(%data.rechargeValue);
}

datablock ItemData(MM_BatteryPackT1Item)
{
	category = "Weapon";
	className = "Weapon";
	shapeFile = "./Shapes/Battery.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Basic Battery Pack";
	doColorShift = true;
	colorShiftColor = "0.309 0.286 0.294 1.000";
	canDrop = true;
    pickupFunc = "MM_BatteryPickup";
    rechargeValue = $MM::MaxBatteryCharge;
};

datablock ItemData(MM_BatteryPackT2Item : MM_BatteryPackT1Item)
{
	uiName = "Improved Battery Pack";
	colorShiftColor = "0.847 0.819 0.800 1.000";
    rechargeValue = $MM::MaxBatteryCharge * 3;
};

datablock ItemData(MM_BatteryPackT3Item : MM_BatteryPackT1Item)
{
	uiName = "Superior Battery Pack";
	colorShiftColor = "0.121 0.337 0.549 1.000";
    rechargeValue = $MM::MaxBatteryCharge * 9;
};