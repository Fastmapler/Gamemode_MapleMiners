datablock AudioProfile(InfernalBlob_PainSound)
{
	fileName = "./EnemyShapes/blob_pain.wav";
	description = AudioClose3d;
	preload = true;
};
datablock AudioProfile(InfernalBlob_DeathSound)
{
	fileName = "./EnemyShapes/blob_death.wav";
	description = AudioClose3d;
	preload = true;
};

datablock PlayerData(InfernalBlobHoleBot : PlayerStandardArmor)
{
	uiName = "";
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	maxItems   = 0;
	maxWeapons = 0;
	maxTools = 0;
	jumpForce = 12 * 90;
	runforce = 40 * 90;
	maxForwardSpeed = 3.5;
	maxBackwardSpeed = 3.5;
	maxSideSpeed = 3.5;
	attackpower = 10; //What does this do?
	rideable = false;
	canRide = false;
	maxdamage = 400;//Health
	jumpSound = "";
	
	boundingBox				= VectorScale("1.25 1.25 1.33", 4);
    crouchBoundingBox		= VectorScale("1.25 1.25 0.05", 4);
    proneBoundingBox		= VectorScale("1.25 1.25 0.05", 4);
	
	useCustomPainEffects = true;
	PainHighImage = "PainHighImage";
	PainMidImage  = "PainMidImage";
	PainLowImage  = "PainLowImage";
	PainSound           = InfernalBlob_PainSound;
	DeathSound          = InfernalBlob_DeathSound;
	
	//Hole Attributes
	isHoleBot = 1;
	
	//Spawning option
	hSpawnTooClose = 0;//Doesn't spawn when player is too close and can see it
	  hSpawnTCRange = 8;//above range, set in brick units
	hSpawnClose = 0;//Only spawn when close to a player, can be used with above function as long as hSCRange is higher than hSpawnTCRange
	  hSpawnCRange = 64;//above range, set in brick units

	hType = enemy; //Enemy,Friendly, Neutral
	  hNeutralAttackChance = 100;
	//can have unique types, nazis will attack zombies but nazis will not attack other bots labeled nazi
	hName = "Giant Blob";//cannot contain spaces
	hTickRate = 3000;
	
	//Wander Options
	hWander = 1;//Enables random walking
	  hSmoothWander = 1;
	  //hReturnToSpawn = 1;//Returns to spawn when too far //Always false
	  //hSpawnDist = 32;//Defines the distance bot can travel away from spawnbrick //Always 10000
	
	//Searching options
	hSearch = 1;//Search for Players
	  hSearchRadius = 2048;//in brick units
	  hSight = 1;//Require bot to see player before pursuing
	  hStrafe = 1;//Randomly strafe while following player
	hSearchFOV = 0;//if enabled disables normal hSearch
	  hFOVRadius = 32;//max 10
	   hHearing = 1;//If it hears a player it'll look in the direction of the sound

	  hAlertOtherBots = 1;//Alerts other bots when he sees a player, or gets attacked

	//Attack Options
	hMelee = 1;//Melee
	  hAttackDamage = 15;//Melee Damage
	  hDamageType = "InfernalBlobMelee";
	hShoot = 1;
	  hWep = InfernalBlobWepImage;
	  hShootTimes = 4;//Number of times the bot will shoot between each tick
	  hMaxShootRange = 2048;//The range in which the bot will shoot the player
	  hAvoidCloseRange = 1;//
		hTooCloseRange = 7;//in brick units
	//hHerding = 0;
	//hSound = 1;
	//hSpawnDetect = -1;//Will not spawn when user is too close and can see spawn
	

	
	//Misc options
	hAvoidObstacles = 1;
	hSuperStacker = 0;
	hSpazJump = 1;//Makes bot jump when the user their following is higher than them

	hAFKOmeter = 1;//Determines how often the bot will wander or do other idle actions, higher it is the less often he does things
	hIdle = 0;// Enables use of idle actions, actions which are done when the bot is not doing anything else
	  hIdleAnimation = 0;//Plays random animations/emotes, sit, click, love/hate/etc
	  hIdleLookAtOthers = 1;//Randomly looks at other players/bots when not doing anything else
	    hIdleSpam = 0;//Makes them spam click and spam hammer/spraycan
	  hSpasticLook = 1;//Makes them look around their environment a bit more.
	hEmote = 1;
	
	scoreModifier = 0.25;
	
	xpDrop = 100;
};

