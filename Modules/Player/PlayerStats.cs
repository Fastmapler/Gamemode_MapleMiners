function GameConnection::InitPlayerStats(%client)
{
    %client.MM_Credits = 25;
    %client.MM_PickaxeLevel = 5;

    for (%i = 0; %i < MatterData.getCount(); %i++)
        %client.MM_Materials[MatterData.getObject(%i)] = 0;

    %client.MM_MaxSpareBatteries = 0;
    %client.MM_SpareBatteries = 0;
    %client.MM_BatteryCharge = 1000 * 10;
}

function GameConnection::PrintMMStats(%client)
{
	if (!isObject(%player = %client.player))
		return;

	%credits = "\c7| \c3" @ (%client.MM_Credits + 0) @ "\c6cr" SPC "\c7|";
	
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

	%battery = "\c6(" @ (%client.MM_SpareBatteries + 0) @ "x) [\c4" @ %bar0 @ "\c7" @ %bar1 @ "\c6} \c7|";
	%text = "<just:center>" @ %credits SPC %health SPC %battery;
	%client.MM_BottomPrint(%text, 2);
}

function MM_PlayerStatLoop()
{
	cancel($MM::PlayerStatLoop);

	for (%i = 0; %i < ClientGroup.getCount(); %i++)
		ClientGroup.getObject(%i).PrintMMStats();

	$MM::PlayerStatLoop = schedule(200, 0, "MM_PlayerStatLoop");
}
schedule(10, 0, "MM_PlayerStatLoop");