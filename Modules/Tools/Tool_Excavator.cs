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
		%hitpos = %hit.getPosition();
		%lookVec = %obj.FaceDirection();
		%cross1 = %obj.FaceDirection(%obj.getRightVector());
		%cross2 = %obj.FaceDirection(%obj.getLeftVector());

		%pos[0] = roundVector(vectorAdd(%hitpos, vectorScale(%cross1, $MM::BrickDistance)));
		%pos[1] = roundVector(vectorAdd(%hitpos, vectorScale(%cross2, $MM::BrickDistance)));
		%pos[2] = roundVector(vectorAdd(%hitpos, vectorScale("0 0 0", $MM::BrickDistance)));

		%raypos = getWords(%ray, 1, 3);

		if (!%client.MM_noMiningDebris)
			spawnExplosion(dirtHitProjectile, %raypos, %client);

		for (%i = 0; %i < 4; %i++)
		{
			RevealBlock(%pos[%i]);
			if (isObject(%brick = $MM::BrickGrid[%pos[%i]]))
			{
				if (getMatterType(%brick.matter).value > 0)
					%obj.MM_AttemptMine(%brick, 0.1, "(Damage reduced)");
				else
					%obj.MM_AttemptMine(%brick);
			}
		}
	}
}

$MM::ItemCost["MMExcavatorT1Item"] = "360\tCredits\t5\tAluminum\t5\tZinc\t10\tCopper";
$MM::ItemDisc["MMExcavatorT1Item"] = "Digs in a 3x1 area, hitting all bricks at once. -90% damage against valued bricks.";
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
	stateTimeoutValue[0]             = 0.33;
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

$MM::ItemCost["MMExcavatorT2Item"] = "7050\tCredits\t5\tLithium\t5\tFluorite\t10\tGraphite";
$MM::ItemDisc["MMExcavatorT2Item"] = "Digs in a 3x1 area, hitting all bricks at once. -90% damage against valued bricks.";
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

$MM::ItemCost["MMExcavatorT3Item"] = "152870\tCredits\t5\tNeodymium\t5\tRuthenium\t10\tOsmium";
$MM::ItemDisc["MMExcavatorT3Item"] = "Digs in a 3x1 area, hitting all bricks at once. -90% damage against valued bricks.";
datablock ItemData(MMExcavatorT3Item : MMExcavatorT1Item)
{
	shapeFile = "./Shapes/T3Pick.dts";
	uiName = "Superior Excavator";
	colorShiftColor = "1.000 1.000 0.000 1.000";
	image = rpgExcavatorT3Image;
	iconName = "./Shapes/T3Pick";
};

datablock ShapeBaseImageData(rpgExcavatorT3Image : rpgExcavatorT1Image)
{
	shapeFile = "./Shapes/T3Pick.dts";

	item = MMExcavatorT3Item;

	doColorShift = MMExcavatorT3Item.doColorShift;
	colorShiftColor = MMExcavatorT3Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.19;
};

function rpgExcavatorT3Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Excavator(6); }

$MM::ItemCost["MMExcavatorT4Item"] = "1113080\tCredits\t5\tPlutonium\t5\tXenon\t10\tHelium";
$MM::ItemDisc["MMExcavatorT4Item"] = "Digs in a 3x1 area, hitting all bricks at once. -90% damage against valued bricks.";
datablock ItemData(MMExcavatorT4Item : MMExcavatorT1Item)
{
	shapeFile = "./Shapes/T4Pick.dts";
	uiName = "Epic Excavator";
	colorShiftColor = "1.000 1.000 0.000 1.000";
	image = rpgExcavatorT4Image;
	iconName = "./Shapes/T4Pick";
};

datablock ShapeBaseImageData(rpgExcavatorT4Image : rpgExcavatorT1Image)
{
	shapeFile = "./Shapes/T4Pick.dts";

	item = MMExcavatorT4Item;

	doColorShift = MMExcavatorT4Item.doColorShift;
	colorShiftColor = MMExcavatorT4Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.14;
};

function rpgExcavatorT4Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Excavator(6); }

$MM::ItemCost["MMExcavatorT5Item"] = "1113080\tCredits\t5\tPlutonium\t5\tXenon\t10\tHelium";
$MM::ItemDisc["MMExcavatorT5Item"] = "Digs in a 3x1 area, hitting all bricks at once. -90% damage against valued bricks.";
datablock ItemData(MMExcavatorT5Item : MMExcavatorT1Item)
{
	shapeFile = "./Shapes/T5Pick.dts";
	uiName = "Legendary Excavator";
	colorShiftColor = "1.000 1.000 0.000 1.000";
	image = rpgExcavatorT5Image;
	iconName = "./Shapes/T5Pick";
};

datablock ShapeBaseImageData(rpgExcavatorT5Image : rpgExcavatorT1Image)
{
	shapeFile = "./Shapes/T5Pick.dts";

	item = MMExcavatorT5Item;

	doColorShift = MMExcavatorT5Item.doColorShift;
	colorShiftColor = MMExcavatorT5Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.11;
};

function rpgExcavatorT5Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Excavator(6); }