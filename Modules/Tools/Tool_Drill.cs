datablock AudioProfile(MMDrillEquipSound)
{
    filename    = "./Sounds/drill_equip.wav";
    description = AudioClosest3d;
    preload = true;
};

datablock AudioProfile(MMDrillStartSound)
{
    filename    = "./Sounds/drill_start.wav";
    description = AudioClosest3d;
    preload = true;
};

datablock AudioProfile(MMDrillEndSound)
{
    filename    = "./Sounds/drill_end.wav";
    description = AudioClosest3d;
    preload = true;
};

$MM::ItemCost["MMDrillPartsItem"] = "500\tCredits\t1\tMagicite\t24\tPlasteel";
$MM::ItemDisc["MMDrillPartsItem"] = "An unfinished shell of a drill. Combine with a pickaxe at an anvil to finish the shell.";
datablock itemData(MMDrillPartsItem)
{
	uiName = "Drill Parts";
	//iconName = "./Shapes/";
	doColorShift = true;
	colorShiftColor = "0.471 0.471 0.471 0.800";
	
	shapeFile = "./Shapes/Drill.dts";
	image = "";
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMPickaxeT1Item"] = MMDrillT1Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMTunnelerT1Item"] = MMDrillT1Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMExcavatorT1Item"] = MMDrillT1Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMMaceratorT1Item"] = MMDrillT1Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMSmasherT1Item"] = MMDrillT1Item;
$MM::ItemDisc["MMDrillT1Item"] = "A highly configurable fuel-based drill, whose complexity can be increased with drillkits. Has a max Complexity Level of 9.";
datablock itemData(MMDrillT1Item)
{
	uiName = "Basic Drill";
	//iconName = "./Shapes/";
	doColorShift = true;
	colorShiftColor = "0.471 0.471 0.471 1.000";
	
	shapeFile = "./Shapes/Drill.dts";
	image = MMDrillT1Image;
	canDrop = true;
	
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	category = "Tools";
};

datablock shapeBaseImageData(MMDrillT1Image)
{
	shapeFile = "./Shapes/Drill.dts";
	item = MMDrillT1Item;
	
	mountPoint = 0;
	offset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	
	eyeOffset = "";
	eyeRotation = "";
	
	correctMuzzleVector = true;
	className = "WeaponImage";
	
	melee = false;
	armReady = true;

	doColorShift = MMDrillT1Item.doColorShift;
	colorShiftColor = MMDrillT1Item.colorShiftColor;

    drillComplexity = 9;
	
	stateName[0]					= "Start";
	stateTimeoutValue[0]			= 0.5;
	stateTransitionOnTimeout[0]	 	= "Ready";
	stateSound[0]					= MMDrillEquipSound;
	
	stateName[1]					= "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]		= true;
	
	stateName[2]					= "Fire";
	stateScript[2]					= "onFire";
	stateTimeoutValue[2]			= 0.5;
	stateAllowImageChange[2]		= false;
	stateTransitionOnTimeout[2]		= "checkFire";
	
	stateName[3]					= "checkFire";
	stateTransitionOnTriggerUp[3] 	= "Ready";
};

function MMDrillT1Image::onMount(%this,%obj,%slot) { %obj.PrintDrillStats(%this.drillComplexity); }

function MMDrillT1Image::onFire(%this,%obj,%slot) { %obj.CreateDrill(%this.drillComplexity); }

$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMPickaxeT2Item"] = MMDrillT2Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMTunnelerT2Item"] = MMDrillT2Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMExcavatorT2Item"] = MMDrillT2Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMMaceratorT2Item"] = MMDrillT2Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMSmasherT2Item"] = MMDrillT2Item;
$MM::ItemDisc["MMDrillT2Item"] = "A highly configurable fuel-based drill, whose complexity can be increased with drillkits. Has a max Complexity Level of 15.";
datablock itemData(MMDrillT2Item : MMDrillT1Item)
{
	uiName = "Imrpoved Drill";
	colorShiftColor = "1.000 1.000 1.000 1.000";
	image = MMDrillT2Image;
};

