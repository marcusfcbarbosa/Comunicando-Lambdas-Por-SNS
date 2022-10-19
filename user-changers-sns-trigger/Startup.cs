using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using user_changes_domain.Commands;
using user_changes_domain.Interfaces;

namespace user_changers_sns_trigger
{
    public static class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ITituloRepository, TituloRepository>();
            serviceCollection.AddMediatR(typeof(EnviarTituloCommand).GetTypeInfo().Assembly);
            return serviceCollection.BuildServiceProvider();
        }
    }
}
