//Pickaxe Sounds
datablock AudioProfile(MM_PickaxeHitSound)
{
   filename    = "./Sounds/pickaxe_hit.wav";
   description = AudioClose3d;
   preload = true;
};

$MM::SoundCount["Granite"] = 5;
datablock AudioProfile(MM_Granite1Sound : MM_PickaxeHitSound) { filename = "./Sounds/granite_01.wav"; };
datablock AudioProfile(MM_Granite2Sound : MM_PickaxeHitSound) { filename = "./Sounds/granite_02.wav"; };
datablock AudioProfile(MM_Granite3Sound : MM_PickaxeHitSound) { filename = "./Sounds/granite_03.wav"; };
datablock AudioProfile(MM_Granite4Sound : MM_PickaxeHitSound) { filename = "./Sounds/granite_04.wav"; };
datablock AudioProfile(MM_Granite5Sound : MM_PickaxeHitSound) { filename = "./Sounds/granite_05.wav"; };

$MM::SoundCount["Metal"] = 5;
datablock AudioProfile(MM_Metal1Sound : MM_PickaxeHitSound) { filename = "./Sounds/metal_01.wav"; };
datablock AudioProfile(MM_Metal2Sound : MM_PickaxeHitSound) { filename = "./Sounds/metal_02.wav"; };
datablock AudioProfile(MM_Metal3Sound : MM_PickaxeHitSound) { filename = "./Sounds/metal_03.wav"; };
datablock AudioProfile(MM_Metal4Sound : MM_PickaxeHitSound) { filename = "./Sounds/metal_04.wav"; };
datablock AudioProfile(MM_Metal5Sound : MM_PickaxeHitSound) { filename = "./Sounds/metal_05.wav"; };

$MM::SoundCount["Quartz"] = 5;
datablock AudioProfile(MM_Quartz1Sound : MM_PickaxeHitSound) { filename = "./Sounds/quartz_01.wav"; };
datablock AudioProfile(MM_Quartz2Sound : MM_PickaxeHitSound) { filename = "./Sounds/quartz_02.wav"; };
datablock AudioProfile(MM_Quartz3Sound : MM_PickaxeHitSound) { filename = "./Sounds/quartz_03.wav"; };
datablock AudioProfile(MM_Quartz4Sound : MM_PickaxeHitSound) { filename = "./Sounds/quartz_04.wav"; };
datablock AudioProfile(MM_Quartz5Sound : MM_PickaxeHitSound) { filename = "./Sounds/quartz_05.wav"; };

$MM::SoundCount["Stone"] = 5;
datablock AudioProfile(MM_Stone1Sound : MM_PickaxeHitSound) { filename = "./Sounds/stone_01.wav"; };
datablock AudioProfile(MM_Stone2Sound : MM_PickaxeHitSound) { filename = "./Sounds/stone_02.wav"; };
datablock AudioProfile(MM_Stone3Sound : MM_PickaxeHitSound) { filename = "./Sounds/stone_03.wav"; };
datablock AudioProfile(MM_Stone4Sound : MM_PickaxeHitSound) { filename = "./Sounds/stone_04.wav"; };
datablock AudioProfile(MM_Stone5Sound : MM_PickaxeHitSound) { filename = "./Sounds/stone_05.wav"; };

$MM::SoundCount["Wood"] = 5;
datablock AudioProfile(MM_Wood1Sound : MM_PickaxeHitSound) { filename = "./Sounds/wood_01.wav"; };
datablock AudioProfile(MM_Wood2Sound : MM_PickaxeHitSound) { filename = "./Sounds/wood_02.wav"; };
datablock AudioProfile(MM_Wood3Sound : MM_PickaxeHitSound) { filename = "./Sounds/wood_03.wav"; };
datablock AudioProfile(MM_Wood4Sound : MM_PickaxeHitSound) { filename = "./Sounds/wood_04.wav"; };
datablock AudioProfile(MM_Wood5Sound : MM_PickaxeHitSound) { filename = "./Sounds/wood_05.wav"; };

$MM::SoundCount["Drill"] = 4;
datablock AudioProfile(MM_Drill1Sound : MM_PickaxeHitSound) { filename = "./Sounds/drill_01.wav"; };
datablock AudioProfile(MM_Drill2Sound : MM_PickaxeHitSound) { filename = "./Sounds/drill_02.wav"; };
datablock AudioProfile(MM_Drill3Sound : MM_PickaxeHitSound) { filename = "./Sounds/drill_03.wav"; };
datablock AudioProfile(MM_Drill4Sound : MM_PickaxeHitSound) { filename = "./Sounds/drill_04.wav"; };

$MM::SoundCount["Break"] = 5;
datablock AudioProfile(MM_Break1Sound : MM_PickaxeHitSound) { filename = "./Sounds/break_01.wav"; };
datablock AudioProfile(MM_Break2Sound : MM_PickaxeHitSound) { filename = "./Sounds/break_02.wav"; };
datablock AudioProfile(MM_Break3Sound : MM_PickaxeHitSound) { filename = "./Sounds/break_03.wav"; };
datablock AudioProfile(MM_Break4Sound : MM_PickaxeHitSound) { filename = "./Sounds/break_04.wav"; };
datablock AudioProfile(MM_Break5Sound : MM_PickaxeHitSound) { filename = "./Sounds/break_05.wav"; };

$MM::SoundCount["Meat"] = 5;
datablock AudioProfile(MM_Meat1Sound : MM_PickaxeHitSound) { filename = "./Sounds/meat_01.wav"; };
datablock AudioProfile(MM_Meat2Sound : MM_PickaxeHitSound) { filename = "./Sounds/meat_02.wav"; };
datablock AudioProfile(MM_Meat3Sound : MM_PickaxeHitSound) { filename = "./Sounds/meat_03.wav"; };
datablock AudioProfile(MM_Meat4Sound : MM_PickaxeHitSound) { filename = "./Sounds/meat_04.wav"; };
datablock AudioProfile(MM_Meat5Sound : MM_PickaxeHitSound) { filename = "./Sounds/meat_05.wav"; };

//Upgrade Sounds
datablock AudioProfile(UpgradePickaxeSound)
{
   filename    = "./Sounds/UpgradePick.wav";
   description = AudioClose3d;
   preload = true;
};
