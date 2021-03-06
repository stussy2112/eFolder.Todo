﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using eFolder.Todo.Backup;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Unity.WebApi;

namespace eFolder.Todo
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      var formatters = config.Formatters;
      var jsonFormatter = formatters.JsonFormatter;
      var settings = jsonFormatter.SerializerSettings;
      settings.Formatting = Formatting.Indented;
      settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
      settings.NullValueHandling = NullValueHandling.Ignore;
      settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      settings.Converters.Add(new StringEnumConverter());
      settings.Converters.Add(new IsoDateTimeConverter());
      config.Formatters.Remove(config.Formatters.XmlFormatter);

      //var container = new UnityContainer();
      //container.RegisterType<IBackupService, BackupService>(new HierarchicalLifetimeManager());
      ////config.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
      //config.DependencyResolver = new UnityDependencyResolver(container);

      UnityConfig.RegisterComponents(config);

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new
          {
            id = RouteParameter.Optional
          }
      );
    }
  }
}
