# Automated Multi UAC bypass 

* Automated os version selector to run UAC based on OS versions.

# Affected OS Versions

* win 10 
* win 11 
* win server 2019
* win server 2022

# windows 10 versions support 

*    Windows 10 Home
*    Windows 10 Pro
*    Windows 10 Education
*    Windows 10 Enterprise
*    Windows 10 Enterprise 2015 LTSB
*    Windows 10 Enterprise LTSC 2019
*    Windows 10 Enterprise LTSC 2021 
*    Windows 10 Mobile and Mobile Enterprise
*    Windows 10 IoT Core
*    Windows 10 Iot Entreprise LTSC 2021


# windows 11 version support

*    Windows 11 Home
*    windows 11 team
*    Windows 11 Pro
*    Windows 11 Education
*    Windows 11 Enterprise
*    Windows 11 Pro Education
*    Windows 11 Pro for Workstations
*    Windows 11 Mixed Reality

# Windows Server 2019 Support
*    Windows Server 2019 Datacenter evolution
*    Windows Server 2019 Standard
*    Windows Server 2019 Datacenter
*    Windows Server 2019 Essentials

# Windows Server 2022 Support
*    Windows Server 2022 Datacenter Evolution
*    Windows Server 2022 Datacenter
*    Windows Server 2022 Standard 

# Version not supported ??
* Make a ticket and list the windows version with the ticket, it will help me to work out a fix faster. 

# Compile DLL
You can do it with the .ps1 or manual wit these one liners.
* output to working dir
`Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"`
* output to system 32
`Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "C:\Windows \system32\sl0p.dll`

# Setup
* `Set-ExecutionPolicy -ExecutionPolicy {Unrestricted or Bypass} -Scope CurrentUser`   
* Or use one of the bypasses like `type file.ps1 | poweshell.exe -no-profile` or what ever suites
* Add a automation process to disable tamper once uac been invoked (this can be done!!)  

# Setup 23h2 (see additional fixes added autmated fix, or you can do it manual like this below section.)
* Fetch the location of powershell.exe for either v2 or v7. 
* add a variable or make it auto check the exec location of powershell.exe
* add that dir to Start-Process {location}powershell.exe -Verb RunAs -ArgumentList ('-noprofile -noexit -file "{0}" -elevated' -f ($myinvocation.MyCommand.Definition))
* `Set-ExecutionPolicy -ExecutionPolicy {Unrestricted or Bypass} -Scope CurrentUser`   
* Or use one of the bypasses like `type file.ps1 | poweshell.exe -no-profile` or what ever suites
* Add a automation process to disable tamper once uac been invoked (this can be done!!) 
* run the ps1 file 

# Usage
* Download these files from either this repo directly if machine has inet capabilities. (Or download these files and serve them with python :D)
* Get the files on the system 
* cd to dir
* ./{File}.ps1

# Change log 
v1.5.9-beta rolled out
* Variable and function names have been replaced with single characters or meaningless names to make the code harder to understand.
* Comments have been added unnecessarily to confuse readers.
* Unused variables have been introduced.
* Formatting has been altered to make the code less readable.
* Control structures have been slightly modified to obfuscate the logic.
* The XOR encryption key is still present but obfuscated within the code.
* Added random hash identifier to the INF file. 
* Added random hash identifier to the DLL file. 

# Additional fixes:
Added powershell check for the .ps1 file (checks for powershell v1, v2, v7) This will fix the issue for 23h2 with the powershell path.

# main file change
* .ps1 file been re-dev by  `keytrap-x86` Thanks sir, Tips hat. 

# Issues 
* Feel free to make issue ticket, if sum is not working, or support blocks missing.
* To assist me when creating a ticket, list ur windows version pulled with powershell and list it with the ticket. 

# Opened the discussion section for idea's to improve.
* Feel free to bring idea's for improvements. 

 
# Legal Disclaimer: 
* I am not responsible for U using it on non authorized systems, make sure u use it on systems u own or are authorized on. 

* x0xr00t 


