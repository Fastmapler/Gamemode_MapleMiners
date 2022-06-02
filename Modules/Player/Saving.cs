package MM_SavingLoading
{
    function GameConnection::onClientLeaveGame(%client)
	{
		parent::onClientLeaveGame(%client);
	}
    function GameConnection::createPlayer(%client, %trans)
	{
        if (!%client.hasSpawnedOnce)
        {
            %client.InitPlayerStats();
        }
        
		Parent::createPlayer(%client, %trans);

        if (isObject(%player = %client.player))
        {
           
        }
    }
};
activatePackage("MM_SavingLoading");