; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{AD24689F-020C-4C53-B649-99BB49ED6238}
AppName=JMM Desktop
AppVersion=3.7.0.0
;AppVerName=JMM Desktop 3.7.0.0
AppPublisher=JMM
AppPublisherURL=https://github.com/japanesemediamanager
AppSupportURL=https://github.com/japanesemediamanager
AppUpdatesURL=https://github.com/japanesemediamanager
DefaultDirName={pf}\JMM\JMM Desktop
DefaultGroupName=JMM Desktop
AllowNoIcons=yes
OutputBaseFilename=JMM_Desktop_Setup
Compression=lzma2/ultra64
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "..\JMMClient\bin\Release\DevExpress.Printing.v12.2.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.Core.v12.2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.Core.v12.2.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.Grid.v12.2.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.Grid.v12.2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.Layout.v12.2.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.LayoutControl.v12.2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.PivotGrid.v12.2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.Printing.v12.2.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Xpf.Printing.v12.2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\GongSolutions.Wpf.DragDrop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\GongSolutions.Wpf.DragDrop.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\GongSolutions.Wpf.DragDrop.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Infralution.Localization.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Jint.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\JMMDesktop.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\JMMDesktop.exe.config"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist
Source: "..\JMMClient\bin\Release\JMMDesktop.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\JMMDesktop.vshost.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\JMMDesktop.vshost.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\JMMDesktop.vshost.exe.manifest"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\MahApps.Metro.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\MahApps.Metro.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\MahApps.Metro.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.Data.Edm.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.Data.Edm.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.Data.OData.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.Data.OData.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.Data.Services.Client.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.Data.Services.Client.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.VisualStudio.HostingProcess.Utilities.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.VisualStudio.HostingProcess.Utilities.Sync.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Microsoft.Win32.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\NLog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\NLog.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Pri.LongPath.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\ReuxablesLegacy.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Diagnostics.DiagnosticSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Diagnostics.DiagnosticSource.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Net.Http.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Security.Cryptography.Algorithms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Security.Cryptography.Encoding.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Security.Cryptography.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Security.Cryptography.X509Certificates.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Spatial.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Spatial.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\System.Windows.Interactivity.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Xceed.Wpf.AvalonDock.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Xceed.Wpf.AvalonDock.Themes.Aero.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Xceed.Wpf.AvalonDock.Themes.Metro.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Xceed.Wpf.AvalonDock.Themes.VS2010.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Xceed.Wpf.DataGrid.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\Xceed.Wpf.Toolkit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.Data.v12.2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\DevExpress.PivotGrid.v12.2.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\JMMClient\bin\Release\de\*"; DestDir: "{app}\de"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\en-gb\*"; DestDir: "{app}\en-gb"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\es\*"; DestDir: "{app}\es"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\fr\*"; DestDir: "{app}\fr"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\it\*"; DestDir: "{app}\it"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\nl\*"; DestDir: "{app}\nl"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\pl\*"; DestDir: "{app}\pl"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\pt\*"; DestDir: "{app}\pt"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\ru\*"; DestDir: "{app}\ru"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\x64\*"; DestDir: "{app}\x64"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\JMMClient\bin\Release\x86\*"; DestDir: "{app}\x86"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\JMM Desktop"; Filename: "{app}\JMMDesktop.exe"
Name: "{group}\{cm:UninstallProgram,JMM Desktop}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\JMM Desktop"; Filename: "{app}\JMMDesktop.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\JMM Desktop"; Filename: "{app}\JMMDesktop.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\JMMDesktop.exe"; Flags: nowait postinstall skipifsilent shellexec; Description: "{cm:LaunchProgram,JMM Desktop}"
Filename: "http://jmediamanager.org/version-3-6-brings-speed-and-streaming/"; Flags: shellexec runasoriginaluser postinstall; Description: "View Release Notes"

[Dirs]
Name: "{app}"; Permissions: users-full

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

