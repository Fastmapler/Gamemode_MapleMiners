exec("./MatterData.cs");

function GameConnection::GetMaterial(%client, %type)
{
    if (%client.MM_Materials[%type] < 0 || %client.MM_Materials[%type] $= "")
        %client.MM_Materials[%type] = 0;

    return %client.MM_Materials[%type];
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