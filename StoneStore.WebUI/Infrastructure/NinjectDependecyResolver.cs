using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Moq;
using StoneStore.Domain.Abstract;
using StoneStore.Domain.Entities;
using StoneStore.Domain.Concrete;
using System.Configuration;
using StoneStore.WebUI.Infrastructure.Abstract;
using StoneStore.WebUI.Infrastructure.Concrete;

namespace StoneStore.WebUI.Infrastructure
{
    public class NinjectDependecyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependecyResolver(IKernel kernelParam)
        {
            mykernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type myserviceType)
        {
            return mykernel.TryGet(myserviceType);
        }

        public IEnumerable<object> GetServices(Type myserviceType)
        {
            return mykernel.GetAll(myserviceType);
        }

        private void AddBindings()
        {
            mykernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            mykernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("setting", emailSettings);

            mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}