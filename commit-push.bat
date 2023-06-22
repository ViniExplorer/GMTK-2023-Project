@echo off
git pull
git add -A
set /p summary= "Please summarise your new commit: "
git commit -m "%summary%"
git push origin master
set /p exit="Press enter to exit."