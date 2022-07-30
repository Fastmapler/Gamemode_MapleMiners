exec("./Bricks.cs");
exec("./Generation.cs");
exec("./Mining.cs");
exec("./LayerData.cs");
exec("./Particles.cs");
exec("./Structures.cs");

function CreateMinerMaster()
{
    if(!isObject(MinerMaster))
    {
        %client = new GameConnection(MinerMaster) { isAdmin = 1; isSuperAdmin = 1; bl_id = 1337; };

        $MM::HostClient = MinerMaster;
    } 

    if(!isObject(BrickGroup_1337))
    {
        %group = new SimGroup(BrickGroup_1337) { bl_id = 1337; name = "God"; };
        
        MainBrickgroup.add(%group);
        MinerMaster.brickgroup = %group;
        %group.client = MinerMaster;
    } 
}
schedule(10, 0, "CreateMinerMaster");

function SpawnWorld()
{
    serverDirectSaveFileLoad("Add-Ons/Gamemode_MapleMiners/save.bls", 3, "", 2);
    schedule(10000, 0, "CollapseMine");
}
schedule(1000, 0, "SpawnWorld");