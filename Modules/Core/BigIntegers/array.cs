//
// JavaScript-like Array object
//
// Author: McTwist
// Version: 2018-01-19
//


// Create new Array object to add items to
// Argument is an another Array that will be copied over
function Array(%copy)
{
	%array = new ScriptObject()
	{
		class = Array;
		begin = 0;
		end = 0;
	};

	if (isObject(%copy) && %copy.class $= Array)
	{
		%array.begin = %copy.begin;
		%array.end = %copy.end;

		for (%i = %copy.begin; %i < %copy.end; %i++)
			%array.data[%i] = %copy.data[%i];
	}

	return %array;
}

// Push item at the back of the list
function Array::push(%this, %a)
{
	%this.data[%this.end] = %a;
	%this.end++;
	return %this.length();
}

// Pop item from the back of the list
function Array::pop(%this)
{
	if (%this.begin == %this.end)
		return "";
	%this.end--;
	%a = %this.data[%this.end];
	%this.data[%this.end] = "";
	return %a;
}

// Shift item from the front of the list
function Array::shift(%this)
{
	if (%this.begin == %this.end)
		return "";
	%a = %this.data[%this.begin++];
	%this.data[%this.begin] = "";
	return %a;
}

// Put item at the front of the list
function Array::unshift(%this, %a)
{
	%this.data[%this.begin--] = %a;
	return %this.length();
}

// Get the length of the array
function Array::length(%this)
{
	return %this.end - %this.begin;
}

// Get a value from the array
function Array::get(%this, %i)
{
	return %this.data[%i + %this.begin];
}

// Set a value to the array
function Array::set(%this, %i, %value)
{
	%this.data[%i + %this.begin] = %value;
}

// Iterate through the list with a function for each element
// func(%item, %index, %array)
function Array::forEach(%this, %func)
{
	for (%i = %this.begin; %i < %this.end; %i++)
		call(%func, %this.data[%i], %i - %this.begin, %this);
}

// Copy this array
function Array::slice(%this, %begin, %end)
{
	if (%begin $= "")
		%begin = %this.begin;
	else
		%begin += %this.begin;
	if (%end $= "")
		%end = %this.end;
	else
		%end += %this.end;

	%array = Array();
	%array.begin = 0;
	%array.end = %end - %begin;

	for (%i = %array.begin; %i < %array.end; %i++)
		%array.data[%i] = %this.data[%i + %begin];

	return %array;
}

// Put together this with array to the right
function Array::concat(%this, %array)
{
	// Note: Maybe optimize this without dependency on other methods
	for (%i = 0; %i < %array.length(); %i++)
		%this.push(%array.get(%i));
}

// Reverse the array
function Array::reverse(%this)
{
	%half = mFloor(%this.length() / 2);
	for (%i = 0; %i < %half; %i++)
	{
		%temp = %this.data[%this.begin + %i];
		%this.data[%this.begin + %i] = %this.data[%this.end - 1 - %i];
		%this.data[%this.end - 1 - %i] = %temp;
	}
}

// Resize to certain size
// If length is bigger, append. Otherwise crop.
function Array::resize(%this, %length)
{
	if (%length > %this.length())
	{
		%this.end = %this.begin + %length;
	}
	else if (%length < %this.length())
	{
		%end = %this.end;
		%this.end = %this.begin + %length;
		for (%i = %end - 1; %i >= %this.end; %i--)
			%this.data[%i] = "";
	}
}

