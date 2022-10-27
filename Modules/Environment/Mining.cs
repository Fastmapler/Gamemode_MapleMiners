function fxDtsBrick::getMiningLevel(%brick)
{
    return getMax(mCeil(getMatterType(%brick.matter).level * (1 - %brick.armorDamage)), 1);
}

function Player::MM_AttemptMine(%obj, %hit, %damagemod, %bonustext)
{
	if (!isObject(%client = %obj.client) || %hit.getClassName() !$= "fxDtsBrick" || !%hit.canMine)
		return;
	

	%matter = getMatterType(%hit.matter);

    if (%matter.canPump && !%client.warnPump[%matter.name])
    {
        %client.chatMessage("\c6You need a fluid pump to extract and loot this<color:" @ getSubStr(%matter.color, 0, 6) @ ">" SPC %matter.name @ "\c6!", 2);
		%client.warnPump[%matter.name] = true;
    }

	if (%client.MM_PickaxeLevel < %hit.getMiningLevel())
	{
        %burnText = %hit.armorDamage > 0 ? "\c0(" @ mRound(%hit.armorDamage * 100) @ "\% LVL REQ. BURNT)" : "";
		%client.MM_CenterPrint("You need to be atleast level\c3" SPC %hit.getMiningLevel() SPC "\c6to learn how to mine this<color:" @ getSubStr(%matter.color, 0, 6) @ ">" SPC %matter.name @ "\c6!<br>" @ %burnText, 2);
		return;
	}

	if (%matter.hitSound !$= "")
		%hit.playSound("MM_" @ %matter.hitSound @ getRandom(1, $MM::SoundCount[%matter.hitSound]) @ "Sound");
    
    %damage = %client.GetPickaxeDamage();

    if (hasField(%obj.MM_ActivatedModules, "DirtBreaker") && %matter.value <= 0)
        %damage += mRound(%hit.health * 0.15);

	if (%damagemod !$= "")
		%damage = mRound(%damage * %damagemod);

    if (%matter.rare)
        %bonustext = %bonustext NL "<color:0000ff>\cp<color:ffff00>Rare Ore!\co";

    if (GetMatterValue(%matter) > 0 && !%matter.unobtainable && !%matter.unsellable)
	    %client.MM_CenterPrint("<color:" @ getSubStr(%matter.color, 0, 6) @ ">" @ %matter.name NL "\c6" @ getMax(%hit.health - %damage, 0) SPC "HP<br>\c3" @ GetMatterValue(%matter) @ "\c6cr" NL %bonustext, 2);
    else
        %client.MM_CenterPrint("<color:" @ getSubStr(%matter.color, 0, 6) @ ">" @ %matter.name NL "\c6" @ getMax(%hit.health - %damage, 0) SPC "HP" NL %bonustext, 2);
	
    %hit.MineDamage(%damage, "Pickaxe", %client);
}

