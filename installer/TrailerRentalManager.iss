#define AppName      "Trailer Rental Manager"
#define AppVersion   "1.0.0"
#define AppPublisher "Sami9619"
#define AppExeName   "Trailer Rental Manager.exe"
#define SourceDir    "..\Trailer Rental Manager\bin\Release"

[Setup]
AppId={{8F3A2C1D-4B5E-4F6A-9C2D-1E8F7A3B5C9D}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppPublisher}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
OutputDir=..\release
OutputBaseFilename=TrailerRentalManager-Setup-v{#AppVersion}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
ArchitecturesInstallIn64BitMode=x64compatible

[Languages]
Name: "german";   MessagesFile: "compiler:Languages\German.isl"
Name: "english";  MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "{#SourceDir}\{#AppExeName}";          DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}\{#AppExeName}.config";   DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}\x64\SQLite.Interop.dll"; DestDir: "{app}\x64"; Flags: ignoreversion
Source: "{#SourceDir}\x86\SQLite.Interop.dll"; DestDir: "{app}\x86"; Flags: ignoreversion

[Icons]
Name: "{group}\{#AppName}";              Filename: "{app}\{#AppExeName}"
Name: "{group}\{cm:UninstallProgram,{#AppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppName}";        Filename: "{app}\{#AppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#AppName}}"; Flags: nowait postinstall skipifsilent
