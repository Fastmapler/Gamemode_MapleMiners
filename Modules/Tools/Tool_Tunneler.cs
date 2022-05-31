function Player::MMPickaxe_Tunneler(%obj, %dist)
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

		%hitpos = %hit.getPosition();
		%hit.MineDamage(%damage, "Pickaxe", %client);
		
		%pos2 = roundVector(vectorAdd(%hitpos, vectorScale("0 0 -1", $MM::BrickDistance)));
		if ($MM::BrickGrid[%pos2] $= "")
			RevealBlock(%pos2);
		if (isObject($MM::BrickGrid[%pos2]))
			$MM::BrickGrid[%pos2].MineDamage(%damage, "Pickaxe", %client);
	}
}

datablock ItemData(MMTunnelerT1Item : swordItem)
{
	shapeFile = "./Shapes/Pickaxe.dts";
	uiName = "Basic Tunneler";
	doColorShift = true;
	colorShiftColor = "1.000 0.000 0.000 1.000";

	image = rpgTunnelerT1Image;
	canDrop = true;
	iconName = "./Shapes/icon_Pickaxe";
};

datablock ShapeBaseImageData(rpgTunnelerT1Image)
{
	shapeFile = "./Shapes/Pickaxe.dts";
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
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]                    = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "CheckFire";
	stateTimeoutValue[2]            = 0.66;
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