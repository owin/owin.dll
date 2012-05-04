:@echo off
cls
".nuget\nuget.exe" install -OutputDirectory packages .\packages.config
"packages\FAKE.1.62.1\tools\Fake.exe" "build.fsx" %*
pause
