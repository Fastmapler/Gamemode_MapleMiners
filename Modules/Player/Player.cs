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

	if (%this.pickupFunc !$= "")
	{
		if (call(%this.pickupFunc, %this, %player))
		{
			if (%obj.isStatic())
				%obj.Respawn();
			else 
				%obj.delete();
		}
		
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

function Armor::onCollision (%this, %obj, %col, %vec, %speed)
{
	if (%obj.getState () $= "Dead")
	{
		return;
	}
	if (%col.getDamagePercent () >= 1)
	{
		return;
	}
	%colClassName = %col.getClassName ();
	if (%colClassName $= "Item")
	{
		%client = %obj.client;
		%colData = %col.getDataBlock ();
		%i = 0;
		while (%i < %client.GetMaxInvSlots())
		{
			if (%obj.tool[%i] == %colData)
			{
				return;
			}
			%i += 1;
		}
		%obj.pickup (%col);
	}
	else if (%colClassName $= "Player" || %colClassName $= "AIPlayer")
	{
		if (%col.getDataBlock ().canRide && %this.rideAble && %this.nummountpoints > 0)
		{
			if (getSimTime () - %col.lastMountTime <= $Game::MinMountTime)
			{
				return;
			}
			%colZpos = getWord (%col.getPosition (), 2);
			%objZpos = getWord (%obj.getPosition (), 2);
			if (%colZpos <= %objZpos + 0.2)
			{
				return;
			}
			%canUse = 0;
			if (isObject (%obj.spawnBrick))
			{
				%vehicleOwner = findClientByBL_ID (%obj.spawnBrick.getGroup ().bl_id);
			}
			if (isObject (%vehicleOwner))
			{
				if (getTrustLevel (%col, %obj) >= $TrustLevel::RideVehicle)
				{
					%canUse = 1;
				}
			}
			else 
			{
				%canUse = 1;
			}
			if (miniGameCanUse (%col, %obj) == 1)
			{
				%canUse = 1;
			}
			if (miniGameCanUse (%col, %obj) == 0)
			{
				%canUse = 0;
			}
			if (!%canUse)
			{
				if (!isObject (%obj.spawnBrick))
				{
					return;
				}
				%ownerName = %obj.spawnBrick.getGroup ().name;
				%msg = %ownerName @ " does not trust you enough to do that";
				if ($lastError == $LastError::Trust)
				{
					%msg = %ownerName @ " does not trust you enough to ride.";
				}
				else if ($lastError == $LastError::MiniGameDifferent)
				{
					if (isObject (%col.client.miniGame))
					{
						%msg = "This vehicle is not part of the mini-game.";
					}
					else 
					{
						%msg = "This vehicle is part of a mini-game.";
					}
				}
				else if ($lastError == $LastError::MiniGameNotYours)
				{
					%msg = "You do not own this vehicle.";
				}
				else if ($lastError == $LastError::NotInMiniGame)
				{
					%msg = "This vehicle is not part of the mini-game.";
				}
				commandToClient (%col.client, 'CenterPrint', %msg, 1);
				return;
			}
			for (%i = 0; %i < %this.nummountpoints; %i += 1)
			{
				if (%this.mountNode[%i] $= "")
				{
					%mountNode = %i;
				}
				else 
				{
					%mountNode = %this.mountNode[%i];
				}
				%blockingObj = %obj.getMountNodeObject (%mountNode);
				if (isObject (%blockingObj))
				{
					if (!%blockingObj.getDataBlock ().rideAble)
					{
						continue;
					}
					if (%blockingObj.getMountedObject (0))
					{
						continue;
					}
					%blockingObj.mountObject (%col, 0);
					if (%blockingObj.getControllingClient () == 0)
					{
						%col.setControlObject (%blockingObj);
					}
					%col.setTransform ("0 0 0 0 0 1 0");
					%col.setActionThread (root, 0);
					continue;
				}
				%obj.mountObject (%col, %mountNode);
				%col.setActionThread (root, 0);
				if (%i == 0)
				{
					if (%obj.isHoleBot)
					{
						if (%obj.controlOnMount)
						{
							%col.setControlObject (%obj);
						}
					}
					else if (%obj.getControllingClient () == 0)
					{
						%col.setControlObject (%obj);
					}
					if (isObject (%obj.spawnBrick))
					{
						%obj.lastControllingClient = %col;
					}
				}
				break;
			}
		}
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

function GameConnection::DisplayNews(%client, %num)
{
	
}

package MM_Player
{
	function Armor::onTrigger(%this, %obj, %triggerNum, %val)
	{
		if (isObject(%client = %obj.client))
		{
			if (%triggerNum == 4 && %val && getWord(%obj.getVelocity(), 2) < -1 )
			{
				if (hasField(%obj.MM_ActivatedModules, "JetStablizers") && %client.ChangeBatteryEnergy(-100))
				{
					%obj.playAudio(0, MMJetThrustSound);
					%obj.setVelocity(getWords(%obj.getVelocity(), 0, 1) SPC 1);
				}
			}
		}
		

		return Parent::onTrigger(%this, %obj, %triggerNum, %val);
	}
};
activatePackage("MM_Player");