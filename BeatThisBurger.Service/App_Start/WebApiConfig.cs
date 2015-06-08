using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Http;
using BeatThisBurger.DataObjects;
using BeatThisBurger.Service.Migrations;
using BeatThisBurger.Service.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.AppService.Config;

namespace BeatThisBurger.Service
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            AppServiceExtensionConfig.Initialize();

            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //Database.SetInitializer(new MobileServiceInitializer());

            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }
    }

    //public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    //{
    //    protected override void Seed(MobileServiceContext context)
    //    {
    //        //List<TodoItem> todoItems = new List<TodoItem>
    //        //{
    //        //    new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
    //        //    new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
    //        //};

    //        //foreach (TodoItem todoItem in todoItems)
    //        //{
    //        //    context.Set<TodoItem>().Add(todoItem);
    //        //}

    //        base.Seed(context);
    //    }
    //}
}

