@echo off

chcp 1250

mkdir "%ProgramFiles%\Slávek Rydval\GoatJira"
copy .\*.* "%ProgramFiles%\Slávek Rydval\GoatJira"

regedit "%ProgramFiles%\Slávek Rydval\GoatJira\GoatJira.reg"

%windir%\Microsoft.NET\Framework\v4.0.30319\regasm "%ProgramFiles%\Slávek Rydval\GoatJira\GoatJira.dll" /codebase

@pause