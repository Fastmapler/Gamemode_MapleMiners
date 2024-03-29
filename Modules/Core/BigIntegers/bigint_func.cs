// ===============
// Big Integer library for unlimited size integer arithmetics
// ===============
// Author: McTwist
// Version: 2018-01-19
// This library is a pure port. Few, if none, is edited and just
// made to work out of the box with TorqueScript.
// Source: https://github.com/peterolson/BigInteger.js
//
// This file is for direct manipulation through functions

// Add two values together and return their sum
function bigint_add(%a, %b)
{
	%a = bigint__parseValue(%a);
	%b = bigint__parseValue(%b);
	if (%a.sign != %b.sign)
	{
		%b.sign = !%b.sign;
		%value = bigint__subtract(%a, %b);
	}
	else
	{
		%value = bigint__addAny(%a, %b);
	}

	%ret = bigint__toString(%value);
	
	%a.delete();
	%b.delete();
	%value.delete();
	return %ret;
}

// Subtract two values and retiurn their difference
function bigint_subtract(%a, %b)
{
	%a = bigint__parseValue(%a);
	%b = bigint__parseValue(%b);
	if (%a.sign != %b.sign)
	{
		%b.sign = !%b.sign;
		%value = bigint__add(%a, %b);
	}
	else
	{
		%value = bigint__subtract(%a, %b);
	}

	%ret = bigint__toString(%value);

	%a.delete();
	%b.delete();
	%value.delete();
	return %ret;
}

// Multiply two values together and return their product
function bigint_multiply(%a, %b)
{
	%a = bigint__parseValue(%a);
	%b = bigint__parseValue(%b);
	%sign = %a.sign != %b.sign;

	if (bigint__useKaratsuba(%a.length(), %b.length()))
		%value = bigint__multiplyKaratsuba(%a, %b);
	else
		%value = bigint__multiplyLong(%a, %b);

	%value.sign = %sign;

	%ret = bigint__toString(%value);

	%a.delete();
	%b.delete();
	%value.delete();
	return %ret;
}

// Divide two values and return their quotition
// Get the remainder from $bigint::remainder
function bigint_divmod(%a, %b)
{
	%a = bigint__parseValue(%a);
	%b = bigint__parseValue(%b);
	%sign = %a.sign != %b.sign;

	%value = bigint__divMod(%a, %b);

	%value.sign = %sign;
	$bigint::_remainder.sign = %a.sign;

	$bigint::remainder = bigint__toString($bigint::_remainder);
	%ret = bigint__toString(%value);

	%a.delete();
	%b.delete();
	%value.delete();
	$bigint::_remainder.delete();
	return %ret;
}