datablock shapeBaseImageData(MMDrillT2Image : MMDrillT1Image)
{
	item = MMDrillT2Item;
	doColorShift = MMDrillT2Item.doColorShift;
	colorShiftColor = MMDrillT2Item.colorShiftColor;

    drillComplexity = 15;
};

function MMDrillT2Image::onMount(%this,%obj,%slot) { %obj.PrintDrillStats(%this.drillComplexity); }

function MMDrillT2Image::onFire(%this,%obj,%slot) { %obj.CreateDrill(%this.drillComplexity); }

$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMPickaxeT3Item"] = MMDrillT3Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMTunnelerT3Item"] = MMDrillT3Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMExcavatorT3Item"] = MMDrillT3Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMMaceratorT3Item"] = MMDrillT3Item;
$MM::ToolCraftingRecipe["MMDrillPartsItem", "MMSmasherT3Item"] = MMDrillT3Item;
$MM::ItemDisc["MMDrillT3Item"] = "A highly configurable fuel-based drill, whose complexity can be increased with drillkits. Has a max Complexity Level of 21.";
datablock itemData(MMDrillT3Item : MMDrillT1Item)
{
	uiName = "Superior Drill";
	colorShiftColor = "0.121 0.337 0.549 1.000";
	image = MMDrillT3Image;
};

datablock shapeBaseImageData(MMDrillT3Image : MMDrillT1Image)
{
	item = MMDrillT3Item;
	doColorShift = MMDrillT3Item.doColorShift;
	colorShiftColor = MMDrillT3Item.colorShiftColor;

    drillComplexity = 21;
};

function MMDrillT3Image::onMount(%this,%obj,%slot) { %obj.PrintDrillStats(%this.drillComplexity); }

function MMDrillT3Image::onFire(%this,%obj,%slot) { %obj.CreateDrill(%this.drillComplexity); }

$MM::ItemCost["MMDrillDebugItem"] = "1\tInfinity";
$MM::ItemDisc["MMDrillDebugItem"] = "You shouldn't have this.";
datablock itemData(MMDrillDebugItem : MMDrillT1Item)
{
	uiName = "Debug Drill";
	colorShiftColor = "0.000 0.000 0.000 1.000";
	image = MMDrillDebugImage;
};

datablock shapeBaseImageData(MMDrillDebugImage : MMDrillT1Image)
{
	item = MMDrillDebugItem;
	doColorShift = MMDrillDebugItem.doColorShift;
	colorShiftColor = MMDrillDebugItem.colorShiftColor;

    drillComplexity = 1337;
};

function MMDrillDebugImage::onMount(%this,%obj,%slot) { %obj.PrintDrillStats(%this.drillComplexity); }

function MMDrillDebugImage::onFire(%this,%obj,%slot) { %obj.CreateDrill(%this.drillComplexity); }

registerOutputEvent("GameConnection", "RefineFuel", "", true);
$MM::RefineFuelRatio = 100;
function GameConnection::RefineFuel(%client)
{
    if (!isObject(%player = %client.player))
        return;

    %bsm = new ScriptObject()
	{
		superClass = "BSMObject";
        class = "MM_bsmRefineFuel";
        
		title = "Loading...";
		format = "arial:24" TAB "\c2" TAB "<div:1>\c6" TAB "<div:1>\c2" TAB "\c7";

		entry[0]  = "[Finish]" TAB "closeMenu";

        hideOnDeath = true;
        deleteOnFinish = true;

		entryCount = 1;

        shopPosition = %player.getPosition();
	};

    MissionCleanup.add(%bsm);

	%client.brickShiftMenuEnd();
	%client.brickShiftMenuStart(%bsm);

    %client.RFUpdateInterface();
    %client.SOICheckDistance(); //It is basically the same code
}

