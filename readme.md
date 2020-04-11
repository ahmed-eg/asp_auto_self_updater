

# ASP Auto Self-Update

Designed for ASP Core application for Self-Update 

working as following:

- Check for update by calling HTTP GET API
- Check the curent DLL version for target .Net Core application.
- If save version then abort.
- Download the new version by calling another URL
- Unzip the downloaded finl to extract the new DLL and the dependansis.
- Add "app_offline.htm" file to force the application to stop.
- Copy the files.
- Remove the "app_offline.htm"
- abort