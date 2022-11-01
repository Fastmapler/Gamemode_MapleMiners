// Test to verify the functionality of the library

exec("./bigint.cs");

// Test all possible functionalities
function biginttest()
{
	if (bigint_add(500, 150) != 650)
		echo("Add 500 + 150 invalid");
	if (bigint_add(1500, 2500) != 4000)
		echo("Add 1500 + 2500 invalid");
	if (bigint_add("9483049584726374829384728377323947585720345827309682730496827304968273049682730940680298344509283475", "1981724981245897126895716298751692875691827365918369867227365981723659823627272721726598172635981726") !$= "11464774565972271956280444676075640461412173193228052597724193286691932873310003662406896517145265201")
		echo("Add 100 digits invalid");

	if (bigint_subtract(500, 150) != 350)
		echo("Subtract 500 - 150 invalid");
	if (bigint_subtract(1500, 2500) != -1000)
		echo("Subtract 1500 - 2500 invalid");
	if (bigint_subtract("9483049584726374829384728377323947585720345827309682730496827304968273049682730940680298344509283475", "1981724981245897126895716298751692875691827365918369867227365981723659823627272721726598172635981726") !$= "7501324603480477702489012078572254710028518461391312863269461323244613226055458218953700171873301749")
		echo("Subtract 100 digits invalid");

	if (bigint_multiply(500, 150) != 75000)
		echo("Multiply 500 * 150 invalid");
	if (bigint_multiply(1500, 2500) !$= "3750000")
		echo("Multiply 1500 * 2500 invalid");
	if (bigint_multiply("9483049584726374829384728377323947585720345827309682730496827304968273049682730940680298344509283475", "1981724981245897126895716298751692875691827365918369867227365981723659823627272721726598172635981726") !$= "18792796260445787696055160227096792534239279007016515534394835468220956438164839345406496804832537137222155163360031021589744576226150452453327124951258832180653560870110769706116840300764427453777850")
		echo("Multiply 100 digits invalid");

	if (bigint_divmod(500, 150) != 3 || $bigint::remainder != 50)
		echo("Div 500 / 150 invalid");
	if (bigint_divmod(1500, 2500) != 0 || $bigint::remainder != 1500)
		echo("Div 1500 / 2500 invalid");
	if (bigint_divmod("9483049584726374829384728377323947585720345827309682730496827304968273049682730940680298344509283475", "1981724981245897126895716298751692875691827365918369867227365981723659823627272721726598172635981726") !$= "4" || $bigint::remainder !$= "1556149659742786321801863182317176082953036363636203261587363378073633755173640053773905653965356571")
		echo("Div 100 digits invalid");
}
