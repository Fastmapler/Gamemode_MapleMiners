package KeepTools
{
	function Armor::onDisabled(%data,%this,%state)
	{
		if(%state $= "Enabled")
		{
			%client = %this.client;

			%this.client.saveTools = %client.GetMaxInvSlots();
			for(%x = 0; %x <%client.GetMaxInvSlots(); %x++)
				if(%this.tool[%x] > 0)
					%this.client.savetool[%x] = %this.tool[%x];
		}
		return Parent::onDisabled(%data, %this, %state);
	}

	//We handle player spawning in Saving.cs
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
		messageClient(%this, '', "Your tools have been restored. Welcome back to the living world.");
		%this.hasRestoredBefore = 1;
	}
}