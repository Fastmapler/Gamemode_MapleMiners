function doTipLoop(%num)
{
	cancel($EOTW::TipLoop);
	%num++;
	switch (%num)
	{
		case 1: %text = "\c5Tip\c6: Ores can often times spawn in square blobs or straight lines.";
		case 2: %text = "\c5Tip\c6: Many purchasable tools require some ores ontop of the normal credit price tag.";
		case 3: %text = "\c5Tip\c6: Deeper layers will have more valuable ores spawn more often.";
		case 4: %text = "\c5Tip\c6: You can view and download the gamemode's code <a:github.com/Fastmapler/Gamemode_MapleMiners>Here!</a>";
		case 5: %text = "\c5Tip\c6: This is not Solar Apocalypse Expanded 2.";
		default: %text = "\c5Tip\c6: There is a <a:discord.gg/qdtpC3hKZY>Discord.</a>"; %num = 0;
	}
	
	messageAll('',%text);
	
	$EOTW::TipLoop = schedule(60000 * 3, 0, "doTipLoop",%num);
}
schedule(60000 * 3, 0, "doTipLoop",%num);

function getNiceNumber(%val)
{
	for (%i = strLen(%val) - 3; %i > -3; %i -= 3)
		%str = getSubStr(%val, getMax(%i, 0), getMin(3, %i + 3)) @ "," @ %str;

	return getSubStr(%str, 0, strLen(%str) - 1);
}

function getFieldFromValue(%list, %value)
{
	for (%i = 0; %i < getFieldCount(%list); %i++)
		if (getField(%list, %i) $= %value)
			return %i;
			
	return -1;
}

function decimalFromHex(%hex)
{
	%seq = "0123456789ABCDEF";
	
	%val = 0;
	for (%i = 0; %i < strLen(%hex); %i++)
	{
		%char = getSubStr(%hex, %i, 1);
		%d = striPos(%seq, %char);
		%val = 16*%val + %d;
	}
	
	return %val;
}
//
function hexFromFloat(%float)
{
	%hex = "0123456789abcdef";
	
	%val = %float * 255;
	
	%first = getSubStr(%hex, mFloor(%val / 16), 1);
	%second = getSubStr(%hex, mFloor(%val % 16), 1);
	
	return %first @ %second;
}

function getColorFromHex(%hex)
{
	for (%i = 0; %i < strLen(%hex); %i += 2)
		%color = %color SPC (decimalFromHex(getSubStr(%hex, %i, 2)) / 255);
		
	if (strLen(%hex) == 6)
		%color = %color SPC "1.0";
	
	return getClosestColor(trim(%color));
}

function ServerCmdHexFromPaintColor(%client)
{
	if (!%client.isSuperAdmin || !isObject(%player = %client.player))
		return;
	
	%color = getColorIDTable(%player.currSprayCan);
	
	%hex = hexFromFloat(getWord(%color, 0)) @ hexFromFloat(getWord(%color, 1)) @ hexFromFloat(getWord(%color, 2)) @ hexFromFloat(getWord(%color, 3));
	SetClipboard(%hex);
	talk(%hex);
}

function getFieldIndex(%text, %field)
{
	for (%i = 0; %i < getFieldCount(%text); %i++)
		if (%field $= getField(%text, %i))
			return %i;
	return -1;
}

function HexToRGB(%hex) {
	if(strLen(%hex) != 6) {
		return;
	}

	%chars = "0123456789abcdef";

	for(%i=0;%i<3;%i++) {
		%value = getSubStr(%hex, 2*%i, 2);

		%first = getSubStr(%value, 0, 1);
		%last = getSubStr(%value, 1, 1);
		
		%first = stripos(%chars, %first)*16;
		%last = stripos(%chars, %last);

		%sum = %first + %last;
		%str = trim(%str SPC %sum);
	}

	return %str;
}

function RGBToHex(%rgb) {
	%rgb = getWords(%rgb,0,2);
	for(%i=0;%i<getWordCount(%rgb);%i++) {
		%dec = mFloor(getWord(%rgb,%i)*255);
		%str = "0123456789ABCDEF";
		%hex = "";

		while(%dec != 0) {
			%hexn = %dec % 16;
			%dec = mFloor(%dec / 16);
			%hex = getSubStr(%str,%hexn,1) @ %hex;    
		}

		if(strLen(%hex) == 1)
			%hex = "0" @ %hex;
		if(!strLen(%hex))
			%hex = "00";

		%hexstr = %hexstr @ %hex;
	}

	if(%hexstr $= "") {
		%hexstr = "FF00FF";
	}
	return %hexstr;
}

function roundVector(%vector)
{
	return mFloor(getWord(%vector, 0)) SPC mFloor(getWord(%vector, 1)) SPC mFloor(getWord(%vector, 2));
}

function hasWord(%str, %word)
{
	for (%i = 0; %i < getWordCount(%str); %i++)
		if (getWord(%str, %i) $= %word)
			return true;
			
	return false;
}

function SimSet::Shuffle(%set)
{
	%count = %set.getCount();
	
	while (%count > 0)
	{
		%count--;
		%obj = %set.getObject(getRandom(0, %count));
		%set.pushToBack(%obj);
	}
}

