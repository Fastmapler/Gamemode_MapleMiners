$MM::ModuleTickRate = 10;

function Player::ModuleTick(%player)
{
    cancel(%player.ModuleTickSchedule);

    if (!isObject(%client = %player.client) || getFieldCount(%player.MM_ActivatedModules) < 1)
        return;

    %mods = %player.MM_ActivatedModules;
    for (%i = 0; %i < getFieldCount(%mods); %i++)
    {
        %funcName = "MM_Module" @ getField(%mods, %i);
        if (!isFunction(%funcName))
            continue;

        %ret = call(%funcName, %player);

        if (!%ret)
        {
            %player.MM_ActivatedModules = "";
            %client.chatMessage("You ran out of power!");
            %player.playAudio(0, MMModuleOffSound);
            break;
        }
    }

    %player.ModuleTickSchedule = %player.schedule(1000 / $MM::ModuleTickRate, "ModuleTick");
}

function Player::ToggleModule(%player, %mod)
{
    if (!isObject(%client = %player.client))
        return;

    if (!%client.ChangeBatteryEnergy(-10))
    {
        %client.chatMessage("You have no power!");
        %player.playAudio(0, errorSound);
        return;
    }
    %player.playThread(0, "plant");
    if (hasField(%player.MM_ActivatedModules, %mod))
    {
        %player.MM_ActivatedModules = removeFieldText(%player.MM_ActivatedModules, %mod);
        %client.chatMessage("\c6Module [\c3" @ %mod @ "\c6] is now \c0OFF");
        %player.playAudio(0, MMModuleOffSound);
    }
    else
    {
        %player.MM_ActivatedModules = trim(%player.MM_ActivatedModules TAB %mod);
        %client.chatMessage("\c6Module [\c3" @ %mod @ "\c6] is now \c2ON");
        %player.playAudio(0, MMModuleOnSound);

        if (!isEventPending(%player.ModuleTickSchedule))
            %player.ModuleTick();
    }
}

datablock AudioProfile(MMModuleOnSound)
{
    filename    = "./Sounds/module_on.wav";
    description = AudioClosest3d;
    preload = true;
};

datablock AudioProfile(MMModuleOffSound)
{
    filename    = "./Sounds/module_off.wav";
    description = AudioClosest3d;
    preload = true;
};

$MM::ItemCost["MMModuleHeatShieldItem"] = "2000\tCredits\t10\tMagma\t5\tGarnet\t5\tGraphite\t5\tNickel";
datablock itemData(MMModuleHeatShieldItem)
{
	uiName = "Module - Heat Shield";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "1.00 0.00 0.00 1.00";
	
	shapeFile = "./Shapes/Module.dts";
	image = MMModuleHeatShieldImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(MMModuleHeatShieldImage)
{
	shapeFile = "./Shapes/Module.dts";
	item = MMModuleHeatShieldItem;
	
	mountPoint = 0;
	offset = "0 0.5 0";
	rotation = 0;
	
	eyeOffset = "";
	eyeRotation = "";
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = MMModuleHeatShieldItem.doColorShift;
	colorShiftColor = MMModuleHeatShieldItem.colorShiftColor;
	
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

function MMModuleHeatShieldImage::onFire(%this, %obj, %slot) { %obj.ToggleModule("HeatShield"); }

function MM_ModuleHeatShield(%player)
{
    return %player.client.ChangeBatteryEnergy($MM::MaxBatteryCharge / (-100 * $MM::ModuleTickRate));
}

$MM::ItemCost["MMModuleRadShieldItem"] = "8000\tCredits\t10\tRadioactive Waste\t5\tTungsten\t5\tOsmium\t5\tUranium";
datablock itemData(MMModuleRadShieldItem : MMModuleHeatShieldItem)
{
	uiName = "Module - Radiation Shield";
	colorShiftColor = "0.00 1.00 0.00 1.00";
	image = MMModuleRadShieldImage;
};

datablock shapeBaseImageData(MMModuleRadShieldImage : MMModuleHeatShieldImage)
{
	item = MMModuleRadShieldItem;
	doColorShift = MMModuleRadShieldItem.doColorShift;
	colorShiftColor = MMModuleRadShieldItem.colorShiftColor;
};

function MMModuleRadShieldImage::onFire(%this, %obj, %slot) { %obj.ToggleModule("RadShield"); }

function MM_ModuleRadShield(%player)
{
    return %player.client.ChangeBatteryEnergy($MM::MaxBatteryCharge / (-33 * $MM::ModuleTickRate));
}

datablock AudioProfile(MMJetThrustSound)
{
    filename    = "./Sounds/thrust_start.wav";
    description = AudioClosest3d;
    preload = true;
};

$MM::ItemCost["MMModuleJetStablizersItem"] = "1000\tCredits\t5\tCobalt\t10\tTin\t15\tCopper\t10\tIron";
datablock itemData(MMModuleJetStablizersItem : MMModuleHeatShieldItem)
{
	uiName = "Module - Jet Stablizers";
	colorShiftColor = "1.00 0.50 0.00 1.00";
	image = MMModuleJetStablizersImage;
};

datablock shapeBaseImageData(MMModuleJetStablizersImage : MMModuleHeatShieldImage)
{
	item = MMModuleJetStablizersItem;
	doColorShift = MMModuleJetStablizersItem.doColorShift;
	colorShiftColor = MMModuleJetStablizersItem.colorShiftColor;
};

function MMModuleJetStablizersImage::onFire(%this, %obj, %slot) { %obj.ToggleModule("JetStablizers"); }

function MM_ModuleJetStablizers(%player)
{
    return %player.client.ChangeBatteryEnergy($MM::MaxBatteryCharge / (-200 * $MM::ModuleTickRate));
}