function GameConnection::RFUpdateInterface(%client)
{
    if (!isObject(%bsm = %client.brickShiftMenu) || %bsm.class !$= "MM_bsmRefineFuel")
        return;

    for (%i = 1; %i < %bsm.entryCount; %i++)
        %bsm.entry[%i] = "";

    %bsm.entryCount = 1;

    for (%i = 0; %i < MatterData.getCount(); %i++)
	{
		%matter = MatterData.getObject(%i);
        %count = %client.GetMaterial(%matter.name);
		if (%matter.fuelPotency <= 0)
			continue;

		%bsm.entry[%bsm.entryCount] = %matter.name SPC "x" @ %count @ "/" @ mCeil($MM::RefineFuelRatio / %matter.fuelPotency) TAB %matter.name;
		%bsm.entryCount++;
	}

    %bsm.title = "<font:tahoma:24>\c3Crude Oil: " @ %client.getMaterial("Crude Oil") @ " | Oil/Dirt Ratio: 1:" @ $MM::RefineFuelRatio;
}

function MM_bsmRefineFuel::onUserMove(%obj, %client, %id, %move, %val)
{
    %client.SOICheckDistance();
    
	if(%move == $BSM::PLT && %id !$= "closeMenu")
	{
		if (isObject(%matter = GetMatterType(%id)) && %matter.fuelPotency > 0 && %client.GetMaterial(%matter.name) > 0)
		{
            %cost = mCeil($MM::RefineFuelRatio / %matter.fuelPotency);

            if (%client.getMaterial(%matter.name) < %cost)
            {
                %client.chatMessage("\c6You do not have enough " @ %matter.name @ "!");
                return;
            }
            else if (%client.getMaterial("Crude Oil") <= 0)
            {
                %client.chatMessage("\c6You need some Crude Oil!");
                return;
            }
            
            %output = 100;
            %client.SubtractMaterial(%cost, %matter.name);
            %client.SubtractMaterial(1, "Crude Oil");
            %client.AddMaterial(%output, "Drill Fuel");
            %client.chatMessage("\c6You refined " @ %cost SPC %matter.name @ " and 1 Crude Oil for " @ %output @ " Drill Fuel.");
            %client.RFUpdateInterface();
		}
        return;
	}

	Parent::onUserMove(%obj, %client, %id, %move, %val);
}

//Drill
datablock StaticShapeData(MMDrillStatic)
{
    shapeFile = "./Shapes/Drill.dts";
};

function MMDrillStatic::onAdd(%this, %obj)
{
    Parent::onAdd(%this, %obj);
}

function Player::GetDrillStats(%obj)
{
    if (isObject(%client = %obj.client))
        return %client.GetDrillStats();

    return "";
}

function GameConnection::GetDrillStats(%client)
{
    //Base Stats
    %cost = 2; //The amount of fuel consumed per tick.
    %health = 2; //Amount of hazards can be drilled through, minus 1, before the drill stops.
    %speed = 1; //The amount of blocks the drill travels per second.
    %range = 64; //The max range setting of the drill, in blocks.
    %radius = 1; //The AoE of the drill. A radius of 1 means a 3x3, 2 is 5x5, 3 is 7x7, etc.
    %efficiency = 1; //Divider for fuel consumption. Decimal values for final cost will randomly round up/down based on the decimal.
    %damaging = 0.0; //The multiplier for how much health will be removed from a revealed brick's max hp.
    %preserving = 0.0; //Chance for a drilled valued ore to be preserved instead of destroyed.
    %color = "1.0 1.0 1.0 1.0"; //Color of the drill.
    %complexity = 0; //Complexity of the upgrades. Drills have a max complexity level.
    for (%i = 0; %i < getFieldCount(%client.MM_Drillkits); %i++)
    {
        %field = getField(%client.MM_Drillkits, %i);
        switch$ (getWord(%field, 0))
        {
            case "Paint":
                //Color stuff
                %complexity += 1;
            case "Efficiency":
                %efficiency *= 1.25;
                %complexity += 4;
            case "Distance":
                %range *= 2;
                %complexity += 4;
            case "Scrapper":
                %damaging += 0.2;
                %cost += 2;
                %complexity += 4;
            case "Shield":
                %health *= 4;
                %cost += 2;
                %complexity += 4;
            case "Speed":
                %speed *= 1.25;
                %efficiency /= 1.1;
                %cost += 4;
                %complexity += 6;
            case "Ore":
                %preserving += 0.25;
                %range /= 2;
                %cost += 6;
                %complexity += 6;
            case "AoE":
                %radius += 1;
                %speed /= 1.25;
                %cost += 6;
                %complexity += 6;
        }
    }

    return %complexity TAB %cost TAB %health TAB %speed TAB %range TAB %radius TAB %efficiency TAB %damaging TAB %preserving;
}

