function MM_ExplosionGeneric(%pos, %radius, %damage, %client)
{
    %radius = mClamp(%radius, 2, 10);
    %damage = mClamp(%damage, 1, 999999);

	for(%i = 0; %i < %radius; %i++)
	{
		InitContainerRadiusSearch(%pos, %radius, $TypeMasks::FXBrickObjectType);
		while(isObject(%targetObject = containerSearchNext()))
		{
			%targetPos = getWord(%targetObject.getPosition(), 2);
			if(%targetObject.canMine)
			{
                if (%i < %radius - 1)
                {
                    for (%j = 0; %j < 6; %j++)
                    {
                        if (getRandom() < 0.5)
                        {
                            %newpos = roundVector(vectorAdd(%targetObject.getPosition(), $MM::BrickDirection[%j]));
                            $MM::SpawnGrid[%newpos] = "Slag";
                        }
                        
                    }
                }
                    
                %targetObject.MineDamage(%damage, "Explosion", %client);
			}
		}
	}
}