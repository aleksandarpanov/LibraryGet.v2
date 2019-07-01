using LibraryGet.Web.CORE.Data;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibraryGet.Web.CORE.Extensions
{
    public static class InitializeDatabase
    {
        public static void SeedDB(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDataContext>();

            DbInitializer.Initialize(context);
        }
    }
}
