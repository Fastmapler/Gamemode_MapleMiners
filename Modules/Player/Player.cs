exec("./Economy.cs");
exec("./Battery.cs");
exec("./PlayerStats.cs");
exec("./Saving.cs");
exec("./Support_BrickShiftMenu.cs");

datablock PlayerData(PlayerMapleMinersArmor : PlayerStandardArmor)
{
	minLookAngle = -1.57;
	maxLookAngle = 1.57;

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

//Hacky way for custom max inventory
function ItemData::onPickup (%this, %obj, %user, %amount)
{
	if (%obj.canPickup == 0)
	{
		return;
	}
	%player = %user;
	%client = %player.client;
	%data = %player.getDataBlock ();
	if (!isObject (%client))
	{
		return;
	}
	%mg = %client.miniGame;
	%canUse = 1;
	if (miniGameCanUse (%player, %obj) == 1)
	{
		%canUse = 1;
	}
	if (miniGameCanUse (%player, %obj) == 0)
	{
		%canUse = 0;
	}
	if (!%canUse)
	{
		if (isObject (%obj.spawnBrick))
		{
			%ownerName = %obj.spawnBrick.getGroup ().name;
		}
		%msg = %ownerName @ " does not trust you enough to use this item.";
		if ($lastError == $LastError::Trust)
		{
			%msg = %ownerName @ " does not trust you enough to use this item.";
		}
		else if ($lastError == $LastError::MiniGameDifferent)
		{
			if (isObject (%client.miniGame))
			{
				%msg = "This item is not part of the mini-game.";
			}
			else 
			{
				%msg = "This item is part of a mini-game.";
			}
		}
		else if ($lastError == $LastError::MiniGameNotYours)
		{
			%msg = "You do not own this item.";
		}
		else if ($lastError == $LastError::NotInMiniGame)
		{
			%msg = "This item is not part of the mini-game.";
		}
		commandToClient (%client, 'CenterPrint', %msg, 1);
		return;
	}
	%freeslot = -1;
	%i = 0;
	while (%i < %client.GetMaxInvSlots())
	{
		if (%player.tool[%i] == 0)
		{
			%freeslot = %i;
			break;
		}
		%i += 1;
	}
	if (%freeslot != -1)
	{
		if (%obj.isStatic ())
		{
			%obj.Respawn ();
		}
		else 
		{
			%obj.delete ();
		}
		%player.tool[%freeslot] = %this;
		if (%user.client)
		{
			messageClient (%user.client, 'MsgItemPickup', '', %freeslot, %this.getId ());
		}
		return 1;
	}
}

function GameConnection::GetMaxInvSlots(%client)
{
	if (%client.MM_MaxInvSlots < 5)
		%client.MM_MaxInvSlots = 5;

	return %client.MM_MaxInvSlots;
}

function GameConnection::SetMaxInvSlots(%client, %amt)
{
	%client.MM_MaxInvSlots = %amt;

	commandToClient(%client, 'PlayGui_CreateToolHud', %client.GetMaxInvSlots());
}