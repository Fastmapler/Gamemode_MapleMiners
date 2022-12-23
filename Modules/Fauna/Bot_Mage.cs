datablock PlayerData(InfernalMinibossHoleBot : PlayerStandardArmor)
{
	//shapeFile = "./EnemyShapes/mite3.dts";
	uiName = "";
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	maxItems   = 0;
	maxWeapons = 0;
	maxTools = 0;
	jumpForce = 12 * 90;
	runforce = 40 * 90;
	maxForwardSpeed = 1;
	maxBackwardSpeed = 1;
	maxSideSpeed = 1;
	attackpower = 10; //What does this do?
	rideable = false;
	canRide = false;
	maxdamage = 3500;//Health
	jumpSound = "";
	
	useCustomPainEffects = true;
	PainHighImage = "PainHighImage";
	PainMidImage  = "PainMidImage";
	PainLowImage  = "PainLowImage";
	PainSound           = InfernalMiniboss_PainSound;
	DeathSound          = InfernalMiniboss_DeathSound;
	
	//boundingBox				= vectorScale("1.1 1.1 1.1", 4); //"2.5 2.5 2.4" 
    //crouchBoundingBox		= vectorScale("1.1 1.1 1.1", 4); //"2.5 2.5 2.4";
	//proneBoundingBox		= vectorScale("1.1 1.1 1.1", 4); //"2.5 2.5 2.4";
	
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
	hName = "Abyssal Demon";//cannot contain spaces
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
	  hAttackDamage = 56;//Melee Damage
	  hDamageType = "InfernalMinibossMelee";
	hShoot = 1;
	  hWep = InfernalMinibossPowerImage;
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
	
	scoreModifier = 1.83;
	isBoss = true;
	xpDrop = 6400;
};

