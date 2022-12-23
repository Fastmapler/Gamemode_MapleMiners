//<Datablocks>
exec("./Bot_Assassin.cs");
exec("./Bot_Blob.cs");
exec("./Bot_Dragon.cs");
exec("./Bot_Golem.cs");
exec("./Bot_Swarmer.cs");
exec("./Bot_Mage.cs");
exec("./Bot_Wasp.cs");

//<Variable>
if (!isObject(EnemyGroup)) new SimSet(EnemyGroup);

//<Spawn Code>							//spawnNewEnemy(%pl.getPosition(), InfernalRangerHoleBot);
function spawnNewEnemy(%trans,%hBotType) //spawnNewZombie("64 64 128 0 0 0 0",UnfleshedHoleBot); //Solar apoc code, in MY Maple Miners!?!
{
	if(!isObject(FakeBotSpawnBrick))
	{
		new FxDtsBrick(FakeBotSpawnBrick)
		{
			datablock = brick1x1Data;
			isPlanted = false;
			itemPosition = 1;
			position = "0 0 -2000";
		};
	}
	%spawnBrick = FakeBotSpawnBrick;
	
	if(!isObject(%hBotType))
	{
		warn(%hBotType @ " is not a datablock!");
		return;
	}
	
	%player = new AIPlayer()
	{
		dataBlock = %hBotType;
		path = "";
		spawnBrick = %spawnBrick;
		mini = $defaultMinigame;
		
		position = getWords(%trans, 0, 2);
		hGridPosition = getWords(%trans, 0, 2);
		rotation = getWords(%trans, 3, 6);
		
		//Apply attributes to Bot
		client = 0;
		isHoleBot = 1;
			
		//Apply attributes to Bot
		Name = %hBotType.hName;
		hType = "enemy";
		hDamageType = (strLen(%hBotType.hDamageType) > 0 ? $DamageType["::" @ %hBotType.hDamageType] : $DamageType::HoleMelee);
		hSearchRadius = %hBotType.hSearchRadius;
		hSearch = %hBotType.hSearch;
		hSight = %hBotType.hSight;
		hWander = %hBotType.hWander;
		hGridWander = 256;
		hReturnToSpawn = false;
		hSpawnDist = 10000;
		hHerding = %hBotType.hHerding;
		hMelee = %hBotType.hMelee;
		hAttackDamage = %hBotType.hAttackDamage;
		hSpazJump = %hBotType.hSpazJump;
		hSearchFOV = %hBotType.hSearchFOV;
		hFOVRadius = %hBotType.hFOVRadius;
		hTooCloseRange = %hBotType.hTooCloseRange;
		hAvoidCloseRange = %hBotType.hAvoidCloseRange;
		hShoot = %hBotType.hShoot;
		hMaxShootRange = %hBotType.hMaxShootRange;
		hStrafe = %hBotType.hStrafe;
		hAlertOtherBots = %hBotType.hAlertOtherBots;
		hIdleAnimation = %hBotType.hIdleAnimation;
		hSpasticLook = %hBotType.hSpasticLook;
		hAvoidObstacles = %hBotType.hAvoidObstacles;
		hIdleLookAtOthers = %hBotType.hIdleLookAtOthers;
		hIdleSpam = false;
		hAFKOmeter = %hBotType.hAFKOmeter + getRandom( 0, 2 );
		hHearing = true;
		hIdle = %hBotType.hIdle;
		hSmoothWander = %hBotType.hSmoothWander;
		hEmote = %hBotType.hEmote;
		hSuperStacker = %hBotType.hSuperStacker;
		hNeutralAttackChance = %hBotType.hNeutralAttackChance;
		hFOVRange = %hBotType.hFOVRange;
		hMoveSlowdown = %hBotType.hMoveSlowdown;
		hMaxMoveSpeed = 1.0;
		hActivateDirection = %hBotType.hActivateDirection;
	};
		
	missionCleanup.add(%player);
	EnemyGroup.add(%player);
	//applyBotSkin(%player);
	
	if (%hBotType.hShoot)
		%player.mountImage(%hBotType.hWep,0);
	
	%player.hGridPosition = getWords(%trans, 0, 2);
	%player.scheduleNoQuota(10,spawnProjectile,"audio2d","spawnProjectile","0 0 0", 1);
	
	return %player;
}

package MM_Fauna
{
	function Armor::damage(%this, %obj, %sourceObj, %position, %damage, %damageType)
	{
		Parent::damage(%this, %obj, %sourceObj, %position, %damage, %damageType);

		if (%obj.getClassName() $= "AIPlayer")
		{
			if (%obj.getDatablock().isBoss)
			{
				%obj.setShapeNameDistance(128);
				%obj.setShapeNameColor("1 0 0");
			}
			else
			{
				%obj.setShapeNameDistance(16);
				%obj.setShapeNameColor("1 0.5 0");
			}
			
			%obj.setShapeName(mCeil((1 - %obj.getDamagePercent()) * 100) @ "\% HP", 8564862);
		}
	}
};
activatePackage("MM_Fauna");