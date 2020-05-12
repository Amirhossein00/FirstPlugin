using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Projects.Factory;
using Nop.Plugin.Projects.Services;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Projects.ViewComponents
{
    [ViewComponent(Name = "PartialProjectList")]
    public class ProjectListViewComponent : NopViewComponent
    {
        #region Dependencie
        IProjectService _projectService;
        IProjectModelFactory _projectModelfactory;

        public ProjectListViewComponent(IProjectService projectService,
            IProjectModelFactory projectFactoryModel)
        {
            _projectService = projectService;
            _projectModelfactory = projectFactoryModel;
        }
        #endregion
   
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _projectModelfactory.GetModelListFromProjectList(_projectService.GetLastPublishedProjects(4).ToList());
            return await Task.FromResult((IViewComponentResult)View("~/Plugins/Projects/Views/Shared/Default.cshtml", model));
        }
    }
}
