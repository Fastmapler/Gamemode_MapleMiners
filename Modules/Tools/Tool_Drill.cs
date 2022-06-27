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
