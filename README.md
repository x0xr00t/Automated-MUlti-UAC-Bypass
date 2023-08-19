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

# Compile Dll

* powershell (the .NET Framework SDK is needed for this.)
```
# Define the paths
$originalCsFilePath = "C:\path\to\CMSTPBypass.cs"      # Replace with the actual path
$newCsFilePath = "C:\path\to\sl0puacb.cs"               # Replace with the desired new path
$originalDllPath = "C:\output\directory\CMSTPBypass.dll"   # Replace with the actual path
$newDllPath = "C:\output\directory\sl0p.dll"             # Replace with the desired new path
# Rename the C# file
Rename-Item -Path $originalCsFilePath -NewName $newCsFilePath
# Rename the compiled DLL
Rename-Item -Path $originalDllPath -NewName $newDllPath
```
* You can do it also with the .ps1 or do it manual with the oneliner in there...!! (.NET Framework SDK is needed for this.)

# Setup 
* In sum cases u must remake the dll (if needed)
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
v1.5.1-beta rolled out
* Changed sl0puacb.cs to have more advanced methods
* Changed sl0puacb.cs to have anti forensics methods

* Added junk data to the .INF file

* {Notify} !! The DLL needs to be remade if u want to use the new sl0puacb.cs !! {Notify}
* The old sl0puacb.cs been moved to /bakcup/sl0puacb.cs, sl0p.dll been moved to /backup/ too.

# Issues 
* Feel free to make issue ticket, if sum is not working, or support blocks missing.
* To assist me when creating a ticket, list ur windows version pulled with powershell and list it with the ticket. 
 
# Legal Disclaimer: 
* I am not responsible for U using it on non authorized systems, make sure u use it on systems u own or are authorized on. 

* x0xr00t 


