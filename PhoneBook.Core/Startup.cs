using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Core.FileHandlers;
using PhoneBook.Core.Interfaces;
using PhoneBook.Core.Repositories;
namespace PhoneBook.Core
{
    public static class Startup
    {

        public static void ConfigureImplementation(this IServiceCollection services)
        {
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();
            services.AddScoped<IFileHandler, JsonFileHandler>();
            services.AddScoped<IFileHandler, XmlFileHandler>();
            services.AddScoped<IFileHandler, BinaryFileHandler>();
        }
    }
}
