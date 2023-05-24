# Automated Multi UAC bypass 

* Automated os selector to run UAC based on OS.

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

# Setup 
* change the username on each block {obvious} >> (if u want u can use a powershell command to get username automated.)
* `Set-ExecutionPolicy -ExecutionPolicy {Unrestricted or Bypass} -Scope CurrentUser`   
* Or use one of the bypasses like `type file.ps1 | poweshell.exe -no-profile` or what ever suites


# Usage
* Download these files from either this repo directly if machine has inet capabilities. (Or downlaod these files and serve them with python :D)
* Get the files on the system 
* cd to dir
* ./{File}.ps1 (or use the .exe)


# Change log 
v1.4-beta rolled out

* added more supported versions to check for, to start uac bypass based on OS versionS
* see list above for added version windows server2019 and 2022 :D
* the exe is outdated atm needs to be rolled out a new version of it ... soon will come
* This is the last version update i have pushed..
 
# Legal Disclaimer: 
* I am not responsible for U using it on non authorized systems, make sure u use it on systems u own or are authorized on. 

* x0xr00t 


