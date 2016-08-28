﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;

namespace eFolder.Todo.Console
{
  public class Startup
  {
    public void Configuration(IAppBuilder appBuilder)
    {
      // Configure Web API for self-host. 
      HttpConfiguration config = new HttpConfiguration();

      WebApiConfig.Register(config);
      //config.Routes.MapHttpRoute(
      //    name: "DefaultApi",
      //    routeTemplate: "api/{controller}/{id}",
      //    defaults: new
      //    {
      //      id = RouteParameter.Optional
      //    }
      //);
            
      appBuilder.UseWebApi(config);

    }

  }

  //public class ValuesController : ApiController
  //{
  //  // GET api/values
  //  public IEnumerable<string> Get()
  //  {
  //    return new string[] { "value1", "value2" };
  //  }

  //  // GET api/values/5
  //  public string Get(int id)
  //  {
  //    return "value";
  //  }

  //  // POST api/values
  //  public void Post([FromBody]string value)
  //  {
  //  }

  //  // PUT api/values/5
  //  public void Put(int id, [FromBody]string value)
  //  {
  //  }

  //  // DELETE api/values/5
  //  public void Delete(int id)
  //  {
  //  }
  //}
}
