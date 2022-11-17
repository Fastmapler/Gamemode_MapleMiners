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

$MM::AscensionData["Legacy", "Cost"] = 1;
$MM::AscensionData["Legacy", "Prereq"] = "";
$MM::AscensionData["Legacy", "Fluff"] = "You have learned from your previous character's past.";
$MM::AscensionData["Legacy", "Discription"] = "Your mininum pickaxe level is now 20.";

$MM::AscensionData["Maxwell!", "Cost"] = 1337;
$MM::AscensionData["Maxwell!", "Prereq"] = "";
$MM::AscensionData["Maxwell!", "Fluff"] = "meow mrw";
$MM::AscensionData["Maxwell!", "Discription"] = "Goldifies Maxwell while held. Togglable.";

$MM::AscensionData["Farming", "Cost"] = 1;
$MM::AscensionData["Farming", "Prereq"] = "Legacy";
$MM::AscensionData["Farming", "Fluff"] = "farming 2";
$MM::AscensionData["Farming", "Discription"] = "Unlocks access to your personal farm, enabling the growth of plants and the creation of powerful potions.";

$MM::AscensionData["GMO Fertlizer", "Cost"] = 1;
$MM::AscensionData["GMO Fertlizer", "Prereq"] = "Farming";
$MM::AscensionData["GMO Fertlizer", "Fluff"] = "Patience is NOT key. I need my plants and I need them now!";
$MM::AscensionData["GMO Fertlizer", "Discription"] = "Plants grow to maturity 100% faster.";

$MM::AscensionData["Floro-Freeze Ray", "Cost"] = 1;
$MM::AscensionData["Floro-Freeze Ray", "Prereq"] = "Farming";
$MM::AscensionData["Floro-Freeze Ray", "Fluff"] = "Don't you dare die on me!";
$MM::AscensionData["Floro-Freeze Ray", "Discription"] = "Plants take 300% as long to die of age after maturity.";

$MM::AscensionData["Acheing in Acres I", "Cost"] = 1;
$MM::AscensionData["Acheing in Acres I", "Prereq"] = "Farming";
$MM::AscensionData["Acheing in Acres I", "Fluff"] = "From Backyard to Field.";
$MM::AscensionData["Acheing in Acres I", "Discription"] = "Grants 2x farm space from base amount.";

$MM::AscensionData["Horader", "Cost"] = 1;
$MM::AscensionData["Horader", "Prereq"] = "Legacy";
$MM::AscensionData["Horader", "Fluff"] = "Still not as bad as the Penny Mongler... Wait who?";
$MM::AscensionData["Horader", "Discription"] = "Doubles your tool storage bank space.";

$MM::AscensionData["Dexterious Might I", "Cost"] = 1;
$MM::AscensionData["Dexterious Might I", "Prereq"] = "Legacy";
$MM::AscensionData["Dexterious Might I", "Fluff"] = "Still not as bad as the Penny Mongler... Wait who?";
$MM::AscensionData["Dexterious Might I", "Discription"] = "Doubles your tool storage bank space.";

$MM::AscensionData["Personalized Ore", "Cost"] = 1;
$MM::AscensionData["Personalized Ore", "Prereq"] = "Legacy";
$MM::AscensionData["Personalized Ore", "Fluff"] = "Every Ore is personalized.";
$MM::AscensionData["Personalized Ore", "Discription"] = "Unlocks your every own personal ore! it will spawn in the mines and can be upgraded.";

$MM::AscensionData["Cache Smasher", "Cost"] = 1;
$MM::AscensionData["Cache Smasher", "Prereq"] = "Legacy";
$MM::AscensionData["Cache Smasher", "Fluff"] = "Was opening loot caches really that hard?";
$MM::AscensionData["Cache Smasher", "Discription"] = "You can now mine and open loot caches regardless of pickaxe level.";

$MM::AscensionData["Wormhole Phone", "Cost"] = 1;
$MM::AscensionData["Wormhole Phone", "Prereq"] = "Legacy";
$MM::AscensionData["Wormhole Phone", "Fluff"] = "It is like that one potion from Terraria.";
$MM::AscensionData["Wormhole Phone", "Discription"] = "Unlocks the \"Return\" function on your PDA, which will return you to the last place you used \"Warp Home\".";

$MM::AscensionData["Fit To Mine I", "Cost"] = 1;
$MM::AscensionData["Fit To Mine I", "Prereq"] = "Legacy";
$MM::AscensionData["Fit To Mine I", "Fluff"] = "Just going to the gym a bit makes a big difference.";
$MM::AscensionData["Fit To Mine I", "Discription"] = "Your pickaxes now deal a total of 25\% more base damage.";

$MM::AscensionData["Fit To Mine II", "Cost"] = 1;
$MM::AscensionData["Fit To Mine II", "Prereq"] = "Fit To Mine I";
$MM::AscensionData["Fit To Mine II", "Fluff"] = "Professional body builder!";
$MM::AscensionData["Fit To Mine II", "Discription"] = "Your pickaxes now deal a total of 75\% more base damage.";

$MM::AscensionData["Fit To Mine III", "Cost"] = 1;
$MM::AscensionData["Fit To Mine III", "Prereq"] = "Fit To Mine II";
$MM::AscensionData["Fit To Mine III", "Fluff"] = "Definately not using steroids.";
$MM::AscensionData["Fit To Mine III", "Discription"] = "Your pickaxes now deal a total of 150\% more base damage.";

$MM::AscensionData["Walk It Off I", "Cost"] = 1;
$MM::AscensionData["Walk It Off I", "Prereq"] = "Legacy";
$MM::AscensionData["Walk It Off I", "Fluff"] = "Dipped you leg in magma? Just walk it off silly!";
$MM::AscensionData["Walk It Off I", "Discription"] = "You now recover 1 HP and 5 Rads every minute.";

$MM::AscensionData["Walk It Off II", "Cost"] = 1;
$MM::AscensionData["Walk It Off II", "Prereq"] = "Walk It Of I";
$MM::AscensionData["Walk It Off II", "Fluff"] = "The Black Knight award goes to...";
$MM::AscensionData["Walk It Off II", "Discription"] = "You now recover 1 HP and 5 Rads every 20 seconds.";

$MM::AscensionData["True Consitution", "Cost"] = 1;
$MM::AscensionData["True Consitution", "Prereq"] = "Walk It Of II";
$MM::AscensionData["True Consitution", "Fluff"] = "What do you mean I lost 5 liters of blood? I feel completely fine!";
$MM::AscensionData["True Consitution", "Discription"] = "You now take 30% less damage and radiation.";

$MM::AscensionData["Better Batteries I", "Cost"] = 1;
$MM::AscensionData["Better Batteries I", "Prereq"] = "Legacy";
$MM::AscensionData["Better Batteries I", "Fluff"] = "Preventing the lead acid from spilling out makes a huge difference.";
$MM::AscensionData["Better Batteries I", "Discription"] = "Your batteries are now 35% more efficent in power usage than base levels.";

$MM::AscensionData["Better Batteries II", "Cost"] = 1;
$MM::AscensionData["Better Batteries II", "Prereq"] = "Better Batteries II";
$MM::AscensionData["Better Batteries II", "Fluff"] = "Using the cutting edge technology known as lithium ion batteries.";
$MM::AscensionData["Better Batteries II", "Discription"] = "Your batteries are now 100% more efficent in power usage than base levels.";