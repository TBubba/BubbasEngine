@ECHO OFF

REM This batch should be run after compiling the game
REM This is for getting everything necessary to run the game from VisualStudio
REM
REM Arguments:
REM 	%1: Solution Directory (where the solution is located (projects root directory))
REM 	%2: Target Directory (where the game was compiled)

REM Import content to target directory
CALL ImportContent %1Content\ %2Content\

PAUSE