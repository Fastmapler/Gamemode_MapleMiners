datablock itemData(MMPDAItem)
{
	uiName = "Mining PDA";
	iconName = "./Shapes/icon_PDA";
	doColorShift = true;
	colorShiftColor = "1.00 1.00 1.00 1.00";
	
	shapeFile = "./Shapes/PDA.dts";
	image = MMPDAImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(MMPDAImage)
{
	shapeFile = "./Shapes/PDA.dts";
	item = MMPDAItem;
	
	mountPoint = 0;
	offset = "0 0.3 0";
	rotation = 0;
	
	eyeOffset = "0 1 0";
	eyeRotation = eulerToMatrix("0 0 90");
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = MMPDAItem.doColorShift;
	colorShiftColor = MMPDAItem.colorShiftColor;
	
	stateName[0]					= "Start";
	stateTimeoutValue[0]			= 0.1;
	stateTransitionOnTimeout[0]	 	= "Ready";
	stateSound[0]					= weaponSwitchSound;
	
	stateName[1]					= "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]		= true;
	
	stateName[2]					= "Fire";
	stateScript[2]					= "onFire";
	stateTimeoutValue[2]			= 0.1;
	stateAllowImageChange[2]		= false;
	stateTransitionOnTimeout[2]		= "checkFire";
	
	stateName[3]					= "checkFire";
	stateTransitionOnTriggerUp[3] 	= "Ready";
};

function MMPDAImage::onMount(%this, %obj, %slot)
{
    Parent::onMount(%this, %obj, %slot);

	if (!isObject(%client = %obj.client))
        return;

    %client.sellOreStack = 1;

    %bsm = new ScriptObject()
	{
		superClass = "BSMObject";
        class = "MM_bsmPDA";
        
		title = "Loading...";
		format = "arial:24" TAB "\c2" TAB "\c6" TAB "\c2" TAB "\c7";

		entryCount = 0;

        hideOnDeath = true;
        deleteOnFinish = true;

		cut = 2;
	};

	if (!%client.MM_WarnPDAControls)
	{
		%client.MM_WarnPDAControls = true;
		%client.chatMessage("\c6Use [\c3Brick Shift Away/Towards\c6] keys to scroll the PDA's menu.");
		%client.chatMessage("\c6Use [\c3Primary Fire\c6] key to scan the brick infront of you.");
		%client.chatMessage("\c6Use [\c3Cancel Brick\c6] to bring up gadget options.");
	}

	for (%i = 0; %i < MatterData.getCount(); %i++)
	{
		%matter = MatterData.getObject(%i);
        %count = %client.GetMaterial(%matter.name);
		if (GetMatterValue(%matter) <= 0 || %matter.unsellable || %count <= 0)
			continue;

		%bsm.entry[%bsm.entryCount] = %matter.name SPC "x" @ %count TAB %matter.name;
		%bsm.entryCount++;
	}

    MissionCleanup.add(%bsm);

	%client.brickShiftMenuEnd();
	%client.brickShiftMenuStart(%bsm);

    %client.PDAUpdateInterface();
}

function MMPDAImage::OnUnmount(%this, %obj, %slot)
{
    Parent::OnUnmount(%this, %obj, %slot);

	if (!isObject(%client = %obj.client))
        return;

	%client.brickShiftMenuEnd();
}

function MMPDAImage::onFire(%this, %obj, %slot)
{
	if (!isObject(%client = %obj.client))
		return;

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, 10)), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)))
	{
		if (%hit.canMine)
		{
			%matter = getMatterType(%hit.matter);
			%client.chatMessage("\c6---");
			%client.chatMessage("<color:" @ getSubStr(%matter.color, 0, 6) @ ">" @ %matter.name @ " \c6(LVL\c3 " @ %matter.level @ "\c6) (POSITION\c3 " @ roundVector(%hit.getPosition()) @ "\c6)");
			%client.chatMessage("\c3" @ %hit.health @ "\c6/\c3" @ %matter.health @ " \c6HP");
			if (%matter.value > 0)
				%client.chatMessage("\c6Base Value:\c3 " @ %matter.value @ "\c6cr");
			if (%matter.hitFunc $= "MM_HeatDamage" || %matter.harvestFunc $= "MM_HeatDamage")
				%client.chatMessage("\c0BURNING");
			if (%matter.hitFunc $= "MM_RadDamage" || %matter.harvestFunc $= "MM_RadDamage")
				%client.chatMessage("\c2RADIOACTIVE");
			if (%matter.hitFunc $= "MM_CancerSpread" || %matter.harvestFunc $= "MM_CancerSpread")
				%client.chatMessage("\c7CANCEROUS");
				
		}
		else if(%hit.getDataBlock().getName() $= "brickMMWarpPadData")
		{
			if (%obj.TelepadTarget !$= "")
			{
				%hit.TelepadTarget = %obj.TelepadTarget;
				%client.chatMessage("\c6Warp Pad target set to location " @ %hit.TelepadTarget @ ".");
			}
			else
			{
				%client.chatMessage("\c6Define a target position first! Hold the Mining PDA and press [\c3Cancel Brick\c6] to buffer your current transform.");
			}
		}
		else
		{
			%client.chatMessage("\c6No data found at the targeted brick.");
			return;
		}
	}
}

