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