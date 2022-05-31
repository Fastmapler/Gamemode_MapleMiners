exec("./MatterData.cs");

function GameConnection::ChangeMaterial(%client, %amount, %type)
{
    %change = mClamp(%client.MaterialInventory[%type] + %amount, 0, 999999) - %client.MaterialInventory[%type];
    %client.MaterialInventory[%type] = mClamp(%client.MaterialInventory[%type] + %amount, 0, 999999);

    return %change;
}