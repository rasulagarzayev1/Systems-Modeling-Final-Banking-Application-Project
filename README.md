# sysmod_project_group13_14

This repository holds the code and all other necessary files for the final project of the Systems Modeling course. The project team is formed by the members of group 13 and group 14: Jekaterina Gorohhova, Karl Jääts, Taavi Luik, Ida Maria Orula, Lauri Leiten, Rasul Agharzayev and Einar Linde.

**Architecture report**: https://docs.google.com/document/d/1CQXUlI5ZOviGVCQCW1FU7ZGHNmpS2k9aK-z5FxZRa1I/edit#

## Requirements
* Visual Studio

## Install
1. Clone the repository
2. Open SysModelBank/SysModelBank.sln with Visual Studio
3. You can now run / build the app

## Tips
* Notifications
```C#
ViewBag.Notification = new NotificationModel("Account registration was successful!").asSuccess();
```
```C#
ViewBag.Notification = new NotificationModel().withMessage("Account registration was successful!").asSuccess();
```
## IMPORTANT - Never modify database manually. Use EntityFrameWork migrations 
### And don't modify existing migrations
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

Commands: 
To add new migration: `dotnet ef migrations add <migration name> -p SysModelBank`

To update database to latest: `dotnet ef database update -p SysModelBank`
  
