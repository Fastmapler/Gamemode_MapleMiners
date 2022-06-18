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

	%rng = getRandom();
    
    if (%rng < 0.10 / (%bonus + 1))
    {
        //Double reroll
        MM_LootCacheT1Image::onFire(%this, %obj, %slot, %bonus + 2);
        MM_LootCacheT1Image::onFire(%this, %obj, %slot, %bonus + 2);
    }
    else if (%rng < 0.20)
    {
        //Pickaxe Levels
        %levels = getRandom(1, 2);
        %client.chatMessage("\c2The loot cache had " @ %levels @ " Pickaxe Levels!");
        %client.MM_PickaxeLevel += %levels;
    }
    else if (%rng < 0.40)
    {
        //Credits
        %credits = getRandom(500, 1000);
        %client.chatMessage("\c2The loot cache had " @ %credits @ " credits!");
        %client.AddMaterial(%credits, "Credits");
    }
    else if (%rng < 0.60)
    {
        //Ores
        %layer = GetLayerType("Packed Dirt");

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
            %client.chatMessage("\c2The loot cache had some ore!");
            for (%i = 0; %i < 5; %i++)
            {
                %ore = getOreFromVein(%spawnData);
                %client.chatMessage("\c6+1" SPC %ore);
                %client.AddMaterial(1, %ore);
            }
        }
    }
    else if (%rng < 0.80)
    {
        //Tools
        %client.chatMessage("\c2The loot cache had an assortment of tools!");
        %list = "MM_DynamiteT1Item" TAB "MM_DynamiteT2Item" TAB "MM_BatteryPackT1Item" TAB "MM_BatteryPackT2Item" TAB "MMHealpackHealthItem";

        for (%i = 0; %i < 3; %i++)
        {
            %item = new Item()
            {
                datablock = getField(%list, getRandom(0, getFieldCount(%list) - 1));
                static    = "0";
                position  = vectorAdd(%obj.getPosition(), "0 0 1");
            };
            MissionCleanup.add(%item);
        }
        
    }
    else if (%rng < 1.00)
    {
        //Yield
        if (getRandom() < 0.33)
            MM_ChangeYield(-0.01, "opened a Basic Loot Cache", %client);
        else
            MM_ChangeYield(0.01, "opened a Basic Loot Cache", %client);
    }
    else
    {
        %client.chatMessage("\c2The loot cache had nothing...");
    }

    %currSlot = %obj.cacheSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T2
datablock ItemData(MM_LootCacheT2Item : MM_LootCacheT1Item)
{
	uiName = "Improved Loot Cache";
	colorShiftColor = "0.847 0.819 0.800 1.000";
	image = MM_LootCacheT2Image;
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

	%rng = getRandom();
    
    if (%rng < 0.10 / (%bonus + 1))
    {
        //Double reroll
        MM_LootCacheT2Image::onFire(%this, %obj, %slot, %bonus + 2);
        MM_LootCacheT2Image::onFire(%this, %obj, %slot, %bonus + 2);
    }
    else if (%rng < 0.20)
    {
        //Pickaxe Levels
        %levels = getRandom(1, 2);
        %client.chatMessage("\c2The loot cache had " @ %levels @ " Pickaxe Levels!");
        %client.MM_PickaxeLevel += %levels;
    }
    else if (%rng < 0.40)
    {
        //Credits
        %credits = getRandom(500, 1000) * 2;
        %client.chatMessage("\c2The loot cache had " @ %credits @ " credits!");
        %client.AddMaterial(%credits, "Credits");
    }
    else if (%rng < 0.60)
    {
        //Ores
        %layer = GetLayerType("Stone");

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
            %client.chatMessage("\c2The loot cache had some ore!");
            for (%i = 0; %i < 5; %i++)
            {
                %ore = getOreFromVein(%spawnData);
                %client.chatMessage("\c6+1" SPC %ore);
                %client.AddMaterial(1, %ore);
            }
        }
    }
    else if (%rng < 0.80)
    {
        //Tools
        %client.chatMessage("\c2The loot cache had an assortment of tools!");
        %list = "MM_DynamiteT2Item" TAB "MM_BatteryPackT2Item" TAB "MMHealpackHealthItem";

        for (%i = 0; %i < 4; %i++)
        {
            %item = new Item()
            {
                datablock = getField(%list, getRandom(0, getFieldCount(%list) - 1));
                static    = "0";
                position  = vectorAdd(%obj.getPosition(), "0 0 1");
            };
            MissionCleanup.add(%item);
        }
        
    }
    else if (%rng < 1.00)
    {
        //Yield
        if (getRandom() < 0.33)
            MM_ChangeYield(-0.02, "opened an Improved Loot Cache", %client);
        else
            MM_ChangeYield(0.02, "opened an Improved Loot Cache", %client);
    }
    else
    {
        %client.chatMessage("\c2The loot cache had nothing...");
    }

    %currSlot = %obj.cacheSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T3
datablock ItemData(MM_LootCacheT3Item : MM_LootCacheT1Item)
{
	uiName = "Superior Loot Cache";
	colorShiftColor = "0.121 0.337 0.549 1.000";
	image = MM_LootCacheT3Image;
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

	%rng = getRandom();

    %currSlot = %obj.cacheSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T4
datablock ItemData(MM_LootCacheT4Item : MM_LootCacheT1Item)
{
	uiName = "Epic Loot Cache";
	colorShiftColor = "0.286 0.156 0.356 1.000";
	image = MM_LootCacheT4Image;
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

	%rng = getRandom();

    %currSlot = %obj.cacheSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T5
datablock ItemData(MM_LootCacheT5Item : MM_LootCacheT1Item)
{
	uiName = "Legendary Loot Cache";
	colorShiftColor = "0.749 0.121 0.129 1.000";
	image = MM_LootCacheT5Image;
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

	%rng = getRandom();

    %currSlot = %obj.cacheSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}