registerOutputEvent("fxDTSBrick", "SellOres", "", true);
function fxDTSBrick::SellOres(%this, %client) { %client.SellOres(); }

function GameConnection::SellOres(%client)
{
    %sum = 0;
    for (%i = 0; %i < MatterData.getCount(); %i++)
    {
        %matter = MatterData.getObject(%i);
        if (%matter.value > 0)
        {
            %count = %client.MM_Materials[%matter.name];
            %sum += %count * %matter.value;
            %client.MM_Materials[%matter.name] = 0;
        }
    }

    %client.chatMessage("\c6You sold all your valued materials for" SPC %sum @ "cr!");
    %client.MM_Credits += %sum;
}