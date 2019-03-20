REM remove everything from the logs folder so new results can be saved
set folder="logs"
cd /d %folder%
for /F "delims=" %%i in ('dir /b') do (rmdir "%%i" /s/q || del "%%i" /s/q)
cd ..
REM run tests
mstest /testcontainer:bin\Debug\EarlyDocsTest.net20.dll /resultsfile:logs\EarlyDocsTestOutput.net20.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net30.dll /resultsfile:logs\EarlyDocsTestOutput.net30.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net35.dll /resultsfile:logs\EarlyDocsTestOutput.net35.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net40.dll /resultsfile:logs\EarlyDocsTestOutput.net40.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net45.dll /resultsfile:logs\EarlyDocsTestOutput.net45.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net451.dll /resultsfile:logs\EarlyDocsTestOutput.net451.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net452.dll /resultsfile:logs\EarlyDocsTestOutput.net452.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net46.dll /resultsfile:logs\EarlyDocsTestOutput.net46.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net461.dll /resultsfile:logs\EarlyDocsTestOutput.net461.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net462.dll /resultsfile:logs\EarlyDocsTestOutput.net462.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net47.dll /resultsfile:logs\EarlyDocsTestOutput.net47.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net471.dll /resultsfile:logs\EarlyDocsTestOutput.net471.Debug.trx
mstest /testcontainer:bin\Debug\EarlyDocsTest.net472.dll /resultsfile:logs\EarlyDocsTestOutput.net472.Debug.trx
