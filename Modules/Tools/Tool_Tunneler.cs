function Player::MMPickaxe_Tunneler(%obj, %dist)
{
	if (!isObject(%client = %obj.client))
		return;

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::PlayerObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, mClamp(%dist, 3, 100))), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)))
	{
		%damage = %client.GetPickaxeDamage();
		%hitpos = %hit.getPosition();
		%lookVec = %obj.FaceDirection();
		%cross1 = %obj.FaceDirection(%obj.getLeftVector());
		%cross2 = vectorCross(%cross1, %lookVec);

		if (getSimTime() - %obj.MM_TunnelerLastUse > 2500)
			for (%i = 0; %i < 4; %i++)
				%obj.MM_TunnelerPos[%i] = "";

		%obj.MM_TunnelerLastUse = getSimTime();

		for (%i = 0; %i < 4; %i++)
			if (isObject($MM::BrickGrid[%obj.MM_TunnelerPos[%i]]))
				%skipBrickScan = true;

		if (!%skipBrickScan)
		{
			%obj.MM_TunnelerPos[0] = roundVector(vectorAdd(%hitpos, vectorScale(%cross1, $MM::BrickDistance)));
			%obj.MM_TunnelerPos[1] = roundVector(vectorAdd(%hitpos, vectorScale(vectorAdd(%cross1, %cross2), $MM::BrickDistance)));
			%obj.MM_TunnelerPos[2] = roundVector(vectorAdd(%hitpos, vectorScale(%cross2, $MM::BrickDistance)));
			%obj.MM_TunnelerPos[3] = roundVector(vectorAdd(%hitpos, vectorScale("0 0 0", $MM::BrickDistance)));
		}

		%raypos = getWords(%ray, 1, 3);

		if (!%client.MM_noMiningDebris)
			spawnExplosion(dirtHitProjectile, %raypos, %client);

		if (%hit.getType() & $Typemasks::PlayerObjectType)
			%obj.MM_AttemptMine(%hit);
		
		for (%i = 0; %i < 4; %i++)
		{
			RevealBlock(%obj.MM_TunnelerPos[%i]);
			if (isObject(%brick = $MM::BrickGrid[%obj.MM_TunnelerPos[%i]]))
			{
				%args = "";
				if (getMatterType(%brick.matter).hazard && %client.ChangeBatteryEnergy(-1000))
					%args = "bypassHitFunc\tbypassHarvestFunc";

				%obj.MM_AttemptMine(%brick, 0.5, "", %args);
			}
		}
	}
}

$MM::ItemCost["MMTunnelerT1Item"] = "360\tCredits\t3\tAntimony\t5\tAluminum\t10\tIron";
$MM::ItemDisc["MMTunnelerT1Item"] = "Targets a 2x2 area, while dealing 50\% damage. Uses energy to migitate hazard effects.";
datablock ItemData(MMTunnelerT1Item : swordItem)
{
	shapeFile = "./Shapes/T1Pick.dts";
	uiName = "Basic Tunneler";
	doColorShift = true;
	colorShiftColor = "1.000 0.000 0.000 1.000";

	image = rpgTunnelerT1Image;
	canDrop = true;
	iconName = "./Shapes/T1Pick";
};

datablock ShapeBaseImageData(rpgTunnelerT1Image)
{
	shapeFile = "./Shapes/T1Pick.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";

	correctMuzzleVector = false;

	className = "WeaponImage";

	item = MMTunnelerT1Item;
	ammo = " ";
	projectile = gunProjectile;
	projectileType = Projectile;

	melee = true;
	doRetraction = false;

	armReady = true;

	doColorShift = MMTunnelerT1Item.doColorShift;
	colorShiftColor = MMTunnelerT1Item.colorShiftColor;

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

function rpgTunnelerT1Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Tunneler(4); }