function fxDtsBrick::MineDamage(%obj, %damage, %type, %client)
{
    if (!%obj.canMine || !isObject(%matter = getMatterType(%obj.matter))) return;

    %player = %client.player;

    if (strPos(%type, "Explosion") > -1 && %matter.bombResist > 0)
        %damage = uint_mul(%damage, 1 - %matter.bombResist);

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
            %lowerLotto = mFloor($MM::ServerBuffLevel["Lotto"]);
            %amount = 1 + ($MM::ServerBuffLevel["Lotto"] - %lowerLotto > getRandom() ? %lowerLotto + 1 : %lowerLotto);
            if (isObject(%player) && hasField(%player.MM_ActivatedModules, "Gambler") && %matter.value > 0)
            {
                %roll = getRandom(1, 6);
                if (%roll > 2)
                {
                    %client.ChangeBatteryEnergy(-1 * $MM::MaxBatteryCharge);
                    %amount *= 2;
                    %client.chatMessage("\c6You rolled a " @ %roll @ "! Ore duplicated!");
                }
                else
                {
                    %amount *= 0;
                    %client.chatMessage("\c0You rolled a " @ %roll @ "... Ore destroyed.");
                }
            }

            if (!%matter.unobtainable)
                %client.AddMaterial(%amount, %matter.name);

            if (%matter.harvestFunc !$= "")
                for (%i = 0; %i < %amount; %i++)
                    call(%matter.harvestFunc, %client, %obj, %matter.harvestFuncArgs);
   
        }
        
        if (%type $= "Pickaxe")
        {
            if (!%client.MM_noMiningDebris)
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

function MM_CancerSpread(%client, %brick, %radius, %curRadius, %sourcePos, %matter)
{
    if (%curRadius $= "")
    {
        %sourcePos = %brick.getPosition();
        %matter = getMatterType(%brick.matter);
    }
    else
    {
        InitContainerRadiusSearch(%sourcePos, %curRadius, $TypeMasks::FXBrickObjectType);
        while(isObject(%targetObject = containerSearchNext()))
        {
            %targetPos = getWord(%targetObject.getPosition(), 2);
            %targetMatter = getMatterType(%targetObject.matter);

            if(%targetObject.canMine && %i < %radius - 1 && %matter.name !$= %targetMatter.name && getRandom() > %targetMatter.bombResist)
                ReplaceBrick(%targetObject.getPosition(), %matter.name);
        }
    }
    

    if (%curRadius < %radius)
        schedule(100, 0, "MM_CancerSpread", %client, %brick, %radius, %curRadius + 1, %sourcePos, %matter);
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

function MM_ServerBuff(%client, %brick, %type)
{
    switch$ (%type)
    {
        case "Frenzy":
            messageAll('MsgAdminForce', "\c6" @ %client.netName @ " broke a Frenzy Orb (Forb)! Ore values greatly increased!");
            $MM::ServerBuffLevel["Frenzy"] += 0.75;
        case "Lotto":
            messageAll('MsgAdminForce', "\c6" @ %client.netName @ " broke a Lotto Orb (Lorb)! Chance for double ores mined!");
            $MM::ServerBuffLevel["Lotto"] += 0.5;
        case "Berserk":
            messageAll('MsgAdminForce', "\c6" @ %client.netName @ " broke a Berserk Orb (Borb)! Pickaxe damage significantly increased!");
            $MM::ServerBuffLevel["Berserk"] += 2.5;
        case "Mystical":
            messageAll('MsgAdminForce', "\c6" @ %client.netName @ " broke a Mystical Orb (Morb)! Ore values, mined ore doubling, and pickaxe damage increased!");
            $MM::ServerBuffLevel["Frenzy"] += 0.375;
            $MM::ServerBuffLevel["Lotto"] += 0.25;
            $MM::ServerBuffLevel["Berserk"] += 1.25;
        case "Extender":
            messageAll('MsgAdminForce', "\c6" @ %client.netName @ " broke an Extender Orb (Eorb)! Current/Future server buffs have been extended!");
            $MM::ServerBuffTime = getMax(9, $MM::ServerBuffTime + 3);
            
    }

    if ($MM::ServerBuffTime < 6)
        $MM::ServerBuffTime = 6;

    MM_ServerBuffTick(true);
}

function MM_ServerBuffTick(%bypassTick)
{
    cancel($MM::ServerBuffSchedule);

    if (!%bypassTick)
        $MM::ServerBuffTime--;
    
    messageAll('', "\c6Server Buff Time: " @ $MM::ServerBuffTime @ " minute(s)");

    if ($MM::ServerBuffTime > 0)
    {
        if ($MM::ServerBuffLevel["Frenzy"] > 0)
            messageAll('', "\c6Ore Value Multiplier: " @ (1 + $MM::ServerBuffLevel["Frenzy"]) @ "x");
        if ($MM::ServerBuffLevel["Lotto"] > 0)
            messageAll('', "\c6Mined Ore Duplication Chance: " @ mRound(100 * $MM::ServerBuffLevel["Lotto"]) @ "\%");
        if ($MM::ServerBuffLevel["Berserk"] > 0)
            messageAll('', "\c6Pickaxe Damage Multiplier: " @ (1 + $MM::ServerBuffLevel["Berserk"]) @ "x");
    }
    else
    {
        $MM::ServerBuffLevel["Frenzy"] = 0;
        $MM::ServerBuffLevel["Lotto"] = 0;
        $MM::ServerBuffLevel["Berserk"] = 0;
        messageAll('MsgAdminForce', "\c6Server Buffs have ran out...");
        return;
    }

    schedule(1000 * 60, 0, "MM_ServerBuffTick");
}