if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
	if(!$RTB::RTBR_ServerControl_Hook)
		exec("Add-Ons/System_ReturnToBlockland/RTBR_ServerControl_Hook.cs");
	RTB_registerPref("Keep Tools on Death", "KeepTools", "$Pref::Server::KeepToolsOnDeath", "bool", "Script_KeepTools", 1, 0, 1);
	RTB_registerPref("Num Tools to Keep", "KeepTools", "$Pref::Server::NumToolsToKeep", "string 3", "Script_KeepTools", "ALL", 0, 1);
	RTB_registerPref("Allow Restore in Minigame", "KeepTools", "$Pref::Server::AllowRestore", "bool", "Script_KeepTools", 1, 0, 1);
}
else if($Pref::Server::KeepToolsOnDeath $= "")
{
	$Pref::Server::KeepToolsOnDeath = 1;
	$Pref::Server::NumToolsToKeep = "ALL";
}

function serverCmdToggleKeepTools(%client)
{
	if(%client.bl_id == getNumKeyID() || findLocalClient() == %client)
	{
		$Pref::Server::KeepToolsOnDeath = $Pref::Server::KeepToolsOnDeath ? false : true;
		messageAll('',"Keep Tools on Death: " @ $Pref::Server::KeepToolsOnDeath);
	}
}

function serverCmdNumKeepTools(%client,%num)
{
	if(%client.bl_id == getNumKeyID() || findLocalClient() == %client)
	{
		if(%num > 20)
			%num = "ALL";
		if(%num <= 0)
			%num = "ALL";
		$Pref::Server::NumToolsToKeep = %num;
		messageAll('',"Number of Tools Kept on Death: " @ %num);
	}
}

function serverCmdToggleAllowRestore(%client)
{
	if(%client.bl_id == getNumKeyID() || findLocalClient() == %client)
	{
		$Pref::Server::AllowRestore = $Pref::Server::AllowRestore ? false : true;
		messageAll('',"Allow Restore in Minigame: " @ $Pref::Server::AllowRestore);
	}
}

package KeepTools
{
	function Armor::onDisabled(%data,%this,%state)
	{
		if(%state $= "Enabled" && $Pref::Server::KeepToolsOnDeath)
		{
			if($Pref::Server::NumToolsToKeep > 20)
				$Pref::Server::NumToolsToKeep = "ALL";
			if($Pref::Server::NumToolsToKeep $= "ALL" || $Pref::Server::NumToolsToKeep/1 <= 0)
			{
				%this.client.saveTools = %data.maxTools;
				for(%x=0;%x<%data.maxTools;%x++)
					if(%this.tool[%x] > 0)
						%this.client.savetool[%x] = %this.tool[%x];
			}
			else
			{
				%this.client.saveTools = $Pref::Server::NumToolsToKeep;
				for(%x=0;%x<%this.client.saveTools;%x++)
					if(%this.tool[%x] > 0)
						%this.client.savetool[%x] = %this.tool[%x];
			}
		}
		return Parent::onDisabled(%data,%this,%state);
	}

	//We handle player spawning in Saving.cs

	function GameConnection::autoAdminCheck(%this)
	{
		if((%this.bl_id == getNumKeyID() || findLocalClient() == %this) && !$RTB::RTBR_ServerControl_Hook)
		{
			messageClient(%this,'',"<color:ffffff>Script_KeepTools commands");
			messageClient(%this,'',"<color:00ff00>/ToggleKeepTools   /NumKeepTools NUM or ALL   /ToggleAllowRestore");
		}
		return Parent::autoAdminCheck(%this);
	}
};

activatePackage(KeepTools);

function GameConnection::restoreTools(%this)
{	
	for(%x=0;%x<%this.saveTools;%x++)
	{
		if (!isObject(%this.saveTool[%x]))
			continue;

		%this.player.tool[%x] = %this.saveTool[%x].getID();
		messageClient(%this, 'MsgItemPickup', "", %x, %this.saveTool[%x].getID(), 1); //dunno what the 1 is
		if(%this.player.tool[%x].className $= "Weapon")
			%this.player.weaponCount++;
		%this.saveTool[%x] = 0;
	}
	%this.saveTools = 0;
	if(!%this.hasRestoredBefore)
	{
		messageClient(%this, '', "Your tools have been restored from the time you died. You may type /restoreDefaultTools to undo this.");
		messageClient(%this, '', "Using this command in a minigame will kill you then restore the default tools.");
		%this.hasRestoredBefore = 1;
	}
}

function serverCmdRestoreDefaultTools(%client)
{
	if(isObject(%client.player))
	{
		if(%client.minigame <= 0)
			%client.player.giveDefaultEquipment();
		else if($Pref::Server::AllowRestore)
		{
			%client.player.kill();
			%client.saveTools = 0;
		}
		else
			commandToClient(%client,'MessageBoxOK',"Restore Default Tools", "You are not allowed to restore tools in minigames on this server.");
	}
}