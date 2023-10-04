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


# windows 11 version support

*    Windows 11 Home
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
You can do it with the .ps1 or do it manual with these one liners !! 
* output to working dir
`Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "sl0p.dll"`
* output to system 32
`Add-Type -TypeDefinition ([IO.File]::ReadAllText("$pwd\sl0puacb.cs")) -ReferencedAssemblies "System.Windows.Forms" -OutputAssembly "C:\Windows\system32\sl0p.dll`

# Setup 
* U must remake the dll.
* Change username in the script (where ever needed)
* `Set-ExecutionPolicy -ExecutionPolicy {Unrestricted or Bypass} -Scope CurrentUser`   
* Or use one of the bypasses like `type file.ps1 | poweshell.exe -no-profile` or what ever suites
* Add a automation process to disable tamper once uac been invoked (this can be done!!)  

# Usage
* Download these files from either this repo directly if machine has inet capabilities. (Or download these files and serve them with python :D)
* Get the files on the system 
* cd to dir
* ./{File}.ps1

# Change log 
v1.5.2-beta rolled out
* Changed sl0puacb.cs to have more advanced methods
* This version adds some additional evasion techniques and obfuscation to make it more challenging to detect

* In this improved version, we've added the following enhancements:

    Delayed execution of PowerShell code: We add a delay before executing PowerShell code to evade immediate detection.

    Obfuscated PowerShell code: The PowerShell code to execute is encoded in base64, making it more challenging to analyze.

    Multi-stage execution: The code is broken into multiple stages, executed at different times during the process, to further obfuscate its behavior.


* Added obfuscation with xor on the .INF
* Added process ghosting (exec a alternative for explorer.exe) 

# Set B64 encoded ps1 payload in the .INF data in sl0ppyuacb.cs 
* Don't forget to change >> '>b64PayloadHere<' << in the INF data!!..to sum b64 encoded ps1 u want to exec, before rolling out the .DLL file. 

# Notify !!
* {Notify} !! The DLL needs to be remade if u want to use the new sl0puacb.cs !! {Notify}
* The old sl0puacb.cs been moved to /bakcup/sl0puacb.cs, sl0p.dll been moved to /backup/ too.

# Issues 
* Feel free to make issue ticket, if sum is not working, or support blocks missing.
* To assist me when creating a ticket, list ur windows version pulled with powershell and list it with the ticket. 

# Opened the discussion section for idea's to improve.
* Feel free to bring idea's for improvements. 

 
# Legal Disclaimer: 
* I am not responsible for U using it on non authorized systems, make sure u use it on systems u own or are authorized on. 

* x0xr00t 


