function fxDtsBrick::MineDamage(%obj, %damage, %type, %client)
{
    if (!%obj.canMine) return;

    %obj.health -= %damage;

    if (%obj.health <= 0)
    {
        if (isObject(%client) && isObject(%matter = getMatterType(%obj.matter)))
        {
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