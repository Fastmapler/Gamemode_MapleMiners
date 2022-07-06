//Upgrade Debris
datablock DebrisData(upgradePart2Debris)
{

	shapeFile = "./Shapes/toolboxgear.dts";
	lifetime = 2.8;
	spinSpeed			= 1200.0;
	minSpinSpeed = -3600.0;
	maxSpinSpeed = 3600.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 3;
	staticOnMaxBounce = false;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 4;
};

datablock ParticleData(upgradeOpenExplosionParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0.4;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 2200;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/cloud";
	spinSpeed			= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]			= "0.8 0.8 0.6 0.3";
	colors[1]			= "0.8 0.8 0.6 0.0";
	sizes[0]			= 6.25;
	sizes[1]			= 8.25;

	useInvAlpha = true;
};

datablock ParticleEmitterData(upgradeOpenExplosionEmitter)
{
	ejectionPeriodMS	= 1;
	periodVarianceMS	= 0;
	ejectionVelocity	= 4;
	velocityVariance	= 1.0;
	ejectionOffset  	= 0.0;
	thetaMin			= 89;
	thetaMax			= 90;
	phiReferenceVel		= 0;
	phiVariance			= 360;
	overrideAdvance		= false;
	particles			= "upgradeOpenExplosionParticle";
};

datablock ParticleData(upgradeOpenChunksParticle)
{
	dragCoefficient			= 1;
	gravityCoefficient		= 6;
	inheritedVelFactor		= 0.2;
	constantAcceleration	= 0.0;
	lifetimeMS				= 700;
	lifetimeVarianceMS		= 100;
	textureName				= "base/data/particles/nut";
	spinSpeed				= 790.0;
	spinRandomMin			= -790.0;
	spinRandomMax			= 790.0;
	colors[0]				= "0.4 0.2 0.0 0.6";
	colors[1]				= "0.4 0.2 0.0 0.0";
	sizes[0]				= 0.7;
	sizes[1]				= 0.6;

	useInvAlpha				= false;
};

datablock ParticleEmitterData(upgradeOpenChunksEmitter)
{
	lifeTimeMS			= 100;
	ejectionPeriodMS	= 6;
	periodVarianceMS	= 0;
	ejectionVelocity	= 12;
	velocityVariance	= 12.0;
	ejectionOffset		= 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel		= 0;
	phiVariance			= 360;
	overrideAdvance		= false;
	particles			= "upgradeOpenChunksParticle";
};

datablock ParticleData(upgradeOpenSprayParticle)
{
	dragCoefficient			= 0.2;
	gravityCoefficient		= 5;
	inheritedVelFactor		= 0.2;
	constantAcceleration	= 0.0;
	lifetimeMS				= 1000;
	lifetimeVarianceMS		= 200;
	textureName				= "base/data/particles/chunk";
	spinSpeed				= 900.0;
	spinRandomMin			= -900.0;
	spinRandomMax			= 900.0;
	colors[0]				= "0.27 0.1 0.0 1";
	colors[1]				= "0.27 0.1 0.0 1";
	colors[2]				= "0.27 0.1 0.0 0";
	sizes[0]				= 0.8;
	sizes[1]				= 0.8;
	sizes[2]				= 0.0;
	useInvAlpha				= false;
};

datablock ParticleEmitterData(upgradeOpenSprayEmitter)
{
	lifeTimeMS			= 100;
	ejectionPeriodMS	= 1;
	periodVarianceMS	= 0;
	ejectionVelocity	= 18;
	velocityVariance	= 18.0;
	ejectionOffset		= 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel		= 0;
	phiVariance			= 360;
	overrideAdvance		= false;
	particles			= "upgradeOpenSprayParticle";
};

//explosion
//////////////////////////////////////////

datablock ExplosionData(upgradeOpenDebris1Explosion)
{
   particleEmitter = upgradeOpenChunksEmitter;
   particleDensity = 4;
   particleRadius = 0.2;

   debris = upgradePart2Debris;
   debrisNum = 8;
   debrisNumVariance = 6;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 12;
   debrisVelocityVariance = 6;
};

datablock ExplosionData(upgradeOpenDebris2Explosion)
{
   //debris = upgradePart3Debris;
   debrisNum = 1;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 16;
   debrisVelocityVariance = 6;
};

