using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using eFolder.Todo.Backup;

namespace eFolder.Todo
{
  public static class UnityConfig
  {
    public static void RegisterComponents(HttpConfiguration config)
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();
      container.RegisterType<IBackupService, BackupService>(new HierarchicalLifetimeManager());

      config.DependencyResolver = new UnityDependencyResolver(container);
    }
  }
}