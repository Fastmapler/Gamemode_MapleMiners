function MM_ExplosionNapalm(%pos, %radius, %damage, %client, %throw)
{
    %radius = mClamp(%radius, 2, 10);
    %damage = mClamp(%damage, 1, 999999);

	MM_ExplosionNapalmTick(%pos, %radius, %damage, %client, %throw, 0);
}

function MM_ExplosionNapalmTick(%pos, %radius, %damage, %client, %throw, %curRadius)
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
                    if (getRandom() < 0.30)
                    {
                        %newpos = roundVector(vectorAdd(%targetObject.getPosition(), $MM::BrickDirection[%j]));
                        $MM::SpawnGrid[%newpos] = "Slag";
                    }
                }
            }
            
			%matter = getMatterType(%targetObject.matter);

			if (%matter.value <= 0)
            	%targetObject.MineDamage(%damage, "Explosion", %client);
			else
			{
				%burnDamage = (%damage / %matter.health) * 0.075;
				%targetObject.armorDamage = getMin(%targetObject.armorDamage + %burnDamage, 0.15);
			}
        }
    }

    if (%curRadius < %radius)
        schedule(100, 0, "MM_ExplosionNapalmTick", %pos, %radius, %damage, %client, %throw, %curRadius);
}

datablock ExplosionData(MM_NapalmBombT1Explosion : rocketExplosion)
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

	damageRadius = 5;
	radiusDamage = 60;

	impulseRadius = 5;
	impulseForce = 4000;

	MineType = "Napalm";
};

AddDamageType("NapalmBomb",   '<bitmap:base/client/ui/CI/bomb> %1',    '%2 <bitmap:base/client/ui/CI/bomb> %1',1,0);
datablock ProjectileData(MM_NapalmBombT1Projectile)
{
	projectileShapeName = "./Shapes/NapalmBomb.dts";
	directDamage        = 0;
	directDamageType  = $DamageType::NapalmBomb;
	radiusDamageType  = $DamageType::NapalmBomb;
	impactImpulse	   = 0;
	verticalImpulse	   = 0;
	explosion           = MM_NapalmBombT1Explosion;
	particleEmitter     = MM_DynamiteT1Emitter;

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

$MM::ItemCost["MM_NapalmBombT1Item"] = "30\tCredits\t2\tCopper\t1\tTin";
$MM::ItemDisc["MM_NapalmBombT1Item"] = "Extremely caustic mining explosive that melts the level requirement of ores. Rated up to dirt layers.";
datablock ItemData(MM_NapalmBombT1Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Shapes/NapalmBomb.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Basic Napalm";
	iconName = "./Shapes/icon_NapalmBomb";
	doColorShift = true;
	colorShiftColor = "0.309 0.286 0.294 1.000";

	 // Dynamic properties defined by the scripts
	image = MM_NapalmBombT1Image;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(MM_NapalmBombT1Image)
{
   // Basic Item properties
   shapeFile = "./Shapes/NapalmBomb.dts";
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
   item = MM_NapalmBombT1Item;
   ammo = " ";
   projectile = MM_NapalmBombT1Projectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = MM_NapalmBombT1Item.doColorShift;
   colorShiftColor = MM_NapalmBombT1Item.colorShiftColor;

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

function MM_NapalmBombT1Image::onCharge(%this, %obj, %slot) { %obj.throwSlot = %obj.currTool; %obj.playthread(2, spearReady); }

function MM_NapalmBombT1Image::onAbortCharge(%this, %obj, %slot) { %obj.playthread(2, root); }

function MM_NapalmBombT1Image::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);

	%p = Parent::onFire(%this, %obj, %slot);
	%p.throwEye = %obj.getEyeVector();
	%p.throwFor = %obj.getForwardVector();
	
	%currSlot = %obj.throwSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T2
datablock ExplosionData(MM_NapalmBombT2Explosion : MM_NapalmBombT1Explosion)
{
    damageRadius = 5;
	radiusDamage = 1200;

	impulseRadius = 5;
	impulseForce = 4000;
};

datablock ProjectileData(MM_NapalmBombT2Projectile : MM_NapalmBombT1Projectile)
{
	explosion = MM_NapalmBombT2Explosion;
	particleEmitter = MM_DynamiteT2Emitter;
};

$MM::ItemCost["MM_NapalmBombT2Item"] = "80\tCredits\t2\tNickel\t1\tGraphite";
$MM::ItemDisc["MM_NapalmBombT2Item"] = "Extremely caustic mining explosive that melts the level requirement of ores. Rated up to stone layers.";
datablock ItemData(MM_NapalmBombT2Item : MM_NapalmBombT1Item)
{
	uiName = "Improved Napalm";
	colorShiftColor = "0.847 0.819 0.800 1.000";
	image = MM_NapalmBombT2Image;
};

datablock ShapeBaseImageData(MM_NapalmBombT2Image : MM_NapalmBombT1Image)
{
   item = MM_NapalmBombT2Item;
   projectile = MM_NapalmBombT2Projectile;

   doColorShift = MM_NapalmBombT2Item.doColorShift;
   colorShiftColor = MM_NapalmBombT2Item.colorShiftColor;
};

function MM_NapalmBombT2Image::onCharge(%this, %obj, %slot) { %obj.throwSlot = %obj.currTool; %obj.playthread(2, spearReady); }

function MM_NapalmBombT2Image::onAbortCharge(%this, %obj, %slot) { %obj.playthread(2, root); }

function MM_NapalmBombT2Image::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);

	%p = Parent::onFire(%this, %obj, %slot);
	%p.throwEye = %obj.getEyeVector();
	%p.throwFor = %obj.getForwardVector();
	
	%currSlot = %obj.throwSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}

