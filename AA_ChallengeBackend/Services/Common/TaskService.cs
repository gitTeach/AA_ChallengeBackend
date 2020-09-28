using Data.DbModels;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskService _taskService;

        public TaskService(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void AddTask(int idList, TTask task)
        {
            _taskService.AddTask(idList, task);
        }

        public void DeleteTask(TTask task)
        {
            _taskService.DeleteTask(task);
        }

        public bool Exist(int idTask)
        {
            return _taskService.Exist(idTask);
        }

        public TTask GetTask(int idTask)
        {
            return _taskService.GetTask(idTask);
        }

        public IEnumerable<TTask> GetTasks(int idList)
        {
            return _taskService.GetTasks(idList);
        }

        public void UpdateTask(TTask task)
        {
            _taskService.UpdateTask(task);
        }
    }
}
