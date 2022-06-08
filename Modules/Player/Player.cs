exec("./Economy.cs");
exec("./Battery.cs");
exec("./PlayerStats.cs");
exec("./Saving.cs");
exec("./Support_BrickShiftMenu.cs");

datablock PlayerData(PlayerMapleMinersArmor : PlayerStandardArmor)
{
	minLookAngle = -1.55;
	maxLookAngle = 1.55;

    maxDamage = 100;
	maxEnergy = 200;
	repairRate = 0.33;
	rechargeRate = 0.8;

	runForce = 48 * 90;
	maxForwardSpeed = 7;
	maxBackwardSpeed = 4;
	maxSideSpeed = 6;

	airControl = 0.3; //0.1

	maxForwardCrouchSpeed = 3;
	maxBackwardCrouchSpeed = 2;
	maxSideCrouchSpeed = 2;

	maxUnderwaterForwardSpeed = 8.4;
	maxUnderwaterBackwardSpeed = 7.8;
	maxUnderwaterSideSpeed = 7.8;

	jumpForce = 12 * 90;
    minJetEnergy = 2;
	jetEnergyDrain = 0;
	canJet = true;

	boundingBox = VectorScale ("1.25 1.25 2.65", 4);
	crouchBoundingBox = VectorScale ("1.25 1.25 1.0", 4); //1.25 1.25 1.00

	maxWeapons = 5;
	maxTools = 5;

	uiName = "Maple Miners Player";

	showEnergyBar = true;
};

function GameConnection::GetPickaxeDamage(%client)
{
	return mClamp(%client.MM_PickaxeLevel, 1, 999999);
}

function GameConnection::MM_CenterPrint(%client, %text, %length, %b)
{
	%client.centerPrint("<font:Arial:24>\c6" @ %text, %length, %b);
}

function GameConnection::MM_BottomPrint(%client, %text, %length, %b)
{
	%client.BottomPrint("<font:Arial:24>\c6" @ %text, %length, %b);
}

function Player::FaceDirection(%obj, %forwardVec)
{
	if (!isObject(%client = %obj.client))
		return;

	%x = 1; %y = 0; %z = 0;

	if (%forwardVec $= "")
		%forwardVec = %obj.getEyeVector();
		
	%forwardX = getWord (%forwardVec, 0);
	%forwardY = getWord (%forwardVec, 1);
	%forwardZ = getWord (%forwardVec, 2);
	if (%forwardZ > 0.85)
	{
		%z = 1;
		%x = 0;
	}
	else if (%forwardZ < -0.85)
	{
		%z = -1;
		%x = 0;
	}

	if (%forwardX > 0)
	{
		if (%forwardX > mAbs (%forwardY))
		{
			
		}
		else if (%forwardY > 0)
		{
			%newY = %x;
			%newX = -1 * %y;
			%x = %newX;
			%y = %newY;
		}
		else 
		{
			%newY = -1 * %x;
			%newX = 1 * %y;
			%x = %newX;
			%y = %newY;
		}
	}
	else if (mAbs (%forwardX) > mAbs (%forwardY))
	{
		%x *= -1;
		%y *= -1;
	}
	else if (%forwardY > 0)
	{
		%newY = %x;
		%newX = -1 * %y;
		%x = %newX;
		%y = %newY;
	}
	else 
	{
		%newY = -1 * %x;
		%newX = 1 * %y;
		%x = %newX;
		%y = %newY;
	}

	return %x SPC %y SPC %z;
}