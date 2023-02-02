using BookStore.DataStore.DataAccess;
using BookStore.Models.Translators;
using BookStore.Service.LoginService;
using BookStore.Service.PersonService;
using BookStore.Service.TokenService;
using BookStore.Service.UserService;

namespace BookStore.Api
{
    public static class DependencyInjection
    {
        public static void AddDependencyServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonQueries, PersonQueries>();
            services.AddScoped<IToDtoTranslator, ToDtoTranslator>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserQueries, UserQueries>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
        }
    }
}