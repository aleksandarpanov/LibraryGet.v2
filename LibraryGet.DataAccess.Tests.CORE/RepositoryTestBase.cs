using LibraryGet.DataAccess.STANDARD.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryGet.DataAccess.Tests.CORE
{
    [TestClass]
    public abstract class RepositoryTestBase<T> where T : EntityBaseRepository
    {
        #region Private Methods
        public T CreateRepository(string connectionString)
        {
            return (T)Activator.CreateInstance(typeof(T),connectionString);
        }

        public string Init()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();

            return config["ConnectionString"];
        }

        #endregion
    }
}
