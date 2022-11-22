exec("./Sounds.cs");
exec("./Tool_Pickaxe.cs");
exec("./Tool_Tunneler.cs");
exec("./Tool_Excavator.cs");
exec("./Tool_Macerator.cs");
exec("./Tool_Smasher.cs");
exec("./Tool_LootCaches.cs");
exec("./Tool_Modules.cs");
exec("./Tool_FluidPump.cs");
exec("./Tool_Dynamite.cs");
exec("./Tool_ShrapnelBomb.cs");
exec("./Tool_JackhammerGrenade.cs");
exec("./Tool_NapalmBomb.cs");
exec("./Tool_Healpack.cs");
exec("./Tool_Drill.cs");
exec("./Tool_Drillkit.cs");
exec("./Tool_Purifier.cs");
exec("./Tool_PlasteelGun.cs");
exec("./Tool_Blueprint.cs");
exec("./Tool_PDA.cs");
exec("./Tool_Syringe.cs");

exec("./Weapon_Prototype.cs");

function MM_SpawnItem(%data, %pos)
{
    %item = new Item() { datablock = %data.getID(); };
    MissionCleanup.add(%item);
    %item.setTransform(%pos SPC "0 0 0" SPC (getRandom() * 2 * $pi));
    %item.setVelocity(vectorScale(getRandom(-10, 10) SPC getRandom(-10, 10) SPC getRandom(-10, 10), 0.1));
    %item.schedulePop();
}