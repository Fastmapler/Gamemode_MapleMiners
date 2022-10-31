
datablock AudioProfile(MedipackUseSound)
{
    filename    = "./Sounds/Medipack.wav";
    description = AudioClosest3d;
    preload = true;
};

$MM::ItemCost["MMHealpackItem"] = "75\tCredits\t2\tBiomatter";
$MM::ItemDisc["MMHealpackItem"] = "Instantly heals 75 HP on use. Can be combined with a Radpack.";
datablock itemData(MMHealpackItem)
{
	uiName = "Healpack";
	iconName = "./Shapes/icon_Medpack";
	doColorShift = true;
	colorShiftColor = "1.00 0.00 0.00 1.00";
	
	shapeFile = "./Shapes/Medpack.dts";
	image = MMHealpackImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(MMHealpackImage)
{
	shapeFile = "./Shapes/Medpack.dts";
	item = MMHealpackItem;
	
	mountPoint = 0;
	offset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	
	eyeOffset = "";
	eyeRotation = "";
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = MMHealpackItem.doColorShift;
	colorShiftColor = MMHealpackItem.colorShiftColor;
	
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

function MMHealpackImage::onFire(%this, %obj, %slot)
{
	if (%obj.getDamageLevel() <= 0)
		return;
		
	%obj.addHealth(75);
	ServerPlay3D(MedipackUseSound, %obj.getPosition());
	%obj.setWhiteOut(0.5);
	
	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

$MM::ItemCost["MMRadpackItem"] = "75\tCredits\t2\tBiomatter";
$MM::ItemDisc["MMRadpackItem"] = "Instantly removes 375 rads on use. Can be combined with a Healpack.";
datablock itemData(MMRadpackItem : MMHealpackItem)
{
	uiName = "Radpack";
	colorShiftColor = "0.00 1.00 0.00 1.00";
	image = MMRadpackImage;
};

datablock shapeBaseImageData(MMRadpackImage : MMHealpackImage)
{
	item = MMRadpackItem;
	
	mountPoint = 0;
	offset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	doColorShift = MMRadpackItem.doColorShift;
	colorShiftColor = MMRadpackItem.colorShiftColor;
};

function MMRadpackImage::onFire(%this, %obj, %slot)
{
	if (%obj.MM_RadLevel <= 0)
		return;
		
	%obj.MM_RadLevel = getMax(%obj.MM_RadLevel - 375, 0);
	ServerPlay3D(MedipackUseSound, %obj.getPosition());
	%obj.setWhiteOut(0.5);

	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

$MM::ToolCraftingRecipe["MMHealpackItem", "MMRadpackItem"] = MMDualpackItem;
$MM::ItemDisc["MMDualpackItem"] = "Instantly heals 80 HP and 400 rads on use.";
datablock itemData(MMDualpackItem : MMHealpackItem)
{
	uiName = "Dualpack";
	colorShiftColor = "1.00 1.00 0.00 1.00";
	image = MMDualpackImage;
};

datablock shapeBaseImageData(MMDualpackImage : MMHealpackImage)
{
	item = MMDualpackItem;
	
	mountPoint = 0;
	offset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	doColorShift = MMDualpackItem.doColorShift;
	colorShiftColor = MMDualpackItem.colorShiftColor;
};

function MMDualpackImage::onFire(%this, %obj, %slot)
{
	if (%obj.MM_RadLevel <= 0 && %obj.getDamageLevel() <= 0)
		return;
		
	%obj.addHealth(80);
	%obj.MM_RadLevel = getMax(%obj.MM_RadLevel - 400, 0);
	ServerPlay3D(MedipackUseSound, %obj.getPosition());
	%obj.setWhiteOut(0.75);

	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}