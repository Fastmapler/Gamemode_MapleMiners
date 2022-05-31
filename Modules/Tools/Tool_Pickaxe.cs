function Player::MMPickaxe_Generic(%obj, %dist)
{
	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, mClamp(%dist, 3, 100))), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)) && %hit.getClassName() $= "fxDtsBrick" && %hit.canMine)
	{
		%damage = 5;
		%matter = getMatterType(%hit.matter);

		if (%matter.hitSound !$= "")
			%hit.playSound("MM_" @ %matter.hitSound @ getRandom(1, $MM::SoundCount[%matter.hitSound]) @ "Sound");

		if (isObject(%client = %obj.client))
			%client.centerPrint("<color:" @ getSubStr(%matter.color, 0, 6) @ ">" @ %matter.name NL "\c6" @ getMax(%hit.health - %damage, 0) SPC "HP", 2);

		%hit.MineDamage(%damage, "Pickaxe", %client);
	}
}

datablock ItemData(MMPickaxeT0Item : swordItem)
{
	shapeFile = "./Shapes/Pickaxe.dts";
	uiName = "Flimsy Pickaxe";
	doColorShift = true;
	colorShiftColor = "0.471 0.271 0.111 1.000";

	image = rpgPickaxeT0Image;
	canDrop = true;
	iconName = "./Shapes/icon_Pickaxe";
};

datablock ShapeBaseImageData(rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/Pickaxe.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";

	correctMuzzleVector = false;

	className = "WeaponImage";

	item = MMPickaxeT0Item;
	ammo = " ";
	projectile = gunProjectile;
	projectileType = Projectile;

	melee = true;
	doRetraction = false;

	armReady = true;

	doColorShift = MMPickaxeT0Item.doColorShift;
	colorShiftColor = MMPickaxeT0Item.colorShiftColor;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]                    = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "CheckFire";
	stateTimeoutValue[2]            = 0.40;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;

	stateName[3]					= "CheckFire";
	stateTransitionOnTriggerUp[3]	= "Ready";
	stateTransitionOnTriggerDown[3]	= "Fire";
};

function rpgPickaxeT0Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(4); }

datablock ItemData(MMPickaxeT1Item : MMPickaxeT0Item)
{
	uiName = "Basic Pickaxe";
	colorShiftColor = "0.471 0.471 0.471 1.000";
	image = rpgPickaxeT1Image;
};

datablock ShapeBaseImageData(rpgPickaxeT1Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/Pickaxe.dts";

	item = MMPickaxeT1Item;

	doColorShift = MMPickaxeT1Item.doColorShift;
	colorShiftColor = MMPickaxeT1Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.33;
};

function rpgPickaxeT1Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(6); }

datablock ItemData(MMPickaxeT2Item : MMPickaxeT0Item)
{
	uiName = "Improved Pickaxe";
	colorShiftColor = "0.900 0.900 0.900 1.000";
	image = rpgPickaxeT2Image;
};

datablock ShapeBaseImageData(rpgPickaxeT2Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/Pickaxe.dts";

	item = MMPickaxeT2Item;

	doColorShift = MMPickaxeT2Item.doColorShift;
	colorShiftColor = MMPickaxeT2Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.25;
};

function rpgPickaxeT2Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(6); }

datablock ItemData(MMPickaxeT3Item : MMPickaxeT0Item)
{
	uiName = "Superior Pickaxe";
	colorShiftColor = "0.200 0.200 0.800 1.000";
	image = rpgPickaxeT3Image;
};

datablock ShapeBaseImageData(rpgPickaxeT3Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/Pickaxe.dts";

	item = MMPickaxeT3Item;

	doColorShift = MMPickaxeT3Item.doColorShift;
	colorShiftColor = MMPickaxeT3Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.19;
};

function rpgPickaxeT3Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(7); }

datablock ItemData(MMPickaxeT3Item : MMPickaxeT0Item)
{
	uiName = "Superior Pickaxe";
	colorShiftColor = "0.200 0.200 0.800 1.000";
	image = rpgPickaxeT3Image;
};

datablock ShapeBaseImageData(rpgPickaxeT3Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/Pickaxe.dts";

	item = MMPickaxeT3Item;

	doColorShift = MMPickaxeT3Item.doColorShift;
	colorShiftColor = MMPickaxeT3Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.14;
};

function rpgPickaxeT3Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(7); }

datablock ItemData(MMPickaxeT4Item : MMPickaxeT0Item)
{
	uiName = "Epic Pickaxe";
	colorShiftColor = "0.900 0.100 0.900 1.000";
	image = rpgPickaxeT4Image;
};

datablock ShapeBaseImageData(rpgPickaxeT4Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/Pickaxe.dts";

	item = MMPickaxeT4Item;

	doColorShift = MMPickaxeT4Item.doColorShift;
	colorShiftColor = MMPickaxeT4Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.10;
};

function rpgPickaxeT4Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(8); }