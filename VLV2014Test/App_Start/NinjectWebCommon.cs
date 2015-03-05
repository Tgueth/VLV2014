[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VLV2014Test.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(VLV2014Test.App_Start.NinjectWebCommon), "Stop")]

namespace VLV2014Test.App_Start
{
    using System;
    using System.Web;
    using System.Data;
    using System.Data.Common;
    using System.Configuration;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using log4net;

    using BinaryStarTechnology.CharityEventRevenueMgmtClasses;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static IEvent EventID = null;

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
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["VivaLaVinoConnectionString"].ConnectionString;
                DbProviderFactory factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["VivaLaVinoConnectionString"].ProviderName);
                DataManager dataMgr = new DataManager(factory, connString);

                string strEvent = System.Configuration.ConfigurationManager.AppSettings["EventID"];
                //Event eventID =
                if (strEvent != null)
                {
                    EventID = dataMgr.GetEvent(Convert.ToInt32(strEvent));
                }

                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<IDataManager>().ToConstant(dataMgr);  // get the datamanager
                if (EventID != null)
                {
                    kernel.Bind<IEvent>().ToConstant((EventID));
                }

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
        }        
    }
}
