# Corporate Computer Manager## 
Corporate Computer Manager Client - for Windows XP, Vista, 7, 2003, 2008 - Requires a current .NET framework.
[![Preview of CCM][ccmadminss]]
--------

INSTALLATION:
1. Look in the release folder for the CCM folder.
2. Make it a network share on a server. I use a WINS/DNS alias name so mine looks like this: \\ccm\ccm that is the default of the application. If you need to change this it will look for clientControlPath in the registry under whatever you've specified in the configuration.txt file under setup.
3. Run the CCMClient.exe file from the share.
4. It will/should do the rest. If you don't use the default path or recompile with your own path you'll have to add the registry key to each client.

--------

DESCRIPTON:
Tray icon application that can be deployed to all workstations on a corporate or personal network and used to centrally maintain those machines. Includes features like:

1. Dynamic Update Packages (SERVER -> CLIENT PUSH)
2. Self Reporting (CLIENT -> SERVER PUSH)
3. All control done via text files in a file share. Batch files can be used to trigger events.
4. Dynamic execution blacklists, can be propogated to all workstations in seconds.
5. Version checking, management.
6. Intranet Messaging
7. Intranet File Sending
8. Connection Testing
9. Network Outage Notification.
10. Power outage and automatic shutdown.
11. Application idle timeout. Closes any specified app after idle time exceeded. (have had several uses for this)

--------

I've tried to trim off most of the unnecessary stuff and may have gotten some necessary stuff in the process. This build was created for public use. There are 2 other related applications. The CCMAdmin that gives a graphical environment to manage apps from (a compliled version is included in this package) and the CCMService which is really very useful on Windows XP. It keeps the Client alive.

While the application does function in the post XP Windows environments the "session 0 isolation" does become a bit funky. If demand merits I will enhance the tray support for those OSes. In the meantime it will function quite well as a Task Scheduler process.

Finally, the application is meant to be initially launched from a network share, upon launch it will look for itself on the local computer in C:\CCM ... if it does not find itself, it will install itself and then restart from that location. Thenafter it will always check for a new version in its originating network share. In other words, deployment of updates is a breeze. Just replace the ccmclient.exe file on the network share and send a restart request to the clients. Here is a list of text commands that can replace the file name in packages:

%clientRestart%
%clientExit%
%playSound%   <--- Fun! Much fun.
%sendFile%
%showPopup%
%enableSpecialFunctions%
%enableSendMessage%
%identify%
%disableSendMessage%
%autoHide%
%autoUpdate%
%disablePops%
%disableASD%
%enterSend%
%closeApp%
%windir%
%computerName%
%homeDirectory%
%userName%

That's a lot of them anyway.

Anyway, just thought I'd open source this in case folks need any or all of it.

<>< Reticon

[ccmclientss]: http://veracitek.com/ccmclient.jpg

[ccmadminss]: http://www.veracitek.com/ccmTNs.jpg