function Player::PrintDrillStats(%obj, %complexity)
{
    if (!isObject(%client = %obj.client))
        return;

    %client.brickShiftMenuEnd();

    if (!%client.MM_WarnDrillControls)
	{
		%client.MM_WarnDrillControls = true;
		%client.chatMessage("\c6Use [\c3Primary Fire\c6] key to deploy the drill.");
        %client.chatMessage("\c6Use [\c3Cancel Brick\c6] key to cancel your deployed drill.");
        %client.chatMessage("\c6Use [\c3Plant Brick\c6] key to bring up the drillkit interface.");
	}

    if (%complexity $= "")
        %complexity = "---";

    %stats = %obj.GetDrillStats();
    %drillStat["Complexity"] = "\c6Complexity: " @ getField(%stats, 0) @ "/" @ %complexity;
    %drillStat["Cost"] = "\c6Fuel Cost: " @ mFloatLength(getField(%stats, 1) / getField(%stats, 6), 2) @ "/" @ %client.getMaterial("Drill Fuel");
    %drillStat["Health"] = "\c6Max HP: " @ getField(%stats, 2);
    %drillStat["TickRate"] = "\c6Travel Speed: " @ getField(%stats, 3) @ " Blocks";
    %drillStat["Range"] = "\c6Range: " @ getField(%stats, 4) @ " Blocks";
    %aoe = (getField(%stats, 5) * 2) + 1;
    %drillStat["AoE"] = "\c6AoE:" @ %aoe @ "x" @ %aoe;
    %drillStat["Damage"] = getField(%stats, 7);
    %drillStat["Ore"] = "\c6Ore Save Chance: " @ (getField(%stats, 8) * 100) @ "\%";

    %client.MM_CenterPrint("<just:right>" @ %drillStat["Complexity"] NL %drillStat["Cost"] NL %drillStat["Health"] NL %drillStat["TickRate"] NL %drillStat["Range"] NL %drillStat["AoE"] NL %drillStat["Ore"], 5);
}

