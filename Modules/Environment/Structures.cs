function MM_LoadStructure(%name, %pos)
{
    %file = new FileObject();
    %file.openForRead("config/Props and Placement/" @ %name @ ".bls");

	%structSet = new SimSet();
	//Color reading code by Zeblote; thanks!
	//Skip file header
	%file.readLine();
	%cnt = %file.readLine();

	for(%i = 0; %i < %cnt; %i++)
		%file.readLine();

	//generate color table - edited by Conan
	//save color table for each prop so the laggy loading wont happen every time the save is opened.
	//add a function to reset color tables
	if ($MM::Colortable[%name @ "::" @ 0] == 0)
		for(%i = 0; %i < 64; %i++)
			$MM::ColorTable[%name @ "::" @ %i] = getClosestColorID(getColorI(%file.readLine()))+1;

	while (!(getWord(%lastline, 0) $= "Linecount"))
		%lastline = %file.readLine();

	while(!%file.isEOF())
	{
		%line = %file.readLine();
		if (getSubStr(%line, 0, 2) $= "+-")
		{
			MMapplyProperties(%brick, %line);
			continue;
		}

		//thank god for zack0wack0
		//http://forum.blockland.us/index.php?topic=157516.msg3787924#msg3787924
		%line = trim(nextToken(%line, "brickUIname", "\""));
		%brickDatablock = $uiNameTable[%brickUIname];
		if (!isObject(%brickDatablock))
			continue;
		%brickPos = getWords(%line, 0, 2);
		%brickAngle = getWord(%line, 3);
		%brickPrintID = getWord(%line, 4);
		%brickColor = $MM::ColorTable[%name @ "::" @ getWord(%line, 5)]-1;
		%brickPrint = getWord(%line, 6);
		if (%brickPrint !$= "")
			%brickPrint = $printNameTable[%brickPrint];
		%brickColorFX = getWord(%line, 7);
		%brickShapeFX = getWord(%line, 8);

		%brickRaycasting = getWord(%line, 9);
		%brickCollision = getWord(%line, 10);
		%brickRendering = getWord(%line, 11);
		//calculate position offsett
		if (!%hasCalculatedOffset)
		{
			%pos = roundShiftVector(%pos);
			%offset = roundShiftVector(vectorSub(%pos, %brickPos));
			%offset = getWords(%offset, 0, 1) SPC getWord(%offset, 2)+roundToNearestFifth(%brickDatablock.brickSizeZ/10-0.2);
			%hasCalculatedOffset = 1;
		}

		//code from Zeblote
		switch(%brickAngle)
		{
			case 0: %brickRot = "1 0 0 0";
			case 1: %brickRot = "0 0 1 90.0002";
			case 2: %brickRot = "0 0 1 180";
			case 3: %brickRot = "0 0 -1 90.0002";
		}

		%brick = new fxDTSBrick()
		{
			datablock = %brickDatablock;
			position = vectorAdd(%brickPos, %offset);
			rotation = %brickRot;
			angleID = %brickAngle;
			colorID = %brickColor;
			colorFXID = %brickColorFX;
			shapeFXID = %brickShapeFX;
			printID = %brickPrint;

			raycasting = %brickRaycasting;
			collision = %brickCollision;
			rendering = %brickRendering;

            isPlanted = true;
		};
        %brick.plant();
        %brick.setTrusted(1);
        BrickGroup_1337.add(%brick);
		%structSet.add(%brick);
		%brick.structure = %structSet;
	}
	%file.close();
	%file.delete();



	//calculates useful values and saves them into the ghostgroup
	//collects the prop bounds
	for (%i = 0; %i < %structSet.getCount(); %i++)
	{
		%box = %structSet.getObject(%i).getWorldBox();
		%minX = mRound(getWord(%box, 0)*10)/10;
		%minY = mRound(getWord(%box, 1)*10)/10;
		%minZ = mRound(getWord(%box, 2)*10)/10;
		%maxX = mRound(getWord(%box, 3)*10)/10;
		%maxY = mRound(getWord(%box, 4)*10)/10;
		%maxZ = mRound(getWord(%box, 5)*10)/10;
		if (%i == 0)
		{
			%structSet.minX = %minX;
			%structSet.minY = %minY;
			%structSet.minZ = %minZ;
			%structSet.maxX = %maxX;
			%structSet.maxY = %maxY;
			%structSet.maxZ = %maxZ;
			continue;
		}
		if (%structSet.minX > %minX) %structSet.minX = %minX;
		if (%structSet.minY > %minY) %structSet.minY = %minY;
		if (%structSet.minZ > %minZ) %structSet.minZ = %minZ;
		if (%structSet.maxX < %maxX) %structSet.maxX = %maxX;
		if (%structSet.maxY < %maxY) %structSet.maxY = %maxY;
		if (%structSet.maxZ < %maxZ) %structSet.maxZ = %maxZ;
	}
	//gets center of selection, and dimensions
	%structSet.minPos = %structSet.minX SPC %structSet.minY SPC %structSet.minZ;
	%structSet.maxPos = %structSet.maxX SPC %structSet.maxY SPC %structSet.maxZ;
	
	%structSet.structSizeX = %structSet.maxX - %structSet.minX;
	%structSet.structSizeY = %structSet.maxY - %structSet.minY;
	%structSet.structSizeZ = %structSet.maxZ - %structSet.minZ;

	//adjust center if size is odd on x/y axis and even on other.
	%structSet.center = (%structSet.maxX -(%structSet.structSizeX/2)) SPC (%structSet.maxY -(%structSet.structSizeY/2)) SPC (%structSet.maxZ -(%structSet.structSizeZ/2));

	//Reveal surrounding blocks
	revealArea(vectorAdd(%structSet.minPos, "-1 -1 -1"), vectorAdd(%structSet.maxPos, "1 1 2"));
}

