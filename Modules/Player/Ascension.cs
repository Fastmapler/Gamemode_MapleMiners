function GetAscensionTokenReward(%val)
{
    return bigint_div(PickaxeUpgradeCostSum(%val), 2000000);
}