//T1
datablock ItemData(MM_LootCacheT1Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Shapes/BoxItem.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Basic Loot Cache";
	iconName = "./Shapes/icon_Cache";
	doColorShift = true;
	colorShiftColor = "0.309 0.286 0.294 1.000";

	 // Dynamic properties defined by the scripts
	image = MM_LootCacheT1Image;
	canDrop = true;

    recycleLoot = "4\tFrame Parts";
};

datablock ShapeBaseImageData(MM_LootCacheT1Image)
{
    shapeFile = "./Shapes/BoxItem.dts";
    emap = true;

    mountPoint = 0;
    offset = "-0.5 0.5 0";
    rotation = eulerToMatrix( "0 0 0" );

    className = "WeaponImage";

    item = MM_LootCacheT1Item;
    ammo = " ";

    melee = false;
    armReady = true;

    doColorShift = MM_LootCacheT1Item.doColorShift;
    colorShiftColor = MM_LootCacheT1Item.colorShiftColor;

    stateName[0]					= "Activate";
    stateTimeoutValue[0]			= 0.5;
    stateTransitionOnTimeout[0]		= "Ready";
    stateSound[0]					= weaponSwitchSound;

    stateName[1]					= "Ready";
    stateTransitionOnTriggerDown[1]	= "Charge";
    stateAllowImageChange[1]		= true;

    stateName[2]					= "Charge";
    stateTransitionOnTimeout[2]		= "Fire";
    stateTimeoutValue[2]			= 0.2;
    stateFire[2]					= true;
    stateScript[2]					= "onCharge";
    stateWaitForTimeout[2]			= true;
    stateAllowImageChange[2]		= false;

    stateName[3]					= "Fire";
    stateTransitionOnTimeout[3]		= "Ready";
    stateTimeoutValue[3]			= 0.5;
    stateFire[3]					= true;
    stateScript[3]					= "onFire";
    stateWaitForTimeout[3]			= true;
    stateAllowImageChange[3]		= false;
};

function MM_LootCacheT1Image::onMount(%this, %obj, %slot) { %obj.playThread(0, "armReadyBoth"); }

function MM_LootCacheT1Image::onUnMount(%this, %obj, %slot) { %obj.playThread(0, "root"); }

function MM_LootCacheT1Image::onCharge(%this, %obj, %slot) { %obj.cacheSlot = %obj.currTool; %obj.playThread(0, "shiftDown"); }

function MM_LootCacheT1Image::onFire(%this, %obj, %slot, %bonus)
{
    if (!isObject(%client = %obj.client))
        return;

	%client.MMLootCacheReward_Roll(%this, %obj, %slot, 1, 8, "Compressed Dirt");
}

//T2
datablock ItemData(MM_LootCacheT2Item : MM_LootCacheT1Item)
{
	uiName = "Improved Loot Cache";
	colorShiftColor = "0.847 0.819 0.800 1.000";
	image = MM_LootCacheT2Image;

    recycleLoot = "8\tFrame Parts\t2\tMechanism Parts";
};

datablock ShapeBaseImageData(MM_LootCacheT2Image : MM_LootCacheT1Image)
{
    item = MM_LootCacheT2Item;
    doColorShift = MM_LootCacheT2Item.doColorShift;
    colorShiftColor = MM_LootCacheT2Item.colorShiftColor;
};

function MM_LootCacheT2Image::onMount(%this, %obj, %slot) { %obj.playThread(0, "armReadyBoth"); }

function MM_LootCacheT2Image::onUnMount(%this, %obj, %slot) { %obj.playThread(0, "root"); }

function MM_LootCacheT2Image::onCharge(%this, %obj, %slot) { %obj.cacheSlot = %obj.currTool; %obj.playThread(0, "shiftDown"); }

function MM_LootCacheT2Image::onFire(%this, %obj, %slot)
{
    if (!isObject(%client = %obj.client))
        return;

	%client.MMLootCacheReward_Roll(%this, %obj, %slot, 2, 8, "Compressed Stone");
}