function Player::CreateDrill(%obj, %target)
{
    if (!isObject(%client = %obj.client))
        return;

    if (%target $= "")
        %target = 9;

    %stats = %obj.GetDrillStats();

    %remainingComplexity = %target - getField(%stats, 0);
    if (%remainingComplexity < 0)
    {
        %client.chatMessage("\c6You are \c6" @ (%remainingComplexity * -1) @ "\c6 point(s) over the Complexity limit! Remove some drillkits by pressing [\c3Plant Brick\c6].");
        return;
    }

    %scanDist = 10;
    %eye = %obj.getEyePoint();
	%dir = %obj.getEyeVector();
	%for = %obj.getForwardVector();
	%face = getWords(vectorScale(getWords(%for, 0, 1), vectorLen(getWords(%dir, 0, 1))), 0, 1) SPC getWord(%dir, 2);
	%mask = $Typemasks::fxBrickAlwaysObjectType | $Typemasks::TerrainObjectType;
	%ray = containerRaycast(%eye, vectorAdd(%eye, vectorScale(%face, mClamp(%scanDist, 3, 100))), %mask, %obj);
    %pos = getWords(%ray, 1, 3);
	if(isObject(%hit = firstWord(%ray)) && %hit.getClassName() $= "fxDtsBrick" && %hit.canMine)
	{
		%hitpos = roundVector(vectorSub(interpolateVector(%pos, %hit.getPosition(), -0.1), "0 0 0.1"));
		%lookVec = %obj.FaceDirection();

        //createBoxMarker(%hitpos, "1 0 0 1", 0.5).schedule(2000, "delete");
        //drawArrow(%hitpos, %lookVec, "0 1 0 1", 1, "0 0 0").schedule(2000, "delete");

        if (isObject(%obj.DrillStatic))
            %obj.DrillStatic.DrillEnd("Cancelled.");
        %drill = new StaticShape()
        {
            datablock = MMDrillStatic;
            client = %obj.client;
            player = %obj;

            drillStat["Cost"] = mFloatLength(getField(%stats, 1) / getField(%stats, 6), 2);
            drillStat["Health"] = getField(%stats, 2);
            drillStat["TickRate"] = 1000 / getField(%stats, 3);
            drillStat["Range"] = getField(%stats, 4);
            drillStat["AoE"] = getField(%stats, 5);
            drillStat["Damage"] = getField(%stats, 7);
            drillStat["Ore"] = getField(%stats, 8);
        };
        %obj.DrillStatic = %drill;

        
        %drill.setTransform(%hitpos SPC getWords(%obj.getEyeTransform(), 3, 6));
        %drill.schedule(getMax((%drill.drillStat["TickRate"] * 2) - 700, 33), "playAudio", 1, MMDrillStartSound);
        %drill.DrillTickSchedule = %drill.schedule(%drill.drillStat["TickRate"] * 2, "DrillTick");
    }
}

