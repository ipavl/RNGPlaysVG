RNGPlaysVG
==========

Author: ipavl <https://github.com/ipavl/RNGPlaysVG>, February 2014

A program that works in similar fashion to the script used on http://www.twitch.tv/rngplayspokemon,
written in C# with the AutoIt Library (http://www.autoitscript.com/site/). It can theoretically
attach itself to any process and send key inputs generated using random numbers.

At the "Attach to which process?" you should enter the name of the program you wish to attach the
program to (e.g. visualboyadvance-m for VBA-M if the executable is called VisualBoyAdvance-M.exe)
and if you want a delay between generating inputs you should specify that at the second prompt (the
default is 100ms if you do not enter anything).

Key mapping
-----------
Keys are defined in the `KeyBank` enum. `COUNT` should always be the final item in the enum as
this is what tells the RNG how many possible outputs there are to generate between. You will manually
have to define which key maps to what, but as the keys are defined in the enum all you need to do
is add a block of code like so:

	else if (key == (int)KeyBank.SELECT)
	{
		Console.WriteLine("rng=" + key + " => SELECT");
		AutoItX3Declarations.AU3_Send("{BACKSPACE}", 0);
	}
	
You should change `(int)KeyBank.SELECT` to the key defined in the enum and the `AutoItX3Declarations`
line should contain the actual key you want to map to that enum value. The `Console.WriteLine` line is
purely informative and is shown in the console window when the application is running.

See AutoIt's website for further reference on key namings and what else you can put where `{BACKSPACE}`
is: https://www.autoitscript.com/autoit3/docs/functions/Send.htm

The default mapping is:

* A button  =>  Z
* B button  =>  X
* START     =>  Enter
* SELECT    =>  Backspace
* D-Up      =>  Up Arrow
* D-Down    =>  Down Arrow
* D-Left    =>  Left Arrow
* D-Right   =>  Right Arrow