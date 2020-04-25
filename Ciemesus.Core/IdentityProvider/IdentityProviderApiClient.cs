using Ciemesus.Core.Contracts;
using Ciemesus.Core.Infrastructure;
using FluentValidation.Results;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ciemesus.Core.IdentityProvider
{
    public class IdentityProviderApiClient
    {
        private readonly IHttpHandler _httpClient;
        private readonly IdentityProviderApiClientSettings _clientSettings;
        private readonly HttpContext _httpContext;
        private readonly IdentityProviderSettings _identityProviderSettings;
        private readonly IHttpClientFactory _clientFactory;

        public IdentityProviderApiClient(
            IHttpHandler httpClient,
            IdentityProviderApiClientSettings clientSettings,
            IHttpContextAccessor contextAccessor,
            IdentityProviderSettings identityProviderSettings,
            IHttpClientFactory clientFactory)
        {
            _httpClient = httpClient;
            _clientSettings = clientSettings;
            _httpContext = contextAccessor.HttpContext;
            _identityProviderSettings = identityProviderSettings;
            _clientFactory = clientFactory;
        }

        public async Task<IResponseBase<CommandResult>> CreateLogin(CreateLoginApiCommand command)
        {
            var response = new ResponseBase<CommandResult>();

            SetBaseAddress();

            using var content = await GetHttpContent(command);

            var apiResponse = await _httpClient.PostAsync("User/Create", content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseContent))
                {
                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure("Account", apiResponse.ReasonPhrase),
                    };
                }
                else
                {
                    dynamic error = JsonConvert.DeserializeObject(responseContent);

                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                    };
                }
            }
            else
            {
                response.Result.UserId = apiResponse.Content.ReadAsStringAsync().Result;
            }

            return response;
        }

        public async Task<IResponseBase> DeleteLogin(DeleteLoginApiCommand command)
        {
            var response = new ResponseBase<CommandResult>();

            SetBaseAddress();

            using var content = await GetHttpContent(command);

            var apiResponse = await _httpClient.PostAsync("User/Delete", content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                dynamic error = JsonConvert.DeserializeObject(responseContent);

                response.Errors = new List<ValidationFailure>
                {
                    new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                };
            }

            return response;
        }

        public async Task<IResponseBase> DeleteLoginDelegate(DeleteLoginApiCommand command)
        {
            var response = new ResponseBase<CommandResult>();

            SetBaseAddress();

            using var content = await GetHttpContent(command);

            var apiResponse = await _httpClient.PostAsync("User/Delete", content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                dynamic error = JsonConvert.DeserializeObject(responseContent);

                response.Errors = new List<ValidationFailure>
                {
                    new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                };
            }

            return response;
        }

        public async Task<IResponseBase<PasswordTokenQueryResult>> GetPasswordToken(PasswordTokenApiQuery query)
        {
            var response = new ResponseBase<PasswordTokenQueryResult>();

            SetBaseAddress();

            await SetAuthenticationToken();

            var apiResponse = await _httpClient.GetAsync($"User/ResetPassword?userid={query.UserId}");

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseContent))
                {
                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure("Account", apiResponse.ReasonPhrase),
                    };
                }
                else
                {
                    dynamic error = JsonConvert.DeserializeObject(responseContent);

                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                    };
                }
            }
            else
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                response.Result = new PasswordTokenQueryResult()
                {
                    Token = responseContent.ToString(),
                };
            }

            return response;
        }

        public async Task<IResponseBase> UpdatePassword(UpdatePasswordApiCommand command)
        {
            var response = new ResponseBase();

            SetBaseAddress();

            using var content = await GetHttpContent(command);

            var apiResponse = await _httpClient.PostAsync("User/ResetPassword", content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                dynamic error = JsonConvert.DeserializeObject(responseContent);

                response.Errors = new List<ValidationFailure>
                {
                    new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                };
            }

            return response;
        }

        public async Task<IResponseBase> UpdateEmail(UpdateEmailApiCommand command)
        {
            var response = new ResponseBase<CommandResult>();

            SetBaseAddress();

            using var content = await GetHttpContent(command);

            var apiResponse = await _httpClient.PostAsync("User/Edit", content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                dynamic error = JsonConvert.DeserializeObject(responseContent);

                response.Errors = new List<ValidationFailure>
                {
                    new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                };
            }
            else
            {
                response.Result.UserId = apiResponse.Content.ReadAsStringAsync().Result;
            }

            return response;
        }

        public async Task<IResponseBase<CreateClientResult>> CreateClient(CreateClientCommand command)
        {
            var response = new ResponseBase<CreateClientResult>();

            SetBaseAddress();

            using var content = await GetHttpContent(command);

            var apiResponse = await _httpClient.PostAsync("Client/Create", content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseContent))
                {
                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure("Client", apiResponse.ReasonPhrase),
                    };
                }
                else
                {
                    dynamic error = JsonConvert.DeserializeObject(responseContent);

                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                    };
                }
            }
            else
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();
                response.Result = JsonConvert.DeserializeObject<CreateClientResult>(responseContent);
            }

            return response;
        }

        public async Task<IResponseBase> DeleteClient(DeleteClientCommand command)
        {
            var response = new ResponseBase();

            SetBaseAddress();

            using var content = await GetHttpContent(command);

            var apiResponse = await _httpClient.PostAsync("Client/Delete", content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseContent))
                {
                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure("Client", apiResponse.ReasonPhrase),
                    };
                }
                else
                {
                    dynamic error = JsonConvert.DeserializeObject(responseContent);

                    response.Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure(error.propertyName.Value, error.errorMessage.Value),
                    };
                }
            }

            return response;
        }

        private async Task<StringContent> GetHttpContent(object data)
        {
            await SetAuthenticationToken();
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task SetAuthenticationToken()
        {
            if (!_httpClient.GetType().Equals(typeof(Infrastructure.HttpClientHandler)))
            {
                return;
            }

            if (_httpClient.Token != null)
            {
                return;
            }

            await SetAuthenticationUsingIdentityProvider();
        }

        private async Task SetAuthenticationUsingIdentityProvider()
        {
            if (_httpContext == null)
            {
                return;
            }

            using var client = _clientFactory.CreateClient();
            var discovery = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _identityProviderSettings.Authority,
                Policy =
                    {
                        RequireHttps = false,
                    },
            });

            var token = _httpContext.Request.Headers["Authorization"];

            if (token.Count > 0)
            {
                _httpClient.Token = new AuthenticationHeaderValue("Bearer", await GetAccessTokenUsingDelegate(client, discovery.TokenEndpoint, token[0].ToString()));
            }
            else
            {
                _httpClient.Token = new AuthenticationHeaderValue("Bearer", await GetAccessTokenUsingClientCredential(client, discovery.TokenEndpoint));
            }
        }

        private void SetBaseAddress()
        {
            if (_httpClient.BaseAddress == null || !_httpClient.BaseAddress.ToString().Contains(_clientSettings.BaseUri.ToString()))
            {
                _httpClient.BaseAddress = _clientSettings.BaseUri;
            }
        }

        private async Task<string> GetAccessTokenUsingDelegate(HttpClient client, string tokenEndpoint, string requestToken)
        {
            using var request = new TokenRequest
            {
                Address = tokenEndpoint,
                GrantType = "delegation",
                ClientId = _identityProviderSettings.ClientId,
                ClientSecret = _identityProviderSettings.ClientSecret,
                Parameters =
                {
                    {
                        "scope", _identityProviderSettings.Scope
                    },
                    {
                        "token", requestToken
                    },
                },
            };

            var tokenResponse = await client.RequestTokenAsync(request);

            return tokenResponse.AccessToken;
        }

        private async Task<string> GetAccessTokenUsingClientCredential(HttpClient client, string tokenEndpoint)
        {
            using var request = new TokenRequest
            {
                Address = tokenEndpoint,
                GrantType = "client_credentials",
                ClientId = _identityProviderSettings.ClientId,
                ClientSecret = _identityProviderSettings.ClientSecret,
                Parameters =
                {
                    {
                        "scope", _identityProviderSettings.Scope
                    },
                },
            };

            var tokenResponse = await client.RequestTokenAsync(request);

            return tokenResponse.AccessToken;
        }

        public class CreateLoginApiCommand
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandResult
        {
            public string UserId { get; set; }
        }

        public class DeleteLoginApiCommand
        {
            public Guid UserId { get; set; }
        }

        public class PasswordTokenApiQuery
        {
            public Guid UserId { get; set; }
        }

        public class PasswordTokenQueryResult
        {
            public string Token { get; set; }
        }

        public class UpdatePasswordApiCommand
        {
            public Guid UserId { get; set; }
            public string Token { get; set; }
            public string Password { get; set; }
        }

        public class UpdateEmailApiCommand
        {
            public Guid UserId { get; set; }
            public string NewEmail { get; set; }
            public string NewUserName { get; set; }
        }

        public class CreateClientCommand
        {
            public Guid ClientId { get; set; }
            public string ClientName { get; set; }
        }

        public class CreateClientResult
        {
            public Guid ClientId { get; set; }
            public string ClientSecret { get; set; }
        }

        public class DeleteClientCommand
        {
            public Guid ClientId { get; set; }
        }

        public class DeleteClientResult
        {
            public Guid ClientId { get; set; }
        }
    }
}
