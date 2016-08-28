using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace eFolder.Todo.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      string baseAddress = "http://localhost:9000/";

      // Start OWIN host 
      IDisposable webApplication = WebApp.Start<Startup>(baseAddress);

      try
      {
        System.Console.WriteLine("Started...");
        System.Console.WriteLine("Press any key to stop server...");
        System.Console.ReadKey();
      }
      finally
      {
        webApplication.Dispose();
      }
    }
  }
}
