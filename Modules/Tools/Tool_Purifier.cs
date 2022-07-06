
datablock ParticleData(PurifierTrailParticle)
{
	dragCoefficient      = 2.9;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 1.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0. 0 0.1 1";
	colors[1]     = "1 0.5 0 0";
   	colors[2]     = "1 1 1 1";
   	colors[3]     = "1 0 0 0.5";

	sizes[0]      = 0.4;
	sizes[1]      = 0.8;
 	sizes[2]      = 1.2;
 	sizes[3]      = 1.6;

   	times[0] = 0;
   	times[1] = 1;
   	times[2] = 2;
  	times[3] = 2;

	useInvAlpha = false;
};

datablock ParticleEmitterData(PurifierTrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 20;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientOnVelocity = true;
   particles = "PurifierTrailParticle";
};

datablock ParticleData(PurifierIdleParticle)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 3.0;
	lifetimeMS           = 1500;
	lifetimeVarianceMS   = 1000;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1   1   0.3 0.0";
	colors[1]	= "1   1   0.3 1.0";
	colors[2]	= "0.6 0.0 0.0 0.0";

	sizes[0]	= 0.0;
	sizes[1]	= 0.2;
	sizes[2]	= 0.15;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(PurifierIdleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0.5;
   ejectionVelocity = 0;
   ejectionOffset   = 0.00;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0.1;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = PurifierIdleParticle;   
};

datablock ExplosionData(PurifierExplosion)
{
   //explosionShape = "";

   lifeTimeMS = 1000;

   faceViewer     = true;

   particleEmitter = PurifierTrailEmitter;
   explosionScale = "2 2 2";

   shakeCamera = false;

   // Dynamic light
   lightStartRadius = 0.5;
   lightEndRadius = 0.25;
   lightStartColor = "1 0.5 0 1";
   lightEndColor = "1 0.5 0 1";

   damageRadius = 5;
   radiusDamage = 1;

   playerBurnTime = 5000;
};

AddDamageType("Purifier",   '<bitmap:add-ons/Weapon_Gun/CI_Gun> %1',    '%2 <bitmap:add-ons/Weapon_Gun/CI_Gun> %1',0.2,1);
datablock ProjectileData(PurifierProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";
   directDamage        = 30;
   directDamageType    = $DamageType::Purifier;
   radiusDamageType    = $DamageType::Purifier;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 0;
   verticalImpulse	  = 0;
   explosion           = PurifierExplosion;
   particleEmitter     = PurifierTrailEmitter;

   muzzleVelocity      = 25;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 600;
   fadeDelay           = 600;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = true;
   gravityMod = 0.25;

   hasLight    = true;
   lightRadius = 1;
   lightColor  = "1 0.5 0 1";

   uiName = "";
};

//////////
// item //
//////////
$MM::ItemCost["PurifierItem"] = "1\tInfinity";
$MM::ItemDisc["PurifierItem"] = "Purifies cancerous and explosive materials such as tumors and condensed void. Uses drill fuel.";
datablock ItemData(PurifierItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Shapes/Purifier.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Purifier";
	iconName = "./Shapes/icon_Purifier";
	doColorShift = true;
	colorShiftColor = "0.902 0.341 0.078 1.000";

	 // Dynamic properties defined by the scripts
	image = PurifierImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(PurifierImage)
{
   // Basic Item properties
   shapeFile = "./Shapes/Purifier.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BowItem;
   ammo = " ";
   projectile = PurifierProjectile;
   projectileType = Projectile;

	casing = gunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = PurifierItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                    = "Activate";
	stateTimeoutValue[0]            = 0.28;
	stateTransitionOnTimeout[0]     = "Ready";
	stateSound[0]                   = weaponSwitchSound;

	stateName[1]                    = "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateTransitionOnTimeout[1]     = "Ready";
	stateTimeoutValue[1]            = 0.07;
	stateEmitter[1]					= PurifierIdleEmitter;
	stateEmitterTime[1]				= 0.07;
	stateEmitterNode[1]				= "muzzleNode";
	stateAllowImageChange[1]        = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Fire";
	stateTransitionOnTriggerUp[2]   = "Smoke";
	stateTimeoutValue[2]            = 0.14;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]			        = rocketLoopSound;

	stateName[3] = "Smoke";
	stateEmitter[3]					= gunSmokeEmitter;
	stateEmitterTime[3]				= 0.14;
	stateEmitterNode[3]				= "muzzleNode";
	stateTimeoutValue[3]            = 0.28;
	stateTransitionOnTimeout[3]     = "Ready";

};

function PurifierImage::onMount(%this,%obj,%slot)
{
    %obj.DisplayMaterial("Drill Fuel");
}

function PurifierImage::onFire(%this,%obj,%slot)
{
    if (!isObject(%client = %obj.client))
        return;

	%projectile = PurifierProjectile;
    %spread = 0;
	%shellcount = 1;

    if (%client.GetMaterial("Drill Fuel") >= %shellcount)
    {
        %client.SubtractMaterial(%shellcount, "Drill Fuel");

        for(%shell=0; %shell<%shellcount; %shell++)
        {
            %vector = %obj.getMuzzleVector(%slot);
            %objectVelocity = %obj.getVelocity();
            %vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
            %vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
            %velocity = VectorAdd(%vector1,%vector2);
            %x = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
            %y = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
            %z = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
            %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
            %velocity = MatrixMulVector(%mat, %velocity);

            %p = new (%this.projectileType)()
            {
                dataBlock = %projectile;
                initialVelocity = %velocity;
                initialPosition = %obj.getMuzzlePoint(%slot);
                sourceObject = %obj;
                sourceSlot = %slot;
                client = %obj.client;
            };
            MissionCleanup.add(%p);
        }
    }
    else
    {
        //Empty ammo tick sound
    }

	%obj.DisplayMaterial("Drill Fuel");
}

function Player::DisplayMaterial(%obj, %type)
{
    if (!isObject(%client = %obj.client) || !isObject(%matter = getMatterType(%type)))
        return;

    %amount = %client.GetMaterial(%matter.name);
    %client.MM_CenterPrint("<just:right>\c6" @ %matter.name @ ": "@ (%amount > 0 ? "\c3" : "\c0") @ %amount, 3);
}