datablock AudioProfile(InfernalMiniboss_PainSound)
{
   fileName = "./EnemyShapes/miniboss_pain.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(InfernalMiniboss_DeathSound)
{
   fileName = "./EnemyShapes/miniboss_death.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(InfernalMiniboss_rangeFire)
{
   fileName = "./EnemyShapes/miniboss_rangeFire.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(InfernalMiniboss_rangeHit)
{
   fileName = "./EnemyShapes/miniboss_rangeHit.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(InfernalMiniboss_rangeHit)
{
   fileName = "./EnemyShapes/miniboss_rangeHit.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(InfernalMiniboss_magicFire)
{
   fileName = "./EnemyShapes/miniboss_magic.wav";
   description = AudioClose3d;
   preload = true;
};


AddDamageType("InfernalMinibossMelee",   '%1 slapped itself.',    '%2 slapped %1.',0.2,1);

datablock ExplosionData(InfernalMinibossRangeExplosion : spearExplosion)
{
   explosionShape = "./EnemyShapes/rangeExplosion.dts";
   soundProfile = InfernalMiniboss_rangeHit;
   lifeTimeMS = 333;
   faceViewer = false;
   
   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.5 1.5 1.5";
   camShakeDuration = 1.0;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 4;
   lightEndRadius = 3;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 1000;

   //radius damage
   radiusDamage        = 15;
   damageRadius        = 2.0;
};

datablock ParticleData(InfernalMinibossRangedTrailParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 625;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/dot";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "1 1 1 0.4";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.5;
	sizes[1]      = 0.25;

	useInvAlpha = false;
};
datablock ParticleEmitterData(InfernalMinibossRangedTrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 1;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "InfernalMinibossRangedTrailParticle";
};

AddDamageType("InfernalMinibossRanged",   '%1 crushed itself.',    '%2 crushed %1.',0.2,1);
datablock ProjectileData(InfernalMinibossRangedProjectile)
{
   projectileShapeName = "./EnemyShapes/Stone.dts";
   directDamage        = 28;
   directDamageType    = $DamageType::InfernalMinibossRanged;
   radiusDamageType    = $DamageType::InfernalMinibossRanged;

   brickExplosionRadius = 0;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse		= 0;
   verticalImpulse		= 0;
   explosion           = InfernalMinibossRangeExplosion;
   particleEmitter     = "InfernalMinibossRangedTrailEmitter"; //bulletTrailEmitter;

   muzzleVelocity      = 10;
   velInheritFactor    = 0;

   armingDelay         = 00;
   lifetime            = 10000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "Miniboss Stone";
};

function InfernalMinibossRangedProjectile::onCollision(%this, %obj, %col)
{
	initContainerRadiusSearch(%obj.getPosition(), 10, $TypeMasks::PlayerObjectType);
	for (%i = 0; isObject(%target = containerSearchNext()); %i++)
	{
		if (%target.getClassName() $= "Player")
		{
			if (mAbs(getWord(%target.getPosition(),2) - getWord(%obj.getPosition(),2)) < 0.5)
				%target.damage(%obj, %target.getPosition(), 28, $DamageType::InfernalMinibossRanged);
		}
	}
	parent::onCollision(%this, %obj, %col);
}

datablock ParticleData(InfernalMinibossMageTrailParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 625;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/dot";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "1 0 0 1.0";
	colors[1]     = "0.1 0.1 0.1 1.0";
	sizes[0]      = 1.0;
	sizes[1]      = 0.5;

	useInvAlpha = true;
};
datablock ParticleEmitterData(InfernalMinibossMageTrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 6.5;
   velocityVariance = 0;
   ejectionOffset   = 0.5;
   thetaMin         = 89;
   thetaMax         = 90;
   phiReferenceVel  = 60000;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "InfernalMinibossMageTrailParticle";
};

AddDamageType("InfernalMinibossMage",   '%1 smited itself.',    '%2 smited %1.',0.2,1);
datablock ProjectileData(InfernalMinibossMageProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";
   directDamage        = 28;
   directDamageType    = $DamageType::InfernalMinibossMage;
   radiusDamageType    = $DamageType::InfernalMinibossMage;

   brickExplosionRadius = 0;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse		= 0;
   verticalImpulse		= 0;
   explosion           = InfernalRangerStoneExplosion;
   particleEmitter     = "InfernalMinibossMageTrailEmitter"; //bulletTrailEmitter;

   muzzleVelocity      = 35;
   velInheritFactor    = 0;

   armingDelay         = 00;
   lifetime            = 10000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "Miniboss Fireball";
};

package HomingFireball
{
	function Projectile::onAdd(%obj,%a,%b)
	{
		Parent::onAdd(%obj,%a,%b);
		if(%obj.dataBlock.getID() == InfernalMinibossMageProjectile.getID())
		{
			if(!%obj.doneHomingDelay)
				%obj.schedule(300,spawnHomingFireball);
			else
				%obj.schedule(75,spawnHomingFireball);
		}
	}
};activatePackage(HomingFireball);

function Projectile::spawnHomingFireball(%this)
{
	if(!isObject(%this.client) && %this.sourceObject.getClassName() !$= "AIPlayer")
		return;
	
	if(!isObject(%this) || vectorLen(%this.getVelocity()) == 0)
		return;
		
	%client = %this.client;
	%muzzle = vectorLen(%this.getVelocity());
	
	if(!isObject(%this.homingTarget) || %this.homingTarget.getState() $= "Dead" || %this.homingTarget.getMountedImage(0) == adminWandImage.getID() || vectorDist(%this.getPosition(),%this.homingTarget.getHackPosition()) > 30)
	{
		if (%this.sourceObject.getClassName() $= "AIPlayer")
		{
			%pos = %this.getPosition();
			%radius = 100;
			%searchMasks = $TypeMasks::PlayerObjectType;
			InitContainerRadiusSearch(%pos, %radius, %searchMasks);
			%minDist = 1000;
			while ((%searchObj = containerSearchNext()) != 0 )
			{
				if(isObject(%searchObj.client) && miniGameCanDamage(%searchObj.client,%this.sourceObject))
				{
					if(%searchObj.getState() $= "Dead")
						continue;
						
					if(%this.sourceObject == %searchObj)
						continue;
					
					if(%searchObj.getClassName() $= "AIPlayer")
						continue;
					
					if(%searchObj.getMountedImage(0) == adminWandImage.getID())
						continue;
					
					if(%searchObj.isCloaked)
						continue;
						
					if(%searchObj != %this.target)
						continue;
						
					if(%searchObj.isCrouched()) //Don't continue since we want homing to end.
						return;
					
					%d = vectorDist(%pos,%searchObj.getPosition());
					if(%d < %minDist)
					{
						%minDist = %d;
						%found = %searchObj;
					}
				}
			}
		}
		else
		{
			%pos = %this.getPosition();
			%radius = 100;
			%searchMasks = $TypeMasks::PlayerObjectType;
			InitContainerRadiusSearch(%pos, %radius, %searchMasks);
			%minDist = 1000;
			while ((%searchObj = containerSearchNext()) != 0 )
			{
				if((miniGameCanDamage(%client,%searchObj)) == 1)
				{
					if(%searchObj.getState() $= "Dead")
						continue;
					
					//if(%client == %searchObj.client)
					if(%this.sourceObject == %searchObj)
						continue;
					
					if(isTeamFriendly(%this,%searchObj) != 0)
						continue;
					
					if(%searchObj.getMountedImage(0) == adminWandImage.getID())
						continue;
					
					if(%searchObj.isCloaked)
						continue;
						
					if(%searchObj != %this.target)
						continue;
						
					if(%searchObj.isCrouched()) //Don't continue since we want homing to end.
						return;
					
					%d = vectorDist(%pos,%searchObj.getPosition());
					if(%d < %minDist)
					{
						%minDist = %d;
						%found = %searchObj;
					}
				}
			}
		}
		
		if(isObject(%found))
			%this.homingTarget = %found;
		else
		{
			%this.schedule(300,spawnHomingFireball);
			return;
		}
	}
	%found = %this.homingTarget;
	
	%pos = %this.getPosition();
	%start = %pos;
	%end = %found.getHackPosition();
	%enemypos = %end;
	%vec = vectorNormalize(vectorSub(%end,%start));
	for(%i=0;%i<5;%i++)
	{
		%t = vectorDist(%start,%end) / vectorLen(vectorScale(getWord(%vec,0) SPC getWord(%vec,1),%muzzle));
		%velaccl = vectorScale(%accl,%t);
		
		%x = getword(%velaccl,0);
		%y = getword(%velaccl,1);
		%z = getWord(%velaccl,2);
		
		%x = (%x < 0 ? 0 : %x);
		%y = (%y < 0 ? 0 : %y);
		%z = (%z < 0 ? 0 : %z);
		
		%vel = vectorAdd(vectorScale(%found.getVelocity(),%t),%x SPC %y SPC %z);
		%end = vectorAdd(%enemypos,%vel);
		%vec = vectorNormalize(vectorSub(%end,%start));
	}
	
	//%addVec = vectorAdd(%this.getVelocity(),vectorScale(%vec,180/vectorDist(%pos,%end)*(%muzzle/40)));
	%addVec = vectorAdd(%this.getVelocity(),vectorScale(%vec,180/vectorDist(%pos,%end)*(%muzzle/10)));
	%vec = vectorNormalize(%addVec);
	
	if(%found.isCrouched()) //Don't continue since we want homing to end.
		return;
						
	%p = new Projectile()
	{
		dataBlock = %this.dataBlock;
		initialPosition = %pos;
		initialVelocity = vectorScale(%vec,%muzzle);
		sourceObject = %this.sourceObject;
		client = %this.client;
		sourceSlot = 0;
		originPoint = %this.originPoint;
		doneHomingDelay = 1;
		homingTarget = %this.homingTarget;
		reflectTime = %this.reflectTime;
		target = %this.target;
	};
	
	if(isObject(%p))
	{
		MissionCleanup.add(%p);
		%p.setScale(%this.getScale());
		%this.delete();
	}
}

function isTeamFriendly(%obj1,%obj2)
{
	// 0 = enemy
	// 1 = allied
	//-1 = neutral
	%mini1 = getMinigameFromObject(%obj1);
	%mini2 = getMinigameFromObject(%obj2);
	if(%mini1 != %mini2)
		return -1;
	
	if(!isObject(%mini1))
		return -1;
	
	if(%obj1 == %obj2)
		return 1;
	
	//With Team Deathmatch, players on different TDM teams are enemies, as well as those in team -1 (neutral)
	%usingTDM = (isObject(GameModeStorerSO) && GameModeStorerSO.modeUsesTeams[%mini1.gamemode]);
	
	//With either zombie mod, players that are on a zombies team and players that aren't are enemies (handily both are "player.isZombie")
	
	//Iban's zombies will never run with Team Deathmatch
	%usingIbanZombies = ($ZAPT::Version > 0 && miniGameUsingTDMGameMode(%mini1) && %mini1.ZAPT_enabled);
	
	//Rotondo's zombies can - if enabled, players on a neutral TDM team are allied.
	%usingRotZombies = ($pref::Server::ZombiesEnabled && %mini1.EnableZombies);
	
	switch$(%obj1.getClassName())
	{
		case "Player":
			%cl1 = %obj1.client;
			%zombie1 = (%usingIbanZombies && %obj1.isZombie);
		case "AIPlayer":
			%zombie1 = %obj1.isZombie;
			if(isObject(%obj1.client))
				%cl1 = %obj1.client;
			else if(isObject(%obj1.spawnBrick) && %usingTDM)
				%team1 = %obj1.spawnBrick.tdmAlliedTeam-1;
		case "Projectile":
			%zombie1 = (isObject(%obj1.sourceObject) && (%obj1.sourceObject.getType() & $TypeMasks::PlayerObjectType) && %obj1.sourceObject.isZombie);
			%cl1 = %obj1.client;
		case "GameConnection":
			%zombie1 = (%obj1.player.isZombie);
			%cl1 = %obj1;
		case "AIConnection":
			%zombie1 = (%obj1.player.isZombie);
			%cl1 = %obj1;
		default:
			return -1;
	}
	
	if(%usingTDM && %tdmTeam1 $= "" && isObject(%cl1))
		%team1 = %cl1.tdmTeam;
	
	switch$(%obj2.getClassName())
	{
		case "Player":
			%cl2 = %obj2.client;
			%zombie2 = (%usingIbanZombies && %obj2.isZombie);
		case "AIPlayer":
			%zombie2 = %obj2.isZombie;
			if(isObject(%obj2.client))
				%cl2 = %obj2.client;
			else if(isObject(%obj2.spawnBrick) && %usingTDM)
				%team2 = %obj2.spawnBrick.tdmAlliedTeam-1;
		case "Projectile":
			%zombie2 = (isObject(%obj2.sourceObject) && (%obj2.sourceObject.getType() & $TypeMasks::PlayerObjectType) && %obj2.sourceObject.isZombie);
			%cl2 = %obj2.client;
		case "GameConnection":
			%zombie2 = (%obj2.player.isZombie);
			%cl2 = %obj2;
		case "AIConnection":
			%zombie2 = (%obj2.player.isZombie);
			%cl2 = %obj2;
		default:
			return -1;
	}
	
	if(%usingTDM && %tdmTeam2 $= "" && isObject(%cl2))
		%team2 = %cl2.tdmTeam;
	
	//echo(%team1 SPC %team2);
	
	if(%usingTDM && %team1 == %team2)
	{
		if(%team1 == -1)
			return %usingRotZombies;
		
		return 1;
	}
	
	if(%usingIbanZombies || %usingRotZombies)
		return (%zombie1 == %zombie2);
	
	return 0;
}

datablock ItemData(InfernalMinibossPowerItem)
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
	uiName = "Miniboss Power";
	iconName = "Add-Ons/Weapon_Gun/icon_gun";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1.000";

	 // Dynamic properties defined by the scripts
	image = InfernalMinibossPowerImage;
	canDrop = true;
};

datablock ShapeBaseImageData(InfernalMinibossPowerImage)
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
   item = InfernalMinibossPowerItem;
   ammo = " ";
   projectileA = InfernalMinibossRangedProjectile;
   projectileB = InfernalMinibossMageProjectile;
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
	stateTimeoutValue[2]            = 2.0;
	stateWaitForTimeout[2]			= true;
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Reload";
	stateTimeoutValue[3]            = 1.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]			= true;

	stateName[4]					= "Reload";
	stateTransitionOnTimeout[4]     = "Ready";
	stateTimeoutValue[4]            = 0.0;
	stateWaitForTimeout[4]			= true;

};

function InfernalMinibossPowerImage::onCharge(%this,%obj,%slot)
{
	if (getRandom() <= 0.5)
	{
		%obj.playThread(2, "activate2");
		%obj.infernalAttackMode = 2;
		serverPlay2d(InfernalMiniboss_magicFire,%obj.getposition());
	}
	else
	{
		%obj.playThread(2, "crouch");
		%obj.infernalAttackMode = 1;
	}
	
	if (!%obj.infernalChargingAttack)
	{
		%obj.infernalChargingAttack = true;
		%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() / 4);
		%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() / 4);
		%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() / 4);
	}
}

