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
:r "..\dbo\Tables\System Data\AddApiResources.sql"
:r "..\dbo\Tables\System Data\AddApiScopes.sql"
:r "..\dbo\Tables\System Data\AddApiScopeClaims.sql"
:r "..\dbo\Tables\System Data\AddClients.sql"
:r "..\dbo\Tables\System Data\AddClientSecrets.sql"
:r "..\dbo\Tables\System Data\AddClientGrantTypes.sql"
:r "..\dbo\Tables\System Data\AddClientScopes.sql"
:r "..\dbo\Tables\System Data\AddIdentityResources.sql"
:r "..\dbo\Tables\System Data\AddIdentityClaims.sql"

/* Add Test Data */
:r ".\Test Data\AddUsers.sql"
:r ".\Test Data\AddMembers.sql"
:r ".\Test Data\AddTeams.sql"
:r ".\Test Data\AddTeamMembers.sql"
:r ".\Test Data\AddCiemesus.sql"
:r ".\Test Data\AddCiemesusComments.sql"
:r ".\Test Data\AddClientCorsOrigins.sql"
:r ".\Test Data\AddClientRedirectUris.sql"
:r ".\Test Data\AddClientPostLogoutRedirectUris.sql"
