using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project?> GetProjectByIdAsync(Guid id);
    Task<Project> CreateProjectAsync(Project project);
    Task<Project> UpdateProjectAsync(Project project);
    Task<bool> DeleteProjectAsync(Guid id);
    Task<IEnumerable<Project>> GetProjectsByClientIdAsync(Guid clientId);
    Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status);
    Task<bool> UpdateProjectStatusAsync(Guid projectId, ProjectStatus status);
}