//T3
datablock ItemData(MM_LootCacheT3Item : MM_LootCacheT1Item)
{
	uiName = "Superior Loot Cache";
	colorShiftColor = "0.121 0.337 0.549 1.000";
	image = MM_LootCacheT3Image;

    recycleLoot = "8\tFrame Parts\t4\tMechanism Parts\t2\tCircuitry Parts";
};

datablock ShapeBaseImageData(MM_LootCacheT3Image : MM_LootCacheT1Image)
{
    item = MM_LootCacheT3Item;
    doColorShift = MM_LootCacheT3Item.doColorShift;
    colorShiftColor = MM_LootCacheT3Item.colorShiftColor;
};

function MM_LootCacheT3Image::onMount(%this, %obj, %slot) { %obj.playThread(0, "armReadyBoth"); }

function MM_LootCacheT3Image::onUnMount(%this, %obj, %slot) { %obj.playThread(0, "root"); }

function MM_LootCacheT3Image::onCharge(%this, %obj, %slot) { %obj.cacheSlot = %obj.currTool; %obj.playThread(0, "shiftDown"); }

function MM_LootCacheT3Image::onFire(%this, %obj, %slot)
{
    if (!isObject(%client = %obj.client))
        return;

	%client.MMLootCacheReward_Roll(%this, %obj, %slot, 3, 8, "Compressed Bedrock");
}

//T4
datablock ItemData(MM_LootCacheT4Item : MM_LootCacheT1Item)
{
	uiName = "Epic Loot Cache";
	colorShiftColor = "0.286 0.156 0.356 1.000";
	image = MM_LootCacheT4Image;

    recycleLoot = "16\tFrame Parts\t8\tMechanism Parts\t4\tCircuitry Parts\t2\tComputation Parts";
};

datablock ShapeBaseImageData(MM_LootCacheT4Image : MM_LootCacheT1Image)
{
    item = MM_LootCacheT4Item;
    doColorShift = MM_LootCacheT4Item.doColorShift;
    colorShiftColor = MM_LootCacheT4Item.colorShiftColor;
};

function MM_LootCacheT4Image::onMount(%this, %obj, %slot) { %obj.playThread(0, "armReadyBoth"); }

function MM_LootCacheT4Image::onUnMount(%this, %obj, %slot) { %obj.playThread(0, "root"); }

function MM_LootCacheT4Image::onCharge(%this, %obj, %slot) { %obj.cacheSlot = %obj.currTool; %obj.playThread(0, "shiftDown"); }

function MM_LootCacheT4Image::onFire(%this, %obj, %slot)
{
    if (!isObject(%client = %obj.client))
        return;

	%client.MMLootCacheReward_Roll(%this, %obj, %slot, 4, 8, "Fleshrock");
}

//T5
datablock ItemData(MM_LootCacheT5Item : MM_LootCacheT1Item)
{
	uiName = "Legendary Loot Cache";
	colorShiftColor = "0.749 0.121 0.129 1.000";
	image = MM_LootCacheT5Image;

    recycleLoot = "32\tFrame Parts\t16\tMechanism Parts\t8\tCircuitry Parts\t4\tComputation Parts\t2\tSentient Parts";
};

datablock ShapeBaseImageData(MM_LootCacheT5Image : MM_LootCacheT1Image)
{
    item = MM_LootCacheT5Item;
    doColorShift = MM_LootCacheT5Item.doColorShift;
    colorShiftColor = MM_LootCacheT5Item.colorShiftColor;
};

function MM_LootCacheT5Image::onMount(%this, %obj, %slot) { %obj.playThread(0, "armReadyBoth"); }

function MM_LootCacheT5Image::onUnMount(%this, %obj, %slot) { %obj.playThread(0, "root"); }

function MM_LootCacheT5Image::onCharge(%this, %obj, %slot) { %obj.cacheSlot = %obj.currTool; %obj.playThread(0, "shiftDown"); }

function MM_LootCacheT5Image::onFire(%this, %obj, %slot)
{
    if (!isObject(%client = %obj.client))
        return;

	%client.MMLootCacheReward_Roll(%this, %obj, %slot, 5, 8, "Voidstone");
}

