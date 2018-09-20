print 'inserting into dbo.AddClients'

set identity_insert dbo.Clients on

insert into dbo.Clients
    (Id, AbsoluteRefreshTokenLifetime, AccessTokenLifetime, AccessTokenType, AllowAccessTokensViaBrowser,
    AllowOfflineAccess, AllowPlainTextPkce, AllowRememberConsent, AlwaysIncludeUserClaimsInIdToken,
    AlwaysSendClientClaims, AuthorizationCodeLifetime, ClientId, ClientName, ClientUri, EnableLocalLogin,
    Enabled, IdentityTokenLifetime, IncludeJwtId, LogoUri, LogoutSessionRequired, LogoutUri,
    PrefixClientClaims, ProtocolType, RefreshTokenExpiration, RefreshTokenUsage, RequireClientSecret,
    RequireConsent, RequirePkce, SlidingRefreshTokenLifetime, UpdateAccessTokenClaimsOnRefresh)
values
    (1, 2592000, 3600, 0, 1, 1, 0, 1, 1, 0, 300, 'Ciemesus.SPA', 'Ciemesus - Frontend SPA', null, 1, 1, 
	300, 1, null, 1, null, 1, 'oidc', 0, 0, 1, 0, 0, 1296000, 1);

set identity_insert dbo.Clients off
