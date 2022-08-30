//X Y Z \t Hull Material \t amt1 \t type1 \t amt2 \t type2 etc...
$MM::Buildables["MM_Recycler"] = "Cache Recycler\t2 2 3\tPlaSteel";
$MM::Buildables["MM_Refinery"] = "Oil Refinery\t4 4 2\tPlaSteel\t4\tFrame Parts\t2\tMechanism Parts";
$MM::Buildables["MM_TelePad"] = "Warp Pad\t4 4 2\tPlaSteel\t2\tFrame Parts\t2\tCircuitry Parts\t1\tComputation Parts";
$MM::Buildables["MM_Artillery"] = "Artillery Platform\t8 8 2\tPlaSteel\t16\tFrame Parts\t16\tMechanism Parts\t8\tCircuitry Parts\t4\tComputation Parts";


function MM_CheckBuildArea(%pos, %type)
{
    %pos = roundVector(%pos);
    %data = $MM::Buildables[%type];
    if (%data $= "")
        return "No recipe found.";

    for (%i = 2; %i < getFieldCount(%data); %i += 2)
    {
        %materials = trim(%materials TAB getField(%data, %i));

        if (%i == 2)
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
    if (%buildArea !$= getField(%data, 1))
        return "Incorrect cube dimensions! Found " @ %buildArea @ ", need " @ getField(%data, 1) @ "."; 

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
                        return "Incorrect brick found! Located " @ %matter.name @ " at " @ %testPos @ "."; 
                    }
                }
                else
                {
                    return "Open air pocket found! Located at " @ %testPos @ "."; 
                }
            }
        }
    }

    for (%i = 1; %i < getFieldCount(%materials); %i++)
    {
        %foundCount = %foundMatter[getField(%materials, %i)] + 0;
        %reqCount = getField(%materialCost, %i);
        if (%foundCount > %reqCount)
            return "Found " @ (%foundCount - %reqCount) @ " too many " @ getField(%materials, %i) @ "!";
        else if (%foundCount < %reqCount)
            return "Need " @ (%reqCount - %foundCount) @ " more " @ getField(%materials, %i) @ "!";
    }
        

    for (%x = getWord(%endPos, 0); %x <= getWord(%startPos, 0); %x++)
        for (%y = getWord(%endPos, 1); %y <= getWord(%startPos, 1); %y++)
            for (%z = getWord(%endPos, 2); %z <= getWord(%startPos, 2); %z++)
                if (isObject(%brick = $MM::BrickGrid[%x SPC %y SPC %z]))
                    %brick.delete();

    MM_LoadStructure(%type, interpolateVector(%startPos, %endPos, 0.5));

    return "Success";
}


//Structure special events

//Warp Pad
datablock AudioProfile(MMWarpPadWooshSound)
{
    filename    = "./Sounds/teleport_whoosh.wav";
    description = AudioClosest3d;
    preload = true;
};

datablock StaticShapeData(WarpBeamStatic) { shapeFile = "./Shapes/bullettrail.dts"; };
function spawnBeam(%startpos,%endpos,%size)
{
	%p = new StaticShape() { dataBlock = WarpBeamStatic; };
	MissionCleanup.add(%p);
	
	%vel = vectorNormalize(vectorSub(%startpos,%endpos));
	%x = getWord(%vel,0)/2;
	%y = (getWord(%vel,1) + 1)/2;
	%z = getWord(%vel,2)/2;
	%p.setTransform(%endpos SPC VectorNormalize(%x SPC %y SPC %z) SPC mDegToRad(180));
	%p.setScale(%size SPC vectorDist(%startpos,%endpos) SPC %size);
}
function WarpBeamStatic::onAdd(%this,%obj)
{
	%obj.playThread(0,root);
	%obj.schedule(100,delete);
}

registerOutputEvent("fxDTSBrick", "TelepadWarp", "", true);
function fxDTSBrick::TelepadWarp(%brick, %client)
{
    if (!isObject(%player = %client.player))
        return;

    if (%brick.TelepadTarget $= "")
    {
        %client.chatMessage("\c6No target location setup! Press [\c3Cancel Brick\c6] while holding the PDA to save your transform.");
        %client.chatMessage("\c6Then, press [\c3Mouse Fire\c6] at the Warp Pad's yellow pad to set it's destination.");
        return;
    }

    initContainerBoxSearch(vectorAdd(%brick.TelepadTarget, "0 0 4"), "4 4 4", $TypeMasks::FxBrickAlwaysObjectType );
	while (%hit = containerSearchNext())
        if (isObject(%hit))
            %fail = true;

    if (%fail)
    {
        %client.chatMessage("\c6Target area must be fully clear of obstructions!");
        return;
    }

    %player.teleportEffect();
    spawnBeam(%brick.getPosition(), %brick.TelepadTarget, 4);
    %player.setTransform(%brick.TelepadTarget);
    ServerPlay3D(MMWarpPadWooshSound, %brick.getPosition());
    ServerPlay3D(MMWarpPadWooshSound, %brick.TelepadTarget);
}

//Artillery
datablock ExplosionData(gc_M119ShellMiningExplosion : gc_M119ShellHEExplosion)
{
  damageRadius = 10;
  radiusDamage = 100000; //die
  impulseRadius = 10;
  impulseForce = 400;
};

datablock ProjectileData(MM_M119ShellMiningProjectile : gc_M119ShellHEProjectile)
{
  uiName = "Mining Shell";
  name = "Mining Shell";
  directDamage = 69420;
  explosion = gc_M119ShellMiningExplosion;
};

$MM::ItemCost["MM_M119ShellMiningItem"] = "4\tFrame Parts\t1\tComputation Parts\t5\tRadioactive Waste";
$MM::ItemDisc["MM_M119ShellMiningItem"] = "An expensive artillery shell made for mass clearing practically any layer of dirt.";
datablock ItemData(MM_M119ShellMiningItem : gc_M119ShellHEItem)
{
    uiName = "M119 Shell Mining";
    image = MM_M119ShellMiningImage;
};

datablock ShapeBaseImageData(MM_M119ShellMiningImage : gc_M119ShellHEImage)
{
    item = MM_M119ShellMiningItem;
};

function MM_M119ShellMiningImage::onFire(%this,%obj,%slot)
{
    %raycast = containerRayCast(%obj.getEyePoint(),vectorAdd(%obj.getEyePoint(),vectorScale(%obj.getEyeVector(),3)),$TypeMasks::PlayerObjectType,%obj);
    %thing = firstWord(%raycast);
    if(isObject(%thing) && %thing.dataBlock.getID() == gc_M119TurretPlayer.getID())
    {
        if(getSimTime() - %thing.lastShotTime < 4000) { centerPrint(%obj.client,"I have to wait!",1); return; }
        %thing.lastShotTime = getSimTime();
        %thing.loaded = "MM_M119ShellMiningProjectile";
        serverPlay3D(gc_M119LoadSound,%obj.getTransform());
        centerPrint(%obj.client,"Mining Shell loaded!",1);
        %currSlot = %obj.currTool;
        %obj.tool[%currSlot] = 0;
        %obj.weaponCount--;
        messageClient(%obj.client,'MsgItemPickup','',%currSlot,0);
        serverCmdUnUseTool(%obj.client);
    }
}