function GameConnection::MMLootCacheReward_Roll(%client, %this, %obj, %slot, %tier, %toolRolls, %oreLayer)
{
    %boxLevel = GetMatterType(GetLayerType(%orelayer).dirt).level;
    %rng = getRandom();
    
    if (%rng < 0.10 / (%bonus + 1))
    {
        //Double reroll
        %this.onFire(%obj, %slot, %bonus + 2);
        %this.onFire(%obj, %slot, %bonus + 2);
    }
    else if (%rng < 0.20)
        %client.MMLootCacheReward_Levels(%tier);
    else if (%rng < 0.40)
        %client.MMLootCacheReward_Credits(%boxlevel);
    else if (%rng < 0.60)
        %client.MMLootCacheReward_Yield(%tier);
    else if (%rng < 0.80)
        %client.MMLootCacheReward_Tools(%tier, %toolRolls);
    else if (%rng < 1.00)
        %client.MMLootCacheReward_Ores(%oreLayer);
    else
        %client.chatMessage("\c2The loot cache had nothing...");

    %currSlot = %obj.cacheSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

function GameConnection::MMLootCacheReward_Levels(%client, %tier)
{
    %levels = getRandom(mRound(%tier / 2), mRound(%tier / 2) + (%tier % 2));
    %client.chatMessage("\c2The loot cache had " @ %levels @ " Pickaxe Levels!");
    %client.MM_PickaxeLevel += %levels;
}

function GameConnection::MMLootCacheReward_Credits(%client, %levelBonusCap)
{
    %credits = 4 * getRandom(PickaxeUpgradeCost(%client.MM_PickaxeLevel), PickaxeUpgradeCost(%levelBonusCap));
    %client.chatMessage("\c2The loot cache had " @ %credits @ " credits!");
    %client.AddMaterial(%credits, "Credits");
}

function GameConnection::MMLootCacheReward_Yield(%client, %tier, %failChance)
{
    if (%failChance $= "")
        %failChance = 1/3;

    if (getRandom() < %failChance)
        MM_ChangeYield(mPow(2, %tier) / -200, "opened a tier " @ %tier @ " Loot Cache", %client);
    else
        MM_ChangeYield(mPow(2, %tier) / 200, "opened a tier " @ %tier @ " Loot Cache", %client);
}

function GameConnection::MMLootCacheReward_Ores(%client, %layerName)
{
    %layer = GetLayerType(%layerName);

    for (%i = 0; %i < %layer.veinCount; %i++)
        %weightTotal += getField(%layer.vein[%i], 0);

    %rand = getRandom() * %weightTotal;
    for (%i = 0; %i < %layer.veinCount; %i++)
    {
        %spawnData = %layer.vein[%i];
        %spawnWeight = getField(%spawnData, 0);

        if (%rand < %spawnWeight)
            break;

        %rand -= %spawnWeight;
        %spawnData = "";
    }

    if (%spawnData !$= "")
    {
        %rolls = getField(%spawnData, 3) * 2;
        for (%i = 0; %i < %rolls; %i++)
        {
            %ore = getMatterType(getOreFromVein(%spawnData));
            if (%ore.unobtainable || %client.MM_PickaxeLevel < %ore.level)
                continue;

            %oreCount[%ore.name]++;
            %success = true;
        }

        if (%success)
        {
            %client.chatMessage("\c2The loot cache had some ore!");

            %fieldCount = getFieldCount(%spawnData);
            for (%i = 4; %i < %fieldCount; %i += 2)
            {
                %ore = getMatterType(getField(%spawnData, %i));
                %amount = %oreCount[%ore.name];

                if (%amount > 0)
                {
                    %client.chatMessage("\c6+" @ %amount SPC %ore.name);
                    %client.AddMaterial(%amount, %ore.name);
                }
                
            }

        }
        else
        {
            %client.chatMessage("\c2The loot cache had... A penny?");
            %client.AddMaterial(1, "Penny");
        }
    }
}

$MM::DropLootTableCount["CacheT1"] = 3;
$MM::DropLootTable["CacheT1", 0] = 1.0 TAB "MM_DynamiteT1Item" TAB "MM_JackhammerGrenadeT1Item" TAB "MM_ShrapnelBombT1Item" TAB "MM_NapalmBombT1Item";
$MM::DropLootTable["CacheT1", 1] = 0.5 TAB "MM_BatteryPackT1Item";
$MM::DropLootTable["CacheT1", 2] = 0.1 TAB "FluidPumpItem" TAB "MMModuleJetStablizersItem" TAB "MMDrillPartsItem";

$MM::DropLootTableCount["CacheT2"] = 3;
$MM::DropLootTable["CacheT2", 0] = 1.0 TAB "MM_DynamiteT2Item" TAB "MM_JackhammerGrenadeT2Item" TAB "MM_ShrapnelBombT2Item" TAB "MM_NapalmBombT2Item";
$MM::DropLootTable["CacheT2", 1] = 0.5 TAB "MM_BatteryPackT2Item" TAB "MMHealpackItem";
$MM::DropLootTable["CacheT2", 2] = 0.1 TAB "PlasteelGunItem" TAB "BlueprintItem" TAB "MM_LootCacheT1Item";

$MM::DropLootTableCount["CacheT3"] = 3;
$MM::DropLootTable["CacheT3", 0] = 1.0 TAB "MM_DynamiteT3Item" TAB "MM_JackhammerGrenadeT3Item" TAB "MM_ShrapnelBombT3Item" TAB "MM_NapalmBombT3Item";
$MM::DropLootTable["CacheT3", 1] = 0.5 TAB "MM_BatteryPackT3Item" TAB "MMHealpackItem" TAB "MMRadpackItem";
$MM::DropLootTable["CacheT3", 2] = 0.1 TAB "PurifierItem" TAB "MMDualpackItem" TAB "MM_LootCacheT2Item";


$MM::DropLootTableCount["CacheT4"] = 3;
$MM::DropLootTable["CacheT4", 0] = 1.0 TAB "MM_DynamiteT3Item" TAB "MM_JackhammerGrenadeT3Item" TAB "MM_ShrapnelBombT3Item" TAB "MM_NapalmBombT3Item";
$MM::DropLootTable["CacheT4", 1] = 0.5 TAB "MM_BatteryPackT3Item" TAB "MMHealpackItem" TAB "MMRadpackItem" TAB "MMDualpackItem";
$MM::DropLootTable["CacheT4", 2] = 0.1 TAB "MM_LootCacheT1Item" TAB "MM_LootCacheT1Item" TAB "MM_LootCacheT3Item";


$MM::DropLootTableCount["CacheT5"] = 3;
$MM::DropLootTable["CacheT5", 0] = 1.0 TAB "MM_DynamiteT3Item" TAB "MM_JackhammerGrenadeT3Item" TAB "MM_ShrapnelBombT3Item" TAB "MM_NapalmBombT3Item";
$MM::DropLootTable["CacheT5", 1] = 0.5 TAB "MM_BatteryPackT3Item" TAB "MMDualpackItem";
$MM::DropLootTable["CacheT5", 2] = 0.1 TAB "MM_LootCacheT2Item" TAB "MM_LootCacheT2Item" TAB "MM_LootCacheT4Item";
function GameConnection::MMLootCacheReward_Tools(%client, %tier, %rolls)
{
    if (!isObject(%player = %client.player))
        return;

    %client.chatMessage("\c2The loot cache had some tools!");

    %tier = "CacheT" @ %tier;

    %tableCount = $MM::DropLootTableCount[%tier];

    for (%i = 0; %i < %tableCount; %i++)
        %totalWeight += getField($MM::DropLootTable[%tier, %i], 0);

    for (%i = 0; %i < %rolls; %i++)
    {
        %rand = getRandom() * %totalWeight;
        for (%j = 0; %j < %tableCount; %j++)
        {
            %spawnData = $MM::DropLootTable[%tier, %j];
            %spawnWeight = getField(%spawnData, 0);

            if (%rand < %spawnWeight)
                break;

            %rand -= %spawnWeight;
            %spawnData = "";
        }

        if (%spawnData !$= "")
        {
            %data = getField(%spawnData, getRandom(1, getFieldCount(%list) - 1));
            %pos = vectorAdd(%player.getPosition(), "0 0 1");
            MM_SpawnItem(%data, %pos);
        }
    }
}