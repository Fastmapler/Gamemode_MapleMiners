datablock PlayerData(InfernalNibblerHoleBot : PlayerStandardArmor)
{
	shapeFile = "./EnemyShapes/mite3.dts";
	uiName = "";
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	maxItems   = 0;
	maxWeapons = 0;
	maxTools = 0;
	jumpForce = 12 * 90;
	runforce = 40 * 90;
	maxForwardSpeed = 9;
	maxBackwardSpeed = 9;
	maxSideSpeed = 9;
	attackpower = 10; //What does this do?
	rideable = false;
	canRide = false;
	maxdamage = 100;//Health
	jumpSound = "";
	
	useCustomPainEffects = true;
	PainHighImage = "PainHighImage";
	PainMidImage  = "PainMidImage";
	PainLowImage  = "PainLowImage";
	PainSound           = InfernalNibbler_PainSound;
	DeathSound          = InfernalNibbler_DeathSound;
	
	boundingBox				= vectorScale("1.1 1.1 1.1", 4); //"2.5 2.5 2.4" 
    crouchBoundingBox		= vectorScale("1.1 1.1 1.1", 4); //"2.5 2.5 2.4";
	proneBoundingBox		= vectorScale("1.1 1.1 1.1", 4); //"2.5 2.5 2.4";
	
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
	hName = "Nibbler";//cannot contain spaces
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
	  hAttackDamage = 4;//15;//Melee Damage
	  hDamageType = "InfernalNibblerMelee";
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
	hSpazJump = 1;//Makes bot jump when the user their following is higher than them

	hAFKOmeter = 1;//Determines how often the bot will wander or do other idle actions, higher it is the less often he does things
	hIdle = 0;// Enables use of idle actions, actions which are done when the bot is not doing anything else
	  hIdleAnimation = 0;//Plays random animations/emotes, sit, click, love/hate/etc
	  hIdleLookAtOthers = 1;//Randomly looks at other players/bots when not doing anything else
	    hIdleSpam = 0;//Makes them spam click and spam hammer/spraycan
	  hSpasticLook = 1;//Makes them look around their environment a bit more.
	hEmote = 1;
	
	scoreModifier = 1;
	xpDrop = 100;
};

datablock TSShapeConstructor(InfernalNibblerDts)
{
	baseShape  = "./EnemyShapes/mite3.dts";
	sequence0  = "./EnemyShapes/mite_root.dsq root";

	sequence1  = "./EnemyShapes/mite_run.dsq run";
	sequence2  = "./EnemyShapes/mite_run.dsq walk";
	sequence3  = "./EnemyShapes/mite_back.dsq back";
	sequence4  = "./EnemyShapes/mite_side.dsq side";

	sequence5  = "./EnemyShapes/mite_root.dsq crouch";
	sequence6  = "./EnemyShapes/mite_run.dsq crouchRun";
	sequence7  = "./EnemyShapes/mite_back.dsq crouchBack";
	sequence8  = "./EnemyShapes/mite_side.dsq crouchSide";

	sequence9  = "./EnemyShapes/mite_root.dsq look";
	sequence10 = "./EnemyShapes/mite_root.dsq headside";
	sequence11 = "./EnemyShapes/mite_root.dsq headUp";

	sequence12 = "./EnemyShapes/mite_jump.dsq jump";
	sequence13 = "./EnemyShapes/mite_jump.dsq standjump";
	sequence14 = "./EnemyShapes/mite_fall.dsq fall";
	sequence15 = "./EnemyShapes/mite_root.dsq land";

	sequence16 = "./EnemyShapes/mite_attack.dsq armAttack";
	sequence17 = "./EnemyShapes/mite_root.dsq armReadyLeft";
	sequence18 = "./EnemyShapes/mite_root.dsq armReadyRight";
	sequence19 = "./EnemyShapes/mite_root.dsq armReadyBoth";
	sequence20 = "./EnemyShapes/mite_root.dsq spearready";  
	sequence21 = "./EnemyShapes/mite_attack.dsq spearThrow";

	sequence22 = "./EnemyShapes/mite_attack.dsq talk";  

	sequence23 = "./EnemyShapes/mite_death.dsq death1"; 
	
	sequence24 = "./EnemyShapes/mite_root.dsq shiftUp";
	sequence25 = "./EnemyShapes/mite_root.dsq shiftDown";
	sequence26 = "./EnemyShapes/mite_root.dsq shiftAway";
	sequence27 = "./EnemyShapes/mite_root.dsq shiftTo";
	sequence28 = "./EnemyShapes/mite_root.dsq shiftLeft";
	sequence29 = "./EnemyShapes/mite_root.dsq shiftRight";
	sequence30 = "./EnemyShapes/mite_root.dsq rotCW";
	sequence31 = "./EnemyShapes/mite_root.dsq rotCCW";

	sequence32 = "./EnemyShapes/mite_root.dsq undo";
	sequence33 = "./EnemyShapes/mite_attack.dsq plant";

	sequence34 = "./EnemyShapes/mite_death.dsq sit";

	sequence35 = "./EnemyShapes/mite_root.dsq wrench";

   sequence36 = "./EnemyShapes/mite_attack.dsq activate";
   sequence37 = "./EnemyShapes/mite_attack.dsq activate2";

   sequence38 = "./EnemyShapes/mite_root.dsq leftrecoil";
};    

datablock AudioProfile(InfernalNibbler_PainSound)
{
   fileName = "./EnemyShapes/bug_pain.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(InfernalNibbler_DeathSound)
{
   fileName = "./EnemyShapes/bug_death.wav";
   description = AudioClose3d;
   preload = true;
};

AddDamageType("InfernalNibblerMelee",   '%1 swarmed itself.',    '%2 swarmed %1.',0.2,1);