$MM::ItemCost["MMTunnelerT2Item"] = "7050\tCredits\t4\tApatite\t5\tLithium\t10\tNickel";
$MM::ItemDisc["MMTunnelerT2Item"] = "Targets a 2x2 area, while dealing 50\% damage. Uses energy to migitate hazard effects.";
datablock ItemData(MMTunnelerT2Item : MMTunnelerT1Item)
{
	shapeFile = "./Shapes/T2Pick.dts";
	uiName = "Improved Tunneler";
	colorShiftColor = "1.000 0.000 0.000 1.000";
	image = rpgTunnelerT2Image;
	iconName = "./Shapes/T2Pick";
};

datablock ShapeBaseImageData(rpgTunnelerT2Image : rpgTunnelerT1Image)
{
	shapeFile = "./Shapes/T2Pick.dts";

	item = MMTunnelerT2Item;

	doColorShift = MMTunnelerT2Item.doColorShift;
	colorShiftColor = MMTunnelerT2Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.25;
};

function rpgTunnelerT2Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Tunneler(5); }

$MM::ItemCost["MMTunnelerT3Item"] = "152870\tCredits\t5\tIridium\t10\tNeodymium\t10\tUranium";
$MM::ItemDisc["MMTunnelerT3Item"] = "Targets a 2x2 area, while dealing 50\% damage. Uses energy to migitate hazard effects.";
datablock ItemData(MMTunnelerT3Item : MMTunnelerT1Item)
{
	shapeFile = "./Shapes/T3Pick.dts";
	uiName = "Superior Tunneler";
	colorShiftColor = "1.000 0.000 0.000 1.000";
	image = rpgTunnelerT3Image;
	iconName = "./Shapes/T3Pick";
};

datablock ShapeBaseImageData(rpgTunnelerT3Image : rpgTunnelerT1Image)
{
	shapeFile = "./Shapes/T3Pick.dts";

	item = MMTunnelerT3Item;

	doColorShift = MMTunnelerT3Item.doColorShift;
	colorShiftColor = MMTunnelerT3Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.19;
};

function rpgTunnelerT3Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Tunneler(5); }

$MM::ItemCost["MMTunnelerT4Item"] = "1113080\tCredits\t5\tActinium\t10\tPlutonium\t10\tKrypton";
$MM::ItemDisc["MMTunnelerT4Item"] = "Targets a 2x2 area, while dealing 50\% damage. Uses energy to migitate hazard effects.";
datablock ItemData(MMTunnelerT4Item : MMTunnelerT1Item)
{
	shapeFile = "./Shapes/T4Pick.dts";
	uiName = "Epic Tunneler";
	colorShiftColor = "1.000 0.000 0.000 1.000";
	image = rpgTunnelerT4Image;
	iconName = "./Shapes/T4Pick";
};

datablock ShapeBaseImageData(rpgTunnelerT4Image : rpgTunnelerT1Image)
{
	shapeFile = "./Shapes/T4Pick.dts";

	item = MMTunnelerT4Item;

	doColorShift = MMTunnelerT4Item.doColorShift;
	colorShiftColor = MMTunnelerT4Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.14;
};

function rpgTunnelerT4Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Tunneler(5); }

$MM::ItemCost["MMTunnelerT5Item"] = "1113080\tCredits\t5\tActinium\t10\tPlutonium\t10\tKrypton";
$MM::ItemDisc["MMTunnelerT5Item"] = "Targets a 2x2 area, while dealing 50\% damage. Uses energy to migitate hazard effects.";
datablock ItemData(MMTunnelerT5Item : MMTunnelerT1Item)
{
	shapeFile = "./Shapes/T5Pick.dts";
	uiName = "Legendary Tunneler";
	colorShiftColor = "1.000 0.000 0.000 1.000";
	image = rpgTunnelerT5Image;
	iconName = "./Shapes/T5Pick";
};

datablock ShapeBaseImageData(rpgTunnelerT5Image : rpgTunnelerT1Image)
{
	shapeFile = "./Shapes/T5Pick.dts";

	item = MMTunnelerT5Item;

	doColorShift = MMTunnelerT5Item.doColorShift;
	colorShiftColor = MMTunnelerT5Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.11;
};

function rpgTunnelerT5Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Tunneler(5); }