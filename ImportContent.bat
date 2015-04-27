@ECHO OFF

REM This is for copying content from the content directory to the games content directory
REM
REM Arguments:
REM 	%1: Source Content Directory (where the content should be copied from)
REM 	%2: Target Content Directory (where the games content will be loaded from)

ECHO ImportContent STARTED
ECHO.

REM Remove and create a directory for the target directory
ECHO Emptying target content directory...
IF exist %2 RMDIR /S /Q %2
MKDIR %2
ECHO.

REM Copy content from the source to the target directory
ECHO Copying content from source to target directory...
%WINDIR%\system32\xcopy.exe %1*.* %2 /R /S /Y
ECHO.

IF %errorlevel% NEQ 0 GOTO ERROR
GOTO OK

:ERROR
ECHO ImportContent [FAILED] (ErrorLevel: %errorlevel%)
ECHO.
GOTO END

:OK
ECHO ImportContent [SUCCEEDED]
ECHO.

:END