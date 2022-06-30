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
                %client.chatMessage("\c6You do not have enough" @ %matter.name @ "!");
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

function StaticShape::DrillTick(%obj)
{
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getForwardVector()), "1 0 0 1", 2, 1, "0 0 0").schedule(2000, "delete");
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getUpVector()), "0 1 0 1", 2, 1, "0 0 0").schedule(2000, "delete");
    //drawArrow(%obj.getPosition(), vectorNormalize(%obj.getLeftVector()), "0 0 1 1", 2, 1, "0 0 0").schedule(2000, "delete");

    %radius = 2;
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
				%brick.MineDamage(999999);
                
            //drawArrow2(%obj.getPosition(), %target, "1 1 1 1", 0.5, "0 0 0").schedule(2000, "delete");
        }
    }
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