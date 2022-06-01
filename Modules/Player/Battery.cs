$MM::MaxBatteryCharge = 10000;

function GameConnection::ChangeBatteryEnergy(%client, %change)
{
    if (%change < 0)
    {
        if (%client.MM_BatteryCharge <= 0 && %client.MM_SpareBatteries <= 0)
            return false;

        while (%change < 0)
        {
            %chargeDiff = getMax(%client.MM_BatteryCharge * -1, %change);
            %client.MM_BatteryCharge += %chargeDiff;
            %change -= %chargeDiff;
            echo("DOWN" SPC %chargeDiff SPC %client.MM_BatteryCharge SPC %change);
            if (%client.MM_BatteryCharge <= 0)
            {
                if (%client.MM_SpareBatteries > 0)
                {
                    %client.MM_BatteryCharge += $MM::MaxBatteryCharge;
                    %client.MM_SpareBatteries--;
                }
                else
                {
                    %client.MM_BatteryCharge = 0;
                    break;
                }
            }
        }

        return true;
    }
    else if (%change > 0)
    {
        while (%change > 0)
        {
            %chargeDiff = getMin($MM::MaxBatteryCharge - %client.MM_BatteryCharge, %change);
            %client.MM_BatteryCharge += %chargeDiff;
            %change -= %chargeDiff;
            echo("CHARGE" SPC %chargeDiff SPC %client.MM_BatteryCharge SPC %change);
            if (%client.MM_BatteryCharge >= $MM::MaxBatteryCharge)
            {
                if (%client.MM_SpareBatteries < %client.MM_MaxSpareBatteries && %change > 0)
                {
                    %client.MM_BatteryCharge -= $MM::MaxBatteryCharge;
                    %client.MM_SpareBatteries++;
                }
                else
                {
                    %client.MM_BatteryCharge = $MM::MaxBatteryCharge;
                    break;
                }
            }
        }

        return true;
    }
}