function Player::MMPickaxe_Bulky(%obj, %dist)
{
	if (!isObject(%client = %obj.client))
		return;

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, mClamp(%dist, 3, 100))), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)))
	{
		%obj.MM_AttemptMine(%hit);
		%raypos = getWords(%ray, 1, 3);

		if (!%client.MM_noMiningDebris)
			spawnExplosion(dirtHitProjectile, %raypos, %client);
	}
}

function Player::MMPickaxe_Generic(%obj, %dist)
{
	if (!isObject(%client = %obj.client))
		return;

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, mClamp(%dist, 3, 100))), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)))
	{
		%obj.MM_AttemptMine(%hit, 1, "", "pickaxeBuff");
		%raypos = getWords(%ray, 1, 3);

		if (!%client.MM_noMiningDebris)
			spawnExplosion(dirtHitProjectile, %raypos, %client);
	}
}

datablock ItemData(MMPickaxeT0Item : swordItem)
{
	shapeFile = "./Shapes/T0Pick.dts";
	uiName = "Bulky Pickaxe";
	doColorShift = true;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	image = rpgPickaxeT0Image;
	canDrop = true;
	iconName = "./Shapes/T0Pick";
};

datablock ShapeBaseImageData(rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/T0Pick.dts";
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
	stateTimeoutValue[0]             = 0.33;
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

function rpgPickaxeT0Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Bulky(4); }

$MM::ItemCost["MMPickaxeT1Item"] = "360\tCredits\t5\tZinc\t10\tIron\t10\tCopper";
$MM::ItemDisc["MMPickaxeT1Item"] = "A classic pickaxe! Grants 5% more damage and 5% less mining level requirement with no downside.";
datablock ItemData(MMPickaxeT1Item : MMPickaxeT0Item)
{
	shapeFile = "./Shapes/T1Pick.dts";
	uiName = "Basic Pickaxe";
	colorShiftColor = "1.000 1.000 1.000 1.000";
	image = rpgPickaxeT1Image;
	iconName = "./Shapes/T1Pick";
};

datablock ShapeBaseImageData(rpgPickaxeT1Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/T1Pick.dts";

	item = MMPickaxeT1Item;

	doColorShift = MMPickaxeT1Item.doColorShift;
	colorShiftColor = MMPickaxeT1Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.33;
};

function rpgPickaxeT1Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(6); }

$MM::ItemCost["MMPickaxeT2Item"] = "7050\tCredits\t5\tFluorite\t10\tNickel\t8\tGraphite";
$MM::ItemDisc["MMPickaxeT2Item"] = "A classic pickaxe! Grants 5% more damage and 5% less mining level requirement with no downside.";
datablock ItemData(MMPickaxeT2Item : MMPickaxeT0Item)
{
	shapeFile = "./Shapes/T2Pick.dts";
	uiName = "Improved Pickaxe";
	colorShiftColor = "1.000 1.000 1.000 1.000";
	image = rpgPickaxeT2Image;
	iconName = "./Shapes/T2Pick";
};

datablock ShapeBaseImageData(rpgPickaxeT2Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/T2Pick.dts";

	item = MMPickaxeT2Item;

	doColorShift = MMPickaxeT2Item.doColorShift;
	colorShiftColor = MMPickaxeT2Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.25;
};

function rpgPickaxeT2Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(6); }

$MM::ItemCost["MMPickaxeT3Item"] = "152870\tCredits\t5\tRuthenium\t10\tOsmium\t6\tTungsten";
$MM::ItemDisc["MMPickaxeT3Item"] = "A classic pickaxe! Grants 5% more damage and 5% less mining level requirement with no downside.";
datablock ItemData(MMPickaxeT3Item : MMPickaxeT0Item)
{
	shapeFile = "./Shapes/T3Pick.dts";
	uiName = "Superior Pickaxe";
	colorShiftColor = "1.000 1.000 1.000 1.000";
	image = rpgPickaxeT3Image;
	iconName = "./Shapes/T3Pick";
};

