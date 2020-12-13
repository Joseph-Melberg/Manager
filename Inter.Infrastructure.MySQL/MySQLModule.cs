using System;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Infrastructure.MySQL
{
    public class MySQLModule
    {
        public static void LoadSqlRepository<TFrom, TTo, TContext>(IServiceCollection catalog)
            where TTo : BaseRepository<TContext>,TFrom
            where TFrom : class
            where TContext : DefaultContext
        {
            catalog.AddSingleton<IMySQLConnectionStringProvider, MySQLConnectionStringProvider>();

            catalog.AddTransient<TFrom, TTo>();

            catalog.AddTransient<TContext, TContext>();
        }
    }
}