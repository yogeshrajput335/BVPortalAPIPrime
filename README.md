# RUN API APPLICATION

** dotnet run ** 
#### Swagger Link (change port number as required)
> https://localhost:7037/swagger/index.html
# REFERENCE
```
https://jasonwatmore.com/post/2021/10/26/net-5-connect-to-mysql-database-with-entity-framework-core
https://www.c-sharpcorner.com/article/rest-api-with-asp-net-6-and-mysql/
```

# MIGRATION 

```
dotnet tool install -g dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef database update YourMigrationName
```

# XAMPP
![Alt text](./Images/XAMPP_ControlPanel.jpg?raw=true "XAMPP Control Panel")
```
Download XAMPP from below link
https://www.apachefriends.org/
Start Apache & MySql
Click ADMIN from Control Panel or http://localhost/phpmyadmin/ in Browser
```

# SQL SERVER 
```
Server Name : DESKTOP-BPFI2DP\\SQLEXPRESS
Database name : blueverseportalprimedb
Username : sa
Password : 123456789
```
![Alt text](./Images/CreateServerUser.jpg?raw=true "Create Server User")

# EMAIL SERVICE

* https://codewithmukesh.com/blog/send-emails-with-aspnet-core/
* https://stackoverflow.com/questions/72470777/nodemailer-response-535-5-7-8-username-and-password-not-accepted

# Entity Framework
* https://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx

# Timesheet DB Struction Reference
* https://www.dropbox.com/s/uadl5689v9j0jcs/timesheet_model_schema.sql
* https://www.dropbox.com/s/3wo9qd2fdpe5yow/timesheet_model.png
* https://stackoverflow.com/questions/10526625/database-schema-that-manages-weekly-timesheets

