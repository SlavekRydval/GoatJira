@echo off

chcp 1250

mkdir "%ProgramFiles%\Sl�vek Rydval\GoatJira"
copy .\*.* "%ProgramFiles%\Sl�vek Rydval\GoatJira"

regedit "%ProgramFiles%\Sl�vek Rydval\GoatJira\GoatJira.reg"

%windir%\Microsoft.NET\Framework\v4.0.30319\regasm "%ProgramFiles%\Sl�vek Rydval\GoatJira\GoatJira.dll" /codebase

@pause