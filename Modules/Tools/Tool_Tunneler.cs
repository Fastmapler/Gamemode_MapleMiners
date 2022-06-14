function Player::MMPickaxe_Tunneler(%obj, %dist)
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
		%cross1 = %obj.FaceDirection(%obj.getLeftVector());
		%cross2 = vectorCross(%cross1, %lookVec);

		%pos[0] = roundVector(vectorAdd(%hitpos, vectorScale(%cross1, $MM::BrickDistance)));
		%pos[1] = roundVector(vectorAdd(%hitpos, vectorScale(vectorAdd(%cross1, %cross2), $MM::BrickDistance)));
		%pos[2] = roundVector(vectorAdd(%hitpos, vectorScale(%cross2, $MM::BrickDistance)));
		%pos[3] = roundVector(vectorAdd(%hitpos, vectorScale("0 0 0", $MM::BrickDistance)));

		for (%i = 0; %i < 4; %i++)
		{
			RevealBlock(%pos[%i]);
			if (isObject(%brick = $MM::BrickGrid[%pos[%i]]))
			{
				%obj.MM_AttemptMine(%brick);
				break;
			}
		}
	}
}

$MM::ItemCost["MMTunnelerT1Item"] = "360\tCredits\t3\tAntimony\t5\tAluminum\t10\tIron";
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
	stateTimeoutValue[0]             = 0.1;
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

$MM::ItemCost["MMTunnelerT3Item"] = "1\tInfinity";
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