function GameConnection::PDAUpdateInterface(%client)
{
    if (!isObject(%bsm = %client.brickShiftMenu) || %bsm.class !$= "MM_bsmPDA")
        return;

    for (%i = 0; %i < %bsm.entryCount; %i++)
        %bsm.entry[%i] = "";

    %bsm.entryCount = 0;

    for (%i = 0; %i < MatterData.getCount(); %i++)
	{
		%matter = MatterData.getObject(%i);
        %count = %client.GetMaterial(%matter.name);
		if (%count <= 0)
			continue;

		%bsm.entry[%bsm.entryCount] = %matter.name SPC "x" @ %count SPC "(" @ getNiceNumber(uint_mul(%count, GetMatterValue(%matter))) @ "cr)" TAB %matter.name;
		%bsm.entryCount++;
	}

	%upgradeCreds = uint_add(%client.GetOreValueSum(), %client.getMaterial("Credits"));
	%levelUps = 0;
	for (%i = %client.MM_PickaxeLevel; %upgradeCreds > 0 && %levelUps < 99; %i++)
	{
		%cost = PickaxeUpgradeCost(%i);
		if (%upgradeCreds >= %cost)
		{
			%levelUps++;
			%upgradeCreds = uint_sub(%upgradeCreds, %cost);
		}
		else
			break;
	}

    %bsm.title = "<font:tahoma:16>\c3Credits: " @ getNiceNumber(%client.getMaterial("Credits")) @ "cr | Ore Value: " @ getNiceNumber(%client.GetOreValueSum()) @ "cr | \c3" @ %levelUps @ " Level(s) Buyable";
}

function MM_bsmPDA::onUserMove(%obj, %client, %id, %move, %val)
{
	if(%move == $BSM::PLT)
		return;
	if (%move == $BSM::CLR && isObject(%player = %client.player))
	{
		%bsm = new ScriptObject()
		{
			superClass = "BSMObject";
			class = "MM_bsmPDAGadgets";
			
			title = "<font:tahoma:16>\c3PDA Gadgets";
			format = "arial:24" TAB "\c2" TAB "\c6" TAB "\c2" TAB "\c7";

			entry[0] = "[Close Menu]" TAB "resetPDA";
			entry[1] = "Warp Home" TAB "warpHome"; // title TAB id
			entry[2] = "Buffer User Transform" TAB "bufferTransform";
			entry[3] = "Toggle Pickaxe Debris" TAB "toggleDebris";

			entryCount = 4;

			hideOnDeath = true;
			deleteOnFinish = true;
		};

		%client.brickShiftMenuEnd();
		%client.brickShiftMenuStart(%bsm);

		return;
	}

	Parent::onUserMove(%obj, %client, %id, %move, %val);
}

$MM::HomeWarpLocations[0] = "-31.0753 35.0102 5138.81 0 0 1 2.74985";
$MM::HomeWarpLocations[1] = "-16.1143 -95.5788 5127.6 0 0 1 1.01003";
$MM::HomeWarpLocations[2] = "5.28589 -8.49887 5151.6 0 0 -1 0.369788";
$MM::HomeWarpLocations[3] = "23.8582 -48.1114 5131 0 0 1 1.59591";
$MM::HomeWarpLocations[4] = "37.5835 38.8178 5130.6 0 0 1 0.210912";
$MM::HomeWarpLocations[5] = "7.54747 28.9974 5126.6 0 0 1.00056 0.0126389";
$MM::HomeWarpLocation = 6;
$MM::HomeWarpCooldown = 30;

function MM_bsmPDAGadgets::onUserMove(%obj, %client, %id, %move, %val)
{
	if(%move == $BSM::PLT && isObject(%player = %client.player))
	{
		switch$(%id)
		{
			case "resetPDA":
				%player.unMountImage(0);
				%player.mountImage(MMPDAImage, 0); //Hacky way to go back to the other interface, but we as result do not need to copy all of that stupid code.
			case "warpHome":
				%lastWarp = uint_sub(getSimTime(), %client.lastHomeWarp);
				if (%lastWarp > $MM::HomeWarpCooldown * 1000)
				{
					%target = $MM::HomeWarpLocations[getRandom(0, $MM::HomeWarpLocation - 1)];
					%player.teleportEffect();
					spawnBeam(%player.getPosition(), %target, 4);
					
					%player.setTransform(%target);
					ServerPlay3D(MMWarpPadWooshSound, %player.getPosition());
					ServerPlay3D(MMWarpPadWooshSound, %target);

					%client.lastHomeWarp = getSimTime();
				}
				else
					%client.chatMessage("\c6The warp core is recharging. Please wait \c3" @ ($MM::HomeWarpCooldown - mFloor(%lastWarp / 1000)) @ "\c6 more second(s).");
			case "bufferTransform":
				%player.TelepadTarget = %player.getTransform();
				%client.chatMessage("\c6Buffered your current transform of " @ %player.TelepadTarget @ ". Scan a Warp Pad to set its target.");
			case "toggleDebris":
				%client.MM_noMiningDebris = !%client.MM_noMiningDebris;
    			%client.chatMessage("\c6Pickaxe debris has been set to " @ (!%client.MM_noMiningDebris ? "\c2TRUE" : "\c0FALSE"));
		}

		return;
	}

	Parent::onUserMove(%obj, %client, %id, %move, %val);
}