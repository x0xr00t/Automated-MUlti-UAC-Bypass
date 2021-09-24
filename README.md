# win-server2022-UAC-Bypass

# Introduction

* Hello folks, i am Patrick Hoogeveen Aka x0xr00t. 
* The quest to this hunt started a while back.... 
* The main thing i noticed when installing windows 11 and trying it out, was that i could use a windows 10 serial key activator. 
* This version it validiated on was not included in to the installer of windows 11 wich is enterprice, mine windows activated as such successfull. 
* At that time i realized under the hood it still was the basic windows 10 for most of its structure, so with that came the next logics that came up to me. 
* Most of the previous found local exploits should possible work on widows 11 so, with that in mind i started compiling a couple things like the needed files for the Bypass. 
* I tested it and before i knew i had a cmd system32 incvoked .... i was like cewl we got local privesc :D
* So with that i started to think well the windows server just been released how likely would it be same senario is going on here. 

* Same senario i mean under the hood previuos mentioned part above, the internal structure.
* So i started transfering the files after i saw the windows 10 like start balk in windows server 2022, which made me realize this should be working. 
* I run the files and stumbled on 2 password authentictions, and i was like ugh gotta find a way around this, so i digged around for a solution and well behold. 

* After a while the popups where gone and system32 cmd with powershell admin invoked. Cewl privesc going on here :D 

# The idea to get rid of enoying popups box ... 
* The how to was pretty easy as when the cmd is called its allready elevated, so i was like lets add a user before it calls the exec of the powershell. 
* And well behold there it was working as i wanted it.
* I closed all boxes took me off the admin group rights and rerun the exploit. 

* Well behold we popped it again and i was like fuck its working nicely, added user thats non admin group to the admingroup to be able to exec elevated powershell. 


# Code example of the ps1 file i made for the UAC bypass u can find in the repo
* This particular code needs two files to make it all work i will include all of them here in the repo. 


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
* ./


# Thats it privesc windows server 2022 
* X0xr00t 
