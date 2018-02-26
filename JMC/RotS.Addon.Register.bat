@setlocal enableextensions
@cd /d "%~dp0"

%WinDir%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe RotS.Addon.Core.dll
%WinDir%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe RotS.Addon.Bot.dll
%WinDir%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe RotS.Addon.Group.dll
%WinDir%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe RotS.Addon.Toggle.dll

pause