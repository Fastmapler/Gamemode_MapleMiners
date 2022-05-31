datablock PlayerData(PlayerMapleMinersArmor : PlayerStandardArmor)
{
    maxDamage = 100;
	maxEnergy = 100;
	repairRate = 0.33;
	rechargeRate = 0.8;

	runForce = 48 * 90;
	maxForwardSpeed = 7;
	maxBackwardSpeed = 4;
	maxSideSpeed = 6;

	airControl = 0.3; //0.1

	maxForwardCrouchSpeed = 3;
	maxBackwardCrouchSpeed = 2;
	maxSideCrouchSpeed = 2;

	maxUnderwaterForwardSpeed = 8.4;
	maxUnderwaterBackwardSpeed = 7.8;
	maxUnderwaterSideSpeed = 7.8;

	jumpForce = 12 * 90;
    minJetEnergy = 2;
	jetEnergyDrain = 2;
	canJet = true;

	boundingBox = VectorScale ("1.25 1.25 2.65", 4);
	crouchBoundingBox = VectorScale ("1.25 1.25 1.00", 4); //1.25 1.25 1.00

	maxWeapons = 5;
	maxTools = 5;

	uiName = "Maple Miners Player";

	showEnergyBar = true;
};

function GameConnection::GetPickaxeDamage(%client)
{
	return mClamp(%client.MM_PickaxeLevel, 1, 999999);
}