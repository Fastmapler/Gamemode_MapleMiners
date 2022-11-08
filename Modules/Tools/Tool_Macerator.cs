function Player::MMPickaxe_Macerator(%obj, %dist)
{
	if (!isObject(%client = %obj.client))
		return;

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, mClamp(%dist, 3, 100))), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)) && %hit.canMine)
	{
		if (%obj.lastMacHit == %hit.getID() && %obj.MacHitCount < 10)
			%obj.MacHitCount++;
		else if (!isObject(%obj.lastMacHit) || %obj.lastMacHit != %hit.getID())
		{
			%obj.lastMacHit = %hit.getID();
			%obj.MacHitCount = 0;
		}

		%multiplier = 0.5 + (%obj.MacHitCount * 0.1);
		%raypos = getWords(%ray, 1, 3);

		if (!%client.MM_noMiningDebris)
			spawnExplosion(dirtHitProjectile, %raypos, %client);
		%obj.MM_AttemptMine(%hit, %multiplier, mRound(%multiplier * 100) @ "\% damage");
	}
}

$MM::ItemCost["MMMaceratorT1Item"] = "360\tCredits\t2\tGallium\t5\tAluminum\t10\tTin";
$MM::ItemDisc["MMMaceratorT1Item"] = "Initally deals 50% damage, but scales up to 150% for each consecutive hit.";
datablock ItemData(MMMaceratorT1Item : swordItem)
{
	shapeFile = "./Shapes/T1Pick.dts";
	uiName = "Basic Macerator";
	doColorShift = true;
	colorShiftColor = "0.000 1.000 0.000 1.000";

	image = rpgMaceratorT1Image;
	canDrop = true;
	iconName = "./Shapes/T1Pick";
};

datablock ShapeBaseImageData(rpgMaceratorT1Image)
{
	shapeFile = "./Shapes/T1Pick.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";

	correctMuzzleVector = false;

	className = "WeaponImage";

	item = MMMaceratorT1Item;
	ammo = " ";
	projectile = gunProjectile;
	projectileType = Projectile;

	melee = true;
	doRetraction = false;

	armReady = true;

	doColorShift = MMMaceratorT1Item.doColorShift;
	colorShiftColor = MMMaceratorT1Item.colorShiftColor;

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

function rpgMaceratorT1Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Macerator(4); }

$MM::ItemCost["MMMaceratorT2Item"] = "7050\tCredits\t3\tLead\t5\tLithium\t10\tGarnet";
$MM::ItemDisc["MMMaceratorT2Item"] = "Initally deals 50% damage, but scales up to 150% for each consecutive hit.";
datablock ItemData(MMMaceratorT2Item : MMMaceratorT1Item)
{
	shapeFile = "./Shapes/T2Pick.dts";
	uiName = "Improved Macerator";
	colorShiftColor = "0.000 1.000 0.000 1.000";
	image = rpgMaceratorT2Image;
	iconName = "./Shapes/T2Pick";
};

datablock ShapeBaseImageData(rpgMaceratorT2Image : rpgMaceratorT1Image)
{
	shapeFile = "./Shapes/T2Pick.dts";

	item = MMMaceratorT2Item;

	doColorShift = MMMaceratorT2Item.doColorShift;
	colorShiftColor = MMMaceratorT2Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.25;
};

function rpgMaceratorT2Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Macerator(4); }

$MM::ItemCost["MMMaceratorT3Item"] = "152870\tCredits\t4\tPalladium\t5\tNeodymium\t10\tTungsten";
$MM::ItemDisc["MMMaceratorT3Item"] = "Initally deals 50% damage, but scales up to 150% for each consecutive hit.";
datablock ItemData(MMMaceratorT3Item : MMMaceratorT1Item)
{
	shapeFile = "./Shapes/T3Pick.dts";
	uiName = "Superior Macerator";
	colorShiftColor = "0.000 1.000 0.000 1.000";
	image = rpgMaceratorT3Image;
	iconName = "./Shapes/T3Pick";
};

datablock ShapeBaseImageData(rpgMaceratorT3Image : rpgMaceratorT1Image)
{
	shapeFile = "./Shapes/T3Pick.dts";

	item = MMMaceratorT3Item;

	doColorShift = MMMaceratorT3Item.doColorShift;
	colorShiftColor = MMMaceratorT3Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.19;
};

function rpgMaceratorT3Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Macerator(5); }

$MM::ItemCost["MMMaceratorT4Item"] = "1113080\tCredits\t4\tPromethium\t5\tPlutonium\t10\tBismuth";
$MM::ItemDisc["MMMaceratorT4Item"] = "Initally deals 50% damage, but scales up to 150% for each consecutive hit.";
datablock ItemData(MMMaceratorT4Item : MMMaceratorT1Item)
{
	shapeFile = "./Shapes/T4Pick.dts";
	uiName = "Epic Macerator";
	colorShiftColor = "0.000 1.000 0.000 1.000";
	image = rpgMaceratorT4Image;
	iconName = "./Shapes/T4Pick";
};

datablock ShapeBaseImageData(rpgMaceratorT4Image : rpgMaceratorT1Image)
{
	shapeFile = "./Shapes/T4Pick.dts";

	item = MMMaceratorT4Item;

	doColorShift = MMMaceratorT4Item.doColorShift;
	colorShiftColor = MMMaceratorT4Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.14;
};

function rpgMaceratorT4Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Macerator(5); }

$MM::ItemCost["MMMaceratorT5Item"] = "1113080\tCredits\t4\tPromethium\t5\tPlutonium\t10\tBismuth";
$MM::ItemDisc["MMMaceratorT5Item"] = "Initally deals 50% damage, but scales up to 150% for each consecutive hit.";
datablock ItemData(MMMaceratorT5Item : MMMaceratorT1Item)
{
	shapeFile = "./Shapes/T5Pick.dts";
	uiName = "Legendary Macerator";
	colorShiftColor = "0.000 1.000 0.000 1.000";
	image = rpgMaceratorT5Image;
	iconName = "./Shapes/T5Pick";
};

datablock ShapeBaseImageData(rpgMaceratorT5Image : rpgMaceratorT1Image)
{
	shapeFile = "./Shapes/T5Pick.dts";

	item = MMMaceratorT5Item;

	doColorShift = MMMaceratorT5Item.doColorShift;
	colorShiftColor = MMMaceratorT5Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.11;
};

function rpgMaceratorT5Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Macerator(5); }