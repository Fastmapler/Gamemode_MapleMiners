function MM_ExplosionGeneric(%pos, %radius, %damage, %client)
{
    %radius = mClamp(%radius, 2, 10);
    %damage = mClamp(%damage, 1, 999999);

	MM_ExplosionGenericTick(%pos, %radius, %damage, %client, 0);
}

function MM_ExplosionGenericTick(%pos, %radius, %damage, %client, %curRadius)
{
    %curRadius++;
    InitContainerRadiusSearch(%pos, %curRadius, $TypeMasks::FXBrickObjectType);
    while(isObject(%targetObject = containerSearchNext()))
    {
        %targetPos = getWord(%targetObject.getPosition(), 2);
        if(%targetObject.canMine)
        {
            if (%i < %radius - 1)
            {
                for (%j = 0; %j < 6; %j++)
                {
                    if (getRandom() < 0.15)
                    {
                        %newpos = roundVector(vectorAdd(%targetObject.getPosition(), $MM::BrickDirection[%j]));
                        $MM::SpawnGrid[%newpos] = "Slag";
                    }
                }
            }
                
            %targetObject.MineDamage(%damage, "Explosion", %client);
        }
    }

    if (%curRadius < %radius)
        schedule(100, 0, "MM_ExplosionGenericTick", %pos, %radius, %damage, %client, %curRadius);
}

datablock ExplosionData(MM_DynamiteT1Explosion : rocketExplosion)
{
	soundProfile = rocketExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = rocketExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;

	emitter[0] = rocketExplosionRingEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 10;
	lightEndRadius = 25;
	lightStartColor = "1 1 1 1";
	lightEndColor = "0 0 0 1";

	damageRadius = 6;
	radiusDamage = 50;

	impulseRadius = 6;
	impulseForce = 4000;
};

AddDamageType("Dynamite",   '<bitmap:base/client/ui/CI/bomb> %1',    '%2 <bitmap:base/client/ui/CI/bomb> %1',1,0);
datablock ProjectileData(MM_DynamiteT1Projectile)
{
	projectileShapeName = "./Shapes/Dynamite.dts";
	directDamage        = 0;
	directDamageType  = $DamageType::Dynamite;
	radiusDamageType  = $DamageType::Dynamite;
	impactImpulse	   = 0;
	verticalImpulse	   = 0;
	explosion           = MM_DynamiteT1Explosion;
	//particleEmitter     = fragGrenadeTrailEmitter;

	brickExplosionRadius = 10;
	brickExplosionImpact = false; //destroy a brick if we hit it directly?
	brickExplosionForce  = 0;             
	brickExplosionMaxVolume = 0;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 0;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	muzzleVelocity      = 30;
	velInheritFactor    = 1;
	explodeOnDeath		= true;

	armingDelay         = 4000; //4 second fuse 
	lifetime            = 4000;
	fadeDelay           = 3500;
	bounceAngle         = 90; //stick almost all the time
	minStickVelocity    = 20;
	bounceElasticity    = 0.4;
	bounceFriction      = 0.3;   
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};

datablock ItemData(MM_DynamiteT1Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Shapes/Dynamite.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Basic Dynamite";
	iconName = "./Shapes/icon_dynamite";
	doColorShift = true;
	colorShiftColor = "0.309 0.286 0.294 1.000";

	 // Dynamic properties defined by the scripts
	image = MM_DynamiteT1Image;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(MM_DynamiteT1Image)
{
   // Basic Item properties
   shapeFile = "./Shapes/Dynamite.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   rotation = eulerToMatrix( "90 0 0" );
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = MM_DynamiteT1Item;
   ammo = " ";
   projectile = MM_DynamiteT1Projectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = MM_DynamiteT1Item.doColorShift;
   colorShiftColor = MM_DynamiteT1Item.colorShiftColor;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]					= "Activate";
	stateTimeoutValue[0]			= 0.1;
	stateTransitionOnTimeout[0]		= "Ready";
	stateSequence[0]				= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]					= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]		= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]		= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]			= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]					= "AbortCharge";
	stateTransitionOnTimeout[3]		= "Ready";
	stateTimeoutValue[3]			= 0.3;
	stateWaitForTimeout[3]			= true;
	stateScript[3]					= "onAbortCharge";
	stateAllowImageChange[3]		= false;

	stateName[4]					= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]		= false;

	stateName[5]					= "Fire";
	stateTransitionOnTimeout[5]		= "Ready";
	stateTimeoutValue[5]			= 0.5;
	stateFire[5]					= true;
	stateSequence[5]				= "fire";
	stateScript[5]					= "onFire";
	stateWaitForTimeout[5]			= true;
	stateAllowImageChange[5]		= false;
};

