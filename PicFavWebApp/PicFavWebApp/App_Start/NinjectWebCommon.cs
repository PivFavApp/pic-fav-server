using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common.WebHost;
using NLog;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PicFavWebApp.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PicFavWebApp.NinjectWebCommon), "Stop")]

namespace PicFavWebApp
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            logger.Debug("Configuring Ninject bindings");
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind(x =>
                {
                    x.FromThisAssembly() // Scans assembly
                     .SelectAllClasses() // Retrieve all non-abstract classes
                     .BindDefaultInterface(); // Binds the default interface to them;

                    //x.FromAssemblyContaining<SomeAssemblyMarker>().SelectAllClasses().BindDefaultInterface();                   
                });
                
                //RegisterNonDefaultServices(kernel);
                
                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        ///// <summary>
        ///// Load your modules or register your services here!
        ///// </summary>
        ///// <param name="kernel">The kernel.</param>
        //private static void RegisterNonDefaultServices(IKernel kernel)
        //{
        //    kernel.Rebind<IDapperRepository>().To<DapperRepository>().InRequestScope()
        //        .WithConstructorArgument("connection", ((EntityConnection)
        //                ((IObjectContextAdapter)new HookSweepContext()).ObjectContext.Connection)
        //                    .StoreConnection);

        //    kernel.Bind<IClock>().To<SystemClock>();
        //    kernel.Bind<IMediaServices>().To<WAMSProvider>();
        //}        
    }
}
