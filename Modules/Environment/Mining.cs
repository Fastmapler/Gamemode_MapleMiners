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
        
        GenerateSurroundingBlocks(%obj.getPosition());
        %obj.delete();
    }
}