@ECHO OFF

REM This is for copying game files from the build directory to the output directory 
REM
REM Arguments:
REM 	%1: Source Directory (where the game files should be copied from)
REM 	%2: Target Directory (where the game will be located)

ECHO ExportGame STARTED
ECHO.

REM Remove and create a directory for the target directory
ECHO Emptying target directory...
IF exist %2 RMDIR /S /Q %2
MKDIR %2
ECHO.

REM Copy game files from the source to the target directory
ECHO Copying game files from source to target directory...
%WINDIR%\system32\xcopy.exe %1*.* %2 /EXCLUDE:OutputExludeFiles.txt /R /S /Y
ECHO.

IF %errorlevel% NEQ 0 GOTO ERROR
GOTO OK

:ERROR
ECHO ExportGame [FAILED] (ErrorLevel: %errorlevel%)
ECHO.
GOTO END

:OK
ECHO ExportGame [SUCCEEDED]
ECHO.

:END