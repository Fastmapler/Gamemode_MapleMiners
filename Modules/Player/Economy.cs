registerOutputEvent("GameConnection", "ShowSellOres", "", true);
function GameConnection::ShowSellOres(%client) { %client.SellOresInterface(); }

function GameConnection::SellOres(%client, %maxAmount, %type)
{
    if (%maxAmount $= "")
        %maxAmount = 999999;
    
    if (%type !$= "")
    {
        %matter = GetMatterType(%type);
        %count = getMin(%client.GetMaterial(%matter.name), %maxAmount);
        %sum = uint_mul(%count, GetMatterValue(%matter));
        %client.SubtractMaterial(%count, %matter.name);
        %client.chatMessage("\c6You sold " @ %count SPC %matter.name @ " for" SPC %sum @ "cr!");
        %client.AddMaterial(%sum, "Credits");
    }
    else
    {
        %sum = 0;
        for (%i = 0; %i < MatterData.getCount(); %i++)
        {
            %matter = MatterData.getObject(%i);

            if (GetMatterValue(%matter) > 0 && !%matter.unsellable)
            {
                %count = getMin(%client.GetMaterial(%matter.name), %maxAmount);
                %sum = uint_add(%sum, uint_mul(%count, GetMatterValue(%matter)));
                %client.SubtractMaterial(%count, %matter.name);
            }
        }

        %client.chatMessage("\c6You sold your valued materials for" SPC %sum @ "cr!");
        %client.AddMaterial(%sum, "Credits");

        %client.brickShiftMenuEnd();
    }
    
}

