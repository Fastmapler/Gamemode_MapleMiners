exec("./Economy.cs");
exec("./Battery.cs");
exec("./PlayerStats.cs");
exec("./ToolCrafting.cs");
exec("./Saving.cs");
exec("./Support_BrickShiftMenu.cs");

datablock PlayerData(PlayerMapleMinersArmor : PlayerStandardArmor)
{
	minLookAngle = -1.57;
	maxLookAngle = 1.57;

    maxDamage = 100;
	maxEnergy = 100;
	repairRate = 0.33;
	rechargeRate = 2 / 30;

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

	maxStepHeight = 1;
	jumpForce = 12 * 90;
    minJetEnergy = 0;
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
	return mClamp(%client.MM_PickaxeLevel * getMax(1.0, $MM::ServerBuffLevel["Berserk"]), 1, 999999);
}

function GameConnection::MM_CenterPrint(%client, %text, %length, %b)
{
	%client.centerPrint("<font:Arial:24>\c6" @ %text, %length, %b);
}

function GameConnection::MM_BottomPrint(%client, %text, %length, %b)
{
	%client.BottomPrint("<font:Arial:24>\c6" @ %text, %length, %b);
}

function StaticShape::FaceDirection(%obj, %forwardVec)
{
	return Player::FaceDirection(%obj, %forwardVec);
}

function Player::FaceDirection(%obj, %forwardVec)
{
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

function Player::RemoveTool(%obj, %currSlot)
{
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	if (isObject(%client = %obj.client))
	{
		messageClient(%client, 'MsgItemPickup', '', %currSlot, 0);
		serverCmdUnUseTool(%client);
	}
	else
		%obj.unMountImage(0);
	
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

function Player::ChangeEnergyLevel(%player, %amount)
{
	%player.setEnergyLevel(%player.getEnergyLevel() + %amount);
}

function GameConnection::DisplayNews(%client, %num)
{
	
}

function GameConnection::restoreTools(%this)
{	
	for(%x=0;%x<%this.saveTools;%x++)
	{
		if (!isObject(%this.saveTool[%x]))
			continue;

		%this.player.tool[%x] = %this.saveTool[%x].getID();
		messageClient(%this, 'MsgItemPickup', "", %x, %this.saveTool[%x].getID(), 1); //dunno what the 1 is
		if(%this.player.tool[%x].className $= "Weapon")
			%this.player.weaponCount++;
		%this.saveTool[%x] = 0;
	}
	%this.saveTools = 0;
	if(!%this.hasRestoredBefore)
	{
		messageClient(%this, '', "Your tools have been restored. Welcome back to the living world.");
		%this.hasRestoredBefore = 1;
	}
}

function serverCmdSuicide(%client)
{
	if (isObject(%client.Player))
		commandToClient(%client,'messageBoxYesNo',"Don't do it!", "Death will cost you " @ %client.GetDeathFee() @ "cr, and will be paid with pickaxe levels if you can't afford that!<br><br>Are you sure?", 'ForceSuicide','');
}

function serverCmdForceSuicide(%client)
{
	%player = %client.Player;
	if (isObject(%player))
		%player.Damage(%player, %player.getPosition(), 10000, $DamageType::Suicide);
}

function GameConnection::GetDeathFee(%client)
{
	%fee = 10 + (%client.GetPickUpgradeCost() / 2);

	for (%i = 0; %i < %client.GetMaxInvSlots(); %i++)
	{
		if (isObject(%player = %client.player))
			%item = %player.tool[%i];
		else
			%item = %client.savetool[%x];

		if (isObject(%item) && (%costData = $MM::ItemCost[%item.getName()]) !$= "")
		{
			for (%j = 0; %j < getFieldCount(%costData); %j += 2)
			{
				if (getField(%costData, %j + 1) $= "Credits")
				{
					%fee += getMin(getField(%costData, %j) * 0.05, 2500);
					break;
				}
			}
		}
	}

	return getMax(getMin(mCeil(%fee), 100000), 10);
}

registerOutputEvent("GameConnection", "ShowDeathFee", "");
function GameConnection::ShowDeathFee(%client)
{
	%fee = %client.GetDeathFee();
	if (%client.GetMaterial("Credits") >= %fee)
	{
		%creditFee = %fee;
		%levelFee = 0;
	}
	else
	{
		%remainingFee = %fee;
		%level = %client.MM_PickaxeLevel;
		while (%remainingFee >= %credits)
		{
			%remainingFee -= PickaxeUpgradeCost(%level - 1);
			%level--;
			%levelFee++;
		}
		%creditFee = getMax(%remainingFee, 0);
	}
	%client.chatMessage("\c6I am not actually the reaper, but I just tell the miners that the company has a fee for cloning your burnt radiated corpse.");
	%client.chatMessage("\c6Said fee is determined by your pickaxe level, and the raw credit value of your tools. If you can't pay the fee in full credits you wll pay with pickaxe levels.");
	%client.chatMessage("\c3Your fee currently costs " @ %creditFee @ "cr and " @ %levelFee @ " Pickaxe level(s). Be safe out there bro.");
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
	function Armor::onDisabled(%data,%this,%state)
	{
		if(%state $= "Enabled" && isObject(%client = %this.client))
		{
			//Save tools
			%this.client.saveTools = %client.GetMaxInvSlots();
			for(%x = 0; %x <%client.GetMaxInvSlots(); %x++)
				if(%this.tool[%x] > 0)
					%this.client.savetool[%x] = %this.tool[%x];

			//Give death fee

			if (%client.ignoreFee)
			{
				%client.ignoreFee = false;
				return;
			}

			%client.MM_DeathCount++;
			if (%client.MM_DeathCount <= 1)
			{
				%client.chatMessage("<color:880000>You died! Since this is your first death, cloning and item retrieval fees have been waived. However, be warned any further deaths come at a heafty cost!");
			}
			else if (%client.MM_PickaxeLevel < 10)
			{
				%client.chatMessage("<color:880000>You died! Since you are a new miner, cloning and item retrieval fees have been waived.");
				%client.MM_PickaxeLevel = getMax(%client.MM_PickaxeLevel, 5);
			}
			else
			{
				%fee = %client.GetDeathFee();
				if (%client.GetMaterial("Credits") >= %fee)
				{
					%creditFee = %fee;
					%levelFee = 0;
				}
				else
				{
					%remainingFee = %fee;
					%level = %client.MM_PickaxeLevel;
					while (%remainingFee >= %credits)
					{
						%remainingFee -= PickaxeUpgradeCost(%level - 1);
						%level--;
						%levelFee++;
					}
					%creditFee = getMax(%remainingFee, 0);
				}

				%client.chatMessage("<color:880000>You died! You have been charged " @ %creditFee @ "cr and " @ %levelFee @ " Pickaxe level(s) to cover cloning and item retrieval costs.");
				%client.SubtractMaterial(%creditFee, "Credits");
				%client.MM_PickaxeLevel = getMax(%client.MM_PickaxeLevel - %levelFee, 5);
			}
		}
		return Parent::onDisabled(%data, %this, %state);
	}
};
activatePackage("MM_Player");