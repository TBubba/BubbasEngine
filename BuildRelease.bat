@ECHO OFF

REM This batch should be run when you want to create a release build
REM This copies the game files 
REM
REM Arguments:
REM 	%1: Solution Directory (where the solution is located (projects root directory))
REM 	%2: Build Directory (where the game was compiled)
REM 	%3: Target Directory (where the release build (output) should be)

REM Export game files to output directory
CALL ExportGame %2 %3

REM Import content to target directory
CALL ImportContent %1Content\ %3Content\

PAUSE