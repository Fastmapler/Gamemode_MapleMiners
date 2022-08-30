function MM_MaterialPickup(%data, %player)
{
    if (!isObject(%client = %player.client) || %data.pickupData $= "")
        return;
    return %client.AddMaterial(getField(%data.pickupData, 0), getField(%data.pickupData, 1));
}

$MM::ItemCost["MM_Plasteel12PackItem"] = "60\tCredits\t1\tCrude Oil";
datablock ItemData(MM_Plasteel12PackItem)
{
	category = "Weapon";
	className = "Weapon";
	shapeFile = "base/data/shapes/brickweapon.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Plasteel 12-Pack";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
	canDrop = true;
    pickupFunc = "MM_MaterialPickup";
    pickupData = 12 TAB "PlaSteel";
};

datablock ProjectileData(PlasteelProjectile)
{
	projectileShapeName = "./Shapes/NaplamBomb.dts";
	directDamage        = 0;
	directDamageType    = $DamageType::Impact;
	radiusDamageType  = $DamageType::Impact;
	impactImpulse	   = 0;
	verticalImpulse	   = 0;
	explosion           = PlasteelExplosion;
	particleEmitter     = MM_DynamiteT1Emitter;

	brickExplosionRadius = 10;
	brickExplosionImpact = false; //destroy a brick if we hit it directly?
	brickExplosionForce  = 0;             
	brickExplosionMaxVolume = 0;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 0;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	muzzleVelocity      = 90;
	velInheritFactor    = 1;
	explodeOnDeath		= true;

	armingDelay         = 0;
	lifetime            = 4000;
	fadeDelay           = 3500;
	bounceAngle         = 90;
	minStickVelocity    = 0;
	bounceElasticity    = 0.4;
	bounceFriction      = 0.3;   
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};

function PlasteelProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	if (isObject(%client = %obj.client))
	{
        if (%col.getClassName() $= "fxDTSBrick" && getWord(roundVector(%col.getPosition()), 2) < $MM::ZLayerOffset)
        {
            %pos2 = roundVector(vectorSub(interpolateVector(%pos, %col.getPosition(), -0.1), "0 0 0.1"));
            if (!isObject($MM::BrickGrid[%pos2]))
                %brick = PlaceMineBrick(%pos2, %obj.matterType);

            if (isObject(%brick))
            {
                %brick.health += uint_mul(%client.MM_PickaxeLevel, 19);
                %success = true;
            }
        }
        
        //Refund plasteel if the shot somehow fails
        if (!%success)
        {
            %client.AddMaterial(1, %obj.matterType);
            %client.play2D(errorSound);
        }
            
	}

    parent::onCollision(%this, %obj, %col, %fade, %pos, %normal);
}

$MM::ItemCost["PlasteelGunItem"] = "500\tCredits\t5\tCrude Oil\t5\tBiomatter\t12\tPlaSteel";
$MM::ItemDisc["PlasteelGunItem"] = "An utility tool that shoots chunks of material that solidify on impact. Requires purchased materials.";
datablock itemData(PlasteelGunItem)
{
	uiName = "PlaSteel Gun";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.70 0.70 0.70 1.00";
	
	shapeFile = "base/data/shapes/printGun.dts";
	image = PlasteelGunImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(PlasteelGunImage)
{
	shapeFile = "base/data/shapes/printGun.dts";
	item = PlasteelGunItem;
	
	mountPoint = 0;
	offset = "0 0 0";
	rotation = 0;
	
	eyeOffset = "";
	eyeRotation = "";
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = PlasteelGunItem.doColorShift;
	colorShiftColor = PlasteelGunItem.colorShiftColor;

	printPlayerBattery = true;
	
	stateName[0]					= "Start";
	stateTimeoutValue[0]			= 0.5;
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

function PlasteelGunImage::onMount(%this,%obj,%slot)
{
	if (%obj.placerMatter $= "")
		%obj.placerMatter = "PlaSteel";

    %obj.DisplayMaterial(%obj.placerMatter);
}

function PlasteelGunImage::onFire(%this,%obj,%slot)
{
    if (!isObject(%client = %obj.client))
        return;

	%projectile = PlasteelProjectile;
    %spread = 0;
	%shellcount = 1;

	if (!isObject(getMatterType(%obj.placerMatter)))
		%obj.placerMatter = "PlaSteel";
	
	%type = %obj.placerMatter;

    if (%client.GetMaterial(%type) >= %shellcount)
    {
        %client.SubtractMaterial(%shellcount, %type);

        for(%shell = 0; %shell < %shellcount; %shell++)
        {
            %vector = %obj.getEyeVector();
            %objectVelocity = %obj.getVelocity();
            %vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
            %vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
            %velocity = VectorAdd(%vector1,%vector2);
            %x = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
            %y = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
            %z = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
            %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
            %velocity = MatrixMulVector(%mat, %velocity);

            %p = new Projectile()
            {
                dataBlock = %projectile;
                initialVelocity = %velocity;
                initialPosition = %obj.getEyePoint();
                sourceObject = %obj;
                sourceSlot = %slot;
                client = %obj.client;

				matterType = %type;
            };
            MissionCleanup.add(%p);
        }
    }
    else
    {
        //Empty ammo tick sound
    }

	%obj.DisplayMaterial(%type);
}

package MM_PlaSteelGun
{
	function serverCmdRotateBrick(%client, %dir)
	{
		if(isObject(%player = %client.player) && isObject(%image = %player.getMountedImage(0)) && %image.getID() == PlasteelGunImage.getID())
		{
			%types = "PlaSteel\tFrame Parts\tMechanism Parts\tCircuitry Parts\tComputation Parts\tSentient Parts";
			%idx = getFieldFromValue(%types, %player.placerMatter);
			%newIdx = %idx + %dir;
			if (%newIdx < getFieldCount(%types) && %newIdx >= 0)
			{
				%player.placerMatter = getField(%types, %newIdx);
				%player.DisplayMaterial(%player.placerMatter);
			}
			else
				return;
		}

		Parent::serverCmdRotateBrick(%client, %dir);
	}
};
activatePackage("MM_PlaSteelGun");