function StaticShape::DrillTick(%obj)
{
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getForwardVector()), "1 0 0 1", 2, 1, "0 0 0").schedule(2000, "delete");
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getUpVector()), "0 1 0 1", 2, 1, "0 0 0").schedule(2000, "delete");
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getLeftVector()), "0 0 1 1", 2, 1, "0 0 0").schedule(2000, "delete");

    cancel(%obj.DrillTickSchedule);

    if (!isObject(%client = %obj.client) || !isObject(%player = %obj.player))
    {
        %obj.DrillEnd();
        return;
    }

    if (%client.GetMaterial("Drill Fuel") < %obj.drillStat["Cost"])
    {
        %obj.DrillEnd("Insufficent Fuel.");
        return;
    }
    

    %radius = %obj.drillStat["AoE"];
    for (%z = -2; %z <= 1; %z++)
    {
        for (%x = %radius * -1; %x <= %radius; %x++)
        {
            for (%y = %radius * -1; %y <= %radius; %y++)
            {
                %target = vectorAdd(roundVector(%obj.getPosition()), vectorScale(vectorNormalize(%obj.getForwardVector()), %z));
                %target = vectorAdd(%target, vectorScale(vectorNormalize(%obj.getLeftVector()), %x));
                %target = vectorAdd(%target, vectorScale(vectorNormalize(%obj.getUpVector()), %y));
                %target = roundVector(%target);

                RevealBlock(%target);
                if (isObject(%brick = $MM::BrickGrid[%target]))
                {
                    %blockhit = true;
                    
                    %matter = getMatterType(%brick.matter);

                    if (%obj.client.MM_PickaxeLevel < %matter.level)
                    {
                        if (%x == 0 && %y == 0)
                            %breakFail = true;

                        continue;
                    }

                    if (%matter.value > 0 && %obj.drillStat["Ore"] > 0 && (getRandom() < %obj.drillStat["Ore"] || %z < 1)) //Ore preservation proc
                        continue;

                    if (%matter.hitFunc $= "MM_HeatDamage" || %matter.harvestFunc $= "MM_HeatDamage" || %matter.hitFunc $= "MM_RadDamage" || %matter.harvestFunc $= "MM_RadDamage")
                    {
                        
                        if (%obj.drillLostIntegrity > %obj.drillStat["Health"])
                        {
                            if (!%obj.warnIntegrityLoss)
                            {
                                %obj.client.chatMessage("Integrity loss from drilling too many hazards. Ignoring future hazards.");
                                %obj.warnIntegrityLoss = true;
                            }
                            
                            continue;
                        }
                        else
                            %obj.drillLostIntegrity++;
                    }

                    %brick.MineDamage(999999); //TODO: Use a better value.
                }

                //Check to see if there is still a block in the way
                if (isObject(%brick) && %x == 0 && %y == 0)
                    %breakFail = true;
                    
                //drawArrow2(%obj.getPosition(), %target, "1 1 1 1", 0.5, "0 0 0").schedule(2000, "delete");
            }
        }
    }

    if (%blockhit)
    {
        %cost = (((%obj.drillStat["Cost"] - mFloor(%obj.drillStat["Cost"])) > getRandom()) ? mFloor(%obj.drillStat["Cost"]) + 1 : mFloor(%obj.drillStat["Cost"]));
        %client.SubtractMaterial(%cost, "Drill Fuel");
        %obj.stopAudio(0);
        %obj.playAudio(0, "MM_Drill" @ getRandom(1, $MM::SoundCount["Drill"]) @ "Sound");
    }

    if (%breakFail)
    {
        %obj.DrillEnd("Failed to break adjacent brick.");
        return;
    }

    %obj.drillDistance++;
    if (%obj.drillDistance >= %obj.drillStat["Range"])
    {
        %obj.DrillEnd("Reached maxinum distance.");
        return;
    }

    %layer = LayerData.getObject(0);
    %multiplier = 1.0;
	for (%i = 0; %i < LayerData.getCount(); %i++)
	{
		%testLayer = LayerData.getObject(%i);
		if (getWord(%obj.getPosition(), 2) <= ($MM::ZLayerOffset + %testLayer.startZ))
            %multiplier = (%testLayer.drillReduction > 0 ? %testLayer.drillReduction : 1.0);
	}

    %time = %obj.drillStat["TickRate"] * %multiplier;
    %obj.LerpMove((vectorAdd(%obj.getPosition(), vectorScale(%obj.getForwardVector(), -1))), %time, 30);
    %obj.DrillTickSchedule = %obj.schedule(%time + 10, "DrillTick");
}

function StaticShape::DrillEnd(%obj, %reason)
{
    %obj.playAudio(1, MMDrillEndSound);
    cancel(%obj.DrillTickSchedule);
    cancel(%obj.LerpMoveSchedule);

    if (isObject(%obj.client))
        %obj.client.chatMessage("\c6Drill finished. Reason: " @ %reason);
    %obj.schedule(1000, "delete");
}

function StaticShape::LerpMove(%obj, %target, %time, %ticks)
{
    if (%ticks < 1)
        %ticks = 10;

    while (%time / %ticks < 33 && %ticks > 1) //Yucky inefficent. Is there just one formula we can use? We have to do this since the minimal time for a schedule is 33 ish ms.
        %ticks--;

    %time /= %ticks;

    %move = vectorScale(vectorSub(%obj.getPosition(), %target), 1 / %ticks);
    %obj.LerpMoveTick(%move, %time, %ticks - 1);
}

function StaticShape::LerpMoveTick(%obj, %move, %time, %movesLeft)
{
    cancel(%obj.LerpMoveSchedule);

    %pos = %obj.getPosition();
    %obj.setTransform(vectorAdd(getWords(%pos, 0, 2), %move) SPC getWords(%pos, 3));

    if (%movesLeft > 0)
        %obj.LerpMoveSchedule = %obj.schedule(%time, "LerpMoveTick", %move, %time, %movesLeft - 1);
}

