function fxDtsBrick::MineDamage(%obj, %damage, %type, %client)
{
    if (!%obj.canMine) return;

    %obj.health -= %damage;

    if (%obj.health <= 0)
    {
        if (isObject(%client) && isObject(%matter = getMatterType(%obj.matter)) && %type !$= "Explosion")
        {
            if (%matter.harvestFunc !$= "")
                call(%matter.harvestFunc, %client, %matter.harvestFuncArgs);
            else
                %client.ChangeMaterial(1, %matter.name);
        }
        
        if (%type $= "Pickaxe")
        {
            %obj.spawnExplosion(dirtExplosionProjectile, 0.5);
            
            if (%matter.breakSound !$= "")
			    %obj.playSound("MM_" @ %matter.breakSound @ getRandom(1, $MM::SoundCount[%matter.breakSound]) @ "Sound");
            else
                %obj.playSound("MM_Break" @ getRandom(1, $MM::SoundCount["Break"]) @ "Sound");
        }
            
            
        GenerateSurroundingBlocks(%obj.getPosition());
        %obj.delete();
    }
}

function MM_GetLootCache(%client, %tier)
{
    if (!isObject(%player = %client.player))
        return;

    %tier = mClamp(%tier, 1, 5);
    %client.chatMessage("\c2You found a Tier" SPC %tier SPC "Loot Cache!");
    %item = new Item()
    {
        datablock = "MM_LootCacheT" @ %tier @ "Item";
        static    = "0";
        position  = vectorAdd(%player.getPosition(), "0 0 1");
        craftedItem = true;
    };
}