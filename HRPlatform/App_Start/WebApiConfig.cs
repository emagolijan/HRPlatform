using AutoMapper;
using HRPlatform.Interfaces;
using HRPlatform.Models;
using HRPlatform.Repository;
using HRPlatform.Resolver;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HRPlatform
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Tracing
            config.EnableSystemDiagnosticsTracing();

            //Unity
            var container = new UnityContainer();
            container.RegisterType<ICandidateRepository, CandidateRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISkillRepository, SkillRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Candidate, CandidateDTO>();
                cfg.CreateMap<Skill, SkillDTO>();
            });

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
