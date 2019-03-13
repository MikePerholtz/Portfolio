// S E T U P  A N D  S C A F F O L D I N G

References: 

Main Setup Guide for EF Scaffolding Azure Data Studio on Mac/Linux
https://www.phillipsj.net/posts/working-with-sql-server-on-linux-for-dotnet-development

Adding GeoLocation to EF Core
https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite/



// * mPerholtz * This will scaffold out the controller classes 
$ dotnet aspnet-codegenerator controller -name ContactUsMessageController -m ContactUsMessage -dc PortfolioContext --relativeFolderPath Controllers
 
--useDefaultLayout --referenceScriptLibraries

// * mPerholtz * This initialize the migration from EF Core 
$ dotnet ef migrations add InitialCreate

// once the migration is complete then ef will create the databases
$ dotnet ef database update


