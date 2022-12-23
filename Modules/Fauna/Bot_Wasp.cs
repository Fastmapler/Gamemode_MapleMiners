datablock PlayerData(InfernalBatHoleBot : PlayerStandardArmor)
{
	shapeFile = "./EnemyShapes/blue_wasp2.dts";
	uiName = "";
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	maxItems   = 0;
	maxWeapons = 0;
	maxTools = 0;
	jumpForce = 12 * 90;
	runforce = 40 * 90;
	maxForwardSpeed = 7;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;
	attackpower = 10; //What does this do?
	rideable = false;
	canRide = false;
	maxdamage = 250;//Health
	jumpSound = "";
	
	boundingBox				= vectorScale("1.82 1.82 1.4", 4); //"2.5 2.5 2.4";
    crouchBoundingBox		= vectorScale("1.82 1.82 0.9", 4); //"2.5 2.5 2.4";
    proneBoundingBox		= vectorScale("1.82 1.82 0.9", 4); //"2.5 2.5 2.4";
   
	maxEnergy = 150;
	rechargeRate = 1.5;
	showEnergyBar = true;

	useCustomPainEffects = true;
	PainHighImage		= "PainHighImage";
	PainMidImage		= "PainMidImage";
	PainLowImage		= "PainLowImage";
	PainSound			= InfernalBat_PainSound;
	DeathSound			= InfernalBat_DeathSound;
	
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
	hName = "Wasp";//cannot contain spaces
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
	  hAttackDamage = 8;//15;//Melee Damage
	  hDamageType = "InfernalBatMelee";
	hShoot = 1;
	  hWep = InfernalBatStingerImage;
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
	
	scoreModifier = 0.8;
	xpDrop = 200;
};

datablock TSShapeConstructor(InfernalBatDts)
{
	baseShape  = "./EnemyShapes/blue_wasp2.dts";
	sequence0  = "./EnemyShapes/blwsp_root.dsq root";

	sequence1  = "./EnemyShapes/blwsp_run.dsq run";
	sequence2  = "./EnemyShapes/blwsp_run.dsq walk";
	sequence3  = "./EnemyShapes/blwsp_back.dsq back";
	sequence4  = "./EnemyShapes/blwsp_side.dsq side";

	sequence5  = "./EnemyShapes/blwsp_crouch.dsq crouch";
	sequence6  = "./EnemyShapes/blwsp_crouchMove.dsq crouchRun";
	sequence7  = "./EnemyShapes/blwsp_crouchBack.dsq crouchBack";
	sequence8  = "./EnemyShapes/blwsp_crouchSide.dsq crouchSide";

	sequence9  = "./EnemyShapes/blwsp_root.dsq look";
	sequence10 = "./EnemyShapes/blwsp_root.dsq headside";
	sequence11 = "./EnemyShapes/blwsp_root.dsq headUp";

	sequence12 = "./EnemyShapes/blwsp_jump.dsq jump";
	sequence13 = "./EnemyShapes/blwsp_jump.dsq standjump";
	sequence14 = "./EnemyShapes/blwsp_fly_loop.dsq fall";
	sequence15 = "./EnemyShapes/blwsp_root.dsq land";

	sequence16 = "./EnemyShapes/blwsp_root.dsq armAttack";
	sequence17 = "./EnemyShapes/blwsp_root.dsq armReadyLeft";
	sequence18 = "./EnemyShapes/blwsp_root.dsq armReadyRight";
	sequence19 = "./EnemyShapes/blwsp_root.dsq armReadyBoth";
	sequence20 = "./EnemyShapes/blwsp_root.dsq spearready";  
	sequence21 = "./EnemyShapes/blwsp_bite.dsq spearThrow";

	sequence22 = "./EnemyShapes/blwsp_bite.dsq talk";  

	sequence23 = "./EnemyShapes/blwsp_death.dsq death1"; 
	
	sequence24 = "./EnemyShapes/blwsp_root.dsq shiftUp";
	sequence25 = "./EnemyShapes/blwsp_root.dsq shiftDown";
	sequence26 = "./EnemyShapes/blwsp_root.dsq shiftAway";
	sequence27 = "./EnemyShapes/blwsp_root.dsq shiftTo";
	sequence28 = "./EnemyShapes/blwsp_root.dsq shiftLeft";
	sequence29 = "./EnemyShapes/blwsp_root.dsq shiftRight";
	sequence30 = "./EnemyShapes/blwsp_root.dsq rotCW";
	sequence31 = "./EnemyShapes/blwsp_root.dsq rotCCW";

	sequence32 = "./EnemyShapes/blwsp_root.dsq undo";
	sequence33 = "./EnemyShapes/blwsp_bite.dsq plant";

	sequence34 = "./EnemyShapes/blwsp_crouch.dsq sit";

	sequence35 = "./EnemyShapes/blwsp_bite.dsq wrench";

   sequence36 = "./EnemyShapes/blwsp_bite.dsq activate";
   sequence37 = "./EnemyShapes/blwsp_bite.dsq activate2";

   sequence38 = "./EnemyShapes/blwsp_root.dsq leftrecoil";
   
   sequence39 = "./EnemyShapes/blwsp_sting_ready.dsq stingReady";	//stingReady so it will work with scorpion sting weapons
   sequence40 = "./EnemyShapes/blwsp_fly.dsq Fly";
   sequence41 = "./EnemyShapes/blwsp_sting.dsq sting";
};