datablock ExplosionData(upgradeExplosionExplosion)
{

   explosionShape = "";

   lifeTimeMS = 150;

   //debris = upgradePart4Debris;
   debrisNum = 1;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 16;
   debrisVelocityVariance = 6;

   particleEmitter = upgradeOpenExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = upgradeOpenSprayEmitter;
   emitter[1] = upgradeOpenStreakEmitter;
   emitter[2] = upgradeOpenChunksEmitter;

   subExplosion[0] = upgradeOpenDebris1Explosion;
   subExplosion[1] = upgradeOpenDebris2Explosion;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "3.0 10.0 3.0";
   camShakeDuration = 0.1;
   camShakeRadius = 20.0;
};

datablock ExplosionData(upgradeExplosion)
{

   explosionShape = "";

   lifeTimeMS = 150;

   particleEmitter = upgradeOpenExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = upgradeOpenSprayEmitter;
   emitter[1] = upgradeOpenStreakEmitter;
   emitter[2] = upgradeOpenChunksEmitter;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "3.0 10.0 3.0";
   camShakeDuration = 0.1;
   camShakeRadius = 20.0;
};

datablock ProjectileData(upgradeExplosionProjectile)
{
	uiname							= "";

	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath						= true;
	explosion						= upgradeExplosionExplosion;

};

datablock ProjectileData(upgradeExplosion2Projectile)
{
	uiname							= "";

	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath						= true;
	explosion						= upgradeExplosion;

};

//Mining Debris
datablock ParticleData(largeRDirtParticle)
{
   dragCoefficient = 3;
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1;
   constantAcceleration = 0;
   lifetimeMS         = 600;
   lifetimeVarianceMS = 250;
   textureName = "base/data/particles/cloud";
   spinSpeed     = 0;
   spinRandomMin = -20;
   spinRandomMax = 20;
   colors[0] = "0.625 0.375 0.000 1.000";
   colors[1] = "0.625 0.375 0.000 1.000";
   colors[2] = "0.625 0.375 0.000 0.000";
   sizes[0] = 1.5;
   sizes[1] = 0.3;
   sizes[2] = 0.04;
   times[1] = 0.5;
   times[2] = 1;
   useInvAlpha = true;
};

datablock ParticleEmitterData(largeRDirtEmitter)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;
   ejectionVelocity = 4;
   velocityVariance = 0;
   ejectionOffset   = 0;
   thetaMin = 0;
   thetaMax = 180;
   phiReferenceVel = 0;
   phiVariance     = 360;
   overrideAdvance = false;
   lifetimeMS = 3500;
   particles = "largeRDirtParticle";

   uiName = "";
};

datablock ParticleData(smallDirtParticle)
{
   dragCoefficient = 3;
   gravityCoefficient = 0.5;
   inheritedVelFactor = 0.3;
   constantAcceleration = 0;
   lifetimeMS         = 100;
   lifetimeVarianceMS = 250;
   textureName = "base/data/particles/dot";
   spinSpeed     = 0;
   spinRandomMin = -20;
   spinRandomMax = 20;
   colors[0] = "0.625 0.375 0.000 1.000";
   colors[1] = "0.625 0.375 0.000 3.000 ";
   colors[2] = "0.625 0.375 0.000 0.000";
   sizes[0] = 0.06;
   sizes[1] = 0.2;
   sizes[2] = 0.04;
   times[1] = 0.5;
   times[2] = 1;
   useInvAlpha = true;
};

datablock ParticleEmitterData(smallDirtEmitter)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 0;
   ejectionOffset   = 0;
   thetaMin = 0;
   thetaMax = 180;
   phiReferenceVel = 0;
   phiVariance     = 360;
   overrideAdvance = false;
   lifetimeMS = 3500;
   particles = "smallDirtParticle";

   uiName = "";
};

datablock ParticleData(dirtBurstSprinkleSmallParticle)
{
	dragCoefficient			= 1.0;
	windCoefficient			= 0.0;
	gravityCoefficient		= 3.0;
	inheritedVelFactor		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS				= 600;
	lifetimeVarianceMS		= 200;
	spinSpeed				= 10.0;
	spinRandomMin			= -50.0;
	spinRandomMax			= 50.0;
	useInvAlpha				= true;
	animateTexture			= false;
	
	textureName		= "base/data/particles/dot";
	
	colors[0]	= "0.625 0.375 0.000 0.000";
	colors[1]	= "0.625 0.375 0.000 0.300";
	colors[2]	= "0.625 0.375 0.000 0.300";

	sizes[0]	= 0.0;
	sizes[1]	= 0.1;
	sizes[2]	= 0.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};
datablock ParticleEmitterData(dirtBurstSprinkleSmallEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   lifetimeMS       = 150;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "dirtBurstSprinkleSmallParticle";
};

