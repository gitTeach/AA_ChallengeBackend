﻿using Data.DbModels;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TTask> GetTasks(int idList);
        IEnumerable<TaskDetailResponse> GetTasksDetail(string userId, int idList, string category);
        TaskOverallResponse GetTasksOverall(string userId, string category);
        TTask GetTask(int idTask, string userId="");
        void AddTask(int idList, TTask task);
        void UpdateTask(TTask task);
        void DeleteTask(TTask task);
        bool Exist(int idTask);
        bool Save();
    }
}
