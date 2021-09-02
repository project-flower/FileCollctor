@SETLOCAL
@ECHO エクスプローラーのコンテキスト メニューに"FileCollector"を追加します。
@PAUSE
@CD /D %~dp0
@SET REG_DIRECTORY_SHELL=HKCU\Software\Classes\Directory\shell
@SET REG_DIRECTORY_BACKGROUND_SHELL=HKCU\Software\Classes\Directory\Background\shell
@SET REG_DRIVE_SHELL=HKCU\Software\Classes\Drive\shell
@SET COMMANDNAME=FileCollector
@CALL :AddShellCommand %REG_DIRECTORY_SHELL%\FileCollector "%COMMANDNAME%" "\"%%%%1\""
@CALL :AddShellCommand %REG_DIRECTORY_BACKGROUND_SHELL%\FileCollector "%COMMANDNAME%" "\"%%%%V\""
@CALL :AddShellCommand %REG_DRIVE_SHELL%\FileCollector "%COMMANDNAME%" "%%%%1"
@PAUSE
@EXIT /B

:AddShellCommand
@REG ADD %~1 /t REG_SZ /ve /d "%~2" /f
@REG ADD %~1 /t REG_EXPAND_SZ /v Icon /d "%~dp0FileCollector.exe" /f
@REG ADD %~1\command /t REG_EXPAND_SZ /ve /d "\"%~dp0FileCollector.exe\" %~3" /f
@EXIT /B