function MMapplyProperties(%brick, %line, %angleID)
{
	if (getWord(%line, 0) $= "+-EVENT")
		MMapplyEvent(%brick, %line, %angleID);
	if (getWord(%line, 0) $= "+-LIGHT")
		MMapplyLight(%brick, %line);
	if (getWord(%line, 0) $= "+-ITEM")
		MMapplyItem(%brick, %line, %angleID);
	if (getWord(%line, 0) $= "+-EMITTER")
		MMapplyEmitter(%brick, %line, %angleID);
}

function MMapplyItem(%brick, %line, %angleID)
{
	//takes in a %brick and a %line from a save file (including the +- prefix)
	//and applies the line's item onto the brick
	%line = restWords(%line);
	%itemNameEnd = stripos(%line, "\"");
	%itemName = getSubStr(%line, 0, %itemNameEnd);
	%line = getSubStr(%line, %itemNameEnd+2, strlen(%line)-%itemNameEnd-1);
	%itemPos = getWord(%line, 0);
	if (%itemPos > 1)
		%itemPos = 2+(%itemPos-2+%angleID)%4;
	%itemDir = 2+(getWord(%line, 1)-2 + %angleID)%4;
	%itemTimeout = getWord(%line, 2);

	%brick.setItem($uiNameTable_Items[%itemName]);
	%brick.setItemDirection(%itemDir);
	%brick.setItemPosition(%itemPos);
	%brick.setItemRespawnTime(%itemTimeout);
}

function MMapplyEmitter(%brick, %line, %angleID)
{
	//takes in a %brick and a %line from a save file (including the +- prefix)
	//and applies the line's emitter onto the brick
	%line = restWords(%line);
	%emitterNameEnd = stripos(%line, "\"");
	%emitterName = getSubStr(%line, 0, %emitterNameEnd);
	%line = getSubStr(%line, %emitterNameEnd+2, strlen(%line)-%emitterNameEnd-1);
	%emitterDir = getWord(%line, 0);
	if (%emitterDir > 1)
		%emitterDir = 2+(%emitterDir-2+%angleID)%4;

	%brick.setemitter($uiNameTable_emitters[%emitterName]);
	%brick.setemitterDirection(%emitterDir);
}

function MMapplyLight(%brick, %line)
{
	%line = restWords(%line);
	%lightNameEnd = stripos(%line, "\"");
	%light = getSubStr(%line, 0, %lightNameEnd);
	if ($uiNameTable_Lights[%light] > 0)
		%brick.setLight($uiNameTable_Lights[%light]);
}

