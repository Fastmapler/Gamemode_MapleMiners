$MM::BrickDistance = 1; //In tork units
$MM::MegaRollBaseChance = 50000;
$MM::MegaRollChance = $MM::MegaRollBaseChance;
$MM::BrickDirection[0] = vectorScale("0 0 1", $MM::BrickDistance);
$MM::BrickDirection[1] = vectorScale("0 0 -1", $MM::BrickDistance);
$MM::BrickDirection[2] = vectorScale("0 1 0", $MM::BrickDistance);
$MM::BrickDirection[3] = vectorScale("0 -1 0", $MM::BrickDistance);
$MM::BrickDirection[4] = vectorScale("1 0 0", $MM::BrickDistance);
$MM::BrickDirection[5] = vectorScale("-1 0 0", $MM::BrickDistance);

function CollapseMine()
{
	Brickgroup_1337.deleteAll();
	deleteVariables("$MM::SpawnGrid*");
	deleteVariables("$MM::BrickGrid*");
	MM_AttemptSpawn("MM_GoblinShop", "0 0" SPC (GetLayerType("Bedrock").startZ + $MM::ZLayerOffset - 16));
	RevealTop();
}

function RevealTop()
{
	for (%x = -17; %x < 17; %x++)
	{
		for (%y = -17; %y < 17; %y++)
		{
			if (%x < 16 && %x >= -16 && %y < 16 && %y >= -16)
			{
				$MM::SpawnGrid[vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 1), $MM::BrickDistance)] = "---";
				$MM::BrickGrid[vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 1), $MM::BrickDistance)] = "---";
				$MM::SpawnGrid[vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 2), $MM::BrickDistance)] = "---";
				$MM::BrickGrid[vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 2), $MM::BrickDistance)] = "---";
				RevealBlock(vectorScale(%x SPC %y SPC $MM::ZLayerOffset, $MM::BrickDistance));
			}
			else
			{
				$MM::SpawnGrid[vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 1), $MM::BrickDistance)] = "True Slade";
				$MM::SpawnGrid[vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 2), $MM::BrickDistance)] = "True Slade";
				RevealBlock(vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 1), $MM::BrickDistance));
				RevealBlock(vectorScale(%x SPC %y SPC ($MM::ZLayerOffset + 2), $MM::BrickDistance));
			}
			
		}
	}
}

function GenerateSurroundingBlocks(%pos)
{
	for (%i = 0; %i < 6; %i++)
		RevealBlock(vectorAdd(%pos, $MM::BrickDirection[%i]));
}

