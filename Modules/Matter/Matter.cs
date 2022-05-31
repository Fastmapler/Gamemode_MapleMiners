exec("./MatterData.cs");

function GameConnection::ChangeMaterial(%client, %amount, %type)
{
    %change = mClamp(%client.MM_Materials[%type] + %amount, 0, 999999) - %client.MM_Materials[%type];
    %client.MM_Materials[%type] = mClamp(%client.MM_Materials[%type] + %amount, 0, 999999);

    return %change;
}