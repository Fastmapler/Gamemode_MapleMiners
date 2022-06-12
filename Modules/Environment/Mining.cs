function Player::MM_AttemptMine(%obj, %hit, %damagemod, %bonustext)
{
	if (!isObject(%client = %obj.client) || %hit.getClassName() !$= "fxDtsBrick" || !%hit.canMine)
		return;
	%damage = %client.GetPickaxeDamage();

	if (%damagemod !$= "")
		%damage = mRound(%damage * %damagemod);

	%matter = getMatterType(%hit.matter);

	if (%client.MM_PickaxeLevel < %matter.level)
	{
		%client.MM_CenterPrint("You need to be atleast level\c3" SPC %matter.level SPC "\c6to learn how to mine this<color:" @ getSubStr(%matter.color, 0, 6) @ ">" SPC %matter.name @ "\c6!", 2);
		return;
	}

	if (%matter.hitSound !$= "")
		%hit.playSound("MM_" @ %matter.hitSound @ getRandom(1, $MM::SoundCount[%matter.hitSound]) @ "Sound");

	%client.MM_CenterPrint("<color:" @ getSubStr(%matter.color, 0, 6) @ ">" @ %matter.name NL "\c6" @ getMax(%hit.health - %damage, 0) SPC "HP<br>\c3" @ GetMatterValue(%matter) @ "\c6cr" NL %bonustext, 2);

	%hit.MineDamage(%damage, "Pickaxe", %client);

	if (isObject(%hit) && %hit.health > 0)
		%hit.spawnExplosion(dirtHitProjectile, 1.0);
}

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
    else
    {
        if (isObject(%client) && isObject(%matter = getMatterType(%obj.matter)) && %type !$= "Explosion")
        {
            if (%matter.hitFunc !$= "")
                call(%matter.hitFunc, %client, %matter.hitFuncArgs);
        }
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

AddDamageType("MMHeatDamage", '%1 was incinerated.', '%1 was incinerated.', 1, 1);
function MM_HeatDamage(%client, %damage)
{
    if (!isObject(%player = %client.player))
        return;

    if (hasField(%player.MM_ActivatedModules, "HeatShield") && %client.ChangeBatteryEnergy(%damage * -0.05))
    {
        //Play block noise
        return;
    }

    %player.damage(0, %player.getPosition(), %damage, $DamageType::MMHeatDamage);

    if (!%client.MM_WarnHeatDamage)
    {
        %client.chatMessage("Ow! That brick was searing hot to break!");
        %client.MM_WarnHeatDamage = true;
    }
        
}

$MM::SafeRadLimit = 500;
AddDamageType("MMRadDamage", '%1 got too green.', '%1 got too green.', 1, 1);
function MM_RadDamage(%client, %damage)
{
    if (!isObject(%player = %client.player))
        return;

    if (hasField(%player.MM_ActivatedModules, "RadShield") && %client.ChangeBatteryEnergy(%damage * -0.05))
    {
        //Play block noise
        return;
    }

    if (%player.MM_RadLevel > $MM::SafeRadLimit && getRandom() < (mLog(%player.MM_RadLevel) / 100))
    {
        %client.chatMessage("<font:impact:32>AGH!!! I AM MELTTTING!!!!!");
        %player.RadPoisonTick();
    }

    %player.MM_RadLevel += %damage;

    if (!%client.MM_WarnRadDamage)
    {
        %client.chatMessage("I feel unsafe hitting this... Going over " @ $MM::SafeRadLimit @ " rads might be a bad idea.");
        %client.MM_WarnRadDamage = true;
    }
}

function Player::RadPoisonTick(%player)
{
    cancel(%player.RadPoisonTickSchedule);
    
    %player.damage(0, %player.getPosition(), 1, $DamageType::MMRadDamage);
    %player.MM_RadLevel--;

    if (%player.MM_RadLevel > 0)
        %player.RadPoisonTickSchedule = %player.schedule(100, "RadPoisonTick");
}