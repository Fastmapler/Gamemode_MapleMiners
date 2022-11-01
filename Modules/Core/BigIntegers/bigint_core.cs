// ===============
// Big Integer library for unlimited size integer arithmetics
// ===============
// Author: McTwist
// Version: 2018-01-19
// This library is a pure port. Few, if none, is edited and just
// made to work out of the box with TorqueScript.
// Source: https://github.com/peterolson/BigInteger.js
//
// This file is the core of the library and should not be accessed directly

exec("./array.cs");

// The base for each field
// Increase this to make the fields bigger and calculations more extreme
// Keep in mind that normal arithmetics will not work beyond 6 digit numbers
// Either use uint library or manually code in the base for each arithmetic
$bigint::base = 1000;

// Add two arrays together, a.length >= b.length
function bigint__add(%a, %b)
{
	%a_l = %a.length();
	%b_l = %b.length();
	%r = Array();
	%r.resize(%a_l);
	%carry = 0;

	%base = $bigint::base;

	for (%i = 0; %i < %b_l; %i++)
	{
		%sum = %a.get(%i) + %b.get(%i) + %carry;
		%carry = (%sum >= %base) ? 1 : 0;
		%r.set(%i, %sum - %carry * %base);
	}

	while (%i < %a_l)
	{
		%sum = %a.get(%i) + %carry;
		%carry = (%sum == %base) ? 1 : 0;
		%r.set(%i, %sum - %carry * %base);
		%i++;
	}

	if (%carry > 0)
		%r.push(%carry);
	return %r;
}

// Add two arrays together, but make sure it is the right order
function bigint__addAny(%a, %b)
{
	return (%a.length() >= %b.length()) ? bigint__add(%a, %b) : bigint__add(%b, %a);
}

// Add one array an a carry together
function bigint__addSmall(%a, %carry)
{
	%l = %a.length();
	%r = Array();

	%base = $bigint::base;

	for (%i = 0; %i < %l; %i++)
	{
		%sum = %a.get(%i) - %base + %carry;
		%carry = mFloor(%sum / %base);
		%r.set(%i, %sum - %carry * %base);
		%carry++;
	}

	while (%carry > 0)
	{
		%r.set(%i, %carry % %base);
		%i++;
		%carry = mFloor(%sum / %base);
	}

	return %r;
}

// Subtract two arrays, a.length >= b.length
function bigint__subtract(%a, %b)
{
	%a_l = %a.length();
	%b_l = %b.length();
	%r = Array();
	%r.resize(%a_l);
	%borrow = 0;

	%base = $bigint::base;

	for (%i = 0; %i < %b_l; %i++)
	{
		%difference = %a.get(%i) - %borrow - %b.get(%i);
		if (%difference < 0)
		{
			%difference += %base;
			%borrow = 1;
		}
		else
		{
			%borrow = 0;
		}
		%r.set(%i, %difference);
	}

	for (%i = %b_l; %i < %a_l; %i++)
	{
		%difference = %a.get(%i) - %borrow;
		if (%difference < 0)
		{
			%difference += %base;
		}
		else
		{
			%r.set(%i, %difference);
			%i++;
			break;
		}
		%r.set(%i, %difference);
	}

	for (%_ = 0; %i < %a_l; %i++)
	{
		%r.set(%i, %a.get(%i));
	}

	bigint__trim(%r);

	return %r;
}

// Subtract two arrays, but make sure it is the right order
function bigint__subtractAny(%a, %b)
{
	if (bigint__compareAbs(%a, %b) >= 0)
	{
		%value = bigint__subtract(%a, %b);
	}
	else
	{
		%value = bigint__subtract(%b, %a);
	}
	return %value;
}

// Subtract one array with an another value
function bigint__subtractSmall(%a, %b)
{
	%l = %a.length();
	%a = Array();
	%carry = -%b;

	%base = $bigint::base;

	for (%i = 0; %i < %l; %i++)
	{
		%difference = %a.get(%i) + %carry;
		%carry = mFloor(%difference / %base);
		%difference %= %base;
		%r.set(%i, %difference < 0 ? %difference + %base : %difference);
	}

	bigint__trim(%r);

	return %r;
}

// Multiply two arrays together
function bigint__multiplyLong(%a, %b)
{
	%a_l = %a.length();
	%b_l = %b.length();
	%l = %a_l + %b_l;
	%r = bigint__createArray(%l);
	%r.resize(%l);

	%base = $bigint::base;

	for (%i = 0; %i < %a_l; %i++)
	{
		%a_i = %a.get(%i);
		for (%j = 0; %j < %b_l; %j++)
		{
			%b_j = %b.get(%j);
			%product = %a_i * %b_j + %r.get(%i + %j);
			%carry = mFloor(%product / %base);
			%r.set(%i + %j, %product - %carry * %base);
			%r.set(%i + %j + 1, %r.get(%i + %j + 1) + %carry);
		}
	}
	bigint__trim(%r);
	return %r;
}

// Multiply one array and a small number
function bigint__multiplySmall(%a, %b)
{
	%l = %a.length();
	%r = Array();
	%r.resize(%l);
	%carry = 0;

	%base = $bigint::base;

	for (%i = 0; %i < %l; %i++)
	{
		%product = %a.get(%i) * %b + %carry;
		%carry = mFloor(%product / %base);
		%r.set(%i, %product - %carry * %base);
	}
	while (%carry > 0)
	{
		%r.push(%carry % %base);
		%i++;
		%carry = mFloor(%carry / %base);
	}

	return %r;
}

