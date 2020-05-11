using System.Collections.Generic;
using Nop.Plugin.Projects.Domain;
using Nop.Plugin.Projects.Models;

namespace Nop.Plugin.Projects.Factory
{
    public interface IProjectModelFactory
    {
        IList<ProjectModel> GetModeListFromProjectList(List<Project> projectsList);

        ProjectModel GetModelFromProject(Project project);

        Project GetProjectFromModel(ProjectModel projectModel);

        ProjectSearchModel PreaperProjectSearchModel(ProjectSearchModel searchModel);

        ProjectListModel PreaperProjectListModel(ProjectSearchModel projectSearchModel);

        ProjectViewModel GetViewModelFromProject(Project project);
    }
}
