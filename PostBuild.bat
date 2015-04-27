

rem This batch should be run after compiling the game
rem This is for cleaning up and importing content and such
rem
rem Arguments:
rem 	%1: Solution Directory (where the solution is located (projects root directory))
rem 	%2: Target Directory (where the game was compiled)

rem Import content to target directory
ImportContent %1Content\ %2Content\

pause