function CheckArea(%pos, %size)
{
	%bricks = 0;
	initContainerBoxSearch(%pos, %size, $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
        %bricks++;

	if (%bricks > 0)
		return false;
		
	return true;
}

function RevealArea(%startPos, %endPos, %solid)
{
	%startPos = roundVector(%startPos);
	%endPos = roundVector(%endPos);

	%startX = getMin(getWord(%startPos, 0), getWord(%endPos, 0));
	%startY = getMin(getWord(%startPos, 1), getWord(%endPos, 1));
	%startZ = getMin(getWord(%startPos, 2), getWord(%endPos, 2));

	%endX = getMax(getWord(%startPos, 0), getWord(%endPos, 0));
	%endY = getMax(getWord(%startPos, 1), getWord(%endPos, 1));
	%endZ = getMax(getWord(%startPos, 2), getWord(%endPos, 2));

	for (%x = %startX; %x <= %endX; %x += $MM::BrickDistance)
	{
		for (%y = %startY; %y <= %endY; %y += $MM::BrickDistance)
		{
			for (%z = %startZ; %z <= %endZ; %z += $MM::BrickDistance)
			{
				%pos = roundVector(%x SPC %y SPC %z);
				if (%solid || %x == %startX || %x == %endX || %y == %startY || %y == %endY || %z == %startZ || %z == %endZ)
				{
					RevealBlock(%pos);
				}
				else
				{
					$MM::SpawnGrid[%pos] = "---";
					$MM::BrickGrid[%pos] = "---";
				}
			}
		}
	}
}

function RevealBlock(%pos)
{
	%pos = roundVector(%pos);
	//Don't generate anything if something already spawned here before
		
	if ($MM::SpawnGrid[%pos] $= "")
		GenerateBlock(%pos);

	if ($MM::BrickGrid[%pos] !$= "")
		return;

	//if (isObject(%matter = getMatterType($MM::SpawnGrid[%pos])) && %matter.color)
	PlaceMineBrick(%pos, $MM::SpawnGrid[%pos]);
}

function GenerateBlock(%pos)
{
	%pos = roundVector(%pos);
	//Decide what to spawn
	%layer = LayerData.getObject(0);
	%prevLayer = %layer;
	for (%i = 0; %i < LayerData.getCount(); %i++)
	{
		%testLayer = LayerData.getObject(%i);
		if (getWord(%pos, 2) <= ($MM::ZLayerOffset + %testLayer.startZ))
		{
			%prevLayer = %layer;
			%layer = %testLayer;
		}	
	}
	
	%rand = getRandom() * %layer.weightTotal;
	for (%i = 0; %i < %layer.veinCount; %i++)
	{
		%spawnData = %layer.vein[%i];
		%spawnWeight = getField(%spawnData, 0);

		if (%rand < %spawnWeight)
			break;

		%rand -= %spawnWeight;
		%spawnData = "";
	}
	if (%spawnData !$= "")
	{
		%spawnShape = getField(%spawnData, 2);
		if (%spawnShape $= "Line")
		{
			%dir = $MM::BrickDirection[getRandom(0, 5)];
			%length = getRandom(2, getField(%spawnData, 3));
			%linePos = %pos;

			for (%i = 0; %i < %length; %i++)
			{
				if ($MM::SpawnGrid[%linePos] $= "")
				{
					$MM::SpawnGrid[%linePos] = getOreFromVein(%spawnData);
				}
				%linePos = vectorAdd(%linePos, %dir);
			}
			PlaceMineBrick(%pos, $MM::SpawnGrid[%pos]);
		}
		else if (%spawnShape $= "Square")
		{
			%size = getRandom(1, getField(%spawnData, 3)) * $MM::BrickDistance;

			for (%x = %size * -1; %x < %size; %x += $MM::BrickDistance)
			{
				for (%y = %size * -1; %y < %size; %y += $MM::BrickDistance)
				{
					for (%z = %size * -1; %z < %size; %z += $MM::BrickDistance)
					{
						%linePos = vectorAdd(%pos, %x SPC %y SPC %z);
						if ($MM::SpawnGrid[%linePos] $= "")
						{
							if (getRandom() < (1.05 / (vectorLen(vectorSub(%pos, %linePos)) + 1)))
								$MM::SpawnGrid[%linePos] = getOreFromVein(%spawnData);
							else
								$MM::SpawnGrid[%pos] = %layer.name;
						}
					}
				}
			}
		}
		else
			$MM::SpawnGrid[%pos] = getOreFromVein(%spawnData);
	}
	else //Found no vein to spawn, just spawn dirt.
	{
		for (%i = 0; %i < LayerData.getCount(); %i++)
		{
			%testLayer = LayerData.getObject(%i);
			if (%layer.startZ > %testLayer.startZ && (!isObject(%nextLayer) || %nextLayer.startZ < %testLayer.startZ))
			{
				%nextLayer = %testLayer;
			}	
		}

		$MM::SpawnGrid[%pos] = %layer.dirt;

		if (isObject(%nextLayer) && !%nextLayer.skipLayerBlend)
		{
			%dist = mAbs(%nextLayer.startZ - (getWord(%pos, 2) - $MM::ZLayerOffset));
			if (getRandom(5) > %dist)
				$MM::SpawnGrid[%pos] = %nextLayer.dirt;
		}

		if (getRandom(0, $MM::MegaRollChance) == 0)
		{
			$MM::MegaRollChance = $MM::MegaRollBaseChance;
			if (getSimTime() - $MM::LastMegaRoll > (60 * 1000 * 20))
			{
				%LootTable = "Frenzy Orb\tLotto Orb\tBerserk Orb\tMythical Orb";
				$MM::LastMegaRoll = getSimTime();
			}
			else
				%LootTable = "Extender Orb";

			$MM::SpawnGrid[%pos] = getField(%LootTable, getRandom(getFieldCount(%LootTable)) - 1);
		}
		else if ($MM::MegaRollChance > 100)
			$MM::MegaRollChance--;
	}	
}

function PlaceMineBrick(%pos, %type)
{
	%pos = roundVector(%pos);

	CreateMinerMaster();
	
	if (getWord(%pos, 2) > $MM::ZLayerOffset)
		%type = "True Slade";

    if (!isObject(%matter = GetMatterType(%type)) || !isObject(%client = $MM::HostClient) || getWord(%pos, 2) > $MM::ZLayerLimit || !isObject(%matter.data))
        return;

    %brick = new fxDTSBrick()
	{
		client = %client;
		datablock = %matter.data;
		position = %pos;
		rotation = "0 0 0 0";
		colorID = getColorFromHex(%matter.color);
		scale = "1 1 1";
		angleID = "0";
		colorfxID = %matter.colorfx;
		shapefxID = %matter.shapefx;
		printID = $printNameTable[%matter.printID];
		isPlanted = 1;
		stackBL_ID = %client.bl_id;
		matter = %matter.name;
		health = %matter.health;
		canMine = (%matter.level == -1 ? 0 : 1);
	};
    %error = %brick.plant();

	if (%error == 1)
	{
		%brick.delete();
		return;
	}

    %brick.setTrusted(1);

	$MM::BrickGrid[%pos] = %brick;
	$MM::SpawnGrid[%pos] = %matter.name;
	
    %client.brickgroup.add(%brick);

	if ((getWord(getColorIDTable(%brick.colorID), 3) < 0.9 || %brick.shapefxID > 0 || %matter.SurroundCheck $= "Force") && %matter.SurroundCheck !$= "Skip")
		GenerateSurroundingBlocks(%brick.getPosition());

	if (%matter.canPump)
		%brick.FluidCapacity = getRandom(1, 4);

    return %brick;
}

function PlaceGhostBrick(%pos, %type)
{
	%pos = roundVector(%pos);

	CreateMinerMaster();

    if (!isObject(%matter = GetMatterType(%type)) || !isObject(%client = $MM::HostClient) || getWord(%pos, 2) > $MM::ZLayerLimit || !isObject(%matter.data))
        return;

	%ghost = new fxDTSBrick()
	{
		client = %client;
		datablock = %matter.data;
		position = %pos;
		rotation = "0 0 0 0";
		colorID = getColorFromHex(%matter.color);
		scale = "1 1 1";
		angleID = "0";
		colorfxID = %matter.colorfx;
		shapefxID = %matter.shapefx;
		printID = $printNameTable[%matter.printID];
		stackBL_ID = %client.bl_id;
		matter = %matter.name;
		health = %matter.health;
		canMine = (%matter.level == -1 ? 0 : 1);
	};

	return %ghost;
}

function ReplaceBrick(%pos, %type)
{
	%pos = roundVector(%pos);
	//TODO: Optimize (Just modify values instead of spawning a whole new brick?)
	if (isObject(%brick = $MM::BrickGrid[%pos]))
	{
		%brick.delete();
		PlaceMineBrick(%pos, %type);
	}
}