function MMapplyEvent(%brick, %line, %angleID)
{
	%idx = getField(%line, "1");
    %enabled = getField(%line, "2");
    %inputName = getField(%line, "3");
    %delay = getField(%line, "4");
    %targetName = getField(%line, "5");
    %NT = getField(%line, "6");
    %outputName = getField(%line, "7");
    %par1 = getField(%line, "8");
    %par2 = getField(%line, "9");
    %par3 = getField(%line, "10");
    %par4 = getField(%line, "11");
    if (isObject(%par1) && !isInteger(%par1) && getWordCount(%par1) == 1)
    	%par1 = nameToID(%par1);
    if (isObject(%par2) && !isInteger(%par2) && getWordCount(%par2) == 1)
    	%par2 = nameToID(%par2);
    if (isObject(%par3) && !isInteger(%par3) && getWordCount(%par3) == 1)
    	%par3 = nameToID(%par3);
    if (isObject(%par4) && !isInteger(%par4) && getWordCount(%par4) == 1)
    	%par4 = nameToID(%par4);

    %inputEventIdx = inputEvent_GetInputEventIdx(%inputName);
    %targetIdx = inputEvent_GetTargetIndex("fxDTSBrick", %inputEventIdx, %targetName);
    
    if(%targetName == (-1))
        %targetClass = "fxDTSBrick";
	else
	{
		%field = getField($InputEvent_TargetList["fxDTSBrick", %inputEventIdx], %targetIdx);
		%targetClass = getWord(%field, "1");
	}
    %outputEventIdx = outputEvent_GetOutputEventIdx(%targetClass, %outputName);
    %NTNameIdx = (-1);
	
	//check for rotation
	//check for vector parameters
	%paramList = $OutputEvent_ParameterList[%targetClass, %outputEventIdx];
	%paramCount = getFieldCount(%paramList);
	for(%k = 0; %k < %paramCount; %k++)
	{
		if(getWord(getField(%paramList, %k), 0) $= "vector")
		{
			//apply rotation effects
			%vec = %par[%k+1];

			for (%i = 0; %i < 4-%angleID; %i++)
				%vec = -getWord(%vec, 1) SPC getWord(%vec, 0) SPC getWord(%vec, 2);

			%par[%k+1] = %vec;
		}
	}
	//check for relay events
	switch$(%outputName)
	{
		case "fireRelayNorth": %dir = 2;
		case "fireRelayEast":  %dir = 3;
		case "fireRelaySouth": %dir = 4;
		case "fireRelayWest":  %dir = 5;
		default: %dir = -1;
	}
	if(%dir >= 0)
	{
		%rotated = %dir;
		%rotated = (%rotated + %angleID - 2) % 4 + 2;
		//Apply mirror effects
		%outputEventIdx += %rotated - %dir;

		switch(%rotated)
		{
			case 0: %outputName = "fireRelayUp";
			case 1: %outputName = "fireRelayDown";
			case 2: %outputName = "fireRelayNorth";
			case 3: %outputName = "fireRelayEast";
			case 4: %outputName = "fireRelaySouth";
			case 5: %outputName = "fireRelayWest";
		}
	}
    //talk(%brick SPC %enabled SPC %inputEventIdx SPC %delay SPC %targetName SPC %targetIDX SPC %NTNameIdx SPC %outputName SPC %outputEventIdx SPC %par1 SPC %par2 SPC %par3 SPC %par4);
    //talk(%targetClass @ %outputName);
    //talk(%NT);

    %brick.eventEnabled[%idx] = %enabled;
	%brick.eventInput[%idx] = %inputName;
	%brick.eventInputIdx[%idx] = %inputEventIdx;
	%brick.eventDelay[%idx] = %delay;
	%brick.eventTarget[%idx] = %targetName;
	%brick.eventTargetIDX[%idx] = %targetIdx;
	if (%NT !$= "")
		%brick.eventNT[%idx] = %NT;
	%brick.eventOutput[%idx] = %outputName;
	%brick.eventOutputIdx[%idx] = %outputEventIdx;
	if (%par1 !$= "")
		%brick.eventOutputParameter[%idx,1] = %par1;
	if (%par2 !$= "")
		%brick.eventOutputParameter[%idx,2] = %par2;
	if (%par3 !$= "")
		%brick.eventOutputParameter[%idx,3] = %par3;
	if (%par4 !$= "")
		%brick.eventOutputParameter[%idx,4] = %par4;
	%brick.eventOutputAppendClient[%idx] = $outputEvent_AppendClient[%targetName, %eventOutputIdx]; //fix this asap
	%brick.numEvents = %idx+1;
}