function ServerCmdServerStats(%client)
{
	if (%client.viewingServerStats)
	{
		cancel(%client.ssLoop);
		%client.chatMessage("Server Stats view OFF.");
	}
	else
	{
		%client.ssLoop = %client.schedule(100, "viewServerStats");
		%client.chatMessage("Server Stats view ON.");
	}
	%client.viewingServerStats = !%client.viewingServerStats;
}

function GameConnection::viewServerStats(%client)
{
	cancel(%client.ssLoop);
	%client.centerPrint("<just:left>Server FPS: " @ $FPS::Real @ "<br>Schedules: " @ getNumSchedules() @ "<br>Sim Time: " @ getSimTime(), 2);
	%client.ssLoop = %client.schedule(1000, "viewServerStats");
}

function setNewSkyBox(%dml)
{
	for (%i = 0; %i < $EnvGUIServer::SkyCount; %i++)
	{
		if ($EnvGUIServer::Sky[%i] $= %dml)
		{
			servercmdEnvGui_SetVar(EnvMaster, "SkyIdx", %i);
			break;
		}
	}
}

function setNewWater(%water)
{
	for (%i = 0; %i < $EnvGUIServer::WaterCount; %i++)
	{
		if ($EnvGUIServer::Water[%i] $= %water)
		{
			servercmdEnvGui_SetVar(EnvMaster, "WaterIdx", %i);
			break;
		}
	}
}

function serverCmdGullible(%cl)
{
	if ($EOTW::ColorScroll[%cl.bl_id] $= "")
	{
		%cl.chatMessage("\c5Super secret rainbow player name activated!");
		%cl.player.ScrollNameColor();
	}
	else
	{
		%cl.chatMessage("\c5Super secret rainbow player name deactivated!");
		cancel($EOTW::ColorScroll[%cl.bl_id]);
		$EOTW::ColorScroll[%cl.bl_id] = "";
		%cl.player.setShapeNameColor("1 1 1");
	}
}

function Player::ScrollNameColor(%obj,%scroll)
{
	%scroll++;
	switch (%scroll)
	{
		case  1: %obj.setShapeNameColor("1.00 0.00 0.00");
		case  2: %obj.setShapeNameColor("1.00 0.42 0.00");
		case  3: %obj.setShapeNameColor("1.00 0.72 0.00");
		case  4: %obj.setShapeNameColor("0.71 1.00 0.00");
		case  5: %obj.setShapeNameColor("0.30 1.00 0.00");
		case  6: %obj.setShapeNameColor("0.00 1.00 0.12");
		case  7: %obj.setShapeNameColor("0.00 1.00 0.35");
		case  8: %obj.setShapeNameColor("0.00 1.00 1.00");
		case  9: %obj.setShapeNameColor("0.00 0.58 1.00");
		case 10: %obj.setShapeNameColor("0.00 0.15 1.00");
		case 11: %obj.setShapeNameColor("0.28 0.00 1.00");
		case 12: %obj.setShapeNameColor("0.70 0.00 1.00");
		case 13: %obj.setShapeNameColor("1.00 0.00 0.86");
		case 14: %obj.setShapeNameColor("1.00 0.00 0.43"); %scroll = 0;
	}
	
	$EOTW::ColorScroll[%obj.client.bl_id] = %obj.schedule(1000 / 14,"ScrollNameColor",%scroll);
}

function SimObject::getLeftVector(%obj)
{
	return vectorCross(%obj.getEyeVector(),%obj.getUpVector());
}

function SimObject::getRightVector(%obj)
{
	return vectorScale(%obj.getLeftVector(%obj),-1);
}

function mRound(%val)
{
	if (%val - mFloor(%val) < 0.5)
		return mFloor(%val);
	else
		return mCeil(%val);
}

function hasField(%fields, %field)
{
	%count = getFieldCount(%fields);

	for (%i = 0; %i < %count; %i++)
		if (strStr(%field, getField(%fields, %i)) == 0)
			return 1;

	return 0;
}

function getFieldIndex(%fields, %field)
{
	%count = getFieldCount(%fields);

	for (%i = 0; %i < %count; %i++)
		if (strStr(%field, getField(%fields, %i)) == 0)
			return %i;

	return -1;
}

function removeFieldText(%fields, %field)
{
	return removeField(%fields, getFieldIndex(%fields, %field));
}

function getClosestColor(%color)
{
	for(%i=0;%i<getWordCount(%color);%i++)
		if(getWord(%color, %i) > 1)
			%flag = 1;
	if(%flag)
	{
		for(%i=0;%i<getWordCount(%color);%i++)
			%newCol = %newCol SPC getWord(%color, %i) / 255;
		%color = %newCol;
	}
	%color = trim(%color);
	if(getWordCount(%color) == 3)
		%color = %color @ " 1.000000";
	%lowDiff = 10000;
	for(%i=0;%i<64;%i++)
	{
		%flag = 0;
		%test = getColorIDTable(%i);
		for(%j=0;%j<getWordCount(%test);%j++)
			if(getWord(%test, %j) > 1)
				%flag = 1;
		if(%flag)
		{
			for(%j=0;%j<getWordCount(%test);%j++)
				%newCol = %newCol SPC getWord(%test) / 255;
			%test = %newCol;
		}
		trim(%test);
		%diff = mAbs(getWord(%color,0)-getWord(%test,0));
		%diff += mAbs(getWord(%color,1)-getWord(%test,1));
		%diff += mAbs(getWord(%color,2)-getWord(%test,2));
		%diff += mAbs(getWord(%color,3)-getWord(%test,3))*3;
		if(%diff < %lowDiff)
		{
			%lowDiff = %diff;
			%lowColor = %i;
		}
	}
	return %lowColor;
}

