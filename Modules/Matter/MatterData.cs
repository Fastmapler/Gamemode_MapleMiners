function SetupMatterData()
{
	if (isObject(MatterData))
	{
		MatterData.deleteAll();
		MatterData.delete();
	}

	new SimSet(MatterData)
	{
		//Dirts
		new ScriptObject(MatterType) { codename="shale";	name="Shale";				data=brickMMBrickGenericData;	color="f8cfaaff";	colorFX=0;	shapeFX=0;	printID="ModTer/sand2";	value=0;	health=20;		level=5;	hitSound="Stone"; fuelPotency=0.5; };
		
		new ScriptObject(MatterType) { codename="dirt1";	 name="Dirt";					data=brickMMBrickGenericData;	color="b6a593ff";	colorFX=0;	shapeFX=0;	printID="ModTer/dirt-gravel";	value=0;	health=25;		level=5;	hitSound="Stone"; fuelPotency=1; };
		new ScriptObject(MatterType) { codename="dirt2";	 name="Packed Dirt";			data=brickMMBrickGenericData;	color="877564ff";	colorFX=0;	shapeFX=0;	printID="ModTer/dirt-gravel";	value=0;	health=50;		level=10;	hitSound="Stone"; fuelPotency=1; };
		new ScriptObject(MatterType) { codename="dirt3";	 name="Compressed Dirt";		data=brickMMBrickGenericData;	color="605042ff";	colorFX=0;	shapeFX=0;	printID="ModTer/dirt-gravel";	value=0;	health=125;		level=20;	hitSound="Stone"; fuelPotency=1; };

		new ScriptObject(MatterType) { codename="stone1";	 name="Stone";				data=brickMMBrickGenericData;	color="a5a189ff";	colorFX=0;	shapeFX=0;	printID="ModTer/big-pebbles";	value=0;	health=375;		level=40;	hitSound="Stone"; fuelPotency=2; };
		new ScriptObject(MatterType) { codename="stone2";	 name="Packed Stone";			data=brickMMBrickGenericData;	color="797260ff";	colorFX=0;	shapeFX=0;	printID="ModTer/big-pebbles";	value=0;	health=750;		level=60;	hitSound="Stone"; fuelPotency=2; };
		new ScriptObject(MatterType) { codename="stone3";	 name="Compressed Stone";		data=brickMMBrickGenericData;	color="504b3fff";	colorFX=0;	shapeFX=0;	printID="ModTer/big-pebbles";	value=0;	health=1875;	level=80;	hitSound="Stone"; fuelPotency=2; };

		new ScriptObject(MatterType) { codename="bedrock1";	 name="Bedrock";				data=brickMMBrickGenericData;	color="4f494bff";	colorFX=0;	shapeFX=0;	printID="ModTer/slag-stone";	value=0;	health=5625;	level=160;	hitSound="Stone"; fuelPotency=4; };
		new ScriptObject(MatterType) { codename="bedrock2";	 name="Packed Bedrock";		data=brickMMBrickGenericData;	color="2f2d2fff";	colorFX=0;	shapeFX=0;	printID="ModTer/slag-stone";	value=0;	health=11250;	level=200;	hitSound="Stone"; fuelPotency=4; };
		new ScriptObject(MatterType) { codename="bedrock3";	 name="Compressed Bedrock";	data=brickMMBrickGenericData;	color="18161aff";	colorFX=0;	shapeFX=0;	printID="ModTer/slag-stone";	value=0;	health=28125;	level=240;	hitSound="Stone"; fuelPotency=4; };

		new ScriptObject(MatterType) { codename="slade1";	 name="Slade";				data=brickMMBrickGenericData;	color="000000ff";	colorFX=0;	shapeFX=0;	printID="ModTer/Old_Stone_Road";	value=0;	health=999900;	level=9999;	hitSound="Stone"; fuelPotency=8; bombResist=1.0; };
		new ScriptObject(MatterType) { codename="slade2";	 name="True Slade";			data=brickMMBrickGenericData;	color="775238ff";	colorFX=0;	shapeFX=0;	printID="ModTer/Old_Stone_Road";	value=0;	health= -1;		level= -1;	hitSound="Stone"; bombResist=1.0; };

		new ScriptObject(MatterType) { codename="fleshrock";	 name="Fleshrock";			data=brickMMBrickGenericData;	color="931f23ff";	colorFX=0;	shapeFX=0;	printID="ModTer/TTdirt01";	value=0;	health=84375;	level=480;	hitSound="Meat"; fuelPotency=6; };
		new ScriptObject(MatterType) { codename="voidstone";	 name="Voidstone";			data=brickMMBrickGenericData;	color="ffffff01";	colorFX=3;	shapeFX=0;	printID="Letters/-space";	value=0;	health=168750;	level=725;	hitSound="Quartz"; fuelPotency=6; SurroundCheck="Skip";	hitFunc="MM_RadDamage";	hitFuncArgs=1; };
		new ScriptObject(MatterType) { codename="mayhemium";	 name="Mayhemium";			data=brickMMBrickGenericData;	color="e8e4e2ff";	colorFX=6;	shapeFX=1;	printID="ModTer/pixelated";	value=0;	health=421875;	level=1025;	hitSound="Metal"; fuelPotency=6; SurroundCheck="Skip";	hitFunc="MM_RadDamage";	hitFuncArgs=10;	harvestFunc="MM_HeatDamage";	harvestFuncArgs=5;  };

		new ScriptObject(MatterType) { codename="slag";	 name="Slag";					data=brickMMBrickGenericData;	color="4f494bff";	colorFX=0;	shapeFX=0;	printID="ModTer/sand2";	value=0;	health=25;		level=5;	hitSound="Stone"; fuelPotency=5; };
		
		//Ores
		
		//T1, 1:8.25 Value/Health ratio
		new ScriptObject(MatterType) { codename="ore1_0";	 name="Granite";				data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=6;	health=50;		level=5;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore1_1";	 name="Tin";					data=brickMMBrickGenericData;	color="706e6eff";	colorFX=0;	shapeFX=0;	printID="ModTer/snow4";	value=12;	health=100;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore1_2";	 name="Copper";				data=brickMMBrickGenericData;	color="953800ff";	colorFX=0;	shapeFX=0;	printID="ModTer/snow";	value=15;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore1_3";	 name="Iron";					data=brickMMBrickGenericData;	color="4f494bff";	colorFX=0;	shapeFX=0;	printID="ModTer/rock";	value=21;	health=175;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore1_4";	 name="Zinc";					data=brickMMBrickGenericData;	color="89a3b8ff";	colorFX=0;	shapeFX=0;	printID="ModTer/beach-sand";	value=23;	health=190;		level=7;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore1_5";	 name="Aluminum";				data=brickMMBrickGenericData;	color="d8d1ccff";	colorFX=0;	shapeFX=0;	printID="ModTer/rockface";	value=27;	health=220;		level=10;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore1_6";	 name="Antimony";				data=brickMMBrickGenericData;	color="a3b56bff";	colorFX=0;	shapeFX=0;	printID="ModTer/brickRAMP";value=29;	health=240;		level=15;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore1_7";	 name="Gallium";				data=brickMMBrickGenericData;	color="99958cff";	colorFX=0;	shapeFX=0;	printID="ModTer/marble";	value=34;	health=280;		level=20;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore1_8";	 name="Quartz";				data=brickMMBrickGenericData;	color="bcc1c88e";	colorFX=0;	shapeFX=0;	printID="ModTer/lava5";	value=32;	health=260;		level=30;	hitSound="Quartz"; };
		new ScriptObject(MatterType) { codename="ore1_9";	 name="Cobalt";				data=brickMMBrickGenericData;	color="1f568cff";	colorFX=0;	shapeFX=0;	printID="ModTer/pixelated";	value=36;	health=300;		level=40;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore1_R";	 name="Magicite";				data=brickMMBrickGenericData;	color="49285bff";	colorFX=0;	shapeFX=1;	printID="ModTer/lava5";	value=273;	health=1500;		level=5;	hitSound="Metal";	rare=true;};
		//T2, 1:8.5 Value/Health ratio
		new ScriptObject(MatterType) { codename="ore2_0";	 name="Garnet";				data=brickMMBrickGenericData;	color="d15600ff";	colorFX=0;	shapeFX=0;	printID="ModTer/lava5";	value=47;	health=400;		level=40;	hitSound="Quartz"; };
		new ScriptObject(MatterType) { codename="ore2_1";	 name="Graphite";				data=brickMMBrickGenericData;	color="000000ff";	colorFX=0;	shapeFX=0;	printID="Letters/-space";	value=94;	health=800;		level=40;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore2_2";	 name="Nickel";				data=brickMMBrickGenericData;	color="dfc37cff";	colorFX=0;	shapeFX=0;	printID="ModTer/snow";	value=118;	health=1000;	level=40;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore2_3";	 name="Fluorite";				data=brickMMBrickGenericData;	color="9ab6b5ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=141;	health=1200;	level=55;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore2_4";	 name="Lithium";				data=brickMMBrickGenericData;	color="b6a593ff";	colorFX=0;	shapeFX=0;	printID="ModTer/Port_of_Taganrog";	value=211;	health=1800;	level=70;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore2_5";	 name="Apatite";				data=brickMMBrickGenericData;	color="0c2a49ff";	colorFX=0;	shapeFX=0;	printID="ModTer/big-pebbles";	value=294;	health=2500;	level=85;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore2_6";	 name="Lead";					data=brickMMBrickGenericData;	color="49285bff";	colorFX=0;	shapeFX=0;	printID="ModTer/TTdirt01";	value=253;	health=3000;	level=100;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore2_7";	 name="Silver";				data=brickMMBrickGenericData;	color="e8e4e2ff";	colorFX=0;	shapeFX=0;	printID="ModTer/beach-sand";	value=441;	health=3750;	level=120;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore2_8";	 name="Titanium";				data=brickMMBrickGenericData;	color="2f2d2fff";	colorFX=0;	shapeFX=0;	printID="ModTer/contractor-rock";	value=529;	health=4500;	level=140;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore2_9";	 name="Ruby";					data=brickMMBrickGenericData;	color="bf1f21ff";	colorFX=0;	shapeFX=0;	printID="ModTer/whitesand";	value=705;	health=6000;	level=160;	hitSound="Quartz"; };
		new ScriptObject(MatterType) { codename="ore2_R";	 name="Mythril";				data=brickMMBrickGenericData;	color="89bc77ff";	colorFX=0;	shapeFX=1;	printID="ModTer/lava5";	value=5294;	health=30000;		level=40;	hitSound="Metal";	rare=true;};
		//T3, 1:8.75 Value/Health ratio
		new ScriptObject(MatterType) { codename="ore3_0";	 name="Tungsten";				data=brickMMBrickGenericData;	color="4b6926ff";	colorFX=0;	shapeFX=0;	printID="ModTer/Chiseled_Ice";value=1143;	health=10000;	level=160;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore3_1";	 name="Osmium";				data=brickMMBrickGenericData;	color="9ab6b5ff";	colorFX=0;	shapeFX=0;	printID="ModTer/snow4";	value=1371;	health=12000;	level=160;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore3_2";	 name="Uranium";				data=brickMMBrickGenericData;	color="007c3fff";	colorFX=0;	shapeFX=0;	printID="ModTer/marble";	value=1714;	health=15000;	level=160;	hitSound="Metal";	hitFunc="MM_RadDamage";			hitFuncArgs=5;  };
		new ScriptObject(MatterType) { codename="ore3_3";	 name="Ruthenium";			data=brickMMBrickGenericData;	color="d69c6bff";	colorFX=0;	shapeFX=0;	printID="ModTer/sand-texture5";	value=2286;	health=20000;	level=175;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore3_4";	 name="Neodymium";			data=brickMMBrickGenericData;	color="82281fff";	colorFX=0;	shapeFX=0;	printID="ModTer/rock";	value=3429;	health=30000;	level=190;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore3_5"; name="Iridium";				data=brickMMBrickGenericData;	color="a59059ff";	colorFX=0;	shapeFX=0;	printID="ModTer/ground";value=5143;	health=45000;	level=225;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore3_6"; name="Palladium";			data=brickMMBrickGenericData;	color="d15600ff";	colorFX=0;	shapeFX=0;	printID="ModTer/sand03";	value=6857;	health=60000;	level=240;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore3_7"; name="Thorium";				data=brickMMBrickGenericData;	color="000000ff";	colorFX=0;	shapeFX=0;	printID="ModTer/sand2";	value=9142;	health=80000;	level=260;	hitSound="Metal";	hitFunc="MM_RadDamage";			hitFuncArgs=15; };
		new ScriptObject(MatterType) { codename="ore3_8"; name="Gold";					data=brickMMBrickGenericData;	color="e2af13ff";	colorFX=0;	shapeFX=0;	printID="ModTer/Port_of_Taganrog";	value=11429;health=100000;	level=280;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore3_9"; name="Diamond";				data=brickMMBrickGenericData;	color="85acdb8e";	colorFX=0;	shapeFX=0;	printID="ModTer/lava5";	value=15287;health=133769;	level=300;	hitSound="Quartz"; };
		new ScriptObject(MatterType) { codename="ore3_R"; name="Dragonstone";			data=brickMMBrickGenericData;	color="ca959eff";	colorFX=0;	shapeFX=1;	printID="ModTer/lava5";	value=114659;	health=668845;		level=160;	hitSound="Quartz";	rare=true;};
		//T4, 1:9.00 Value/Health ratio
		new ScriptObject(MatterType) { codename="ore4_0"; name="Bismuth";				data=brickMMBrickGenericData;	color="877564ff";	colorFX=6;	shapeFX=0;	printID="ModTer/brickRAMP";	value=22222;health=200000;	level=480;	hitSound="Metal";};
		new ScriptObject(MatterType) { codename="ore4_1"; name="Helium";				data=brickMMBrickGenericData;	color="b8b3aaff";	colorFX=5;	shapeFX=1;	printID="Letters/-space";	value=24444;health=220000;		level=480;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore4_2"; name="Krypton";				data=brickMMBrickGenericData;	color="1f568cff";	colorFX=5;	shapeFX=1;	printID="Letters/-space";	value=25555;health=230000;		level=480;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore4_3"; name="Xenon";				data=brickMMBrickGenericData;	color="49285bff";	colorFX=5;	shapeFX=1;	printID="Letters/-space";	value=26666;health=240000;		level=500;	hitSound="Metal"; };
		new ScriptObject(MatterType) { codename="ore4_4"; name="Plutonium";			data=brickMMBrickGenericData;	color="264b38ff";	colorFX=2;	shapeFX=0;	printID="ModTer/marble";	value=27777;health=250000;	level=520;	hitSound="Metal";	harvestFunc="MM_HeatDamage";	harvestFuncArgs=5; };
		new ScriptObject(MatterType) { codename="ore4_5"; name="Actinium";				data=brickMMBrickGenericData;	color="85acdb8e";	colorFX=3;	shapeFX=0;	printID="ModTer/sand2";		value=41667;health=375000;	level=540;	hitSound="Metal";	harvestFunc="MM_HeatDamage";	harvestFuncArgs=10; };
		new ScriptObject(MatterType) { codename="ore4_6"; name="Promethium";			data=brickMMBrickGenericData;	color="d15600ff";	colorFX=3;	shapeFX=0;	printID="ModTer/contractor-rock";value=52777;health=475000;	level=560;	hitSound="Metal";	harvestFunc="MM_HeatDamage";	harvestFuncArgs=15; };
		new ScriptObject(MatterType) { codename="ore4_7"; name="Francium";				data=brickMMBrickGenericData;	color="dfc37cff";	colorFX=5;	shapeFX=0;	printID="ModTer/france";value=66666;health=600000;	level=600;	hitSound="Metal";	hitFunc="MM_RadDamage";	hitFuncArgs=5;	harvestFunc="MM_HeatDamage";	harvestFuncArgs=20; };
		new ScriptObject(MatterType) { codename="ore4_8"; name="Astatine";				data=brickMMBrickGenericData;	color="e8e4e2ff";	colorFX=3;	shapeFX=0;	printID="Letters/-space";	value=88888;health=800000;	level=660;	hitSound="Metal";	hitFunc="MM_RadDamage";	hitFuncArgs=10;	harvestFunc="MM_HeatDamage";	harvestFuncArgs=25; };
		new ScriptObject(MatterType) { codename="ore4_9"; name="Americium";			data=brickMMBrickGenericData;	color="e8e4e2ff";	colorFX=0;	shapeFX=2;	printID="ModTer/america";	value=111308;health=1001776;	level=725;	hitSound="Metal";	hitFunc="MM_RadDamage";	hitFuncArgs=15;	harvestFunc="MM_HeatDamage";	harvestFuncArgs=30; };
		new ScriptObject(MatterType) { codename="ore4_R"; name="Sturdium";				data=brickMMBrickGenericData;	color="46809eff";	colorFX=0;	shapeFX=0;	printID="ModTer/brickTOP";	value=1000000;health=6000000;	level=480;	hitSound="Metal";	rare=true; };
		//T5, 1:10.00 Value/Health ratio
		new ScriptObject(MatterType) { codename="ore5_0"; name="Naquadah";				data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=200000;	health=2000000;		level=725;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_1"; name="Draconium";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=250000;	health=2500000;		level=725;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_2"; name="Duranium";				data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=350000;	health=3500000;		level=725;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_3"; name="Tritanium";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=500000;	health=5000000;		level=750;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_4"; name="Einsteinium";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=700000;	health=7000000;		level=775;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_5"; name="Oganesson";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=1000000;	health=10000000;	level=825;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_6"; name="Supermatter";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=1250000;	health=12500000;	level=875;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_7"; name="Antimatter";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=1500000;	health=15000000;	level=950;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_8"; name="Neutronium";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=1750000;	health=17500000;	level=1025;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_9"; name="Unobtanium";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=2000000;	health=20000000;	level=1125;	hitSound="Granite"; };
		new ScriptObject(MatterType) { codename="ore5_R"; name="Infinitium";			data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID="ModTer/granite";	value=10000000;	health=60000000;	level=725;	hitSound="Granite";	rare=true; };
		//Ultimate Ore, 1:1 Value/Health ratio
		new ScriptObject(MatterType) { codename="ore6_R"; name="Dog";					data=brickMMBrickGenericData;	color="e8e4e2ff";	colorFX=0;	shapeFX=0;	printID="ModTer/wtf";		value=100000000;health=100000000;	level=5000;	hitSound="Granite";	rare=true;	hitFunc="MM_RadDamage";	hitFuncArgs=69;};

		//Loot Caches

		new ScriptObject(MatterType) { codename="cache1"; name="Basic Loot Cache";			data=brickMMBrickBoxData;	color="4f494bff";	colorFX=0;	shapeFX=0;	printID="ModTer/TTdirt01";	value=10;	unsellable=true;	health=220;		level=7;	hitSound="Wood";	unobtainable=true;	harvestFunc="MM_GetLootCache";	harvestFuncArgs=1; }; //0.309804 0.286275 0.294118 1.000000
		new ScriptObject(MatterType) { codename="cache2"; name="Improved Loot Cache";		data=brickMMBrickBoxData;	color="d8d1ccff";	colorFX=0;	shapeFX=0;	printID="ModTer/TTdirt01";	value=10;	unsellable=true;	health=1320;	level=44;	hitSound="Wood";	unobtainable=true;	harvestFunc="MM_GetLootCache";	harvestFuncArgs=2; }; //0.847059 0.819608 0.800000 1.000000
		new ScriptObject(MatterType) { codename="cache3"; name="Superior Loot Cache";		data=brickMMBrickBoxData;	color="1f568cff";	colorFX=1;	shapeFX=0;	printID="ModTer/TTdirt01";	value=10;	unsellable=true;	health=4440;	level=166;	hitSound="Wood";	unobtainable=true;	harvestFunc="MM_GetLootCache";	harvestFuncArgs=3; }; //0.121569 0.337255 0.549020 1.000000
		new ScriptObject(MatterType) { codename="cache4"; name="Epic Loot Cache";			data=brickMMBrickBoxData;	color="49285bff";	colorFX=1;	shapeFX=0;	printID="ModTer/TTdirt01";	value=10;	unsellable=true;	health=6660;	level=488;	hitSound="Wood";	unobtainable=true;	harvestFunc="MM_GetLootCache";	harvestFuncArgs=4; }; //0.286275 0.156863 0.356863 1.000000
		new ScriptObject(MatterType) { codename="cache5"; name="Legendary Loot Cache";		data=brickMMBrickBoxData;	color="bf1f21ff";	colorFX=6;	shapeFX=0;	printID="ModTer/TTdirt01";	value=10;	unsellable=true;	health=8880;	level=777;	hitSound="Wood";	unobtainable=true;	harvestFunc="MM_GetLootCache";	harvestFuncArgs=5; }; //0.749020 0.121569 0.129412 1.000000
	
		//Hazardous

		new ScriptObject(MatterType) { codename="magma"; name="Magma";				data=brickMMBrickGenericData;	color="ff0000ff";	colorFX=3;	shapeFX=2;	printID="Letters/-space";	value=5;	health=50;	level=10;	hitSound="Granite";	hazard=true;	harvestFunc="MM_HeatDamage";	harvestFuncArgs=20; };
		new ScriptObject(MatterType) { codename="waste"; name="Radioactive Waste";	data=brickMMBrickGenericData;	color="00ff00ff";	colorFX=3;	shapeFX=2;	printID="Letters/-space";	value=5;	health=50;	level=10;	hitSound="Granite";	hazard=true;	hitFunc="MM_RadDamage";			hitFuncArgs=125; };

		new ScriptObject(MatterType) { codename="cancer"; name="Cancerous Growth";		data=brickMMBrickGenericData;	color="b59239ff";	colorFX=1;	shapeFX=2;	printID="ModTer/raw-ground-beef";	value=5;	health=50;	level=10;	hitSound="Meat";	hazard=true;	unobtainable=true;	harvestFunc="MM_CancerSpread";	harvestFuncArgs=3;	};
		new ScriptObject(MatterType) { codename="pon"; name="Pocket of Nothing";	data=brickMMBrickGenericData;	color="000000ff";	colorFX=1;	shapeFX=2;	printID="ModTer/raw-ground-beef";	value=5;	health=50;	level=10;	hitSound="Granite";	hazard=true;	unobtainable=true;	harvestFunc="MM_CancerSpread";	harvestFuncArgs=5;	};
		new ScriptObject(MatterType) { codename="cv"; name="Condensed Void";		data=brickMMBrickGenericData;	color="000000ff";	colorFX=1;	shapeFX=2;	printID="ModTer/raw-ground-beef";	value=5;	health=50;	level=10;	hitSound="Quartz";	hazard=true;	unobtainable=true;	harvestFunc="MM_BrickExplosion";	harvestFuncArgs=boomDatablock;	};

		//Its orbin' time!

		new ScriptObject(MatterType) { codename="forb"; name="Frenzy Orb";				data=brickMMBrickOrbData;	color="89a3b8ff";	colorFX=3;	shapeFX=1;	value=50;	health=1000000;	level=10;	hitSound="Wood";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_ServerBuff";	harvestFuncArgs="Frenzy";	hitFunc="MM_RadDamage";			hitFuncArgs=2;  };
		new ScriptObject(MatterType) { codename="lorb"; name="Lotto Orb";				data=brickMMBrickOrbData;	color="89bc77ff";	colorFX=3;	shapeFX=1;	value=100;	health=1000000;	level=10;	hitSound="Wood";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_ServerBuff";	harvestFuncArgs="Lotto";	hitFunc="MM_RadDamage";			hitFuncArgs=2;  };
		new ScriptObject(MatterType) { codename="borb"; name="Berserk Orb";				data=brickMMBrickOrbData;	color="db9085ff";	colorFX=3;	shapeFX=1;	value=200;	health=1000000;	level=10;	hitSound="Wood";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_ServerBuff";	harvestFuncArgs="Berserk";	hitFunc="MM_RadDamage";			hitFuncArgs=2;  };
		new ScriptObject(MatterType) { codename="morb"; name="Mythical Orb";				data=brickMMBrickOrbData;	color="dfc37cff";	colorFX=2;	shapeFX=1;	value=400;	health=3000000;	level=10;	hitSound="Wood";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_ServerBuff";	harvestFuncArgs="Mystical";	hitFunc="MM_RadDamage";			hitFuncArgs=8;  };
		new ScriptObject(MatterType) { codename="eorb"; name="Extender Orb";				data=brickMMBrickOrbData;	color="e8e4e2ff";	colorFX=6;	shapeFX=1;	value=800;	health=750000;	level=10;	hitSound="Wood";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_ServerBuff";	harvestFuncArgs="Extender";	hitFunc="MM_RadDamage";			hitFuncArgs=4;  };

		//Walls and Constructables

		new ScriptObject(MatterType) { codename="wall_crate"; name="Crate";					data=brickMMBrickGenericData;	color="775238ff";	colorFX=0;	shapeFX=0;	printID="ModTer/bricks";	value=25;	health=100000;	level=10;	hitSound="Wood";	unobtainable=true;  };
		new ScriptObject(MatterType) { codename="plasteel"; name="PlaSteel";					data=brickMMBrickGenericData;	color="797260ff";	colorFX=0;	shapeFX=0;	printID="ModTer/bricks";	value=2;	health=100;		level=5;	hitSound="Metal";	unsellable=true; };
		new ScriptObject(MatterType) { codename="parts1"; name="Frame Parts";				data=brickMMBrickGenericData;	color="4f494bff";	colorFX=0;	shapeFX=0;	printID="ModTer/bricks";	value=2;	health=100;		level=5;	hitSound="Metal";	unsellable=true; };
		new ScriptObject(MatterType) { codename="parts2"; name="Mechanism Parts";			data=brickMMBrickGenericData;	color="d8d1ccff";	colorFX=0;	shapeFX=0;	printID="ModTer/bricks";	value=2;	health=100;		level=5;	hitSound="Metal";	unsellable=true; };
		new ScriptObject(MatterType) { codename="parts3"; name="Circuitry Parts";			data=brickMMBrickGenericData;	color="1f568cff";	colorFX=0;	shapeFX=0;	printID="ModTer/bricks";	value=2;	health=100;		level=5;	hitSound="Metal";	unsellable=true; };
		new ScriptObject(MatterType) { codename="parts4"; name="Computation Parts";		data=brickMMBrickGenericData;	color="49285bff";	colorFX=0;	shapeFX=0;	printID="ModTer/bricks";	value=2;	health=100;		level=5;	hitSound="Metal";	unsellable=true; };
		new ScriptObject(MatterType) { codename="parts5"; name="Sentient Parts";			data=brickMMBrickGenericData;	color="bf1f21ff";	colorFX=0;	shapeFX=0;	printID="ModTer/bricks";	value=2;	health=100;		level=5;	hitSound="Metal";	unsellable=true; };
		
		//Spawner Crystals/Building bases

		new ScriptObject(MatterType) { codename="crystal1"; name="Dirty Crystal";			data=brickMMBrickCrystalData;	color="ffffffff";	colorFX=0;	shapeFX=0;	value=500;	health=10000;	level=11;	hitSound="Quartz";	SurroundCheck="Force";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_CrystalBreak";	harvestFuncArgs="MM_Crate";			hitFunc="MM_RadDamage";			hitFuncArgs=5;  };
		new ScriptObject(MatterType) { codename="crystal2"; name="Dirtier Crystal";			data=brickMMBrickCrystalData;	color="706e6eff";	colorFX=0;	shapeFX=0;	value=1000;	health=40000;	level=222;	hitSound="Quartz";	SurroundCheck="Force";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_CrystalBreak";	harvestFuncArgs="MM_SuperCrate";	hitFunc="MM_RadDamage";			hitFuncArgs=10;  };
		new ScriptObject(MatterType) { codename="crystal3"; name="Dirtiest Crystal";			data=brickMMBrickCrystalData;	color="000000ff";	colorFX=0;	shapeFX=0;	value=1500;	health=160000;	level=555;	hitSound="Quartz";	SurroundCheck="Force";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_CrystalBreak";	harvestFuncArgs="MM_MegaCrate";		hitFunc="MM_RadDamage";			hitFuncArgs=20;  };
		
		new ScriptObject(MatterType) { codename="crystalR"; name="Rainbow Crystal";			data=brickMMBrickCrystalData;	color="ffffffff";	colorFX=6;	shapeFX=0;	value=250;	health=7500;	level=11;	hitSound="Quartz";	SurroundCheck="Force";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_CrystalBreak";	harvestFuncArgs="MM_RainbowCrate";  };
		//Divine Crystal - Legendary shop

		new ScriptObject(MatterType) { codename="remaint"; name="Purified Avarice";			data=brickMMBrickExoticData;	color="ffffffff";	colorFX=3;	shapeFx=0;	value=1;	health="1000000"TAB"1999999";	level=160;	hitSound="Quartz";	SurroundCheck="Force";	unobtainable=true; bombResist=1.0;	harvestFunc="MM_AvariceBreak";	};
		
		new ScriptObject(MatterType) { codename="corium"; name="Flesh-Ridden Corium";		data=brickMMBrickGenericData;	color="561f1cff";	colorFX=0;	shapeFX=0;	printID="ModTer/raw-ground-beef";	value=666;	health=666666;	level=480;	hitSound="Meat";	unobtainable=true;	harvestFunc="MM_CancerSpread";	harvestFuncArgs=2;	};
		
		//Fluids

		new ScriptObject(MatterType) { codename="biomatter"; name="Biomatter";				data=brickMMBrickFluidPoolData;	color="00342aff";	colorFX=0;	shapeFX=0;	value=50;	health=10000;	level=10;	canPump=true;	SurroundCheck="Force";	unobtainable=true;	unsellable=true; };
		new ScriptObject(MatterType) { codename="capitalism"; name="Crude Oil";				data=brickMMBrickFluidPoolData;	color="2f1c11ff";	colorFX=0;	shapeFX=0;	value=50;	health=10000;	level=10;	canPump=true;	SurroundCheck="Force";	unobtainable=true;	unsellable=true; };

		//Misc. Non-Physical Materials

		new ScriptObject(MatterType) { codename="credits"; name="Credits";		color="89bc77ff";	value=1;	unsellable=true; };
		new ScriptObject(MatterType) { codename="fuel"; name="Drill Fuel";	color="2f1c11ff";	value=2;	unsellable=true; };
		new ScriptObject(MatterType) { codename="penny"; name="Penny";		color="953800ff";	value=0.01;	unsellable=true;	keepOnAscend=true; };
		new ScriptObject(MatterType) { codename="scrip"; name="Scrip";		color="ffffffff";	value=777;	unsellable=true;	keepOnAscend=true; };
		new ScriptObject(MatterType) { codename="debug"; name="Infinity";		color="ffffffff";	value=0;	unsellable=true;	keepOnAscend=true; };
	};
}
SetupMatterData();

function GetMatterType(%type)
{
	if (!isObject($MM::MatterType[%type]))
		for (%i = 0; %i < MatterData.getCount(); %i++)
			if (MatterData.getObject(%i).name $= %type)
				$MM::MatterType[%type] = MatterData.getObject(%i);

	return $MM::MatterType[%type];
}

function GetMatterValue(%type)
{
	%matter = %type;
	if (isObject(%matter) || isObject(%matter = GetMatterType(%type)))
		return mRound(%matter.value * $MM::EconomyYield * (1 + $MM::ServerBuffLevel["Frenzy"]));

	return -1;
}