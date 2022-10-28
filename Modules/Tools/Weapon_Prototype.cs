datablock itemData(MMPrototypeItem)
{
	uiName = "Prototype";
	iconName = "./Shapes/icon_Prototype";
	doColorShift = false;
	colorShiftColor = "1.00 1.00 1.00 1.00";
	
	shapeFile = "./Shapes/Prototype.dts";
	image = MMPrototypeImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(MMPrototypeImage)
{
	shapeFile = "./Shapes/Prototype.dts";
	item = MMPrototypeItem;
	
	mountPoint = 0;
	offset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	
	eyeOffset = "";
	eyeRotation = "";
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = MMPrototypeItem.doColorShift;
	colorShiftColor = MMPrototypeItem.colorShiftColor;
	
	stateName[0]					= "Start";
	stateTimeoutValue[0]			= 0.1;
	stateTransitionOnTimeout[0]	 	= "Ready";
	stateSound[0]					= weaponSwitchSound;
	
	stateName[1]					= "Ready";
	stateTransitionOnTriggerDown[1] = "Charge";
	stateAllowImageChange[1]		= true;

	stateName[2]					= "Charge";
	stateScript[2]					= "onCharge";
	stateTransitionOnTriggerUp[2]	= "Fire";
	stateAllowImageChange[2]		= true;
	
	stateName[3]					= "Fire";
	stateScript[3]					= "onFire";
	stateTimeoutValue[3]			= 0.5;
	stateAllowImageChange[3]		= false;
	stateTransitionOnTimeout[3]		= "Ready";
};

function MMPrototypeImage::onCharge(%this, %obj, %slot)
{
	%obj.PrototypeScope();
}

function MMPrototypeImage::onFire(%this, %obj, %slot)
{
	cancel(%obj.PrototypeSchedule);

	%muzzlepoint = vectorSub(%obj.getMuzzlePoint(0), %obj.getMuzzleVector(0));
	%muzzlevec = %obj.getMuzzleVector(0);
	%shellcount = 1;
	%range = 500;
	
	for(%shell=0;%shell<%shellcount;%shell++)
	{
		%velocity = VectorNormalize(%muzzlevec);
		if(%range <= 500)
		{
			%raycast = containerRayCast(%muzzlepoint,VectorAdd(%muzzlepoint,VectorScale(%velocity,%range)),$TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::fxBrickObjectType | $TypeMasks::StaticObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::ForceFieldObjectType,%obj);
			%col = firstWord(%raycast);
			if(%col == 0)
				%raypos = VectorAdd(%muzzlepoint,VectorScale(%velocity,%range));
			else
				%raypos = posFromRaycast(%raycast);
		}
		else
		{
			%raycast = containerRayCast(%muzzlepoint,VectorAdd(%muzzlepoint,VectorScale(%velocity,%range/2)),$TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::fxBrickObjectType | $TypeMasks::StaticObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::ForceFieldObjectType,%obj);
			if(!isObject(firstWord(%raycast)))
			{
				%midpos = posFromRaycast(%raycast);
				%raycast = containerRayCast(%midpos,VectorAdd(%midpos,VectorScale(%velocity,%range/2)),$TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::fxBrickObjectType | $TypeMasks::StaticObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::ForceFieldObjectType,%obj);
			}
			%col = firstWord(%raycast);
			if(%col == 0)
				%raypos = VectorAdd(%muzzlepoint,VectorScale(%velocity,%range));
			else
				%raypos = posFromRaycast(%raycast);
		}
		if(isObject(%col))
		{
			// %p = new Projectile()
			// {
			// 	dataBlock = LightningGunImpactProjectile;
			// 	initialPosition = %raypos;
			// 	initialVelocity = %velocity;
			// 	sourceObject = %obj;
			// 	sourceSlot = 0;
			// 	client = %obj.client;
			// };
			// MissionCleanup.add(%p);
			if ((%col.getType() & $TypeMasks::PlayerObjectType) || (%col.getType() & $TypeMasks::VehicleObjectType))
				if(getMiniGameFromObject(%col) != -1 && miniGameCanDamage(%obj,%col) != 0)
					%col.damage(%obj, %raypos, (%obj.scopeDamage + 1) * getWord(%col.getScale(), 2), %this.directDamageType);
			
		}

		spawnBeam(%muzzlepoint, %raypos, %obj.scopeDamage / 50);
	}
	
	%obj.playThread(2, jump);
}

function Player::PrototypeScope(%player, %scopeLevel)
{
	if (!isObject(%client = %player.client))
		return;

	cancel(%player.PrototypeSchedule);
	
	%scopeDistance = 20;
	%scopeRange = mAbs(%scopeDistance - %scopeLevel);
	for (%i = 0; %i < %scopeRange * 2; %i++)
		%spacing = %spacing @ " ";

	%player.scopeDamage = getMax(mRound(mPow(mPow((%scopeDistance - %scopeRange) / %scopeDistance, 3) * 4, 5) / 2), 25);
	%client.centerPrint("<color:ff00ff><font:impact:24><br><br><br><br>>" @ %spacing @ "<<br>" @ %player.scopeDamage @ "\%", 1);

	%player.PrototypeSchedule = %player.schedule(%scopeDistance * 2 - %scopeLevel, "PrototypeScope", getMin(%scopeLevel + 1, %scopeDistance * 1.5));
}