function MM_DynamiteT1Image::onCharge(%this, %obj, %slot) { %obj.playthread(2, spearReady); }

function MM_DynamiteT1Image::onAbortCharge(%this, %obj, %slot) { %obj.playthread(2, root); }

function MM_DynamiteT1Image::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
	
	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T2
datablock ExplosionData(MM_DynamiteT2Explosion : MM_DynamiteT1Explosion)
{
    damageRadius = 6;
	radiusDamage = 1000;

	impulseRadius = 6;
	impulseForce = 4000;
};

datablock ProjectileData(MM_DynamiteT2Projectile : MM_DynamiteT1Projectile) { explosion = MM_DynamiteT2Explosion; };

datablock ItemData(MM_DynamiteT2Item : MM_DynamiteT1Item)
{
	uiName = "Improved Dynamite";
	colorShiftColor = "0.847 0.819 0.800 1.000000";
	image = MM_DynamiteT2Image;
};

datablock ShapeBaseImageData(MM_DynamiteT2Image : MM_DynamiteT1Image)
{
   item = MM_DynamiteT2Item;
   projectile = MM_DynamiteT2Projectile;

   doColorShift = MM_DynamiteT2Item.doColorShift;
   colorShiftColor = MM_DynamiteT2Item.colorShiftColor;
};

function MM_DynamiteT2Image::onCharge(%this, %obj, %slot) { %obj.playthread(2, spearReady); }

function MM_DynamiteT2Image::onAbortCharge(%this, %obj, %slot) { %obj.playthread(2, root); }

function MM_DynamiteT2Image::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
	
	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T3
datablock ExplosionData(MM_DynamiteT3Explosion : MM_DynamiteT1Explosion)
{
    damageRadius = 6;
	radiusDamage = 20000;

	impulseRadius = 6;
	impulseForce = 4000;
};

datablock ProjectileData(MM_DynamiteT3Projectile : MM_DynamiteT1Projectile) { explosion = MM_DynamiteT3Explosion; };

datablock ItemData(MM_DynamiteT3Item : MM_DynamiteT1Item)
{
	uiName = "Superior Dynamite";
	colorShiftColor = "0.121 0.337 0.549 1.000";
	image = MM_DynamiteT3Image;
};

datablock ShapeBaseImageData(MM_DynamiteT3Image : MM_DynamiteT1Image)
{
   item = MM_DynamiteT3Item;
   projectile = MM_DynamiteT3Projectile;

   doColorShift = MM_DynamiteT3Item.doColorShift;
   colorShiftColor = MM_DynamiteT3Item.colorShiftColor;
};

function MM_DynamiteT3Image::onCharge(%this, %obj, %slot) { %obj.playthread(2, spearReady); }

function MM_DynamiteT3Image::onAbortCharge(%this, %obj, %slot) { %obj.playthread(2, root); }

function MM_DynamiteT3Image::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
	
	%currSlot = %obj.currTool;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

package MM_Explosives
{
    function ProjectileData::onExplode(%this, %obj, %pos)
    {
        if (isObject(%client = %obj.sourceObject.client))
        {
            if (isObject(%explosion = %obj.getDataBlock().explosion) && %explosion.radiusDamage >= 1)
            {
                MM_ExplosionGeneric(%obj.getPosition(), %explosion.damageRadius, %explosion.radiusDamage * 2, %client);
            }
        }
            
        Parent::onExplode(%this, %obj, %pos);
    }
};
activatePackage("MM_Explosives");