using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public interface ITaskService
{
    Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(Guid projectId);
    Task<ProjectTask?> GetTaskByIdAsync(Guid id);
    Task<ProjectTask> CreateTaskAsync(ProjectTask task);
    Task<ProjectTask> UpdateTaskAsync(ProjectTask task);
    Task<bool> DeleteTaskAsync(Guid id);    Task<bool> UpdateTaskStatusAsync(Guid taskId, Models.TaskStatus status);
    Task<IEnumerable<ProjectTask>> GetTasksByStatusAsync(Models.TaskStatus status);
}