//T3
datablock ExplosionData(MM_NapalmBombT3Explosion : MM_NapalmBombT1Explosion)
{
    damageRadius = 5;
	radiusDamage = 26754;

	impulseRadius = 5;
	impulseForce = 4000;
};

datablock ProjectileData(MM_NapalmBombT3Projectile : MM_NapalmBombT1Projectile)
{
	explosion = MM_NapalmBombT3Explosion;
	particleEmitter = MM_DynamiteT3Emitter;
};

$MM::ItemCost["MM_NapalmBombT3Item"] = "230\tCredits\t2\tUranium\t1\tOsmium";
$MM::ItemDisc["MM_NapalmBombT3Item"] = "Extremely caustic mining explosive that melts the level requirement of ores. Rated up to bedrock layers.";
datablock ItemData(MM_NapalmBombT3Item : MM_NapalmBombT1Item)
{
	uiName = "Superior Napalm";
	colorShiftColor = "0.121 0.337 0.549 1.000";
	image = MM_NapalmBombT3Image;
};

datablock ShapeBaseImageData(MM_NapalmBombT3Image : MM_NapalmBombT1Image)
{
   item = MM_NapalmBombT3Item;
   projectile = MM_NapalmBombT3Projectile;

   doColorShift = MM_NapalmBombT3Item.doColorShift;
   colorShiftColor = MM_NapalmBombT3Item.colorShiftColor;
};

function MM_NapalmBombT3Image::onCharge(%this, %obj, %slot) { %obj.throwSlot = %obj.currTool; %obj.playthread(2, spearReady); }

function MM_NapalmBombT3Image::onAbortCharge(%this, %obj, %slot) { %obj.playthread(2, root); }

function MM_NapalmBombT3Image::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);

	%p = Parent::onFire(%this, %obj, %slot);
	%p.throwEye = %obj.getEyeVector();
	%p.throwFor = %obj.getForwardVector();
	
	%currSlot = %obj.throwSlot;
	%obj.tool[%currSlot] = 0;
	%obj.weaponCount--;
	messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
	serverCmdUnUseTool(%obj.client);
}