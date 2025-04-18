[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Setup]
; Nombre de la aplicación
AppName=Teknogadged
; Versión de la aplicación
AppVersion=26.0
; Carpeta de salida del instalador
OutputDir=Output
; Nombre del archivo del instalador
OutputBaseFilename=Teknogadged Software
; Carpeta de instalación predeterminada
DefaultDirName={pf}\Teknogadged Software
; Nombre del grupo de programas
DefaultGroupName=Teknogadged Software
; Icono del instalador (opcional)
SetupIconFile=Tek.ico
; Idioma del instalador
LanguageDetectionMethod=UILanguage

[Files]
; Archivos a incluir en el instalador
Source: "publish\TeknoGadeged Software.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "publish\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "publish\brand\Tek.png"; DestDir: "{app}\brand"; Flags: ignoreversion
Source: "publish\brand\Tek.ico"; DestDir: "{app}\brand"; Flags: ignoreversion


[Icons]
; Crear accesos directos
Name: "{group}\TeknoGadeged Software"; Filename: "{app}\TeknoGadeged Software.exe"
Name: "{commondesktop}\TeknoGadeged Software"; Filename: "{app}\TeknoGadeged Software.exe"; Tasks: desktopicon

[Run]
; Ejecutar la aplicación después de la instalación
Filename: "{app}\TeknoGadeged Software.exe"; Description: "{cm:LaunchProgram,TeknoGadeged Software}"; Flags: nowait postinstall skipifsilent