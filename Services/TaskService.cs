using Microsoft.EntityFrameworkCore;
using Aplikasi_Proyek___Tagihan_Freelancer.Data;
using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(Guid projectId)
    {
        return await _context.ProjectTasks
            .Include(t => t.Project)
            .Where(t => t.ProjectId == projectId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<ProjectTask?> GetTaskByIdAsync(Guid id)
    {
        return await _context.ProjectTasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<ProjectTask> CreateTaskAsync(ProjectTask task)
    {
        task.Id = Guid.NewGuid();
        task.CreatedAt = DateTime.UtcNow;
        
        _context.ProjectTasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<ProjectTask> UpdateTaskAsync(ProjectTask task)
    {
        _context.ProjectTasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var task = await _context.ProjectTasks.FindAsync(id);
        if (task == null)
            return false;

        _context.ProjectTasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }    public async Task<bool> UpdateTaskStatusAsync(Guid taskId, Models.TaskStatus status)
    {
        var task = await _context.ProjectTasks.FindAsync(taskId);
        if (task == null)
            return false;

        task.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ProjectTask>> GetTasksByStatusAsync(Models.TaskStatus status)
    {
        return await _context.ProjectTasks
            .Include(t => t.Project)
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
