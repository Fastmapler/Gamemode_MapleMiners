exec("./MatterData.cs");

function GameConnection::GetMaterial(%client, %type)
{
    if (%client.MM_Materials[%type] < 0 || %client.MM_Materials[%type] $= "")
        %client.MM_Materials[%type] = 0;

    return %client.MM_Materials[%type];
}

function GameConnection::SetMaterial(%client, %amount, %type)
{
    %client.MM_Materials[%type] = %amount;

    if (%client.MM_Materials[%type] < 0)
        %client.MM_Materials[%type] = 0;
}

function GameConnection::AddMaterial(%client, %amount, %type)
{
    %val = %client.MM_Materials[%type];
    %client.MM_Materials[%type] = uint_add(%val, %amount);

    if (%client.MM_Materials[%type] < 0)
        %client.MM_Materials[%type] = 0;
}

function GameConnection::SubtractMaterial(%client, %amount, %type)
{
    %val = %client.MM_Materials[%type];
    %client.MM_Materials[%type] = uint_sub(%val, %amount);
    
    if (%client.MM_Materials[%type] < 0)
        %client.MM_Materials[%type] = 0;
}

function Player::DisplayMaterial(%obj, %type)
{
    if (!isObject(%client = %obj.client) || !isObject(%matter = getMatterType(%type)))
        return;

    %amount = %client.GetMaterial(%matter.name);
    %client.MM_CenterPrint("<just:right>\c6" @ %matter.name @ ": "@ (%amount > 0 ? "\c3" : "\c0") @ %amount, 3);
}