datablock ShapeBaseImageData(rpgPickaxeT3Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/T3Pick.dts";

	item = MMPickaxeT3Item;

	doColorShift = MMPickaxeT3Item.doColorShift;
	colorShiftColor = MMPickaxeT3Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.19;
};

function rpgPickaxeT3Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(7); }

$MM::ItemCost["MMPickaxeT4Item"] = "1113080\tCredits\t5\tXenon\t10\tHelium\t6\tBismuth";
$MM::ItemDisc["MMPickaxeT4Item"] = "A classic pickaxe! Grants 5% more damage and 5% less mining level requirement with no downside.";
datablock ItemData(MMPickaxeT4Item : MMPickaxeT0Item)
{
	shapeFile = "./Shapes/T4Pick.dts";
	uiName = "Epic Pickaxe";
	colorShiftColor = "1.000 1.000 1.000 1.000";
	image = rpgPickaxeT4Image;
	iconName = "./Shapes/T4Pick";
};

datablock ShapeBaseImageData(rpgPickaxeT4Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/T4Pick.dts";

	item = MMPickaxeT4Item;

	doColorShift = MMPickaxeT4Item.doColorShift;
	colorShiftColor = MMPickaxeT4Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.14;
};

function rpgPickaxeT4Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(7); }

$MM::ItemCost["MMPickaxeT5Item"] = "1113080\tCredits\t5\tXenon\t10\tHelium\t6\tBismuth";
$MM::ItemDisc["MMPickaxeT5Item"] = "A classic pickaxe! Grants 5% more damage and 5% less mining level requirement with no downside.";
datablock ItemData(MMPickaxeT5Item : MMPickaxeT0Item)
{
	shapeFile = "./Shapes/T5Pick.dts";
	uiName = "Legendary Pickaxe";
	colorShiftColor = "1.000 1.000 1.000 1.000";
	image = rpgPickaxeT5Image;
	iconName = "./Shapes/T5Pick";
};

datablock ShapeBaseImageData(rpgPickaxeT5Image : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/T5Pick.dts";

	item = MMPickaxeT5Item;

	doColorShift = MMPickaxeT5Item.doColorShift;
	colorShiftColor = MMPickaxeT5Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.11;
};

function rpgPickaxeT5Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Generic(7); }

datablock ItemData(MMPickaxeDebugItem : MMPickaxeT0Item)
{
	shapeFile = "./Shapes/T5Pick.dts";
	uiName = "Debug Pickaxe";
	colorShiftColor = "0.000 0.000 0.000 1.000";
	image = rpgPickaxeDebugImage;
	iconName = "./Shapes/T5Pick";

	recycleLoot = "1\tInfinity\t1000\tCredits";
};

$MM::ItemCost["rpgPickaxeDebugImage"] = "1\tInfinity";
$MM::ItemDisc["rpgPickaxeDebugImage"] = "What are you doing?";
datablock ShapeBaseImageData(rpgPickaxeDebugImage : rpgPickaxeT0Image)
{
	shapeFile = "./Shapes/T5Pick.dts";

	item = MMPickaxeDebugItem;

	doColorShift = MMPickaxeDebugItem.doColorShift;
	colorShiftColor = MMPickaxeDebugItem.colorShiftColor;

	stateTimeoutValue[2]            = 0.01;
};

function rpgPickaxeDebugImage::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Debug(32); }

function Player::MMPickaxe_Debug(%obj, %dist)
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
		%matter = getMatterType(%hit.matter);

		if (GetMatterValue(%matter) > 0 && !%obj.isCrouched())
			return;

		if (%matter.hitSound !$= "")
			%hit.playSound("MM_" @ %matter.hitSound @ getRandom(1, $MM::SoundCount[%matter.hitSound]) @ "Sound");

		%hit.MineDamage(999999, "Debug");
	}
}