function InfernalMinibossPowerImage::onFire(%this,%obj,%slot)
{
	if((%obj.lastFireTime+%this.minShotTime) > getSimTime() || %obj.getState() $= "DEAD" || !isObject(%target = %obj.hFollowing))
		return;
		
	%obj.lastFireTime = getSimTime();
	
	%obj.playThread(2, "root");
	
	if (%obj.infernalChargingAttack)
	{
		%obj.infernalChargingAttack = false;
		%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() * 4);
		%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() * 4);
		%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() * 4);
	}

	if (%obj.infernalAttackMode == 1)
	{
		%projectile = %this.projectileA;
		%spread = 0.0;
		%shellcount = 1;
		%position = vectorAdd(%target.getPosition(), getRandom(0, 0) SPC getRandom(0, 0) SPC "10");
		%vector = "0 0 -1";
		serverPlay2d(InfernalMiniboss_rangeFire,%obj.getposition());
	}
	else
	{
		%projectile = %this.projectileB;
		%spread = 0.0;
		%shellcount = 1;
		%position = vectorAdd(%obj.getPosition(),"0 0 0");
		%vector = vectorNormalize(vectorSub(%target.getHackPosition(),%obj.getPosition()));
	}

	for(%shell = 0; %shell < %shellcount; %shell++)
	{
		%objectVelocity = %obj.getVelocity();
		%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
		%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
		%velocity = VectorAdd(%vector1,%vector2);
		%x = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%y = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%z = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		%velocity = MatrixMulVector(%mat, %velocity);
		
		%p = new (%this.projectileType)()
		{
			dataBlock = %projectile;
			initialVelocity = %velocity;
			initialPosition = %position;
			sourceObject = %obj;
			sourceSlot = %slot;
			client = %obj.client;
			target = %target;
		};
		MissionCleanup.add(%p);
	}
	
	return %p;
}