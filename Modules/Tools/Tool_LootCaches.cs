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
	iconName = "./Shapes/BoxItem";
	doColorShift = true;
	colorShiftColor = "0.309 0.286 0.294 1.000";

	 // Dynamic properties defined by the scripts
	image = MM_LootCacheT1Image;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
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

function MM_LootCacheT1Image::onMount(%this, %obj, %slot)
{
    %obj.playThread(0, "armReadyBoth");
}

function MM_LootCacheT1Image::onUnMount(%this, %obj, %slot)
{
    %obj.playThread(0, "root");
}

function MM_LootCacheT1Image::onCharge(%this, %obj, %slot)
{
	%obj.playThread(0, "shiftDown");
}

function MM_LootCacheT1Image::onFire(%this, %obj, %slot)
{
    if (!isObject(%client = %obj.client))
        return;

	%rng = getRandom();

    if (%rng < 0.10)
    {
        //Pickaxe Levels
        %levels = getRandom(2, 4);
        %client.chatMessage("\c2The loot cache had " @ %levels @ " Pickaxe Levels!");
        %client.MM_PickaxeLevel += %levels;
    }
    else if (%rng < 0.20)
    {
        //Something
    }
    else if (%rng < 0.40)
    {
        //Credits
        %credits = getRandom(250, 1000);
        %client.chatMessage("\c2The loot cache had " @ %credits @ " credits!");
        %client.MM_Materials["Credits"] += %credits;
    }
    else if (%rng < 0.60)
    {
        //Ores
    }
    else if (%rng < 0.80)
    {
        //Tools
        //Todo: Add more stuff
        %item = new Item()
        {
            datablock = MM_DynamiteT1Item;
            static    = "0";
            position  = vectorAdd(%obj.getPosition(), "0 0 1");
        };
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

    %currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}