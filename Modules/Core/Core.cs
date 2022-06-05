exec("./Extra.cs");

function createDefaultMinigame()
{
    if (isObject($DefaultMiniGame))
        return;

    $DefaultMiniGame = new ScriptObject ("")
    {
        class = MiniGameSO;
        owner = 0;
        title = "Maple Miners";
        colorIdx = 1;
        numMembers = 0;
        InviteOnly = false;
        UseAllPlayersBricks = true;
        PlayersUseOwnBricks = false;
        UseSpawnBricks = true;
        Points_BreakBrick = 0;
        Points_PlantBrick = 0;
        Points_KillPlayer = 0;
        Points_KillSelf = 0;
        Points_KillBot = 0;
        Points_Die = 0;
        RespawnTime = 5;
        VehicleRespawnTime = 10;
        BrickRespawnTime = 30;
        BotRespawnTime = 5;
        FallingDamage = false;
        WeaponDamage = false;
        SelfDamage = true;
        VehicleDamage = false;
        BrickDamage = false;
        BotDamage = true;
        EnableWand = true;
        EnableBuilding = true;
        EnablePainting = true;
        PlayerDataBlock = PlayerMapleMinersArmor;
        StartEquip0 = 0;
        StartEquip1 = 0;
        StartEquip2 = 0;
        StartEquip3 = 0;
        StartEquip4 = 0;
        TimeLimit = -1;
    };
    MiniGameGroup.add ($DefaultMiniGame);
}
schedule(10, 0, "createDefaultMinigame");