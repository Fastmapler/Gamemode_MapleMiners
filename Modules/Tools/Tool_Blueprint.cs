$MM::ItemCost["BlueprintItem"] = "1000\tCredits";
$MM::ItemDisc["BlueprintItem"] = "Allows the assembly of various buildable structures.";
datablock itemData(BlueprintItem)
{
	uiName = "Blueprint";
	iconName = "./Shapes/icon_Blueprint";
	doColorShift = true;
	colorShiftColor = "0.70 0.70 0.70 1.00";
	
	shapeFile = "./Shapes/Blueprint.dts";
	image = BlueprintImage;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(BlueprintImage)
{
	shapeFile = "./Shapes/Blueprint.dts";
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
	stateTimeoutValue[2]			= 1.0;
	stateAllowImageChange[2]		= false;
	stateTransitionOnTimeout[2]		= "checkFire";
	
	stateName[3]					= "checkFire";
	stateTransitionOnTriggerUp[3] 	= "Ready";
};

function BlueprintImage::onMount(%this,%obj,%slot)
{
	if (%obj.blueprintDesign $= "")
		%obj.blueprintDesign = "MM_Recycler";

    %obj.DisplayBlueprint(%obj.blueprintDesign);
}

function BlueprintImage::onFire(%this, %obj, %slot)
{
	if (!isObject(%client = %obj.client) || $MM::Buildables[%obj.blueprintDesign] $= "")
		return;

	%eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, 10)), %mask, %obj);
	if(isObject(%hit = firstWord(%ray)))
		%client.chatMessage(MM_CheckBuildArea(%hit.getPosition(), %obj.blueprintDesign));
}

function Player::DisplayBlueprint(%obj, %type)
{
    if (!isObject(%client = %obj.client) || $MM::Buildables[%obj.blueprintDesign] $= "")
        return;
	%recipe = $MM::Buildables[%obj.blueprintDesign];
	%design = "\c6Design: \c3" @ getField(%recipe, 0);
	%baseCost = "\c6Requires... A " @ strReplace(getField(%recipe, 1), " ", "x") SPC getField(%recipe, 2) @ (getFieldCount(%recipe) > 3 ? " cube containing..." : " cube.");
	for (%i = 4; %i < getFieldCount(%recipe); %i += 2)
		%cost[%i/2 - 2] = "\c6" @ getField(%recipe, %i - 1) SPC getField(%recipe, %i);

    %client.MM_CenterPrint("<just:right>" @ %design NL %baseCost NL %cost[0] NL %cost[1] NL %cost[2] NL %cost[3] NL %cost[4], 10);
}

$MM::BuildableStructures = "MM_Recycler\tMM_Refinery\tMM_TelePad\tMM_Artillery\tMM_FleshPortal";
package MM_Blueprint
{
	function serverCmdRotateBrick(%client, %dir)
	{
		if(isObject(%player = %client.player) && isObject(%image = %player.getMountedImage(0)) && %image.getID() == BlueprintImage.getID())
		{
			%idx = getFieldFromValue($MM::BuildableStructures, %player.blueprintDesign);
			%newIdx = %idx + %dir;
			if (%newIdx < getFieldCount($MM::BuildableStructures) && %newIdx >= 0)
			{
				%player.blueprintDesign = getField($MM::BuildableStructures, %newIdx);
				%player.DisplayBlueprint(%player.placerMatter);
			}
			else
				return;
		}

		Parent::serverCmdRotateBrick(%client, %dir);
	}
};
activatePackage("MM_Blueprint");