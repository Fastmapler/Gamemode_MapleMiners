function Player::UseDrillKit(%obj)
{
    if (!isObject(%client = %obj.client) || !isObject(%image = %obj.getMountedImage(0)) || !%client.ApplyDrillKit(%image.DrillKitType))
		return;
	
	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

function GameConnection::ApplyDrillKit(%obj, %type)
{
	if (%type $= "")
		return false;
	if (getFieldCount(%obj.MM_Drillkits) > 10)
	{
		%obj.chatMessage("\c6You have maxxed out your applied drillkit count!");
		return false;
	}

	%obj.MM_Drillkits = trim(%obj.MM_Drillkits TAB %type);
	%obj.chatMessage("\c6You added the \c3" @ %type @ " \c6DrillKit to your drills!");
	return true;
}

//Efficiency
$MM::ItemCost["MMDrillKitEfficiencyItem"] = "250\tCredits\t5\tCopper\t5\tGallium";
$MM::ItemDisc["MMDrillKitEfficiencyItem"] = "(4 Complexity). Increases the fuel efficiency of the drill by 25\%, reducing overall fuel cost.";
datablock itemData(MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - Efficiency";
	iconName = "./Shapes/icon_DrillKit";
	doColorShift = true;
	colorShiftColor = "1.00 0.00 0.00 1.00";
	
	shapeFile = "./Shapes/DrillKit.dts";
	image = MMDrillKitEfficiencyImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(MMDrillKitEfficiencyImage)
{
	shapeFile = "./Shapes/Drillkit.dts";
	item = MMDrillKitEfficiencyItem;
	
	mountPoint = 0;
	offset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	
	eyeOffset = "";
	eyeRotation = "";
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = MMDrillKitEfficiencyItem.doColorShift;
	colorShiftColor = MMDrillKitEfficiencyItem.colorShiftColor;

    DrillKitType = "Efficiency";
	
	stateName[0]					= "Start";
	stateTimeoutValue[0]			= 0.1;
	stateTransitionOnTimeout[0]	 	= "Ready";
	stateSound[0]					= weaponSwitchSound;
	
	stateName[1]					= "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]		= true;
	
	stateName[2]					= "Fire";
	stateScript[2]					= "onFire";
	stateTimeoutValue[2]			= 0.1;
	stateAllowImageChange[2]		= false;
	stateTransitionOnTimeout[2]		= "checkFire";
	
	stateName[3]					= "checkFire";
	stateTransitionOnTriggerUp[3] 	= "Ready";
};

function MMDrillKitEfficiencyImage::onFire(%this, %obj, %slot) { %obj.UseDrillKit(); }

//Distance
$MM::ItemCost["MMDrillKitDistanceItem"] = "250\tCredits\t5\tIron\t5\tQuartz";
$MM::ItemDisc["MMDrillKitDistanceItem"] = "(4 Complexity). Doubles the max range of the drill.";
datablock itemData(MMDrillKitDistanceItem : MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - Distance";
	colorShiftColor = "1.00 0.50 0.00 1.00";
	shapeFile = "./Shapes/DrillKit.dts";
	image = MMDrillKitDistanceImage;
};

datablock shapeBaseImageData(MMDrillKitDistanceImage : MMDrillKitEfficiencyImage)
{
	item = MMDrillKitDistanceItem;
	doColorShift = MMDrillKitDistanceItem.doColorShift;
	colorShiftColor = MMDrillKitDistanceItem.colorShiftColor;
    DrillKitType = "Distance";
};

function MMDrillKitDistanceImage::onFire(%this, %obj, %slot) { %obj.UseDrillKit(); }

//Scrapper
$MM::ItemCost["MMDrillKitScrapperItem"] = "750\tCredits\t5\tNickel\t5\tSilver";
$MM::ItemDisc["MMDrillKitScrapperItem"] = "(4 Complexity, +2 Fuel Cost). Causes the drill to randomly shoot beams, reducing the health of impacted bricks.";
datablock itemData(MMDrillKitScrapperItem : MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - Scrapper";
	colorShiftColor = "1.00 1.00 0.00 1.00";
	shapeFile = "./Shapes/DrillKit.dts";
	image = MMDrillKitScrapperImage;
};

datablock shapeBaseImageData(MMDrillKitScrapperImage : MMDrillKitEfficiencyImage)
{
	item = MMDrillKitScrapperItem;
	doColorShift = MMDrillKitScrapperItem.doColorShift;
	colorShiftColor = MMDrillKitScrapperItem.colorShiftColor;
    DrillKitType = "Scrapper";
};

function MMDrillKitScrapperImage::onFire(%this, %obj, %slot) { %obj.UseDrillKit(); }

//Shield
$MM::ItemCost["MMDrillKitShieldItem"] = "750\tCredits\t5\tFluorite\t5\tTitanium";
$MM::ItemDisc["MMDrillKitShieldItem"] = "(4 Complexity, +2 Fuel Cost). Quadruples the drill's health, allowing it to drill through more hazards.";
datablock itemData(MMDrillKitShieldItem : MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - Shield";
	colorShiftColor = "0.50 1.00 0.00 1.00";
	shapeFile = "./Shapes/DrillKit.dts";
	image = MMDrillKitShieldImage;
};

datablock shapeBaseImageData(MMDrillKitShieldImage : MMDrillKitEfficiencyImage)
{
	item = MMDrillKitShieldItem;
	doColorShift = MMDrillKitShieldItem.doColorShift;
	colorShiftColor = MMDrillKitShieldItem.colorShiftColor;
    DrillKitType = "Shield";
};

function MMDrillKitShieldImage::onFire(%this, %obj, %slot) { %obj.UseDrillKit(); }

//Speed
$MM::ItemCost["MMDrillKitSpeedItem"] = "2250\tCredits\t5\tUranium\t5\tThorium";
$MM::ItemDisc["MMDrillKitSpeedItem"] = "(6 Complexity, +4 Fuel Cost). Increases the drill's dig speed by 25\%, but decreases fuel efficiency by 10\%.";
datablock itemData(MMDrillKitSpeedItem : MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - Speed";
	colorShiftColor = "0.00 1.00 0.00 1.00";
	shapeFile = "./Shapes/DrillKit.dts";
	image = MMDrillKitSpeedImage;
};

datablock shapeBaseImageData(MMDrillKitSpeedImage : MMDrillKitEfficiencyImage)
{
	item = MMDrillKitSpeedItem;
	doColorShift = MMDrillKitSpeedItem.doColorShift;
	colorShiftColor = MMDrillKitSpeedItem.colorShiftColor;
    DrillKitType = "Speed";
};

function MMDrillKitSpeedImage::onFire(%this, %obj, %slot) { %obj.UseDrillKit(); }

//Ore
$MM::ItemCost["MMDrillKitOreItem"] = "2250\tCredits\t5\tRuthenium\t5\tGold";
$MM::ItemDisc["MMDrillKitOreItem"] = "(6 Complexity, +6 Fuel Cost). Give a 25\% chance for the drill to not destroy drilled ores, but halves max drill distance.";
datablock itemData(MMDrillKitOreItem : MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - Ore";
	colorShiftColor = "0.00 1.00 0.50 1.00";
	shapeFile = "./Shapes/DrillKit.dts";
	image = MMDrillKitOreImage;
};

datablock shapeBaseImageData(MMDrillKitOreImage : MMDrillKitEfficiencyImage)
{
	item = MMDrillKitOreItem;
	doColorShift = MMDrillKitOreItem.doColorShift;
	colorShiftColor = MMDrillKitOreItem.colorShiftColor;
    DrillKitType = "Ore";
};

function MMDrillKitOreImage::onFire(%this, %obj, %slot) { %obj.UseDrillKit(); }

//AoE
$MM::ItemCost["MMDrillKitAoEItem"] = "6750\tCredits\t2\tMagicite\t2\tMythril\t2\tDragonstone";
$MM::ItemDisc["MMDrillKitAoEItem"] = "(6 Complexity, +6 Fuel Cost). Increases the drill's digging radius by 1, but reduces dig speed by 25\%.";
datablock itemData(MMDrillKitAoEItem : MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - AoE";
	colorShiftColor = "0.00 1.00 1.00 1.00";
	shapeFile = "./Shapes/DrillKit.dts";
	image = MMDrillKitAoEImage;
};

datablock shapeBaseImageData(MMDrillKitAoEImage : MMDrillKitEfficiencyImage)
{
	item = MMDrillKitAoEItem;
	doColorShift = MMDrillKitAoEItem.doColorShift;
	colorShiftColor = MMDrillKitAoEItem.colorShiftColor;
    DrillKitType = "AoE";
};

function MMDrillKitAoEImage::onFire(%this, %obj, %slot) { %obj.UseDrillKit(); }