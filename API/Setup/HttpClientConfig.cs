using Domain.Option;
using Domain.Repository;
using Infrastructure.Repository;

namespace API.Setup
{
    public static class HttpClientConfig
    {
        public static void AddConfiguredHttpClients(this IServiceCollection services, Secrets secrets)
        {
            services.AddHttpClient<IAuthenticationRepository, AuthenticationRepository>(client =>
            {
                client.BaseAddress = new Uri(secrets.Api.Auth);
            });
        }
    }
}
