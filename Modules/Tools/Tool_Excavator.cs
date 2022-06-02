function Player::MMPickaxe_Excavator(%obj, %dist)
{
	if (!isObject(%client = %obj.client))
		return;

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, mClamp(%dist, 3, 100))), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)) && %hit.getClassName() $= "fxDtsBrick" && %hit.canMine)
	{
		%damage = %client.GetPickaxeDamage();
		%matter = getMatterType(%hit.matter);

		if (%client.MM_PickaxeLevel < %matter.level)
		{
			%client.MM_CenterPrint("You need to be atleast level\c3" SPC %matter.level SPC "\c6to learn how to mine this<color:" @ getSubStr(%matter.color, 0, 6) @ ">" SPC %matter.name @ "\c6!", 2);
			return;
		}

		if (%matter.hitSound !$= "")
			%hit.playSound("MM_" @ %matter.hitSound @ getRandom(1, $MM::SoundCount[%matter.hitSound]) @ "Sound");

		%client.MM_CenterPrint("<color:" @ getSubStr(%matter.color, 0, 6) @ ">" @ %matter.name NL "\c6" @ getMax(%hit.health - %damage, 0) SPC "HP<br>\c3" @ %matter.value @ "\c6cr", 2);

		%hitpos = %hit.getPosition();
		%hit.MineDamage(%damage, "Pickaxe", %client);
	}
}

datablock ItemData(MMExcavatorT1Item : swordItem)
{
	shapeFile = "./Shapes/T1Pick.dts";
	uiName = "Basic Excavator";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 0.000 1.000";

	image = rpgExcavatorT1Image;
	canDrop = true;
	iconName = "./Shapes/T1Pick";
};

datablock ShapeBaseImageData(rpgExcavatorT1Image)
{
	shapeFile = "./Shapes/T1Pick.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";

	correctMuzzleVector = false;

	className = "WeaponImage";

	item = MMExcavatorT1Item;
	ammo = " ";
	projectile = gunProjectile;
	projectileType = Projectile;

	melee = true;
	doRetraction = false;

	armReady = true;

	doColorShift = MMExcavatorT1Item.doColorShift;
	colorShiftColor = MMExcavatorT1Item.colorShiftColor;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]                    = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "CheckFire";
	stateTimeoutValue[2]            = 0.33;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;

	stateName[3]					= "CheckFire";
	stateTransitionOnTriggerUp[3]	= "Ready";
	stateTransitionOnTriggerDown[3]	= "Fire";
};

function rpgExcavatorT1Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Excavator(4); }

datablock ItemData(MMExcavatorT2Item : MMExcavatorT1Item)
{
	shapeFile = "./Shapes/T2Pick.dts";
	uiName = "Improved Excavator";
	colorShiftColor = "1.000 1.000 0.000 1.000";
	image = rpgExcavatorT2Image;
	iconName = "./Shapes/T2Pick";
};

datablock ShapeBaseImageData(rpgExcavatorT2Image : rpgExcavatorT1Image)
{
	shapeFile = "./Shapes/T2Pick.dts";

	item = MMExcavatorT2Item;

	doColorShift = MMExcavatorT2Item.doColorShift;
	colorShiftColor = MMExcavatorT2Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.25;
};

function rpgExcavatorT2Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Excavator(5); }