function GetAscensionTokenReward(%val)
{
    return bigint_divmod(PickaxeUpgradeCostSum(%val), 2000000);
}