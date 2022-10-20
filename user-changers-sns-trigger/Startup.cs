using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using user_changes_domain;
using user_changes_domain.Commands;
using user_changes_domain.Data;
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
            serviceCollection.AddTransient(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<FibraContext>();
                optionsBuilder.UseSqlServer(Acceess.GetConnectionStrings());
                return new FibraContext(optionsBuilder.Options);
            });
            return serviceCollection.BuildServiceProvider();
        }
    }
}
