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
}

function fxDtsBrick::MineDamage(%obj, %damage, %type, %client)
{
    if (!%obj.canMine || !isObject(%matter = getMatterType(%obj.matter))) return;

    %obj.health = uint_sub(%obj.health, %damage);

    if (isObject(%client) && strPos(%type, "Explosion") == -1)
    {
        if (%matter.hitFunc !$= "")
            call(%matter.hitFunc, %client, %obj, %matter.hitFuncArgs);
    }

    if (%obj.health <= 0)
    {
        if (isObject(%client) && strPos(%type, "Explosion") == -1)
        {
            if (%matter.harvestFunc !$= "")
                call(%matter.harvestFunc, %client, %obj, %matter.harvestFuncArgs);
                
            if (!%matter.unobtainable)
                %client.AddMaterial(1, %matter.name);
        }
        
        if (%type $= "Pickaxe")
        {
            %obj.spawnExplosion(dirtExplosionProjectile, 1.0);
            
            if (%matter.breakSound !$= "")
			    %obj.playSound("MM_" @ %matter.breakSound @ getRandom(1, $MM::SoundCount[%matter.breakSound]) @ "Sound");
            else
                %obj.playSound("MM_Break" @ getRandom(1, $MM::SoundCount["Break"]) @ "Sound");
        }
            
            
        GenerateSurroundingBlocks(%obj.getPosition());
        %obj.delete();
    }
}

function MM_GetLootCache(%client, %brick, %tier)
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
function MM_HeatDamage(%client, %brick, %damage)
{
    if (!isObject(%player = %client.player))
        return;

    if (hasField(%player.MM_ActivatedModules, "HeatShield") && %client.ChangeBatteryEnergy(-1 * mCeil(%damage * 0.05)))
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
function MM_RadDamage(%client, %brick, %damage)
{
    if (!isObject(%player = %client.player))
        return;

    if (hasField(%player.MM_ActivatedModules, "RadShield") && %client.ChangeBatteryEnergy(-1 * mCeil(%damage * 0.05)))
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

function MM_CrystalBreak(%client, %brick, %type)
{
    %pos = MM_AttemptSpawn(%type, %brick.getPosition());

    if (%pos !$= "" && isObject(%player = %client.player))
        %client.chatMessage("\c6A structure has appeared at\c3" SPC %pos @ "\c6... The PDA might help in locating it.");
}