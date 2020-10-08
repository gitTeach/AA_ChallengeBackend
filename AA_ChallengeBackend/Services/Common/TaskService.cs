using Data.DbModels;
using Data.Repositories;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void AddTask(int idList, TTask task)
        {
            _taskRepository.AddTask(idList, task);
        }

        public void DeleteTask(TTask task)
        {
            _taskRepository.DeleteTask(task);
        }

        public bool Exist(int idTask)
        {
            return _taskRepository.Exist(idTask);
        }

        public TTask GetTask(int idTask)
        {
            return _taskRepository.GetTask(idTask);
        }

        public IEnumerable<TTask> GetTasks(int idList)
        {
            return _taskRepository.GetTasks(idList);
        }

        public IEnumerable<TaskDetailResponse> GetTasksForUser(string userId)
        {
            return _taskRepository.GetTasksForUser(userId);
        }

        public void UpdateTask(TTask task)
        {
            _taskRepository.UpdateTask(task);
        }
    }
}
