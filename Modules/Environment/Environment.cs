exec("./Bricks.cs");
exec("./Generation.cs");
exec("./Mining.cs");
exec("./LayerData.cs");

function CreateMinerMaster()
{
    if(!isObject(MinerMaster))
    {
        %client = new ScriptObject(MinerMaster) { isAdmin = 1; isSuperAdmin = 1; bl_id = 1337; };
        %group = new SimGroup(BrickGroup_1337) { bl_id = 1337; name = "God"; };
        
        MainBrickgroup.add(%group);
        %client.brickgroup = %group;
        %group.client = %client;

        $MM::HostClient = MinerMaster;
    } 
}
schedule(10, 0, "CreateMinerMaster");