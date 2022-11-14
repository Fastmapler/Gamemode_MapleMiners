//Can we just find the folder names automatically?
$EOTW::Modules = "AddOns Core Environment Player Matter Tools";

for (%i = 0; %i < getWordCount($EOTW::Modules); %i++)
	exec("./Modules/" @ getWord($EOTW::Modules, %i) @ "/" @ getWord($EOTW::Modules, %i) @ ".cs");

//One of our goals is to keep the code as organized as possible. We split each section of code into their own
//folders. Even though loading more files may take longer, the better code organization and readability is worth it.

function EnableTracing()
{
	if (isFunction("trace2"))
		trace2(1);
}
EnableTracing();