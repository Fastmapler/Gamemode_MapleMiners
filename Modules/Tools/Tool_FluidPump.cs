datablock AudioProfile(FluidPumpEquipSound)
{
    filename    = "./Sounds/PumpEquip.wav";
    description = AudioClosest3d;
    preload = true;
};

datablock AudioProfile(FluidPumpTickSound)
{
    filename    = "./Sounds/PumpTick.wav";
    description = AudioClosest3d;
    preload = true;
};

$MM::ItemCost["FluidPumpItem"] = "420\tCredits\t5\tTin\t5\tCopper\t15\tGranite";
$MM::ItemDisc["FluidPumpItem"] = "A specialized electric tool to extract fluid-like materials from fluid pools.";
datablock itemData(FluidPumpItem)
{
	uiName = "Fluid Pump";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.70 0.70 0.25 1.00";
	
	shapeFile = "base/data/shapes/printGun.dts";
	image = FluidPumpImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(FluidPumpImage)
{
	shapeFile = "base/data/shapes/printGun.dts";
	item = FluidPumpItem;
	
	mountPoint = 0;
	offset = "0 0.25 0.15";
	rotation = eulerToMatrix("0 5 70");
	
	eyeOffset = "0.75 1.15 -0.24";
	eyeRotation = eulerToMatrix("0 5 70");
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = FluidPumpItem.doColorShift;
	colorShiftColor = FluidPumpItem.colorShiftColor;

	printPlayerBattery = true;
	
	stateName[0]					= "Start";
	stateTimeoutValue[0]			= 0.5;
	stateTransitionOnTimeout[0]	 	= "Ready";
	stateSound[0]					= FluidPumpEquipSound;
	
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

function FluidPumpImage::onFire(%this, %obj, %slot) { %obj.attemptCollectFluid(1.0); }

function FluidPumpImage::onUnmount(%this, %obj, %slot) { cancel(%obj.collectFluidSchedule); }

$MM::ItemCost["UltraSuckerItem"] = "42069\tCredits\t5\tOsmium\t5\tUranium\t15\tTungsten";
$MM::ItemDisc["UltraSuckerItem"] = "An upgraded fluid pump that sucks 50% faster than base speed. Less energy efficent.";
datablock itemData(UltraSuckerItem : FluidPumpItem)
{
	uiName = "Ultra Sucker";
	colorShiftColor = "0.70 0.70 0.25 0.75";
	image = UltraSuckerImage;
};

datablock shapeBaseImageData(UltraSuckerImage : FluidPumpImage)
{
	item = FluidPumpItem;
	doColorShift = FluidPumpItem.doColorShift;
	colorShiftColor = FluidPumpItem.colorShiftColor;
};

function UltraSuckerImage::onFire(%this, %obj, %slot) { %obj.attemptCollectFluid(1.5); }

function UltraSuckerImage::onUnmount(%this, %obj, %slot) { cancel(%obj.collectFluidSchedule); }

function Player::attemptCollectFluid(%obj, %speed)
{
	if (!isObject(%client = %obj.client))
        return;

    %eye = %obj.getEyePoint();
    %dir = %obj.getEyeVector();
    %for = %obj.getForwardVector();
    %face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
    %mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
    %ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, 5)), %mask, %obj);
    if(isObject(%hit = firstWord(%ray)) && %hit.getClassName() $= "fxDtsBrick")
    {
		%matter = getMatterType(%hit.matter);
        if (%matter.canPump && %hit.FluidCapacity > 0)
        {
			if (%client.MM_PickaxeLevel < %hit.getMiningLevel())
			{
				%burnText = %hit.armorDamage > 0 ? "\c0(" @ mRound(%hit.armorDamage * 100) @ "\% LVL REQ. BURNT)" : "";
				%client.MM_CenterPrint("You need to be atleast level\c3" SPC %hit.getMiningLevel() SPC "\c6to learn how to pump this<color:" @ getSubStr(%matter.color, 0, 6) @ ">" SPC %matter.name @ "\c6!<br>" @ %burnText, 2);
				return;
			}
			
			%hit.lastGatherTick = getSimTime();
			%obj.collectFluidLoop(%hit, %speed);
        }
    }
}

function Player::collectFluidLoop(%obj, %target, %speed)
{
	cancel(%obj.collectFluidSchedule);
	
    if (!isObject(%client = %obj.client) || !isObject(%matter = getMatterType(%target.matter)))
        return;

	if (%speed $= "")
		%speed = 1;

    %eye = %obj.getEyePoint();
    %dir = %obj.getEyeVector();
    %for = %obj.getForwardVector();
    %face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
    %mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
    %ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, 5)), %mask, %obj);
    if(isObject(%hit = firstWord(%ray)) && %hit.getClassName() $= "fxDtsBrick" && %hit == %target)
    {
		if (!%client.ChangeBatteryEnergy(mCeil(-2 * mPow(%speed, 1.1))))
		{
			%client.chatMessage("\c6You need power to use this!");
			%client.play2D(errorSound);
			return;
		}

		%PowerTickRate = 50;
		%target.gatherProcess += (getSimTime() - %hit.lastGatherTick) * %speed;

		%totalTime = %matter.health;
		if (%target.gatherProcess >= %totalTime)
		{
			%client.AddMaterial(1, %matter.name);
			ServerPlay3D(FluidPumpTickSound, %target.getPosition());
			%target.gatherProcess = 0;
			%target.FluidCapacity--;

			if (%target.FluidCapacity <= 0)
			{
				%target.delete();
				return;
			}
		}

		%client.MM_CenterPrint("\Sucking " @ %matter.name @ "... (" @ %target.FluidCapacity @ " unit(s) left)<br>\c6" @ mFloor((%target.gatherProcess / %totalTime) * 100) @ "% done.", 1);

		%hit.lastGatherTick = getSimTime();
		%obj.collectFluidSchedule = %obj.schedule(1000 / %PowerTickRate, "collectFluidLoop", %target, %speed);
	
    }
}