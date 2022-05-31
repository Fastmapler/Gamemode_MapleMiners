$MM::ZLayerOffset = 1024;

function SetupLayerData()
{
	if (isObject(LayerData))
	{
		LayerData.deleteAll();
		LayerData.delete();
	}

	new SimSet(LayerData)
	{
        //Vein[i] = Weight TAB Name TAB Shape TAB Size TAB ore1 TAB weight1 TAB ...
		new ScriptObject(LayerType)
        {
            name = "SladeTop";
            dirt = "Slade";
            startZ = 999999;
            veinCount = 0;
        };

		new ScriptObject(LayerType)
        {
            name = "Dirt";
            dirt = "Dirt";
            startZ = 0;
            veinCount = 8;
            weightTotal = 100;
            vein[0] = 1.1 TAB "Boulder" TAB "Square" TAB 3 TAB "Granite" TAB 1.00;
            vein[1] = 1.0 TAB "Tetrahedrite" TAB "Line" TAB 4 TAB "Copper" TAB 1.00 TAB "Antimony" TAB 0.3;
            vein[2] = 1.0 TAB "Cassiterite" TAB "Line" TAB 4 TAB "Tin" TAB 1.00;
            vein[3] = 0.8 TAB "Limonite" TAB "Square" TAB 2 TAB "Iron" TAB 1.00;
            vein[4] = 0.4 TAB "Sphalerite" TAB "Square" TAB 4 TAB "Zinc" TAB 1.00 TAB "Iron" TAB 0.3;
            vein[5] = 0.1 TAB "Bauxite" TAB "Line" TAB 3 TAB "Aluminum" TAB 1.00 TAB "Gallium" TAB 0.2;
            vein[6] = 0.0 TAB "Quartzite" TAB "None" TAB 1 TAB "Quartz" TAB 1.00;
            vein[7] = 0.0 TAB "Cobaltite" TAB "Line" TAB 2 TAB "Cobalt" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Packed Dirt";
            dirt = "Packed Dirt";
            startZ = -32;
            veinCount = 8;
            weightTotal = 100;
            vein[0] = 1.0 TAB "Boulder" TAB "Square" TAB 4 TAB "Granite" TAB 1.00;
            vein[1] = 0.9 TAB "Tetrahedrite" TAB "Line" TAB 5 TAB "Copper" TAB 1.00 TAB "Antimony" TAB 0.4;
            vein[2] = 0.7 TAB "Cassiterite" TAB "Line" TAB 5 TAB "Tin" TAB 1.00;
            vein[3] = 1.0 TAB "Limonite" TAB "Square" TAB 2 TAB "Iron" TAB 1.00;
            vein[4] = 0.8 TAB "Sphalerite" TAB "Square" TAB 4 TAB "Zinc" TAB 1.00 TAB "Iron" TAB 0.4;
            vein[5] = 0.6 TAB "Bauxite" TAB "Line" TAB 3 TAB "Aluminum" TAB 1.00 TAB "Gallium" TAB 0.3;
            vein[6] = 0.1 TAB "Quartzite" TAB "None" TAB 1 TAB "Quartz" TAB 1.00;
            vein[7] = 0.0 TAB "Cobaltite" TAB "Line" TAB 2 TAB "Cobalt" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Compressed Dirt";
            dirt = "Compressed Dirt";
            startZ = -64;
            veinCount = 8;
            weightTotal = 100;
            vein[0] = 0.9 TAB "Boulder" TAB "Square" TAB 4 TAB "Granite" TAB 1.00;
            vein[1] = 0.7 TAB "Tetrahedrite" TAB "Line" TAB 5 TAB "Copper" TAB 1.00 TAB "Antimony" TAB 0.5;
            vein[2] = 0.5 TAB "Cassiterite" TAB "Line" TAB 5 TAB "Tin" TAB 1.00;
            vein[3] = 0.9 TAB "Limonite" TAB "Square" TAB 3 TAB "Iron" TAB 1.00;
            vein[4] = 1.0 TAB "Sphalerite" TAB "Square" TAB 5 TAB "Zinc" TAB 1.00 TAB "Iron" TAB 0.5;
            vein[5] = 0.8 TAB "Bauxite" TAB "Line" TAB 3 TAB "Aluminum" TAB 1.00 TAB "Gallium" TAB 0.3;
            vein[6] = 0.6 TAB "Quartzite" TAB "None" TAB 1 TAB "Quartz" TAB 1.00;
            vein[7] = 0.2 TAB "Cobaltite" TAB "Line" TAB 2 TAB "Cobalt" TAB 1.00;
        };

        new ScriptObject(LayerType)
        {
            name = "Stone";
            dirt = "Stone";
            startZ = -96;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Packed Stone";
            dirt = "Packed Stone";
            startZ = -144;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Compressed Stone";
            dirt = "Compressed Stone";
            startZ = -192;
            veinCount = 0;
        };

        new ScriptObject(LayerType)
        {
            name = "Bedrock";
            dirt = "Bedrock";
            startZ = -240;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Packed Bedrock";
            dirt = "Packed Bedrock";
            startZ = -304;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Compressed Bedrock";
            dirt = "Compressed Bedrock";
            startZ = -368;
            veinCount = 0;
        };

        new ScriptObject(LayerType)
        {
            name = "SladeA";
            dirt = "Slade";
            startZ = -432;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Fleshrock";
            dirt = "Fleshrock";
            startZ = -496;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "SladeB";
            dirt = "Slade";
            startZ = -624;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Voidstone";
            dirt = "Voidstone";
            startZ = -688;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "SladeC";
            dirt = "Slade";
            startZ = -816;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Mayhemium";
            dirt = "Mayhemium";
            startZ = -880;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "SladeD";
            dirt = "Slade";
            startZ = -1008;
            veinCount = 0;
        };
	};
}
SetupLayerData();

function getOreFromVein(%vein)
{
    %fieldCount = getFieldCount(%vein);

    for (%i = 4; %i < %fieldCount; %i += 2)
        %totalWeight += getField(%vein, %i + 1);

    %rand = getRandom() * %totalWeight;
	for (%i = 4; %i < %fieldCount; %i += 2)
	{
		%spawnData = getField(%vein, %i);
		%spawnWeight = getField(%vein, %i + 1);

		if (%rand < %spawnWeight)
			break;

		%rand -= %spawnWeight;
		%spawnData = "";
	}

    return %spawnData;
}