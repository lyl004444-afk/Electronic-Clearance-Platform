@echo off
cd /d "%~dp0"
start "DotNet Server" dotnet run --urls "http://localhost:7182"
timeout /t 6 >nul
start http://localhost:7182/
start "Serveo Tunnel" cmd /k "ssh -o StrictHostKeyChecking=no -R 80:localhost:7182 serveo.net"