function ServerCmdTop(%client)
{
	if (%client.isSuperAdmin)
	{
		CollapseMine();
		if (isObject(%client.player))
			%client.player.setTransform("0 0 " @ ($MM::ZLayerOffset + 4));
	}
}

function DumpOres()
{
	for (%i = 0; %i < MatterData.getCount(); %i++)
		PlaceMineBrick((%i * $MM::BrickDistance) SPC "5 5", MatterData.getObject(%i).name);
}

function DumpUpgradeCosts()
{
	for (%i = 1; %i < 10000; %i++)
		$MM::UpgradeCost["Pickaxe", %i] = %i SPC PickaxeUpgradeCost(%i);

	//export("$MM::UpgradeCostPickaxe*", "config/pickaxes.txt");

	%file = new FileObject();
    if(%file.openForWrite("config/pickaxes2.txt"))
    {
		for (%i = 1; %i < 10000; %i++)
        	%file.writeLine($MM::UpgradeCost["Pickaxe", %i]);
	}
	%file.close();
	%file.delete();
}

function TestOreGeneration(%zlimit)
{
	for (%i = 0; %i < 2; %i++)
	{
		for (%z = $MM::ZLayerLimit; %z > %zlimit; %z -= $MM::BrickDistance)
		{
			for (%y = -32; %y < 32; %y += $MM::BrickDistance)
			{
				for (%x = -32; %x < 32; %x += $MM::BrickDistance)
				{
					%pos = %x SPC %y SPC %z;
					if (%i == 0 && $MM::SpawnGrid[%pos] $= "")
						GenerateBlock(%pos);
					else if (isObject(%matter = getMatterType($MM::SpawnGrid[%pos])) && %matter.value > 0)
						RevealBlock(%pos);
				}
			}
		}
	}
	
}

function ServerCmdCheckLayer(%client, %layer, %verbose)
{
	%layer = LayerData.getObject(%layer);

	if (isObject(%layer))
	{
		for (%i = 0; %i < %layer.veinCount; %i++)
			%weightSum += getField(%layer.vein[%i], 0);

		for (%i = 0; %i < %layer.veinCount; %i++)
		{
			%vein = %layer.vein[%i];
			%fieldCount = getFieldCount(%vein);

			for (%j = 4; %j < %fieldCount; %j += 2)
        		%totalOreWeight += getField(%vein, %j + 1);

			%finalValue = 0;
			for (%j = 4; %j < %fieldCount; %j += 2)
			{
				%matter = getMatterType(getField(%vein, %j));
				%oreValue = %matter.value * (getField(%vein, %j + 1) / %totalOreWeight);
				if (%verbose $= "1") talk(%matter.name SPC %oreValue);
				%finalValue += %oreValue;
			}

			%size = (getField(%vein, 3) + 2) / 2;
			switch$ (getField(%vein, 2))
			{
				case "Line":
					%finalValue *= %size;
				case "Square":
					%finalValue *= mLog(%size) / mLog(2); //VERY rough estimate, probably really off
			}

			if (%verbose $= "1") talk(getField(%vein, 1) SPC %finalValue SPC %size);
			%totalValue += %finalValue * (getField(%vein, 0) / %weightSum);
		}
			

		talk(%layer.name SPC ((%weightSum / %layer.weightTotal) * 100) @ "\%" SPC %totalValue);
	}
}

function ServerCmdInv(%client)
{
	for (%i = 0; %i < MatterData.getCount(); %i++)
	{
		%matter = MatterData.getObject(%i);
		%count = %client.getMaterial(%matter.name);
		if (%count > 0)
			%client.chatMessage("<color:" @ getSubStr(%matter.color, 0, 6) @ ">" @ %matter.name @ "\c6: x" @ %count @ " (" @ (uint_mul(%count, GetMatterValue(%matter))) @ "cr)");
	}
}

function ServerCmdGetAllMats(%client, %getDebugPick)
{
	if (!%client.isSuperAdmin)
		return;

	for (%i = 0; %i < MatterData.getCount(); %i++)
        %client.SetMaterial(999999999,MatterData.getObject(%i).name);

	%client.chatMessage("Got all materials.");

	if (isObject(%player = %client.player) && %getDebugPick $= "1")
	{
		%item = new Item()
		{
			datablock = MMPickaxeDebugItem;
			static    = "0";
			position  = vectorAdd(%player.getPosition(), "0 0 1");
			craftedItem = true;
		};
	}
}