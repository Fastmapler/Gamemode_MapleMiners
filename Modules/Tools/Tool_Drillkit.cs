function Player::UseDrillKit(%obj)
{
    if (%obj.getDamageLevel() <= 0 || !isObject(%image = %obj.getMountedImage(0)) || !%obj.ApplyDrillKit(%image.DrillKitType))
		return;
	
	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//Efficiency
$MM::ItemCost["MMDrillKitEfficiencyItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitEfficiencyItem"] = "(4 Complexity). Increases the fuel efficiency of the drill.";
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

//Speed
$MM::ItemCost["MMDrillKitSpeedItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillKitSpeedItem"] = "(6 Complexity). Increases the drill's mining rate, but decreases fuel efficiency.";
datablock itemData(MMDrillKitSpeedItem : MMDrillKitEfficiencyItem)
{
	uiName = "Drill Kit - Speed";
	colorShiftColor = "1.00 1.00 0.00 1.00";
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