function GameConnection::GetOreValueSum(%client)
{
    %sum = 0;
    for (%i = 0; %i < MatterData.getCount(); %i++)
    {
        %matter = MatterData.getObject(%i);
        if (GetMatterValue(%matter) > 0 && !%matter.unsellable)
        {
            %sum = uint_add(%sum, uint_mul(%client.GetMaterial(%matter.name), GetMatterValue(%matter)));
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
    %client.UpgradePickaxe(%this);
}

function GameConnection::GetPickUpgradeCost(%client)
{
    return PickaxeUpgradeCost(%client.MM_PickaxeLevel);
}

$MM::PickaxeUpgradeCostSum = 5;
function PickaxeUpgradeCostSum(%val)
{
    %val--;
    if (%val < 5)
        return 25;

    if ($MM::PickaxeUpgradeCostSum[%val] !$= "")
        return $MM::PickaxeUpgradeCostSum[%val];

    for (%i = $MM::PickaxeUpgradeCostSum; %i <= %val; %i++)
        $MM::PickaxeUpgradeCostSum[%i] = bigint_add($MM::PickaxeUpgradeCostSum[%i - 1], PickaxeUpgradeCost(%i));

        $MM::PickaxeUpgradeCostSum = %i - 1;

    return $MM::PickaxeUpgradeCostSum[%val];
}

$MM::UpgradeLogMod = mLog(1.09);
function PickaxeUpgradeCost(%val)
{
    if (%val < 5)
        return 25;

    if (%val > 2222)
        return 999999;

    if ($MM::PickaxeUpgradeCost[%val] > 0)
        return $MM::PickaxeUpgradeCost[%val];
        
    $MM::PickaxeUpgradeCost[%val] = getMin(mFloor(4 * %val) + mFloor(0.25 * ((%val - 1) + 300 * mPow(1.2, mLog((%val - 1) / 25) / $MM::UpgradeLogMod))) + 54, 999999);

    return $MM::PickaxeUpgradeCost[%val];
}

$MM::UpgradeLogModOld = mLog(1.08);
function PickaxeUpgradeCostOld(%val)
{
    if (%val < 1)
        return 50;
        
    return mFloor(4 * %val) + mFloor(0.25 * ((%val - 1) + 300 * mPow(1.2, mLog((%val - 1) / 25) / $MM::UpgradeLogModOld))) + 54; //0.0769611 = ln(1.08)
}

function UpdatePlayerMoney()
{
	%loc = $MM::SaveLocation @ "*.txt";
	for(%dir = findFirstFile(%loc); %dir !$= ""; %dir = findNextFile(%loc))
    {
		%configDir = %dir;

		if(isFile(%configDir))
        {
			%file = new fileObject();
            %file.openForRead(%configDir);

            $MM::ConvertLines::Count = 0;
            while(!%file.isEOF()) {
                %line = trim(%file.readLine());

                if(%line $= "")
                    continue;

                %field = getField(%line, 0);
                $MM::ConvertLines::Value[%field] = getFields(%line, 1, 99);

                if (%field $= "MM_MaterialsCredits" && $MM::ConvertLines::Value["MM_PickaxeLevel"] > 5)
                {
                    %sum = 0;
                    for (%i = 0; %i < $MM::ConvertLines::Value["MM_PickaxeLevel"]; %i++)
                        %sum = uInt_add(%sum, uint_sub(PickaxeUpgradeCostOld(%i), PickaxeUpgradeCost(%i)));

                    %line = "MM_MaterialsCredits" TAB uInt_add(%sum, getField(%line, 1));
                    echo(%configDir @ " LVL " @ $MM::ConvertLines::Value["MM_PickaxeLevel"] @ ", +" @ %sum @ "cr");
                }
                $MM::ConvertLines::List[$MM::ConvertLines::Count] = %line;
                $MM::ConvertLines::Count++;
            }
            %file.close();
            %file.delete();

            %file = new FileObject();

            if(%file.openForWrite(%configDir))
                for (%i = 0; %i < $MM::ConvertLines::Count; %i++)
                    %file.writeLine($MM::ConvertLines::List[%i]);

            %file.close();
            %file.delete();

            deleteVariables("$MM::ConvertLines*");
        }
	}
}

function GameConnection::UpgradePickaxe(%client, %brick)
{
    %cost = %client.GetPickUpgradeCost();
    if (%client.GetMaterial("Credits") >= %cost)
    {
        %client.SubtractMaterial(%cost, "Credits");
        %client.MM_PickaxeLevel++;

        if (isObject(%brick))
        {
            %brick.spawnExplosion(upgradeExplosionProjectile, 0.5);
            %brick.playSound(UpgradePickaxeSound);
        }
            
    }
    else
    {
        %client.chatMessage("\c6You need atleast\c3" SPC uint_sub(%cost, %client.GetMaterial("Credits")) @ "\c6 more credits to upgrade to the next level!");
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
            %text = %text @ %client.GetMaterial(getField(%costData, %i + 1)) @ "/" @ getField(%costData, %i) SPC getField(%costData, %i + 1) @ "<br>";

        %client.selectedPurchaseItem = %item;

        if ($MM::ItemDisc[%item.getName()] !$= "")
            %disc = "<br>" @ $MM::ItemDisc[%item.getName()];

        commandToClient(%client,'messageBoxYesNo',"Purchasing", "[" @ %item.uiName @ "]" @ %disc @ "<br>Purchase cost:<br>---<br>" @ %text @ "---<br>Purchase this item?", 'PurchaseItemAccept','PurchaseItemCancel');
    }
}

function ServerCmdPurchaseItemAccept(%client)
{
    if (!isObject(%player = %client.player))
        return;

    %costData = $MM::ItemCost[%client.selectedPurchaseItem.getName()];

    for (%i = 0; %i < getFieldCount(%costData); %i += 2)
    {
        if (%client.GetMaterial(getField(%costData, %i + 1)) < getField(%costData, %i))
        {
            %client.chatMessage("You need more " @ getField(%costData, %i + 1) @ "!");
            ServerCmdPurchaseItemCancel(%client);
            return;
        }
    }

    for (%i = 0; %i < getFieldCount(%costData); %i += 2)
        %client.SubtractMaterial(getField(%costData, %i), getField(%costData, %i + 1));

    %item = new Item()
    {
        datablock = %client.selectedPurchaseItem;
        static    = "0";
        position  = vectorAdd(%player.getPosition(), "0 0 1");
        craftedItem = true;
    };

    ServerCmdPurchaseItemCancel(%client);
}

function ServerCmdPurchaseItemCancel(%client)
{
    %client.selectedPurchaseItem = "";
}

package MM_Economy
{
    function Armor::onCollision(%data,%this,%col,%vec,%vel)
	{
		if(%col.getClassName() $= "Item")
		{
            if (isObject(%col.spawnBrick) && $MM::ItemCost[%col.getDatablock().getName()] !$= "")
            {
                if(!%this.client.messagedAboutCTPU)
                {
                    messageClient(%this.client,'',"This item must be purchased. Click it with an empty hand to see its price and buy it.");
                    %this.client.messagedAboutCTPU = 1;
                }
                return 0;
            }
            else
            {
                %this.pickup(%col);
                return 0;
            }
                
		}
		return Parent::onCollision(%data,%this,%col,%vec,%vel);
	}
    function Armor::onTrigger(%data, %obj, %trig, %tog)
	{
		if(isObject(%client = %obj.client))
		{
			if(%trig == 0 && %tog && !isObject(%obj.getMountedImage(0)))
			{
				%eye = %obj.getEyePoint();
				%dir = %obj.getEyeVector();
				%for = %obj.getForwardVector();
				%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
				%mask = $Typemasks::FxBrickObjectType | $Typemasks::TerrainObjectType | $TypeMasks::ItemObjectType;
				%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, 5)), %mask, %obj);
				if(isObject(%hit = firstWord(%ray)) && %hit.getClassName() $= "Item")
				{
                    %itemData = %hit.getDataBlock();
					for(%x = 0; %x < %data.maxTools; %x++)
                    {
                        if(isObject(%obj.tool[%x]) && %obj.tool[%x].getID() == %itemData.getID())
						{
							%copy = 1;
							break;
						}
                    }

                    if(%hit.canPickup && isObject(%hit.spawnBrick) && $MM::ItemCost[%itemData.getName()] !$= "") // && !%copy
                        %client.PurchaseItem(%itemData);
				}
			}
		}
		Parent::onTrigger(%data, %obj, %trig, %tog);
	}
};
activatePackage("MM_Economy");