// small dirt explosion
datablock ParticleData(dirtBurstSmallParticle)
{
	dragCoefficient			= 1.0;
	windCoefficient			= 0.0;
	gravityCoefficient		= 1.0;
	inheritedVelFactor		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS				= 500;
	lifetimeVarianceMS		= 200;
	spinSpeed				= 10.0;
	spinRandomMin			= -50.0;
	spinRandomMax			= 50.0;
	useInvAlpha				= true;
	animateTexture			= false;
	
	textureName		= "base/data/particles/cloud";
	
	colors[0]	= "0.625 0.375 0.000 0.000";
	colors[1]	= "0.625 0.375 0.000 0.500";
	colors[2]	= "0.625 0.375 0.000 0.300";

	sizes[0]	= 0.2;
	sizes[1]	= 1.0;
	sizes[2]	= 0.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};
datablock ParticleEmitterData(dirtBurstSmallEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   lifetimeMS       = 150;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "dirtBurstSmallParticle";
};

// large dirt explosion (dismembering)
datablock ParticleData(dirtBurstLargeParticle)
{
	dragCoefficient			= 1.0;
	windCoefficient			= 0.0;
	gravityCoefficient		= 1.0;
	inheritedVelFactor		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS				= 400;
	lifetimeVarianceMS		= 200;
	spinSpeed				= 10.0;
	spinRandomMin			= -50.0;
	spinRandomMax			= 50.0;
	useInvAlpha				= true;
	animateTexture			= false;
	
	textureName		= "base/data/particles/cloud";
	
	colors[0]	= "0.625 0.375 0.000 0.000";
	colors[1]	= "0.625 0.375 0.000 0.500";
	colors[2]	= "0.625 0.375 0.000 0.300";

	sizes[0]	= 0.2;
	sizes[1]	= 2.0;
	sizes[2]	= 0.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};
datablock ParticleEmitterData(dirtBurstLargeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   lifetimeMS       = 150;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "dirtBurstLargeParticle";
};
datablock DebrisData(dirtBurstDebris)
{
   emitters = smallDirtEmitter;

	shapeFile = "./Shapes/cube.dts";
	lifetime = 5.0;
	minSpinSpeed = -5000.0;
	maxSpinSpeed = 5000.0;
	elasticity = 0.2;
	friction = 0.8;
	numBounces = 2;
	staticOnMaxBounce = false;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};
datablock ExplosionData(dirtBurstFinalExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 1000;

   soundProfile = "";
   
   emitter[0] = dirtBurstSprinkleSmallEmitter;
   emitter[1] = "";

   particleEmitter = dirtBurstSmallEmitter;
   particleDensity = 5;
   particleRadius = 0.2;

   debris = dirtBurstDebris;
   debrisNum = 15;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = -15;
   debrisThetaMax = 30;
   debrisVelocity = 4;
   debrisVelocityVariance = 2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 0.75;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 0;
   impulseForce = 0;
   impulseVertical = 0;

   //radius damage
   radiusDamage        = 0;
   damageRadius        = 0;

   //burn the players?
   playerBurnTime = 0;
};

datablock ProjectileData(dirtExplosionProjectile)
{
	uiname							= "";

	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath					= true;
	explosion						= dirtBurstFinalExplosion;

};
addExtraResource("Add-Ons/Gamemode_MapleMiners/Modules/Environment/Shapes/brown.blank.png"); //For cube texture
datablock DebrisData(dirtHitDebris)
{
   emitters = smallDirtEmitter;

	shapeFile = "./Shapes/cube.dts";
	lifetime = 1.0;
	minSpinSpeed = -5000.0;
	maxSpinSpeed = 5000.0;
	elasticity = 0.2;
	friction = 0.8;
	numBounces = 0;
	staticOnMaxBounce = false;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};

datablock ExplosionData(dirtHitExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 1000;

   soundProfile = "";
   
   emitter[0] = dirtBurstSprinkleSmallEmitter;
   emitter[1] = "";

   particleEmitter = dirtBurstSmallEmitter;
   particleDensity = 5;
   particleRadius = 0.2;

   debris = dirtHitDebris;
   debrisNum = 5;
   debrisNumVariance = 1;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = -90;
   debrisThetaMax = 90;
   debrisVelocity = 4;
   debrisVelocityVariance = 1;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 0.75;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 0;
   impulseForce = 0;
   impulseVertical = 0;

   //radius damage
   radiusDamage        = 0;
   damageRadius        = 0;

   //burn the players?
   playerBurnTime = 0;
};

datablock ProjectileData(dirtHitProjectile)
{
	uiname							= "";

	lifetime					   	= 10;
	fadeDelay						= 10;
	explodeondeath					= true;
	explosion						= dirtHitExplosion;

};