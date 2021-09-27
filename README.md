# win-server2022-UAC-Bypass
* Affects also other versions 

# Affected OS Versions

* win 10 
* win 11 
* win server 2019
* win server 2022

# Introduction

* Hello folks, i am Patrick Hoogeveen Aka x0xr00t. 
* The quest to this hunt started a while back.... 
* The main thing i noticed when installing windows 11 and trying it out, was that i could use a windows 10 serial key activator. 
* This version it validiated on was not included in to the installer of windows 11 wich is enterprise, mine windows activated as such successfull. 
* At that time i realized under the hood it still was the basic windows 10 for most of its structure, so with that came the next logics that came up to me. 
* Most of the previous found local exploits should possible work on widows 11 so, with that in mind i started compiling a couple things like the needed files for the Bypass. 
* I tested it and before i knew i had a cmd system32 invoked .... i was like cewl we got local privesc :D
* So with that i started to think well the windows server just been released how likely would it be same senario is going on here. 

* Same senario i mean under the hood previous mentioned part above, the internal structure.
* So i started transfering the files after i saw the windows 10 like start balk in windows server 2022, which made me realize this should be working. 
* I run the files and stumbled on 2 password authentictions, and i was like ugh gotta find a way around this, so i digged around for a solution and well behold. 

* After a while the popups where gone and system32 cmd with powershell admin invoked. Cewl privesc going on here :D 

# The idea to get rid of enoying popups box ... 
* The how to was pretty easy as when the cmd is called its allready elevated, so i was like lets add a user before it calls the exec of the powershell. 
* And well behold there it was working as i wanted it.
* I closed all boxes took me off the admin group rights and rerun the exploit. 

* Well behold we popped it again and i was like fuck its working nicely, added user thats non admin group to the admingroup to be able to exec elevated powershell. 


# Code example 
* This particular code needs two files to make it all work i will include all of them here in the repo. 
* The Dll i included it for easyness, but there is no need for it being on ur system as it would been made of the .cs file. 

* UAC-Bypass.ps1
* sl0puacb.cs 


# Setup Windows envo 
* windows server 2022 | 2019
* win 11 | win 10 

* Add a new account non admin, on either windows server editions. Or windows editions. 
* Set Exec policy powershell remote or bypass or unrestricted. 
* Add a rdp 

# Usage 
* Download these files from either this repo directly if machine has inet cappabilities. (Or downlaod these files and serve them with python :D)  
* Get the files on the system with the low user access. 
* cd to dir 
* Edit the UAC-Bypass.ps1 on line 23 {UserNamehere} Put ur made username there without the {} and save the file, and give it a go 
* ./UAC-Bypass.ps1

# What is going on in the code ??
* Running the ps1 will make the dll from the .cs, then uses the $pwd to invoke the cmd with this reflected dll. 
* It then runs the cmd as administrator by reflected dll attack. 
* From here on we can invoke a powershell elevated as admin and disable all security mechanisms, forcefully uninstall defender, and lock the file system in worst case. 
 

 
# Thats it UAC Bypass privesc windows server 2022 with reflected dll.
* x0xr00t 

# Legal Disclaimer: 
* I am not responcible for U using it on non authorized systems, make sure u use it on systems u own or are authorized on. 

