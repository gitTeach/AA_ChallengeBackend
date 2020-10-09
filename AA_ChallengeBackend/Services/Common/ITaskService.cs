using Data.DbModels;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ITaskService
    {
        IEnumerable<TTask> GetTasks(int idList);
        IEnumerable<TaskDetailResponse> GetTasksDetail(string userId, int idList, string category);
        TaskOverallResponse GetTasksOverall(string userId, string category);
        TTask GetTask(int idTask);
        void AddTask(int idList, TTask task);
        void UpdateTask(TTask task);
        void DeleteTask(TTask task);
        bool Exist(int idTask);
    }
}
