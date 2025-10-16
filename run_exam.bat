@echo off
REM =========================
REM Run Exam Backend + Open Browser
REM =========================

REM go to path
cd /d "%~dp0Exam-backend"

REM run be
start "" cmd /k "dotnet run"

REM plz w8 5s for backend start
timeout /t 5 /nobreak > nul

REM open browser at localhost:5154
start "" "http://localhost:5154"

exit