datablock PlayerData(InfernalBlobMeleeHoleBot : InfernalBlobHoleBot)
{
	maxdamage = 180;//Health
	hName = "Melee Blob";//cannot contain spaces
	
	maxForwardSpeed = 7;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;
	
	//Attack Options
	hMelee = 1;//Melee
	  hAttackDamage = 15; //Melee Damage
	  hDamageType = "InfernalBlobMelee";
	hShoot = 0;
	  hWep = InfernalBlobWepImage;
	  hShootTimes = 4;//Number of times the bot will shoot between each tick
	  hMaxShootRange = 2048;//The range in which the bot will shoot the player
	  hAvoidCloseRange = 1;//
		hTooCloseRange = 7;//in brick units
	
	scoreModifier = (5 / 9);
	blobForceAttack = "Melee";
	xpDrop = 100;
};

datablock PlayerData(InfernalBlobRangedHoleBot : InfernalBlobHoleBot)
{
	maxdamage = 180;//Health
	hName = "Ranger Blob";//cannot contain spaces
	
	maxForwardSpeed = 7;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;
	
	//Attack Options
	hMelee = 0;//Melee
	  hAttackDamage = 15; //Melee Damage
	  hDamageType = "InfernalBlobMelee";
	hShoot = 1;
	  hWep = InfernalBlobWepImage;
	  hShootTimes = 4;//Number of times the bot will shoot between each tick
	  hMaxShootRange = 2048;//The range in which the bot will shoot the player
	  hAvoidCloseRange = 1;//
		hTooCloseRange = 7;//in brick units
	
	scoreModifier = 0.56;
	blobForceAttack = "Ranged";
	xpDrop = 100;
};

datablock PlayerData(InfernalBlobMagicHoleBot : InfernalBlobHoleBot)
{
	maxdamage = 180;//Health
	hName = "Mage Blob";//cannot contain spaces
	
	maxForwardSpeed = 7;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;
	
	//Attack Options
	hMelee = 0;//Melee
	  hAttackDamage = 15; //Melee Damage
	  hDamageType = "InfernalBlobMelee";
	hShoot = 1;
	  hWep = InfernalBlobWepImage;
	  hShootTimes = 4;//Number of times the bot will shoot between each tick
	  hMaxShootRange = 2048;//The range in which the bot will shoot the player
	  hAvoidCloseRange = 1;//
		hTooCloseRange = 7;//in brick units
	
	scoreModifier = 0.56;
	blobForceAttack = "Magic";
	xpDrop = 100;
};

AddDamageType("InfernalBlobMelee",   '%1 boiled itself.',    '%2 boiled %1.',0.2,1);
AddDamageType("InfernalBlobRanged",   '%1 desecrated itself.',    '%2 desecrated %1.',0.2,1);
AddDamageType("InfernalBlobMagic",   '%1 burned itself.',    '%2 burned %1.',0.2,1);

datablock ItemData(InfernalBlobWepItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Weapon_Gun/pistol.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Blob Attack";
	iconName = "Add-Ons/Weapon_Gun/icon_gun";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1.000";

	 // Dynamic properties defined by the scripts
	image = InfernalBlobWepImage;
	canDrop = true;
};

datablock ShapeBaseImageData(InfernalBlobWepImage)
{
   // Basic Item properties
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = InfernalBlobWepItem;
   ammo = " ";
   projectile = InfernalBlobWepProjectile;
   projectileType = Projectile;

	casing = gunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = gunItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]					= "Activate";
	stateTimeoutValue[0]			= 0.15;
	stateTransitionOnTimeout[0]		= "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]					= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]		= true;
	stateSequence[1]				= "Ready";
	
	stateName[2]					= "Charge";
	stateTransitionOnTimeout[2]     = "Fire";
	stateTimeoutValue[2]            = 0.6;
	stateWaitForTimeout[2]			= true;
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Reload";
	stateTimeoutValue[3]            = 0.6;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]			= true;
	stateSound[3]					= spearFireSound;

	stateName[4]					= "Reload";
	stateTransitionOnTimeout[4]     = "Ready";
	stateTimeoutValue[4]            = 1.2;
	stateWaitForTimeout[4]			= true;

};

