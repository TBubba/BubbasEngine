

rem This is for copying content from the content directory to the games content directory
rem
rem Arguments:
rem 	%1: Target Content Directory (where the content should be copied from)
rem 	%2: Target Game Content Directory (where the games content will be loaded from)

rem Remove and create a folder for the content
if exist %2 rmdir /s /q %2
mkdir %2

rem Copy content from the content directory to the 
%WINDIR%\system32\xcopy.exe %1 %2 /s /y /e
echo Exit code: %errorlevel%

pause