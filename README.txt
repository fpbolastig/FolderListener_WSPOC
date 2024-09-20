Project Name: Folder Listener Windows Service POC (Proff-of-Concept)
Version: 1.0.0.0 [BASE]
Description: A Windows Service to move any file received from Folder1 to Folder2.
Author: Frederick Bolastig

History:
A request to create a Windows Service in C# that monitors a directory for new files and move the file to another directory.

Side Note:
Had 2 almost identical project(s) with thesame main process wherein to monitor a folder for any incoming file(s) and move the file(s) to its 
designated folders then create logs of the process.

This is the basic form of the project as per instruction for Demo purpose only. Any additional processes are not included 
e.a. monitor multiple folders, filesize checking, process incase of disrupted operation while moving the file(s)

HowTo:
1.Create an executable or use "sc.exe" to install the service. 
You can use a command like this:
   />sc create FolderListener_WSPOC binPath= "C:\Path_to_Service\FolderListener_WSPOC.exe"

2.Start the service:
   />sc start FolderListener_WSPOC

3.View logs:
  - You can check the Event Viewer under "Windows Logs">"Application".
  - Log files will be created in the "logs" directory.