function InfernalBlobWepImage::onCharge(%this,%obj,%slot)
{
	%obj.playThread(2, "crouch");
	
	if (!%obj.infernalChargingAttack)
	{
		%obj.infernalChargingAttack = true;
		%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() / 4);
		%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() / 4);
		%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() / 4);
	}
}

function InfernalBlobWepImage::onFire(%this,%obj,%slot)
{
	if((%obj.lastFireTime+%this.minShotTime) > getSimTime() || %obj.getState() $= "DEAD")
		return;
		
	%obj.lastFireTime = getSimTime();
	
	%obj.playThread(2, "activate2");
	
	if (%obj.infernalChargingAttack)
	{
		%obj.infernalChargingAttack = false;
		%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() * 4);
		%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() * 4);
		%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() * 4);
	}

	if ((%obj.getDatablock().blobForceAttack !$= "Ranged" && getRandom() < 0.5) || %obj.getDatablock().blobForceAttack $= "Magic")
	{ //Magic
		%projectile = InfernalMageMagicProjectile;
		%spread = 0.005;
		%shellcount = 4;
		%magic = true;
	}
	else
	{ //Ranged
		%projectile = InfernalRangerStoneProjectile;
		%spread = 0.001;
		%shellcount = 2;
	}

	for(%shell=0; %shell<%shellcount; %shell++)
	{
		if (%this.melee)
			%vector = %obj.getEyeVector();
		else
			%vector = %obj.getMuzzleVector(%slot);
			
		%objectVelocity = %obj.getVelocity();
		%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
		%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
		%velocity = VectorAdd(%vector1,%vector2);
		
		if (%magic)
		{
			if (getRandom() < 0.5)
				%x = ((getRandom() / 2) + 0.5) * 10 * 3.1415926 * %spread;
			else
				%x = ((getRandom() / -2) - 0.5) * 10 * 3.1415926 * %spread;
				
			if (getRandom() < 0.5)
				%y = ((getRandom() / 2) + 0.5) * 10 * 3.1415926 * %spread;
			else
				%y = ((getRandom() / -2) - 0.5) * 10 * 3.1415926 * %spread;
				
			if (getRandom() < 0.5)
				%z = ((getRandom() / 2) + 0.5) * 10 * 3.1415926 * %spread;
			else
				%z = ((getRandom() / -2) - 0.5) * 10 * 3.1415926 * %spread;
		}
		else
		{
			%x = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
			%y = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
			%z = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		}
		
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		%velocity = MatrixMulVector(%mat, %velocity);

		if (%this.melee)
			%position = %obj.getEyePoint();
		else
			%position = %obj.getMuzzlePoint(%slot);
		
		%p = new (%this.projectileType)()
		{
			dataBlock = %projectile;
			initialVelocity = %velocity;
			initialPosition = %position;
			sourceObject = %obj;
			sourceSlot = %slot;
			client = %obj.client;
		};
		%scale = %obj.getScale();
		
		MissionCleanup.add(%p);
		%p.setScale((getWord(%scale,0) / 2) SPC (getWord(%scale,1) / 2) SPC (getWord(%scale,2) / 2));
	}
	return %p;
}

package InfernalBlobSplit
{
	function Armor::damage(%this, %obj, %sourceObj, %position, %damage, %damageType)
	{
		%toReturn = parent::damage(%this, %obj, %sourceObj, %position, %damage, %damageType);
		
		if (%obj.getState() $= "DEAD" && %obj.getClassName() $= "AIPlayer")
		{
			if (%obj.getDatablock().getName() $= "InfernalBlobHoleBot" && !%obj.summonedChildren)
			{
				%obj.summonedChildren = true;
				%x = getWord(%obj.getPosition(),0);
				%y = getWord(%obj.getPosition(),1);
				%z = getWord(%obj.getPosition(),2);
				spawnNewEnemy((%x + getRandom(-1,1)) SPC (%y + getRandom(-1,1)) SPC %z,"InfernalBlobMeleeHoleBot");
				spawnNewEnemy((%x + getRandom(-1,1)) SPC (%y + getRandom(-1,1)) SPC %z,"InfernalBlobRangedHoleBot");
				spawnNewEnemy((%x + getRandom(-1,1)) SPC (%y + getRandom(-1,1)) SPC %z,"InfernalBlobMagicHoleBot");
				%obj.schedule(100,"delete");
			}
		}
		
		return %toReturn;
	}
};
activatePackage("InfernalBlobSplit");