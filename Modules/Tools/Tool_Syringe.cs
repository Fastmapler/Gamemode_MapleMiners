datablock AudioProfile(MM_SyringeHealSound)
{
   filename    = "./Sounds/Syringe.wav";
   description = AudioClose3d;
   preload = true;
};

$MM::ItemCost["MM_SyringeItem"] = "451\tCredits\t1\tDragonstone\t10\tTungsten";
$MM::ItemDisc["MM_SyringeItem"] = "A reusuable syringe that consumes biomatter to recover 30 HP for you or (more efficently) a friend.";
datablock ItemData(MM_SyringeItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Shapes/Syringe.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Reusable Syringe";
	iconName = "./Shapes/Syringe";
	doColorShift = false;

	 // Dynamic properties defined by the scripts
	image = MM_SyringeImage;
	canDrop = true;
};

datablock ShapeBaseImageData(MM_SyringeImage)
{
   // Basic Item properties
   shapeFile = "./Shapes/Syringe.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.1";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );
	minShotTime = 5000;

   className = "WeaponImage";
   item = MM_SyringeItem;

   //raise your arm up or not
   armReady = true;

   doColorShift = false;

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
	stateTransitionOnTimeout[2]		= "Fire";
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

function MM_SyringeImage::onMount(%this,%obj,%slot)
{
	%obj.DisplayMaterial("Biomatter");

	if (isObject(%client = %obj.client) && !%client.MM_WarnSyringeControls)
	{
		%client.MM_WarnSyringeControls = true;
		%client.chatMessage("\c6TAP [\c3Primary Fire\c6] to heal an ally for 30 HP. 50\% chance to not consume ammo.");
		%client.chatMessage("\c6HOLD [\c3Primary Fire\c6] to heal yourself for 30 HP. Always uses ammo.");
	}
}

function MM_SyringeImage::onCharge(%this,%obj,%slot)
{
	%obj.playthread(2, spearReady);
}

function MM_SyringeImage::onAbortCharge(%this,%obj,%slot)
{
	%obj.playThread(2, shiftDown);

	if (!isObject(%client = %obj.client) || %client.GetMaterial("Biomatter") <= 0)
		return;

	serverPlay3D(MM_SyringeHealSound, %obj.getPosition());

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType | $Typemasks::PlayerObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, 10)), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)))
	{
		if (%hit.getClassName() $= "Player" && %hit.getDamageLevel() > 0) //Using class name and not getType() so we can't heal bots
		{
			%hit.addHealth(30);
			serverPlay3D(MM_SyringeHealSound, %hit.getPosition());
			%hit.setWhiteOut(0.25);

			if (getRandom() < 0.5)
				%client.SubtractMaterial(1, "Biomatter");
			%obj.DisplayMaterial("Biomatter");
		}
	}
}

function MM_SyringeImage::onFire(%this,%obj,%slot)
{
	%obj.playthread(2, shiftUp);

	if (%obj.getDamageLevel() <= 0)
		return;

	if (isObject(%client = %obj.client) && %client.GetMaterial("Biomatter") > 0)
    {
		%obj.addHealth(30);
		serverPlay3D(MM_SyringeHealSound, %obj.getPosition());
		%obj.setWhiteOut(0.25);

		%client.SubtractMaterial(1, "Biomatter");
		%obj.DisplayMaterial("Biomatter");
	}
}
