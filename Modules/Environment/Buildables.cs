//X Y Z \t Hull Material \t amt1 \t type1 \t amt2 \t type2 etc...
$MM::Buildables["MM_Refinery"] = "4 4 2\tPlaSteel\t4\tFrame Parts\t2\tMechanism Parts";

function MM_CheckBuildArea(%pos, %type)
{
    %pos = roundVector(%pos);
    %data = $MM::Buildables[%type];
    if (%data $= "")
        return false;

    for (%i = 1; %i < getFieldCount(%data); %i += 2)
    {
        %materials = trim(%materials TAB getField(%data, %i));

        if (%i == 1)
            %materialCost = trim(%materialCost TAB -1);
        else
            %materialCost = trim(%materialCost TAB getField(%data, %i - 1));
    }
        

    //First, find our bounds
    %startPos = %pos;
    %endPos = %pos;

    //x test
    %testPos = %pos;
    while (isObject(%brick = $MM::BrickGrid[%testPos = vectorAdd(%testPos, "1 0 0")]) && hasField(%materials, getMatterType(%brick.matter).name)) %startPos = vectorAdd(%startPos, "1 0 0");
    %testPos = %pos;
    while (isObject(%brick = $MM::BrickGrid[%testPos = vectorAdd(%testPos, "-1 0 0")]) && hasField(%materials, getMatterType(%brick.matter).name)) %endPos = vectorAdd(%endPos, "-1 0 0");

    //y test
    %testPos = %pos;
    while (isObject(%brick = $MM::BrickGrid[%testPos = vectorAdd(%testPos, "0 1 0")]) && hasField(%materials, getMatterType(%brick.matter).name)) %startPos = vectorAdd(%startPos, "0 1 0");
    %testPos = %pos;
    while (isObject(%brick = $MM::BrickGrid[%testPos = vectorAdd(%testPos, "0 -1 0")]) && hasField(%materials, getMatterType(%brick.matter).name)) %endPos = vectorAdd(%endPos, "0 -1 0");

    //z test
    %testPos = %pos;
    while (isObject(%brick = $MM::BrickGrid[%testPos = vectorAdd(%testPos, "0 0 1")]) && hasField(%materials, getMatterType(%brick.matter).name)) %startPos = vectorAdd(%startPos, "0 0 1");
    %testPos = %pos;
    while (isObject(%brick = $MM::BrickGrid[%testPos = vectorAdd(%testPos, "0 0 -1")]) && hasField(%materials, getMatterType(%brick.matter).name)) %endPos = vectorAdd(%endPos, "0 0 -1");

    //Get total area and verify area fits with req
    %buildArea = vectorAdd(vectorSub(%startPos, %endPos), "1 1 1");
    if (%buildArea !$= getField(%data, 0))
        return 0 TAB "Incorrect cube dimensions! Found " @ %buildArea @ ", need " @ getField(%data, 0) @ "."; 

    //Scan the area for the needed bricks
    for (%x = getWord(%endPos, 0); %x <= getWord(%startPos, 0); %x++)
    {
        for (%y = getWord(%endPos, 1); %y <= getWord(%startPos, 1); %y++)
        {
            for (%z = getWord(%endPos, 2); %z <= getWord(%startPos, 2); %z++)
            {
                %testPos = %x SPC %y SPC %z;

                if (isObject(%brick = $MM::BrickGrid[%testPos]))
                {
                    %matter = getMatterType(%brick.matter);
                    if (hasField(%materials, %matter.name))
                    {
                        %foundMatter[%matter.name]++;
                    }
                    else
                    {
                        return 0 TAB "Incorrect brick found! Located " @ %matter.name @ " at " @ %testPos @ "."; 
                    }
                }
                else
                {
                    return 0 TAB "Open air pocket found! Located at " @ %testPos @ "."; 
                }
            }
        }
    }

    for (%i = 1; %i < getFieldCount(%materials); %i++)
    {
        %foundCount = %foundMatter[getField(%materials, %i)] + 0;
        %reqCount = getField(%materialCost, %i);
        if (%foundCount > %reqCount)
            return 0 TAB "Found " @ (%foundCount - %reqCount) @ " too many " @ getField(%materials, %i) @ "!";
        else if (%foundCount < %reqCount)
            return 0 TAB "Need " @ (%reqCount - %foundCount) @ " more " @ getField(%materials, %i) @ "!";
    }
        

    for (%x = getWord(%endPos, 0); %x <= getWord(%startPos, 0); %x++)
        for (%y = getWord(%endPos, 1); %y <= getWord(%startPos, 1); %y++)
            for (%z = getWord(%endPos, 2); %z <= getWord(%startPos, 2); %z++)
                if (isObject(%brick = $MM::BrickGrid[%x SPC %y SPC %z]))
                    %brick.delete();

    MM_LoadStructure(%type, interpolateVector(%startPos, %endPos, 0.5));

    return 1 TAB "Success";
}