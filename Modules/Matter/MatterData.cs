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
		new ScriptObject(MatterType) { name="Dirt";					data=brickMMBrickGenericData;	color="b6a593ff";	colorFX=0;	shapeFX=0;	printID=80;	value=0;	health=20;		level=5;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Packed Dirt";			data=brickMMBrickGenericData;	color="877564ff";	colorFX=0;	shapeFX=0;	printID=80;	value=0;	health=100;		level=20;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Compressed Dirt";		data=brickMMBrickGenericData;	color="605042ff";	colorFX=0;	shapeFX=0;	printID=80;	value=0;	health=200;		level=40;	hitSound="Stone"; };

		new ScriptObject(MatterType) { name="Stone";				data=brickMMBrickGenericData;	color="a5a189ff";	colorFX=0;	shapeFX=0;	printID=83;	value=0;	health=320;		level=80;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Packed Stone";			data=brickMMBrickGenericData;	color="797260ff";	colorFX=0;	shapeFX=0;	printID=83;	value=0;	health=460;		level=120;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Compressed Stone";		data=brickMMBrickGenericData;	color="504b3fff";	colorFX=0;	shapeFX=0;	printID=83;	value=0;	health=620;		level=160;	hitSound="Stone"; };

		new ScriptObject(MatterType) { name="Bedrock";				data=brickMMBrickGenericData;	color="4f494bff";	colorFX=0;	shapeFX=0;	printID=76;	value=0;	health=800;		level=220;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Packed Bedrock";		data=brickMMBrickGenericData;	color="2f2d2fff";	colorFX=0;	shapeFX=0;	printID=76;	value=0;	health=1000;	level=280;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Compressed Bedrock";	data=brickMMBrickGenericData;	color="18161aff";	colorFX=0;	shapeFX=0;	printID=76;	value=0;	health=1220;	level=340;	hitSound="Stone"; };

		new ScriptObject(MatterType) { name="Slade";				data=brickMMBrickGenericData;	color="000000ff";	colorFX=0;	shapeFX=0;	printID=98;	value=0;	health=999999;	level=999999;	hitSound="Stone"; };

		new ScriptObject(MatterType) { name="Fleshrock";			data=brickMMBrickGenericData;	color="931f23ff";	colorFX=0;	shapeFX=0;	printID=88;	value=0;	health=400;		level=60;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Voidstone";			data=brickMMBrickGenericData;	color="ffffff01";	colorFX=3;	shapeFX=0;	printID=36;	value=0;	health=1240;	level=190;	hitSound="Stone"; };
		new ScriptObject(MatterType) { name="Mayhemium";			data=brickMMBrickGenericData;	color="e8e4e2ff";	colorFX=6;	shapeFX=1;	printID=97;	value=0;	health=2440;	level=400;	hitSound="Stone"; };

		//Ores
		
		new ScriptObject(MatterType) { name="Granite";				data=brickMMBrickGenericData;	color="9e7250ff";	colorFX=0;	shapeFX=0;	printID=79;	value=5;	health=50;		level=1;	hitSound="Granite"; };
		new ScriptObject(MatterType) { name="Copper";				data=brickMMBrickGenericData;	color="953800ff";	colorFX=0;	shapeFX=0;	printID=90;	value=25;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Tin";					data=brickMMBrickGenericData;	color="706e6eff";	colorFX=0;	shapeFX=0;	printID=89;	value=20;	health=100;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Iron";					data=brickMMBrickGenericData;	color="4f494bff";	colorFX=0;	shapeFX=0;	printID=95;	value=35;	health=175;		level=6;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Zinc";					data=brickMMBrickGenericData;	color="89a3b8ff";	colorFX=0;	shapeFX=0;	printID=84;	value=45;	health=190;		level=10;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Aluminum";				data=brickMMBrickGenericData;	color="d8d1ccff";	colorFX=0;	shapeFX=0;	printID=94;	value=50;	health=220;		level=15;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Antimony";				data=brickMMBrickGenericData;	color="a3b56bff";	colorFX=0;	shapeFX=0;	printID=107;value=55;	health=240;		level=20;	hitSound="Granite"; };
		new ScriptObject(MatterType) { name="Gallium";				data=brickMMBrickGenericData;	color="99958cff";	colorFX=0;	shapeFX=0;	printID=77;	value=75;	health=280;		level=25;	hitSound="Granite"; };
		new ScriptObject(MatterType) { name="Quartz";				data=brickMMBrickGenericData;	color="bcc1c88e";	colorFX=0;	shapeFX=0;	printID=99;	value=60;	health=260;		level=35;	hitSound="Quartz"; };
		new ScriptObject(MatterType) { name="Cobalt";				data=brickMMBrickGenericData;	color="1f568cff";	colorFX=0;	shapeFX=0;	printID=97;	value=100;	health=300;		level=45;	hitSound="Metal"; };
		
		new ScriptObject(MatterType) { name="Garnet";				data=brickMMBrickGenericData;	color="d15600ff";	colorFX=0;	shapeFX=0;	printID=99;	value=10;	health=125;		level=5;	hitSound="Quartz"; };
		new ScriptObject(MatterType) { name="Graphite";				data=brickMMBrickGenericData;	color="000000ff";	colorFX=0;	shapeFX=0;	printID=36;	value=10;	health=125;		level=5;	hitSound="Granite"; };
		new ScriptObject(MatterType) { name="Tungsten";				data=brickMMBrickGenericData;	color="4b6926ff";	colorFX=0;	shapeFX=0;	printID=105;value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Lithium";				data=brickMMBrickGenericData;	color="b6a593ff";	colorFX=0;	shapeFX=0;	printID=96;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Fluorite";				data=brickMMBrickGenericData;	color="9ab6b5ff";	colorFX=0;	shapeFX=0;	printID=79;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Nickel";				data=brickMMBrickGenericData;	color="dfc37cff";	colorFX=0;	shapeFX=0;	printID=90;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Silver";				data=brickMMBrickGenericData;	color="e8e4e2ff";	colorFX=0;	shapeFX=0;	printID=84;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Apatite";				data=brickMMBrickGenericData;	color="0c2a49ff";	colorFX=0;	shapeFX=0;	printID=83;	value=10;	health=125;		level=5;	hitSound="Granite"; };
		new ScriptObject(MatterType) { name="Titanium";				data=brickMMBrickGenericData;	color="2f2d2fff";	colorFX=0;	shapeFX=0;	printID=81;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Ruby";					data=brickMMBrickGenericData;	color="bf1f21ff";	colorFX=0;	shapeFX=0;	printID=86;	value=10;	health=125;		level=5;	hitSound="Quartz"; };
		
		new ScriptObject(MatterType) { name="Lead";					data=brickMMBrickGenericData;	color="49285bff";	colorFX=0;	shapeFX=0;	printID=88;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Osmium";				data=brickMMBrickGenericData;	color="9ab6b5ff";	colorFX=0;	shapeFX=0;	printID=89;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Uranium";				data=brickMMBrickGenericData;	color="007c3fff";	colorFX=0;	shapeFX=0;	printID=77;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Ruthenium";			data=brickMMBrickGenericData;	color="d69c6bff";	colorFX=0;	shapeFX=0;	printID=93;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Neodymium";			data=brickMMBrickGenericData;	color="82281fff";	colorFX=0;	shapeFX=0;	printID=95;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Iridium";				data=brickMMBrickGenericData;	color="a59059ff";	colorFX=0;	shapeFX=0;	printID=100;value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Palladium";			data=brickMMBrickGenericData;	color="d15600ff";	colorFX=0;	shapeFX=0;	printID=92;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Thorium";				data=brickMMBrickGenericData;	color="000000ff";	colorFX=0;	shapeFX=0;	printID=91;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Gold";					data=brickMMBrickGenericData;	color="e2af13ff";	colorFX=0;	shapeFX=0;	printID=96;	value=10;	health=125;		level=5;	hitSound="Metal"; };
		new ScriptObject(MatterType) { name="Diamond";				data=brickMMBrickGenericData;	color="85acdb8e";	colorFX=0;	shapeFX=0;	printID=99;	value=10;	health=125;		level=5;	hitSound="Quartz"; };
		
		new ScriptObject(MatterType) { name="Sturdium";				data=brickMMBrickGenericData;	color="46809eff";	colorFX=0;	shapeFX=0;	printID=106;value=10;	health=125;		level=5;	hitSound="Metal"; };
	};
}
SetupMatterData();

function GetMatterType(%type)
{
	if (!isObject($EOTW::MatterType[%type]))
		for (%i = 0; %i < MatterData.getCount(); %i++)
			if (MatterData.getObject(%i).name $= %type)
				$EOTW::MatterType[%type] = MatterData.getObject(%i);

	return $EOTW::MatterType[%type];
}