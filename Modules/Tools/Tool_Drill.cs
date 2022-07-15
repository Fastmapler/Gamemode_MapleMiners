$MM::ItemCost["MMDrillT1Item"] = "1\tInfinity";
$MM::ItemDisc["MMDrillT1Item"] = "A highly configurable fuel-based drill, whose complexity can be increased with drillkits. Has a max Complexity Level of 9.";
datablock itemData(MMDrillT1Item)
{
	uiName = "Basic Drill";
	//iconName = "./Shapes/";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
	
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

function MMDrillT1Image::onFire(%this,%obj,%slot)
{
    %obj.CreateDrill(9);
}

registerOutputEvent("GameConnection", "RefineFuel", "", true);
$MM::RefineFuelRatio = 50;
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
    if (!isObject(%client = %obj.client))
        return;

    //Base Stats
    %cost = 9; //The amount of fuel consumed per tick.
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
                %health *= 2;
                %cost += 2;
                %complexity += 4;
            case "Speed":
                %speed *= 1.25;
                %efficiency /= 1.25;
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
                %cost += 4;
                %complexity += 6;
        }
    }

    return %complexity TAB %cost TAB %health TAB %speed TAB %range TAB %radius TAB %efficiency TAB %damaging TAB %preserving;
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
        %client.chatMessage("\c6You are \c6" @ (%remainingComplexity * -1) @ "\c6 point(s) over the Complexity limit! Remove some drillkits by pressing [\c3Cancel Brick\c6].");
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

        createBoxMarker(%hitpos, "1 0 0 1", 0.5).schedule(2000, "delete");
        drawArrow(%hitpos, %lookVec, "0 1 0 1", 1, "0 0 0").schedule(2000, "delete");
        return;

        %drill = new StaticShape()
        {
            datablock = MMDrillStatic;
            client = %obj.client;

            drillStat["Cost"] = mRound(mClamp(getField(%stats, 1) / getField(%stats, 6), 1, 500));
            drillStat["Health"] = mClamp(getField(%stats, 2), 1, 100);
            drillStat["TickRate"] = 1000 / mClamp(getField(%stats, 3), 0.1, 5);
            drillStat["Range"] = mClamp(getField(%stats, 4), 4, 65536);
            drillStat["AoE"] = mClamp(getField(%stats, 5), 0, 7);
            drillStat["Damage"] = mClamp(getField(%stats, 6), 0, 0.8);
            drillStat["Ore"] = mClamp(getField(%stats, 7), 0, 1.0);
        };
        %obj.DrillStatic = %drill;

        %drill.setTransform(%hitpos SPC "0 0 0 1"); //TODO: Actually get target position and direction
        %obj.DrillTickSchedule = %obj.schedule(%obj.drillStat["TickRate"] * 2, "DrillTick");
    }
}

function StaticShape::DrillTick(%obj)
{
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getForwardVector()), "1 0 0 1", 2, 1, "0 0 0").schedule(2000, "delete");
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getUpVector()), "0 1 0 1", 2, 1, "0 0 0").schedule(2000, "delete");
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getLeftVector()), "0 0 1 1", 2, 1, "0 0 0").schedule(2000, "delete");

    cancel(%obj.DrillTickSchedule);

    if (!isObject(%client = %obj.client))
    {
        %obj.DrillEnd();
        return;
    }

    if (%client.GetMaterial("Drill Fuel") < %obj.drillStat["Cost"])
    {
        %obj.DrillEnd("Insufficent Fuel.");
        return;
    }
    %client.SubtractMaterial(%obj.drillStat["Cost"], "Drill Fuel");

    %radius = %obj.drillStat["AoE"];
    for (%x = %radius * -1; %x <= %radius; %x++)
    {
        for (%y = %radius * -1; %y <= %radius; %y++)
        {
            %target = vectorAdd(%obj.getPosition(), vectorNormalize(%obj.getForwardVector()));
            %target = vectorAdd(%target, vectorScale(vectorNormalize(%obj.getLeftVector()), %x));
            %target = vectorAdd(%target, vectorScale(vectorNormalize(%obj.getUpVector()), %y));
            %target = roundVector(%target);

            RevealBlock(%target);
			if (isObject(%brick = $MM::BrickGrid[%target]))
            {
                %matter = getMatterType(%brick.matter);

                if (%matter.value > 0 && getRandom() < %obj.drillStat["Ore"]) //Ore preservation proc
                    continue;

                %brick.MineDamage(999999); //TODO: Use a better value.
                if (%matter.hitFunc $= "MM_HeatDamage" || %matter.harvestFunc $= "MM_HeatDamage" || %matter.hitFunc $= "MM_RadDamage" || %matter.harvestFunc $= "MM_RadDamage")
                { //Hazard Drilling
                    %obj.drillLostIntegrity++;
                    if (%obj.drillLostIntegrity > %obj.drillStat["Health"])
                    {
                        %obj.DrillEnd("Integrity loss from drilling too many hazards.");
                        return;
                    }
                }
            }

            //Check to see if there is still a block in the way
            if (isObject(%brick) && %x == 0 && %y == 0)
            {
                %obj.DrillEnd("Failed to break adjacent brick.");
                return;
            }
                
            //drawArrow2(%obj.getPosition(), %target, "1 1 1 1", 0.5, "0 0 0").schedule(2000, "delete");
        }
    }

    %obj.drillDistance++;

    if (%obj.drillDistance >= %obj.drillStat["Range"])
    {
        %obj.DrillEnd("Reached maxinum distance.");
        return;
    }

    %time = %obj.drillStat["TickRate"];
    %obj.LerpMove(vectorAdd(%obj.getPosition(), vectorScale(%obj.getForwardVector(), -1)), %time, 30);
    %obj.DrillTickSchedule = %obj.schedule(%time + 10, "DrillTick");
}

function StaticShape::DrillEnd(%obj, %reason)
{
    talk("Drill finished. Reason: " @ %reason);
    %obj.delete();
}

function StaticShape::LerpMove(%obj, %target, %time, %ticks)
{
    if (%ticks < 1)
        %ticks = 10;

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