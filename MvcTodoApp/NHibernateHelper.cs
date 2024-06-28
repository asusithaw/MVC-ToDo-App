using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Cfg;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using MvcTodoApp.Entities;

namespace MvcTodoApp
{
    public class NHibernateHelper
    {

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                try
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = Fluently.Configure()
                            .Database(MySQLConfiguration.Standard
                                .ConnectionString(@"Server=localhost;Database=test1;User Id=root;Password=root123;")
                                 .ShowSql())
                            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())                            
                            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                            .BuildSessionFactory();
                    }
                    return _sessionFactory;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                        Console.ReadKey();
                    }
                    throw;
                }

            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}