exec("./Bricks.cs");
exec("./Generation.cs");
exec("./Mining.cs");
exec("./LayerData.cs");

function CreateMinerMaster()
{
    if(!isObject(MinerMaster))
    {
        %client = new ScriptObject(MinerMaster) { isAdmin = 1; isSuperAdmin = 1; bl_id = 1337; };

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