using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using FwData;
using FwData.Comparer;
using FwData.Entities;
using FwInMemDb;
using Movies.Mappers;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace Movies
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder bldr)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new MovieMapProfile());
            });

            bldr.RegisterInstance(new MovieSortComparerFactory())
                .As<IFactory<IComparer<Movie>, SortAttributes>>()
                .SingleInstance();

            bldr.RegisterInstance(config.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            bldr.RegisterType<MovieRepository>()
              .As<IMovieRepository>()
              .InstancePerRequest();
        }
    }
}