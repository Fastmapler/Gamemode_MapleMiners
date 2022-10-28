function GameConnection::InitPlayerStats(%client)
{
	%client.MM_LoadData();

	if (%client.MM_PickaxeLevel $= "")
	{
		%client.AddMaterial(25, "Credits");
		%client.MM_PickaxeLevel = 5;

		%client.MM_MaxSpareBatteries = 0;
		%client.MM_SpareBatteries = 0;
		%client.MM_BatteryCharge = $MM::MaxBatteryCharge;

		%client.MM_MaxToolStorage = 0;
	}
}

function GameConnection::PrintMMStats(%client)
{
	if (!isObject(%player = %client.player))
		return;

	%credits = "\c7| \c3" @ getNiceNumber(%client.getMaterial("Credits")) @ "\c6cr \c7|";

    %level = "\c6LVL\c3" SPC (%client.MM_PickaxeLevel + 0) SPC "\c7|";
	
	%health = mCeil(100 * (1 - %player.getDamagePercent())) @ "\c6\% HP \c7|";
	if (%player.getDamagePercent() > 0.75)
		%health = "<color:ff0000>" @ %health;
	else if (%player.getDamagePercent() > 0.25)
		%health = "<color:ffff00>" @ %health;
	else
		%health = "<color:00ff00>" @ %health;

	%barCount = 10;
	%bars = mCeil(%barCount * (%client.MM_BatteryCharge / $MM::MaxBatteryCharge));
	for(%i = 0; %i < %bars; %i++)
		%bar0 = %bar0 @ "|";
	for(%i = %bars; %i < %barCount; %i++)
		%bar1 = %bar1 @ "|";

	if (%player.MM_RadLevel > 0)
	{
		%rads = mCeil(%player.MM_RadLevel) @ "\c6rads \c7|";
		if (isEventPending(%player.RadPoisonTickSchedule))
			%rads = "\c7|" SPC "<color:661111>" @ %rads;
		if (%player.MM_RadLevel > $MM::SafeRadLimit)
			%rads = "\c7|" SPC "<color:ff0000>" @ %rads;
		else if (%player.MM_RadLevel > $MM::SafeRadLimit / 2)
			%rads = "\c7|" SPC "<color:ffff00>" @ %rads;
		else
			%rads = "\c7|" SPC "<color:00ff00>" @ %rads;

	}

	if (isObject(%player.DrillStatic))
	{
		%fuel = "\c3" @ mCeil(%client.getMaterial("Drill Fuel")) @ "\c6 Fuel \c7|";
	}

	%battery = "\c6(" @ (%client.MM_SpareBatteries + 0) @ "|" @ (%client.MM_MaxSpareBatteries + 0) @ ") [" @ (getSimTime() - %client.MM_LastBatteryDrain <= 150 ? "\c0" : "\c3") @ %bar0 @ "\c7" @ %bar1 @ "\c6} \c7|";
	%text = "<just:center>" @ %credits SPC %level SPC %health SPC %battery NL %rads SPC %fuel;
	%client.MM_BottomPrint(%text, 2);

	if (%client.MM_PickaxeLevel != %client.score)
	{
		%client.setScore(%client.MM_PickaxeLevel);

		//0 = red
		//1 = blue
		//2 = green
		//3 = yellow
		//4 = cyan
		//5 = pink
		//6 = white
		//7 = gray
		//8 = black

			 if (%client.MM_PickaxeLevel >= 1025)
			%clanColor = "\c8";
		else if  (%client.MM_PickaxeLevel >= 725)
			%clanColor = "\c5";
		else if  (%client.MM_PickaxeLevel >= 480)
			%clanColor = "\c0";
		else if  (%client.MM_PickaxeLevel >= 240)
			%clanColor = "\c4";
		else if  (%client.MM_PickaxeLevel >= 160)
			%clanColor = "\c3";
		else if  (%client.MM_PickaxeLevel >= 80)
			%clanColor = "\c2";
		else if  (%client.MM_PickaxeLevel >= 40)
			%clanColor = "\c1";
		else if  (%client.MM_PickaxeLevel >= 20)
			%clanColor = "\c6";
		else
			%clanColor = "\c7";

		%client.clanPrefix = "\c6[" @ %clanColor @ %client.MM_PickaxeLevel @ "\c6] ";
	}
}

function MM_PlayerStatLoop()
{
	cancel($MM::PlayerStatLoop);

	for (%i = 0; %i < ClientGroup.getCount(); %i++)
		ClientGroup.getObject(%i).PrintMMStats();

	$MM::PlayerStatLoop = schedule(100, 0, "MM_PlayerStatLoop");
}
schedule(10, 0, "MM_PlayerStatLoop");
