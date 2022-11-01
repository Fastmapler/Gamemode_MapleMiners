// ===============
// Big Integer library for unlimited size integer arithmetics
// ===============
// Author: McTwist
// Version: 2018-01-19
// This library is a pure port. Few, if none, is edited and just
// made to work out of the box with TorqueScript.
// Source: https://github.com/peterolson/BigInteger.js
//
// This is access through an object, to reduce overhead when reusing
// the same number several times

// Create a new big integer
// First parameter is the value to parse
// Second argument is used internally
function BigInt(%a, %b)
{
	%this = new ScriptObject()
	{
		class = BigInt;
	};

	if (%a !$= "")
		%this.value = bigint__parseValue(%a);
	else if (%b !$= "")
		%this.value = %b;
	else
		%this.value = bigint__parseValue("0");

	return %this;
}


// Clean up the mess when done
function BigInt::onRemove(%this)
{
	%this.value.delete();
}

// Add two integers
function BigInt::add(%this, %v)
{
	%a = %this.value;
	%b = %v.value;
	if (%a.sign != %b.sign)
	{
		%b.sign = !%b.sign;
		%value = bigint__subtract(%a, %b);
	}
	else
	{
		%value = bigint__addAny(%a, %b);
	}

	return BigInt("", %value);
}

// Subtract two integers
function BigInt::subtract(%this, %v)
{
	%a = %this.value;
	%b = %v.value;
	if (%a.sign != %b.sign)
	{
		%b.sign = !%b.sign;
		%value = bigint__add(%a, %b);
	}
	else
	{
		%value = bigint__subtract(%a, %b);
	}

	return BigInt("", %value);
}

// Multiply two integers
function BigInt::multiply(%this, %v)
{
	%a = %this.value;
	%b = %v.value;

	if (bigint__useKaratsuba(%a.length(), %b.length()))
		%value = bigint__multiplyKaratsuba(%a, %b);
	else
		%value = bigint__multiplyLong(%a, %b);

	%value.sign = %sign;

	return BigInt("", %value);
}


// Divide one integer with an another
// Use $bigint::remainder_obj for the remainder
function BigInt::divmod(%this, %v)
{
	%a = %this.value;
	%b = %v.value;

	if (isObject($bigint::remainder_obj))
		$bigint::remainder_obj.delete();

	%value = bigint__divMod(%a, %b);

	%value.sign = %sign;
	$bigint::_remainder.sign = %a.sign;

	$bigint::remainder_obj = BigInt("", $bigint::_remainder);
	return BigInt("", %value);
}

// Convert the integer to a readable string
function BigInt::toString(%this)
{
	return bigint__toString(%this.value);
}
