using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public static class UnityConfig
    {

        public static void ConfigureUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            registerTypes(container);
        }

        private static void registerTypes(IUnityContainer container)
        {
            container.RegisterType<IRepository<Libro>, Repository<Libro>>();
            container.Resolve<Libro>();
        }

    }
}
