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
$MM::ItemCost["MMDrillKitEfficiencyItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitEfficiencyItem"] = "(? Complexity). Increases the fuel efficiency of the drill.";
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
$MM::ItemCost["MMDrillKitDistanceItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitDistanceItem"] = "(? Complexity). Greatly increases the max range of the drill.";
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
$MM::ItemCost["MMDrillKitScrapperItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitScrapperItem"] = "(? Complexity). Reduces the health of ores revealed by the drill.";
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
$MM::ItemCost["MMDrillKitShieldItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitShieldItem"] = "(? Complexity). Gives the drill extra health, allowing it to drill through more hazards.";
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
$MM::ItemCost["MMDrillKitSpeedItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitSpeedItem"] = "(? Complexity). Increases the drill's dig speed, but decreases fuel efficiency.";
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
$MM::ItemCost["MMDrillKitOreItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitOreItem"] = "(? Complexity). Give a chance for the drill to not destroy drilled ores, but decreases max drill distance.";
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
$MM::ItemCost["MMDrillKitAoEItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitAoEItem"] = "(? Complexity). Increases the drill's digging radius, but greatly reduces dig speed.";
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