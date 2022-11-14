function GetAscensionTokenReward(%val)
{
    if (%val <= 2200)
        return bigint_divmod(PickaxeUpgradeCostSum(%val), 2000000);

    return GetAscensionTokenReward(2200) + mRound((%val - 2200) / 2);
    //After level 2200 we get an addition token effectively every other level.
    //Might as well simulate it rather than actually use the more computationally expensive function.
}

function GameConnection::GetAscensionTokenReward(%client)
{
    return GetAscensionTokenReward(%client.MM_PickaxeLevel);
}

function GameConnection::ConfirmAscend(%client)
{
    %client.AddMaterial(%client.GetAscensionTokenReward(), "Scrip");

    for (%i = 0; %i < MatterData.getCount(); %i++)
			if (!MatterData.getObject(%i).keepOnAscend)
				%client.MM_Material[MatterData.getObject(%i).name] = "";

    %client.AddMaterial(25, "Credits");
    %client.MM_PickaxeLevel = 5;
}