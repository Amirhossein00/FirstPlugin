using System.Collections.Generic;
using Nop.Core;
using Nop.Plugin.Projects.Domain;

namespace Nop.Plugin.Projects.Services
{
    public interface IProjectService
    {
        List<Project> GetAllProjects();

        Project GetProjectById(int projectId);

        void UpdateProject(Project project);

        void InsertProject(Project project);

        void DeleteProject(Project project);

        void DeleteProject(int projectId);

        bool IsPublic(int projectId);

        IList<Project> GetAllPublicProjects();

        IPagedList<Project> SearchProjects(int published = 0, string name = null, string shortDescription = null);

        IList<Project> GetProjectsbyIds(int[] projectIds);

        void DeleteProjectByIds(IList<Project> projects);
    }
}
