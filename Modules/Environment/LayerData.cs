$MM::ZLayerOffset = 5120;
$MM::ZLayerLimit = 5122;

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
            name = "Slade Top";
            dirt = "True Slade";
            startZ = 999999;
            veinCount = 0;
        };

        new ScriptObject(LayerType)
        {
            name = "Shale";
            dirt = "Shale";
            startZ = 0;
            drillReduction = 0.7;
            veinCount = 5;
            weightTotal = 225;
            vein[0] = 1.0 TAB "Boulder" TAB "Square" TAB 2 TAB "Granite" TAB 1.00;
            vein[1] = 1.0 TAB "Tetrahedrite" TAB "Line" TAB 4 TAB "Copper" TAB 1.00;
            vein[2] = 1.0 TAB "Cassiterite" TAB "Line" TAB 4 TAB "Tin" TAB 1.00;
            vein[3] = 1.0 TAB "Limonite" TAB "Square" TAB 1 TAB "Iron" TAB 1.00;
            vein[4] = 0.0001 TAB "LOLWTFBBQ" TAB "None" TAB 1 TAB "Sturdium" TAB 1.00;
        };

		new ScriptObject(LayerType)
        {
            name = "Dirt";
            dirt = "Dirt";
            startZ = -32;
            drillReduction = 0.8;
            veinCount = 10;
            weightTotal = 250;
            vein[0] = 1.0 TAB "Boulder" TAB "Square" TAB 2 TAB "Granite" TAB 1.00;
            vein[1] = 1.0 TAB "Tetrahedrite" TAB "Line" TAB 4 TAB "Copper" TAB 1.00 TAB "Antimony" TAB 0.3;
            vein[2] = 1.0 TAB "Cassiterite" TAB "Line" TAB 4 TAB "Tin" TAB 1.00;
            vein[3] = 0.8 TAB "Limonite" TAB "Square" TAB 1 TAB "Iron" TAB 1.00;
            vein[4] = 0.4 TAB "Sphalerite" TAB "Square" TAB 1 TAB "Zinc" TAB 1.00 TAB "Iron" TAB 0.3;
            vein[5] = 0.1 TAB "Bauxite" TAB "Line" TAB 4 TAB "Aluminum" TAB 1.00 TAB "Gallium" TAB 0.2;
            vein[6] = 0.0 TAB "Quartzite" TAB "Line" TAB 4 TAB "Quartz" TAB 1.00;
            vein[7] = 0.0 TAB "Cobaltite" TAB "Line" TAB 4 TAB "Cobalt" TAB 1.00;
            vein[8] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Basic Loot Cache" TAB 1.00 TAB "Improved Loot Cache" TAB 0.40 TAB "Magicite" TAB 0.25;
            vein[9] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Packed Dirt";
            dirt = "Packed Dirt";
            startZ = -80;
            drillReduction = 0.9;
            veinCount = 10;
            weightTotal = 240;
            vein[0] = 1.0 TAB "Boulder" TAB "Square" TAB 2 TAB "Granite" TAB 1.00;
            vein[1] = 0.9 TAB "Tetrahedrite" TAB "Line" TAB 5 TAB "Copper" TAB 1.00 TAB "Antimony" TAB 0.4;
            vein[2] = 0.7 TAB "Cassiterite" TAB "Line" TAB 5 TAB "Tin" TAB 1.00;
            vein[3] = 1.0 TAB "Limonite" TAB "Square" TAB 1 TAB "Iron" TAB 1.00;
            vein[4] = 0.8 TAB "Sphalerite" TAB "Square" TAB 1 TAB "Zinc" TAB 1.00 TAB "Iron" TAB 0.4;
            vein[5] = 0.6 TAB "Bauxite" TAB "Line" TAB 5 TAB "Aluminum" TAB 1.00 TAB "Gallium" TAB 0.3;
            vein[6] = 0.1 TAB "Quartzite" TAB "Line" TAB 5 TAB "Quartz" TAB 1.00;
            vein[7] = 0.0 TAB "Cobaltite" TAB "Line" TAB 5 TAB "Cobalt" TAB 1.00;
            vein[8] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Basic Loot Cache" TAB 1.00 TAB "Improved Loot Cache" TAB 0.40 TAB "Magicite" TAB 0.25;
            vein[9] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Compressed Dirt";
            dirt = "Compressed Dirt";
            startZ = -128;
            drillReduction = 1.0;
            veinCount = 10;
            weightTotal = 230;
            vein[0] = 1.0 TAB "Boulder" TAB "Square" TAB 3 TAB "Granite" TAB 1.00;
            vein[1] = 0.7 TAB "Tetrahedrite" TAB "Line" TAB 5 TAB "Copper" TAB 1.00 TAB "Antimony" TAB 0.5;
            vein[2] = 0.5 TAB "Cassiterite" TAB "Line" TAB 5 TAB "Tin" TAB 1.00;
            vein[3] = 0.9 TAB "Limonite" TAB "Square" TAB 2 TAB "Iron" TAB 1.00;
            vein[4] = 1.0 TAB "Sphalerite" TAB "Square" TAB 1 TAB "Zinc" TAB 1.00 TAB "Iron" TAB 0.5;
            vein[5] = 0.8 TAB "Bauxite" TAB "Line" TAB 5 TAB "Aluminum" TAB 1.00 TAB "Gallium" TAB 0.3;
            vein[6] = 0.6 TAB "Quartzite" TAB "Line" TAB 5 TAB "Quartz" TAB 1.00;
            vein[7] = 0.4 TAB "Cobaltite" TAB "Line" TAB 5 TAB "Cobalt" TAB 1.00;
            vein[8] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Basic Loot Cache" TAB 1.00 TAB "Improved Loot Cache" TAB 0.40 TAB "Magicite" TAB 0.25;
            vein[9] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };

        new ScriptObject(LayerType)
        {
            name = "Stone";
            dirt = "Stone";
            startZ = -176;
            drillReduction = 1.1;
            veinCount = 12;
            weightTotal = 250;
            vein[0] = 1.0 TAB "Garnet" TAB "Square" TAB 2 TAB "Copper" TAB 1.00 TAB "Garnet" TAB 1.00;
            vein[1] = 1.0 TAB "Graphite" TAB "Square" TAB 1 TAB "Graphite" TAB 5.00 TAB "Diamond" TAB 0.01;
            vein[2] = 0.8 TAB "Pentlandite" TAB "Line" TAB 4 TAB "Nickel" TAB 1.00 TAB "Iron" TAB 0.3;
            vein[3] = 0.6 TAB "Fluorite" TAB "Line" TAB 3 TAB "Fluorite" TAB 1.00 TAB "Apatite" TAB 0.15;
            vein[4] = 0.4 TAB "Lithium" TAB "None" TAB 1 TAB "Lithium" TAB 1.00;
            vein[5] = 0.2 TAB "Galena" TAB "Line" TAB 4 TAB "Lead" TAB 1.00 TAB "Silver" TAB 0.20;
            vein[6] = 0.0 TAB "Argentite" TAB "Square" TAB 1 TAB "Silver" TAB 1.00;
            vein[7] = 0.0 TAB "Ilmenite" TAB "Line" TAB 3 TAB "Titanium" TAB 1.00 TAB "Iron" TAB 0.20;
            vein[8] = 0.0 TAB "Ruby" TAB "None" TAB 1 TAB "Ruby" TAB 1.00;
            vein[9] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Improved Loot Cache" TAB 1.00 TAB "Superior Loot Cache" TAB 0.40 TAB "Mythril" TAB 0.25 TAB "Dirty Crystal" TAB 0.20;
            vein[10] = 0.4 TAB "Magma" TAB "Square" TAB 2 TAB "Magma" TAB 1.00 TAB "Slag" TAB 0.20;
            vein[11] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Packed Stone";
            dirt = "Packed Stone";
            startZ = -240;
            drillReduction = 1.2;
            veinCount = 12;
            weightTotal = 240;
            vein[0] = 0.9 TAB "Garnet" TAB "Square" TAB 2 TAB "Copper" TAB 1.00 TAB "Garnet" TAB 1.00;
            vein[1] = 1.0 TAB "Graphite" TAB "Square" TAB 1 TAB "Graphite" TAB 5.00 TAB "Diamond" TAB 0.01;
            vein[2] = 1.2 TAB "Pentlandite" TAB "Line" TAB 5 TAB "Nickel" TAB 1.00 TAB "Iron" TAB 0.3;
            vein[3] = 1.0 TAB "Fluorite" TAB "Line" TAB 4 TAB "Fluorite" TAB 1.00 TAB "Apatite" TAB 0.15;
            vein[4] = 0.8 TAB "Lithium" TAB "None" TAB 1 TAB "Lithium" TAB 1.00;
            vein[5] = 0.6 TAB "Galena" TAB "Line" TAB 5 TAB "Lead" TAB 1.00 TAB "Silver" TAB 0.20;
            vein[6] = 0.4 TAB "Argentite" TAB "Square" TAB 1 TAB "Silver" TAB 1.00;
            vein[7] = 0.2 TAB "Ilmenite" TAB "Line" TAB 4 TAB "Titanium" TAB 1.00 TAB "Iron" TAB 0.20;
            vein[8] = 0.0 TAB "Ruby" TAB "None" TAB 1 TAB "Ruby" TAB 1.00;
            vein[9] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Improved Loot Cache" TAB 1.00 TAB "Superior Loot Cache" TAB 0.40 TAB "Mythril" TAB 0.50 TAB "Dirty Crystal" TAB 0.20;
            vein[10] = 0.6 TAB "Magma" TAB "Square" TAB 2 TAB "Magma" TAB 1.00 TAB "Slag" TAB 0.20;
            vein[11] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Compressed Stone";
            dirt = "Compressed Stone";
            startZ = -304;
            drillReduction = 1.3;
            veinCount = 12;
            weightTotal = 230;
            vein[0] = 0.7 TAB "Garnet" TAB "Square" TAB 3 TAB "Copper" TAB 1.00 TAB "Garnet" TAB 1.00;
            vein[1] = 0.8 TAB "Graphite" TAB "Square" TAB 2 TAB "Graphite" TAB 5.00 TAB "Diamond" TAB 0.01;
            vein[2] = 0.8 TAB "Pentlandite" TAB "Line" TAB 5 TAB "Nickel" TAB 1.00 TAB "Iron" TAB 0.3;
            vein[3] = 1.0 TAB "Fluorite" TAB "Line" TAB 4 TAB "Fluorite" TAB 1.00 TAB "Apatite" TAB 0.15;
            vein[4] = 1.0 TAB "Lithium" TAB "None" TAB 1 TAB "Lithium" TAB 1.00;
            vein[5] = 0.8 TAB "Galena" TAB "Line" TAB 5 TAB "Lead" TAB 1.00 TAB "Silver" TAB 0.20;
            vein[6] = 0.6 TAB "Argentite" TAB "Square" TAB 1 TAB "Silver" TAB 1.00 TAB "Magma" TAB 0.20;
            vein[7] = 0.4 TAB "Ilmenite" TAB "Line" TAB 4 TAB "Titanium" TAB 1.00 TAB "Iron" TAB 0.20;
            vein[8] = 0.2 TAB "Ruby" TAB "None" TAB 1 TAB "Ruby" TAB 1.00;
            vein[9] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Improved Loot Cache" TAB 1.00 TAB "Superior Loot Cache" TAB 0.40 TAB "Mythril" TAB 0.50 TAB "Dirty Crystal" TAB 0.20;
            vein[10] = 0.8 TAB "Magma" TAB "Square" TAB 3 TAB "Magma" TAB 1.00 TAB "Slag" TAB 0.20;
            vein[11] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };

        new ScriptObject(LayerType)
        {
            name = "Bedrock";
            dirt = "Bedrock";
            startZ = -368;
            drillReduction = 1.4;
            veinCount = 13;
            weightTotal = 250;
            vein[0] = 1.0 TAB "Wolframite" TAB "Square" TAB 2 TAB "Tungsten" TAB 1.00;
            vein[1] = 1.0 TAB "Iridosmium" TAB "Line" TAB 3 TAB "Osmium" TAB 1.00 TAB "Iridium" TAB 0.20;
            vein[2] = 0.8 TAB "Uraninite" TAB "Line" TAB 4 TAB "Uranium" TAB 1.00 TAB "Thorium" TAB 0.1;
            vein[3] = 0.6 TAB "Ruthenium" TAB "Square" TAB 1 TAB "Ruthenium" TAB 1.00;
            vein[4] = 0.4 TAB "Monazite" TAB "None" TAB 1 TAB "Neodymium" TAB 1.00;
            vein[5] = 0.2 TAB "Osmiridium" TAB "Line" TAB 3 TAB "Iridium" TAB 1.00 TAB "Osmium" TAB 0.20;
            vein[6] = 0.0 TAB "Cooperite" TAB "Square" TAB 1 TAB "Palladium" TAB 1.00 TAB "Nickel" TAB 0.33;
            vein[7] = 0.0 TAB "Gold" TAB "Line" TAB 2 TAB "Gold" TAB 1.00;
            vein[8] = 0.0 TAB "Super Graphite" TAB "None" TAB 1 TAB "Graphite" TAB 1.00 TAB "Diamond" TAB 1.00;
            vein[9] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Superior Loot Cache" TAB 1.00 TAB "Epic Loot Cache" TAB 0.40 TAB "Dragonstone" TAB 0.50 TAB "Dirtier Crystal" TAB 0.20;
            vein[10] = 1.0 TAB "Magma" TAB "Square" TAB 3 TAB "Magma" TAB 1.00 TAB "Slag" TAB 0.20;
            vein[11] = 0.8 TAB "Waste" TAB "Line" TAB 6 TAB "Radioactive Waste" TAB 1.00;
            vein[12] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Packed Bedrock";
            dirt = "Packed Bedrock";
            startZ = -448;
            drillReduction = 1.5;
            veinCount = 13;
            weightTotal = 240;
            vein[0] = 0.8 TAB "Wolframite" TAB "Square" TAB 2 TAB "Tungsten" TAB 1.00;
            vein[1] = 0.8 TAB "Iridosmium" TAB "Line" TAB 4 TAB "Osmium" TAB 1.00 TAB "Iridium" TAB 0.20;
            vein[2] = 1.0 TAB "Uraninite" TAB "Line" TAB 5 TAB "Uranium" TAB 1.00 TAB "Thorium" TAB 0.1;
            vein[3] = 1.0 TAB "Ruthenium" TAB "Square" TAB 1 TAB "Ruthenium" TAB 1.00;
            vein[4] = 0.8 TAB "Monazite" TAB "None" TAB 1 TAB "Neodymium" TAB 1.00;
            vein[5] = 0.6 TAB "Osmiridium" TAB "Line" TAB 4 TAB "Iridium" TAB 1.00 TAB "Osmium" TAB 0.20;
            vein[6] = 0.4 TAB "Cooperite" TAB "Square" TAB 1 TAB "Palladium" TAB 1.00 TAB "Nickel" TAB 0.33;
            vein[7] = 0.2 TAB "Gold" TAB "Line" TAB 3 TAB "Gold" TAB 1.00;
            vein[8] = 0.0 TAB "Super Graphite" TAB "None" TAB 1 TAB "Graphite" TAB 1.00 TAB "Diamond" TAB 1.00;
            vein[9] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Superior Loot Cache" TAB 1.00 TAB "Epic Loot Cache" TAB 0.40 TAB "Dragonstone" TAB 0.50 TAB "Dirtier Crystal" TAB 0.20;
            vein[10] = 1.2 TAB "Magma" TAB "Square" TAB 3 TAB "Magma" TAB 1.00 TAB "Slag" TAB 0.20;
            vein[11] = 0.9 TAB "Waste" TAB "Line" TAB 7 TAB "Radioactive Waste" TAB 1.00;
            vein[12] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Compressed Bedrock";
            dirt = "Compressed Bedrock";
            startZ = -528;
            drillReduction = 1.6;
            veinCount = 13;
            weightTotal = 230;
            vein[0] = 0.6 TAB "Wolframite" TAB "Square" TAB 3 TAB "Tungsten" TAB 1.00;
            vein[1] = 0.6 TAB "Iridosmium" TAB "Line" TAB 4 TAB "Osmium" TAB 1.00 TAB "Iridium" TAB 0.20;
            vein[2] = 0.8 TAB "Uraninite" TAB "Line" TAB 5 TAB "Uranium" TAB 5.00 TAB "Thorium" TAB 0.5 TAB "Actinium" TAB 0.01;
            vein[3] = 0.8 TAB "Ruthenium" TAB "Square" TAB 2 TAB "Ruthenium" TAB 1.00;
            vein[4] = 1.0 TAB "Monazite" TAB "None" TAB 1 TAB "Neodymium" TAB 1.00;
            vein[5] = 1.0 TAB "Osmiridium" TAB "Line" TAB 4 TAB "Iridium" TAB 1.00 TAB "Osmium" TAB 0.20;
            vein[6] = 0.8 TAB "Cooperite" TAB "Square" TAB 1 TAB "Palladium" TAB 1.00 TAB "Nickel" TAB 0.33;
            vein[7] = 0.6 TAB "Gold" TAB "Line" TAB 3 TAB "Gold" TAB 1.00;
            vein[8] = 0.4 TAB "Super Graphite" TAB "None" TAB 1 TAB "Graphite" TAB 1.00 TAB "Diamond" TAB 1.00;
            vein[9] = 0.2 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Superior Loot Cache" TAB 1.00 TAB "Epic Loot Cache" TAB 0.40 TAB "Dragonstone" TAB 0.50 TAB "Dirtier Crystal" TAB 0.20;
            vein[10] = 1.4 TAB "Magma" TAB "Square" TAB 4 TAB "Magma" TAB 1.00 TAB "Slag" TAB 0.20;
            vein[11] = 1.0 TAB "Waste" TAB "Line" TAB 7 TAB "Radioactive Waste" TAB 1.00;
            vein[12] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };

        new ScriptObject(LayerType)
        {
            name = "Slade A";
            dirt = "Slade";
            startZ = -608;
            skipLayerBlend = true;
            drillReduction = 10.0;
            veinCount = 1;
            weightTotal = 666;
            vein[0] = 6.66 TAB "Corium" TAB "None" TAB 1 TAB "Flesh-Ridden Corium" TAB 666;
        };
        new ScriptObject(LayerType)
        {
            name = "Fleshrock";
            dirt = "Fleshrock";
            startZ = -624;
            drillReduction = 2.0;
            veinCount = 9;
            weightTotal = 100;
            vein[0] = 1.0 TAB "Bismite" TAB "Square" TAB 3 TAB "Bismuth" TAB 1.00;
            vein[1] = 1.0 TAB "Gas Deposit" TAB "Line" TAB 2 TAB "Helium" TAB 1.00 TAB "Krypton" TAB 0.20 TAB "Xenon" TAB 0.04;
            vein[2] = 0.5 TAB "Common Deposit" TAB "Line" TAB 3 TAB "Plutonium" TAB 1.00 TAB "Actinium" TAB 0.40 TAB "Promethium" TAB 0.10;
            vein[3] = 0.2 TAB "Rare Deposit" TAB "Line" TAB 3 TAB "Francium" TAB 1.00 TAB "Astatine" TAB 0.40 TAB "Americium" TAB 0.10;
            vein[4] = 0.1 TAB "Loot Caches" TAB "None" TAB 1 TAB "Granite" TAB 2.00 TAB "Epic Loot Cache" TAB 1.00 TAB "Legendary Loot Cache" TAB 0.40 TAB "Sturdium" TAB 0.50;
            vein[5] = 0.1 TAB "Magma" TAB "Square" TAB 4 TAB "Magma" TAB 1.00 TAB "Slag" TAB 0.20;
            vein[6] = 0.2 TAB "Waste" TAB "Line" TAB 7 TAB "Radioactive Waste" TAB 1.00;
            vein[7] = 1.0 TAB "Cancer" TAB "Line" TAB 2 TAB "Cancerous Growth" TAB 1.00;
            vein[8] = 0.3 TAB "Fluid Pools" TAB "None" TAB 1 TAB "Crude Oil" TAB 1.00 TAB "Biomatter" TAB 1.00;
        };
        new ScriptObject(LayerType)
        {
            name = "Slade B";
            dirt = "Slade";
            startZ = -752;
            drillReduction = 10.0;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Voidstone";
            dirt = "Voidstone";
            startZ = -768;
            drillReduction = 2.5;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Slade C";
            dirt = "Slade";
            startZ = -896;
            drillReduction = 10.0;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "Mayhemium";
            dirt = "Mayhemium";
            startZ = -912;
            drillReduction = 3.0;
            veinCount = 0;
        };
        new ScriptObject(LayerType)
        {
            name = "True Slade";
            dirt = "True Slade";
            startZ = -1040;
            veinCount = 0;
        };
	};
}
SetupLayerData();

function GetLayerType(%type)
{
	if (!isObject($MM::LayerType[%type]))
		for (%i = 0; %i < LayerData.getCount(); %i++)
			if (LayerData.getObject(%i).name $= %type)
				$MM::LayerType[%type] = LayerData.getObject(%i);

	return $MM::LayerType[%type];
}

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