function GameConnection::SellOresInterface(%client)
{
    if (!isObject(%player = %client.player))
        return;

    %client.sellOreStack = 1;

    %bsm = new ScriptObject()
	{
		superClass = "BSMObject";
        class = "MM_bsmSellOres";
        
		title = "Loading...";
		format = "arial:24" TAB "\c2" TAB "<div:1>\c6" TAB "<div:1>\c2" TAB "\c7";

		entry[0]  = "[Finish]" TAB "closeMenu";
		entry[1]  = "[Sell All]"  TAB "sellAll";

        hideOnDeath = true;
        deleteOnFinish = true;

		entryCount = 2;

        shopPosition = %player.getPosition();
	};

    if (!%client.MM_WarnSellingControls)
	{
		%client.MM_WarnSellingControls = true;
		%client.chatMessage("\c6Use [\c3Brick Shift Away/Towards\c6] keys to scroll the interface's menu.");
        %client.chatMessage("\c6Use [\c3Rotate Brick\c6] keys to change amount of ores to sell.");
		%client.chatMessage("\c6Use [\c3Plant Brick\c6] key to confirm a selection and sell ores.");
	}

	for (%i = 0; %i < MatterData.getCount(); %i++)
	{
		%matter = MatterData.getObject(%i);
        %count = %client.GetMaterial(%matter.name);
		if (GetMatterValue(%matter) <= 0 || %matter.unsellable || %count <= 0)
			continue;

		%bsm.entry[%bsm.entryCount] = %matter.name SPC "x" @ %count TAB %matter.name;
		%bsm.entryCount++;
	}

    MissionCleanup.add(%bsm);

	%client.brickShiftMenuEnd();
	%client.brickShiftMenuStart(%bsm);

    %client.SOIUpdateInterface();
    %client.SOICheckDistance();
}

