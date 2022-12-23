datablock PlayerData(InfernalMeleeHoleBot : PlayerStandardArmor)
{
	shapeFile = "./EnemyShapes/hydralisk.dts";
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
	maxdamage = 750;//Health
	jumpSound = "";
	
	useCustomPainEffects = true;
	PainHighImage = "PainHighImage";
	PainMidImage  = "PainMidImage";
	PainLowImage  = "PainLowImage";
	PainSound			= InfernalMelee_PainSound;
	DeathSound			= InfernalMelee_DeathSound;
	
	boundingBox			= vectorScale("2.2 2.2 4.2", 4);
	crouchBoundingBox	= vectorScale("2.2 2.2 4", 4);
	proneBoundingBox    = vectorScale("2.2 2.2 4.2", 4);
	
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
	hName = "Hydralisk";//cannot contain spaces
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
	  hAttackDamage = 25;//Melee Damage
	  hDamageType = "InfernalMeleeMelee";
	hShoot = 0;
	  hWep = SturdiumBeastWepImage;
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
	hSpazJump = 0;//Makes bot jump when the user their following is higher than them

	hAFKOmeter = 1;//Determines how often the bot will wander or do other idle actions, higher it is the less often he does things
	hIdle = 0;// Enables use of idle actions, actions which are done when the bot is not doing anything else
	  hIdleAnimation = 0;//Plays random animations/emotes, sit, click, love/hate/etc
	  hIdleLookAtOthers = 1;//Randomly looks at other players/bots when not doing anything else
	    hIdleSpam = 0;//Makes them spam click and spam hammer/spraycan
	  hSpasticLook = 1;//Makes them look around their environment a bit more.
	hEmote = 1;
	
	scoreModifier = 0.9375;
	xpDrop = 800;
};

datablock TSShapeConstructor(InfernalMeleeDts)
{
	baseShape  = "./EnemyShapes/hydralisk.dts";
	sequence0  = "./EnemyShapes/hyd_root.dsq root";

	sequence1  = "./EnemyShapes/hyd_move.dsq run";
	sequence2  = "./EnemyShapes/hyd_move.dsq walk";
	sequence3  = "./EnemyShapes/hyd_back.dsq back";
	sequence4  = "./EnemyShapes/hyd_side.dsq side";

	sequence5  = "./EnemyShapes/hyd_crouch.dsq crouch";
	sequence6  = "./EnemyShapes/hyd_crouchMove.dsq crouchRun";
	sequence7  = "./EnemyShapes/hyd_crouch.dsq crouchBack";
	sequence8  = "./EnemyShapes/hyd_crouchSide.dsq crouchSide";

	sequence9  = "./EnemyShapes/hyd_root.dsq look";
	sequence10 = "./EnemyShapes/hyd_root.dsq headside";
	sequence11 = "./EnemyShapes/hyd_root.dsq headUp";

	sequence12 = "./EnemyShapes/hyd_fall.dsq jump";
	sequence13 = "./EnemyShapes/hyd_root.dsq standjump";
	sequence14 = "./EnemyShapes/hyd_fall.dsq fall";
	sequence15 = "./EnemyShapes/hyd_root.dsq land";

	sequence16 = "./EnemyShapes/hyd_root.dsq armAttack";
	sequence17 = "./EnemyShapes/hyd_root.dsq armReadyLeft";
	sequence18 = "./EnemyShapes/hyd_root.dsq armReadyRight";
	sequence19 = "./EnemyShapes/hyd_root.dsq armReadyBoth";
	sequence20 = "./EnemyShapes/hyd_root.dsq spearready";  
	sequence21 = "./EnemyShapes/hyd_root.dsq spearThrow";

	sequence22 = "./EnemyShapes/hyd_root.dsq talk";  

	sequence23 = "./EnemyShapes/hyd_death.dsq death1"; 
	
	sequence24 = "./EnemyShapes/hyd_root.dsq shiftUp";
	sequence25 = "./EnemyShapes/hyd_root.dsq shiftDown";
	sequence26 = "./EnemyShapes/hyd_acid_attack.dsq shiftAway";
	sequence27 = "./EnemyShapes/hyd_root.dsq shiftTo";
	sequence28 = "./EnemyShapes/hyd_root.dsq shiftLeft";
	sequence29 = "./EnemyShapes/hyd_root.dsq shiftRight";
	sequence30 = "./EnemyShapes/hyd_root.dsq rotCW";
	sequence31 = "./EnemyShapes/hyd_root.dsq rotCCW";

	sequence32 = "./EnemyShapes/hyd_root.dsq undo";
	sequence33 = "./EnemyShapes/hyd_root.dsq plant";

	sequence34 = "./EnemyShapes/hyd_sit.dsq sit";

	sequence35 = "./EnemyShapes/hyd_root.dsq wrench";

   sequence36 = "./EnemyShapes/hyd_attack.dsq activate";
   sequence37 = "./EnemyShapes/hyd_attack.dsq activate2";

   sequence38 = "./EnemyShapes/hyd_root.dsq leftrecoil";
};

datablock AudioProfile(InfernalMelee_PainSound)
{
	fileName = "./EnemyShapes/zhyWht01.wav";
	description = AudioClose3d;
	preload = true;
};
datablock AudioProfile(InfernalMelee_DeathSound)
{
	fileName = "./EnemyShapes/zhyDth00.wav";
	description = AudioClose3d;
	preload = true;
};

//<Invisibility Ability>
function InfernalMeleeHoleBot::onAdd(%this,%obj)
{
	parent::onAdd(%this,%obj);
	%obj.schedule(1200,"InfernalInvisCheck");
}

function AIPlayer::InfernalInvisCheck(%obj)
{
	if (%obj.getState() $= "DEAD")
	{
		%obj.unHideNode("ALL");
		return;
	}
	
	initContainerRadiusSearch(%obj.getPosition(), 4, $TypeMasks::PlayerObjectType);
	
	for (%i = 0; isObject(%target = containerSearchNext()); %i++)
	{
		if (%target.getClassName() $= "Player")
		{
			if (%obj.isInvisible)
			{
				%obj.burn(0);
				%obj.unHideNode("ALL");
				schedule(600,%obj,"eval",%obj @ ".hAttackDamage = " @ %obj @ ".getDatablock().hAttackDamage;");
				
				%obj.isInvisible = false;
				%obj.invulnerable = false;
			
				%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() / 4);
				%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() / 4);
				%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() / 4);
				
				GameConnection::ApplyBodyParts(%obj);
				GameConnection::ApplyBodyColors(%obj);
			}
			%obj.InvernalInvisCount = 0;
			break;
		}
	}
	
	%obj.InvernalInvisCount++;
	
	if (%obj.InvernalInvisCount > 2 && !%obj.isInvisible)
	{
		%obj.burn(0);
		%obj.hideNode("ALL");
		%obj.hAttackDamage = 0;
		
		%obj.isInvisible = true;
		%obj.invulnerable = true;
			
		%obj.setMaxForwardSpeed(%obj.getMaxForwardSpeed() * 4);
		%obj.setMaxSideSpeed(%obj.getMaxSideSpeed() * 4);
		%obj.setMaxBackwardSpeed(%obj.getMaxBackwardSpeed() * 4);
	}
	%obj.schedule(1200,"InfernalInvisCheck");
}
AddDamageType("InfernalMeleeMelee",   '%1 impaled itself.',    '%2 impaled %1.',0.2,1);