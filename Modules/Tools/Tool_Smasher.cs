function Player::MMPickaxe_Smasher(%obj, %dist)
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
		%matter = getMatterType(%hit.matter);
		%raypos = getWords(%ray, 1, 3);
		spawnExplosion(dirtHitProjectile, %raypos, %client);

		%maxEnergy = %obj.getDatablock().maxEnergy;
		%multiplier = mFloatLength((%obj.getEnergyLevel() / %maxEnergy) * 5, 2);
		%obj.ChangeEnergyLevel(%maxEnergy / -5);
		%obj.MM_AttemptMine(%hit, %multiplier, %multiplier @ "x Damage");
	}
}

$MM::ItemCost["MMSmasherT1Item"] = "360\tCredits\t2\tQuartz\t5\tAluminum\t10\tCopper";
$MM::ItemDisc["MMSmasherT1Item"] = "Uses player 'Jet Energy' to deal up to 5x damage per swing. Always consumes 20% energy per hit.";
datablock ItemData(MMSmasherT1Item : swordItem)
{
	shapeFile = "./Shapes/T1Pick.dts";
	uiName = "Basic Smasher";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 1.000 1.000";

	image = rpgSmasherT1Image;
	canDrop = true;
	iconName = "./Shapes/T1Pick";
};

datablock ShapeBaseImageData(rpgSmasherT1Image)
{
	shapeFile = "./Shapes/T1Pick.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";

	correctMuzzleVector = false;

	className = "WeaponImage";

	item = MMSmasherT1Item;
	ammo = " ";
	projectile = gunProjectile;
	projectileType = Projectile;

	melee = true;
	doRetraction = false;

	armReady = true;

	doColorShift = MMSmasherT1Item.doColorShift;
	colorShiftColor = MMSmasherT1Item.colorShiftColor;

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

function rpgSmasherT1Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Smasher(4); }

$MM::ItemCost["MMSmasherT2Item"] = "7050\tCredits\t3\tSilver\t6\tLithium\t10\tFluorite";
$MM::ItemDisc["MMSmasherT2Item"] = "Uses player 'Jet Energy' to deal up to 5x damage per swing. Always consumes 20% energy per hit.";
datablock ItemData(MMSmasherT2Item : MMSmasherT1Item)
{
	shapeFile = "./Shapes/T2Pick.dts";
	uiName = "Improved Smasher";
	colorShiftColor = "0.000 0.000 1.000 1.000";
	image = rpgSmasherT2Image;
	iconName = "./Shapes/T2Pick";
};

datablock ShapeBaseImageData(rpgSmasherT2Image : rpgSmasherT1Image)
{
	shapeFile = "./Shapes/T2Pick.dts";

	item = MMSmasherT2Item;

	doColorShift = MMSmasherT2Item.doColorShift;
	colorShiftColor = MMSmasherT2Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.25;
};

function rpgSmasherT2Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Smasher(5); }

$MM::ItemCost["MMSmasherT3Item"] = "152870\tCredits\t4\tThorium\t7\tNeodymium\t10\tRuthenium";
$MM::ItemDisc["MMSmasherT3Item"] = "Uses player 'Jet Energy' to deal up to 5x damage per swing. Always consumes 20% energy per hit.";
datablock ItemData(MMSmasherT3Item : MMSmasherT1Item)
{
	shapeFile = "./Shapes/T3Pick.dts";
	uiName = "Superior Smasher";
	colorShiftColor = "0.000 0.000 1.000 1.000";
	image = rpgSmasherT3Image;
	iconName = "./Shapes/T3Pick";
};

datablock ShapeBaseImageData(rpgSmasherT3Image : rpgSmasherT1Image)
{
	shapeFile = "./Shapes/T3Pick.dts";

	item = MMSmasherT3Item;

	doColorShift = MMSmasherT3Item.doColorShift;
	colorShiftColor = MMSmasherT3Item.colorShiftColor;

	stateTimeoutValue[2]            = 0.19;
};

function rpgSmasherT3Image::onFire(%this, %obj, %slot) { %obj.playThread(0, "shiftDown"); %obj.MMPickaxe_Smasher(5); }