@setlocal enableextensions
@cd /d "%~dp0"

%WinDir%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe RotS.LineParser.Core.dll
%WinDir%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe RotS.LineParser.Bot.dll

pause