//$test.setTransform(vectorAdd($test.getPosition(), $test.faceDirection()) SPC getWords($test.getTransform(),3,7));
//$Test.setTransform(vectorAdd($test.getPosition(),$test.getForwardVector()) SPC getWords($test.getTransform(),3,7));
//$test.setTransform("0 0 5121" SPC getWords(MatrixCreateFromEuler("0 0 1.5708"), 3, 7)); $test.drillTick();
//$test.drillTick(); $test.setTransform(vectorAdd($test.getPosition(),$test.getForwardVector()) SPC getWords(MatrixCreateFromEuler("0 0 0"), 3, 7));
//$test = new StaticShape() {datablock = MMDrillStatic; };

package MM_DrillSupport
{
	function serverCmdPlantBrick(%client, %idx)
	{
		if(isObject(%player = %client.player) && isObject(%image = %player.getMountedImage(0)) && %image.drillComplexity > 0)
		{
            %bsm = new ScriptObject()
            {
                superClass = "BSMObject";
                class = "MM_bsmDrillkits";
                
                title = "Loading...";
                format = "arial:24" TAB "\c2" TAB "\c6" TAB "\c2" TAB "\c7";

                entryCount = 0;

                hideOnDeath = true;
                deleteOnFinish = true;
            };

            if (!%client.MM_WarnDrillkitControls)
            {
                %client.MM_WarnDrillkitControls = true;
                %client.chatMessage("\c6Use [\c3Brick Shift Away/Towards\c6] keys to scroll the drillkit menu.");
                %client.chatMessage("\c6Use [\c3Plant Brick\c6] to remove the selected drillkit.");
            }

            MissionCleanup.add(%bsm);

            %client.brickShiftMenuEnd();
            %client.brickShiftMenuStart(%bsm);

            %client.DrillkitsUpdateInterface();

            %player.unMountImage(0);
			return;
		}
		
		Parent::serverCmdPlantBrick(%client, %idx);
	}

	function serverCmdCancelBrick(%client, %idx)
	{
		if(isObject(%player = %client.player) && isObject(%image = %player.getMountedImage(0)) && %image.drillComplexity > 0 && isObject(%player.DrillStatic))
		{
            %player.DrillStatic.DrillEnd("Cancelled.");
			return;
		}
		
		Parent::serverCmdCancelBrick(%client, %idx);
	}
};
activatePackage("MM_DrillSupport");

function GameConnection::DrillkitsUpdateInterface(%client)
{
    if (!isObject(%bsm = %client.brickShiftMenu) || %bsm.class !$= "MM_bsmDrillkits")
        return;

    for (%i = 0; %i < %bsm.entryCount; %i++)
        %bsm.entry[%i] = "";

    %bsm.entryCount = 0;
    %bsm.entry[%bsm.entryCount] = "[Finish]" TAB "closeMenu";
    %bsm.entryCount++;

    for (%i = 0; %i < GetFieldCount(%client.MM_Drillkits); %i++)
	{
		%kit = getField(%client.MM_Drillkits, %i);

		%bsm.entry[%bsm.entryCount] = %kit TAB %kit;
		%bsm.entryCount++;
	}

    %bsm.title = "<font:tahoma:16>\c3Complexity: " @ getField(%client.GetDrillStats(), 0);
}

function MM_bsmDrillkits::onUserMove(%obj, %client, %id, %move, %val)
{
	if(%move == $BSM::PLT && %id !$= "closeMenu" && isObject(%player = %client.player))
	{
        %kitIdx = getFieldFromValue(%client.MM_Drillkits, %id);
        if (%kitIdx >= 0 && isObject(%tool = ("MMDrillKit" @ %id @ "Item")))
        {
            %client.MM_Drillkits = removeField(%client.MM_Drillkits, %kitIdx);
            %client.chatMessage("\c6You removed the \c3" @ %id @ " \c6DrillKit to your drills!");

            %item = new Item()
            {
                datablock = %tool;
                static    = "0";
                position  = vectorAdd(%player.getPosition(), "0 0 1");
                craftedItem = true;
            };

            %client.DrillkitsUpdateInterface();
        }
        return;
	}

	Parent::onUserMove(%obj, %client, %id, %move, %val);
}