[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnlineShopping.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnlineShopping.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace OnlineShopping.WebUI.App_Start
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Configuration;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;


    using Moq;
    using OnlineShopping.Domain.Abstract;
    using OnlineShopping.Domain.Entities;
    using OnlineShopping.Domain.Concrete;
    using System.Data.Entity;



    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<IProductRepository>()
                                .To<EFProductRepository>();




            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                        .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>()
                                .To<EmailOrderProcessor>()
                                    .WithConstructorArgument("settings", emailSettings);


            //var mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product {Name = "Tequila", Price =23 },
            //    new Product {Name = "Mocktail", Price =120 },
            //    new Product {Name = "Beer", Price =140 }
            //});
            //kernel.Bind<IProductRepository>()
            //            .ToConstant(mock.Object);
        }
    }
}
