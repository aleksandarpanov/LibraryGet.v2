using LibraryGet.DataAccess.STANDARD.Dapper;
using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Web.CORE.Interfaces;
using LibraryGet.Web.CORE.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Extensions
{
    public static class LibraryGetServiceExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds the web API core.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>Returns IServiceCollection</returns>
        public static IServiceCollection AddLibraryGetCore(this IServiceCollection services)
        {
            services.AddServices();
            services.AddRepositories();
            return services;
        }

        /// <summary>
        /// Adds the core services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>Returns IServiceCollection</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookReservationService, BookReservationService>();
            return services;
        }

        /// <summary>
        /// Adds the core repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookReservationRepository, BookReservationRepository>();
            return services;
        }

        #endregion
    }
}