function GameConnection::SOIUpdateInterface(%client)
{
    if (!isObject(%bsm = %client.brickShiftMenu) || %bsm.class !$= "MM_bsmSellOres")
        return;

    for (%i = 2; %i < %bsm.entryCount; %i++)
        %bsm.entry[%i] = "";

    %bsm.entryCount = 2;

    for (%i = 0; %i < MatterData.getCount(); %i++)
	{
		%matter = MatterData.getObject(%i);
        %count = %client.GetMaterial(%matter.name);
		if (GetMatterValue(%matter) <= 0 || %matter.unsellable || %count <= 0)
			continue;

		%bsm.entry[%bsm.entryCount] = %matter.name SPC "x" @ %count TAB %matter.name;
		%bsm.entryCount++;
	}

    %bsm.title = "<font:tahoma:24>\c3Whatcha wanna sell? (" @ %client.sellOreStack @ "x)";
}

function GameConnection::SOICheckDistance(%client)
{
    cancel(%client.CheckBSMMenuDistance);

    if (!isObject(%player = %client.player) || vectorDist(%player.getPosition(), %client.brickShiftMenu.shopPosition) > 8)
    {
        %client.brickShiftMenuEnd();
        return;
    }
    %client.CheckBSMMenuDistance = %client.schedule(100, "SOICheckDistance");
}

function ServerCmdSellAllAccept(%client)
{
    if (!isObject(%bsm = %client.brickShiftMenu) || %bsm.class !$= "MM_bsmSellOres")
        return;

    %client.SellOres();
    %client.SOIUpdateInterface();
}

function MM_bsmSellOres::onUserMove(%obj, %client, %id, %move, %val)
{
    %client.SOICheckDistance();
    
	if(%move == $BSM::PLT && %id !$= "closeMenu")
	{
		if (%id $= "sellAll")
		{
            commandToClient(%client,'messageBoxYesNo',"Sell All Materials", "Are you sure you want to sell all materials?<br><color:ff0000>This includes rare ores!", 'SellAllAccept','');
		}
		else if (isObject(%matter = GetMatterType(%id)) && %client.GetMaterial(%matter.name) > 0)
		{
			%client.SellOres(%client.sellOreStack, %matter.name);
            %client.SOIUpdateInterface();
		}
        return;
	}
    else if (%move == $BSM::ROT)
    {
        if (%val == 1)
        {
            switch (%client.sellOreStack)
            {
                case 1: %client.sellOreStack = 2;
                case 2: %client.sellOreStack = 5;
                case 5: %client.sellOreStack = 10;
                case 10: %client.sellOreStack = 20;
                case 20: %client.sellOreStack = 1000;
                default: %client.sellOreStack = 1;
            }
        }
        else
        {
            switch (%client.sellOreStack)
            {
                case 1000: %client.sellOreStack = 20;
                case 20: %client.sellOreStack = 10;
                case 10: %client.sellOreStack = 5;
                case 5: %client.sellOreStack = 2;
                case 2: %client.sellOreStack = 1;
                default: %client.sellOreStack = 1000;
            }
        }
        %client.SOIUpdateInterface();
    }

	Parent::onUserMove(%obj, %client, %id, %move, %val);
}

$MM::EconomyYield = 1.00;
$MM::EconomyYieldMax = 1.25;
$MM::EconomyYieldMin = 0.75;

function MM_ChangeYield(%change, %reason, %client, %bypass)
{
    $MM::EconomyYield += %change;

    if (!%bypass)
    {
        if ($MM::EconomyYield >= $MM::EconomyYieldMax)
        {
            $MM::EconomyYield = $MM::EconomyYieldMax;
            %text = "(MAX)";
        }
        else if ($MM::EconomyYield <= $MM::EconomyYieldMin)
        {
            $MM::EconomyYield = $MM::EconomyYieldMin;
            %text = "(MIN)";
        }
    }
    
    messageAll('MsgAdminForce', "\c6" @ %client.netName SPC %reason @ ", and changed the economic yield by" SPC (%change * 100) @ "\%. Yield is now" SPC ($MM::EconomyYield * 100) @ "\%." SPC %text);
}