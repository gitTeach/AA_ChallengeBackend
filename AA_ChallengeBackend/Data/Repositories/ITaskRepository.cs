using Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TTask> GetTasks(int idList);
        TTask GetTask(int idTask);
        void AddTask(int idList, TTask task);
        void UpdateTask(TTask task);
        void DeleteTask(TTask task);
        bool Exist(int idTask);
        bool Save();
    }
}