// Multiply two arrays together, optimized version for bigger arrays
function bigint__multiplyKaratsuba(%x, %y)
{
	%n = mMax(%x.length(), %y.length());

	if (%n <= 30) return bigint__multiplyLong(%x, %y);
	%n = mCeil(%n / 2);

	%b = %x.slice(%n);
	%a = %x.slice(0, %n);
	%d = %y.slice(%n);
	%c = %y.slice(0, %n);

	%ac = bigint__multiplyKaratsuba(%a, %c);
	%bd = bigint__multiplyKaratsuba(%b, %d);
	%abcd = bigint__multiplyKaratsuba(bigint__addAny(%a, %b), addAny(%c, %d));

	%v0 = bigint__subtract(%abcd, %ac);
	%v1 = bigint__subtract(%v0, %bd);
	%v2 = bigint__shiftLeft(%v1, %n);
	%v3 = bigint__addAny(%ac, %v2);
	%v4 = bigint__shiftLeft(%bd, 2 * %n);

	%product = bigint__addAny(%v3, %v4);
	bigint__trim(%product);

	%b.delete();
	%a.delete();
	%d.delete();
	%c.delete();
	%ac.delete();
	%bd.delete();
	%abcd.delete();
	%v0.delete();
	%v1.delete();
	%v2.delete();
	%v3.delete();
	%v4.delete();

	return %product;
}

// Check if going to use the bigger algorithm
function bigint__useKaratsuba(%l1, %l2)
{
	return -0.012 * %l1 - 0.012 * %l2 + 0.000015 * %l1 * %l2 > 0;
}

// Divide two arrays with each other
function bigint__divMod(%a, %b)
{
	%a_l = %a.length();
	%b_l = %b.length();
	%result = Array();
	%part = Array();

	%base = $bigint::base;

	while (%a_l)
	{
		%part.unshift(%a.get(%a_l--));
		bigint__trim(%part);
		if (bigint__compareAbs(%part, %b) < 0) {
			%result.push(0);
			continue;
		}

		%xlen = %part.length();
		%highx = %part.get(%xlen - 1) * %base + %part.get(%xlen - 2);
		%highy = %b.get(%b_l - 1) * %base + %b.get(%b_l - 2);
		if (%xlen > %b_l)
			%highx = (%highx + 1) * %base;
		%guess = uint_divCeil(%highx, %highy);
		%first = true;
		// do-while
		while (%first || %guess > 0)
		{
			%first = false;
			%check = bigint__multiplySmall(%b, %guess);
			if (bigint__compareAbs(%check, %part) <= 0) break;
			%guess--;
			%check.delete();
		}
		%result.push(%guess);
		%_part = %part;
		%part = bigint__subtract(%part, %check);
		%_part.delete();
		%check.delete();
	}

	%result.reverse();

	bigint__trim(%result);
	bigint__trim(%part);

	$bigint::_remainder = %part;
	return %result;
}

// Divide an array with a lambda
function bigint__divModSmall(%value, %lambda)
{
	%l = %value.length();
	%quotient = bigint__createArray(%length);
	%remainder = 0;

	%base = $bigint::base;

	for (%i = %length - 1; %i >= 0; %i--)
	{
		%divisor = %remainder * %base + %value.get(%i);
		%q = bigint__truncate(%divisor / %lambda);
		%remainder = %divisor - %q * %lambda;
		%quotient.set(%i, %q | 0);
	}

	$bigint::remainder = %remainder | 0;
	return %quotient;
}

// Shift an array to the left
function bigint__shiftLeft(%x, %n)
{
	%r = Array();
	while (%n > 0)
	{
		%n--;
		%r.push(0);
	}
	return %r.concat(%x);
}

// Compare two arrays, ignoring sign
function bigint__compareAbs(%a, %b)
{
	if (%a.length() != %b.length())
		return %a.length() > %b.length() ? 1 : -1;
	for (%i = %a.length() - 1; %i >= 0; %i--)
		if (%a.get(%i) != %b.get(%i))
			return (%a.get(%i) > %b.get(%i)) ? 1 : -1;
	return 0;
}

// Trim an array from eccess leftmost values
function bigint__trim(%v)
{
	%i = %v.length();
	while (%v.get(%i--) $= 0 && %i > 0) {}
	%v.resize(%i++);
}

// Create an array of certain length
function bigint__createArray(%length)
{
	%x = Array();
	%i = -1;
	while (%i++ < %length)
	{
		%x.push(0);
	}
	return %x;
}

// Truncate a value to up or down depending on sign
function bigint__truncate(%n)
{
	return (%n > 0) ? mFloor(%n) : mCeil(%n);
}

// Parse a value into an array
function bigint__parseValue(%v)
{
	%l = strlen(%v);
	%r = Array();

	%sign = getSubStr(%v, 0, 1) $= "-";
	if (%sign) %v = getSubStr(%v, 1, strlen(%v));
	%r.sign = %sign;
	// Note: No checks for exponent or decimals

	%max = %l;
	%log = 3;
	%min = %max - %log;
	if (%min < 0) %min = 0;
	while (%max > 0)
	{
		%r.push(getSubStr(%v, %min, %max - %min) | 0);
		%min -= %log;
		if (%min < 0)
			%min = 0;
		%max -= %log;
	}
	trim(%r);
	return %r;
}

// Convert an array into a string
function bigint__toString(%v)
{
	%l = %v.length();
	%str = %v.get(%l--);
	%zeros = "000";
	while (%l-- >= 0)
	{
		%digit = %v.get(%l);
		%str = %str @ getSubStr(%zeros, 0, strlen(%zeros) - strlen(%digit)) @ %digit;
	}
	%sign = %v.sign ? "-" : "";
	return %sign @ %str;
}
