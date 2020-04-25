/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/* Add System Data */
:r "..\dbo\Tables\System Data\AddCalendar.sql"
:r "..\dbo\Tables\System Data\AddApplications.sql"
:r "..\dbo\Tables\System Data\AddApplicationRoles.sql"
:r "..\dbo\Tables\System Data\AddTimezones.sql"

