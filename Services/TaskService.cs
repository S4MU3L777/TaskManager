
using TaskManager.API.Models;
using TaskManager.API.Repositories;
using TaskManager.API.Services.Interfaces;

namespace TaskManager.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _taskRepository.GetAllAsync();
    }
    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }
    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        task.Status = "Pendiente";
        await _taskRepository.AddAsync(task);
        return task;
    }
    public async Task<bool> UpdateAsync(int id, TaskItem task)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null) return false;

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.Status = task.Status;

        await _taskRepository.UpdateAsync(existingTask);
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null) return false;

        await _taskRepository.DeleteAsync(id);
        return true;
    }
}
