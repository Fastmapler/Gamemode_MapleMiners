$MM::ModuleTickRate = 10;

function Player::ModuleTick(%player)
{
    cancel(%player.ModuleTickSchedule);

    if (!isObject(%client = %player.client))
        return;

    %mods = %player.MM_ActivatedModules;
    for (%i = 0; %i < getFieldCount(%mods); %i++)
    {
        %funcName = "MM_Module" @ getField(%mods, %i);
        if (!isFunction(%funcName))
            continue;

        %ret = call(%funcName, %player);

        if (!%ret)
        {
            %player.MM_ActivatedModules = "";
            %client.chatMessage("You ran out of power!");
            break;
        }
    }

    %player.ModuleTickSchedule = %player.schedule(1000 / $MM::ModuleTickRate, "ModuleTick");
}

function MM_ModuleHeatShield(%player)
{
    return %player.client.ChangeBatteryEnergy($MM::MaxBatteryCharge / (-100 * $MM::ModuleTickRate));
}