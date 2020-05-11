﻿using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core;
using Nop.Plugin.Projects.Data;
using Nop.Services.Common;
using Nop.Services.Plugins;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Projects
{
    public class ProjectMethod : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {

        ProjectObjectContext _projectObjectConrtext;
        IWebHelper _webHelper;

        public ProjectMethod(ProjectObjectContext projectObjectConrtext,
            IWebHelper webHelper)
        {
            _projectObjectConrtext = projectObjectConrtext;
            _webHelper = webHelper;
        }


        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            _projectObjectConrtext.Install();
            base.Install();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            rootNode.ChildNodes.Add(new SiteMapNode
            {
                Title = "Project",
                Visible = true,
                IconClass = "fa-tie",

                ChildNodes = new List<SiteMapNode>
                {
                    new SiteMapNode
                  {
                      Url = $"{_webHelper.GetStoreLocation()}Admin/ProjectAdmin/Index",
                      Title="List",
                      Visible=true,

                  },
                  new SiteMapNode
                  {
                      Title="Create New",
                      Url=$"{_webHelper.GetStoreLocation()}Admin/ProjectAdmin/Create",
                      Visible=true,

                  }

                }
            });
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            _projectObjectConrtext.Unistall();
            base.Uninstall();
        }
    }
}