function InfernalBatHoleBot::onTrigger(%this, %obj, %trig, %press) {
	if(%trig == 2 && %press && getsimtime() >= %obj.lastjump+200 && %obj.getState() !$= "DEAD")
	{
		//flap code goes here. What follows is a recreation of the standard jump on a standard armor MINUS a check to be sure we're touching the ground - flying is possible with this.

		//pretty sure it doesn't play a sound either, because, again, I didn't need it for my purposes.
		%obj.forbidJump = true;
		%obj.addVelocity("0 0 10");	//10
		%obj.playThread(0, "Fly"); //animation
		%obj.lastjump = getsimtime();
	}
	else Parent::onTrigger(%this, %obj, %trig, %press);
}

datablock AudioProfile(InfernalBat_PainSound)
{
   fileName = "./EnemyShapes/mite_pain.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(InfernalBat_DeathSound)
{
   fileName = "./EnemyShapes/mite_death.wav";
   description = AudioClose3d;
   preload = true;
};


//<Weapon>
AddDamageType("InfernalBatMelee",   '%1 munched on itself.',    '%2 munched on %1.',0.2,1);
AddDamageType("InfernalBatStinger",   '%1 envenomated itself.',    '%2 envenomated %1.',0.2,1);
datablock ProjectileData(InfernalBatStingerProjectile)
{
   projectileShapeName = "./EnemyShapes/Stinger.dts";
   directDamage        = 8;
   directDamageType    = $DamageType::InfernalBatStinger;
   radiusDamageType    = $DamageType::InfernalBatStinger;

   brickExplosionRadius = 0;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 400;
   verticalImpulse	  = 400;
   explosion           = gunExplosion;
   particleEmitter     = arrowTrailEmitter;

   muzzleVelocity      = 20;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 10000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = true;
   gravityMod = 0.5;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "Bat Stinger";
};

function InfernalBatStingerProjectile::onCollision(%this, %obj, %col)
{
	parent::onCollision(%this, %obj, %col);
	
	if (%col.getClassName() $= "AIPlayer" || %col.getClassName() $= "Player") //Bitwise typemask check does not work for some reason.
	{
		%col.setEnergyLevel(%col.getEnergyLevel() - (%col.getDatablock().maxEnergy / 2));
	}
}

datablock ItemData(InfernalBatStingerItem)
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
	uiName = "Bat Stinger";
	iconName = "Add-Ons/Weapon_Gun/icon_gun";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1.000";

	 // Dynamic properties defined by the scripts
	image = InfernalBatStingerImage;
	canDrop = true;
};

datablock ShapeBaseImageData(InfernalBatStingerImage)
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
   item = InfernalBatStingerItem;
   ammo = " ";
   projectile = InfernalBatStingerProjectile;
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
	stateSound[3]					= bowFireSound;

	stateName[4]					= "Reload";
	stateTransitionOnTimeout[4]     = "Ready";
	stateTimeoutValue[4]            = 0.6;
	stateWaitForTimeout[4]			= true;

};

function InfernalBatStingerImage::onCharge(%this,%obj,%slot)
{
	%obj.playThread(2, "stingReady");
	if (!%obj.infernalChargingAttack)
	{
		%obj.infernalChargingAttack = true;
		%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() / 4);
		%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() / 4);
		%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() / 4);
	}
}

function InfernalBatStingerImage::onFire(%this,%obj,%slot)
{
	if((%obj.lastFireTime+%this.minShotTime) > getSimTime() || %obj.getState() $= "DEAD")
		return;
		
	%obj.lastFireTime = getSimTime();
	
	%obj.playThread(2, "sting");
	
	if (%obj.infernalChargingAttack)
	{
		%obj.infernalChargingAttack = false;
		%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() * 4);
		%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() * 4);
		%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() * 4);
	}

	%projectile = %this.projectile;
	%spread = 0.001;
	%shellcount = 1;

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
		%x = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%y = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%z = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		%velocity = MatrixMulVector(%mat, %velocity);

		if (%this.melee)
			%position = %obj.getEyePoint();
		else
			%position = %obj.getMuzzlePoint(%slot);

		if (isObject(%followTarget = %obj.hFollowing) && %followTarget.getType() & ($TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType))
			%velocity = vectorAdd(%followTarget.getVelocity(), %velocity);
		
		%p = new (%this.projectileType)()
		{
			dataBlock = %projectile;
			initialVelocity = %velocity;
			initialPosition = %position;
			sourceObject = %obj;
			sourceSlot = %slot;
			client = %obj.client;
		};
		MissionCleanup.add(%p);
	}
	return %p;
}