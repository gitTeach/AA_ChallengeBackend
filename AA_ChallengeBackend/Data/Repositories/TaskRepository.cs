using Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly DB_Context _Db;

        public TaskRepository(DB_Context db)
        {
            _Db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddTask(int idList, TTask task)
        {
            if (idList == 0)
            {
                throw new ArgumentNullException(nameof(idList));
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            task.IdList = idList;
            _Db.TTask.Add(task);
            _Db.SaveChanges();
        }

        public void DeleteTask(TTask task)
        {
            _Db.TTask.Remove(task);
            _Db.SaveChanges();
        }

        public bool Exist(int idTask)
        {
            if (idTask == 0)
            {
                throw new ArgumentNullException(nameof(idTask));
            }

            return _Db.TTask.Any(x=> x.Id == idTask);
        }

        public TTask GetTask(int idTask)
        {
            if (idTask == 0)
            {
                throw new ArgumentNullException(nameof(idTask));
            }

            return _Db.TTask.Where(x => x.Id == idTask).FirstOrDefault();
        }

        public IEnumerable<TTask> GetTasks(int idList)
        {
            if (idList == 0)
            {
                throw new ArgumentNullException(nameof(idList));
            }

            return _Db.TTask.Where(x => x.IdList == idList).OrderBy(x => x.Description).ToList();
        }
        public void UpdateTask(TTask task)
        {
            if (task.Id == 0)
            {
                throw new ArgumentNullException(nameof(task.Id));
            }

            if (task != null) {
                _Db.SaveChanges();
            }

            //var dbtask = _Db.TTask.Where(x => x.Id == task.Id).FirstOrDefault();

            //if (dbtask != null)
            //{
            //    dbtask.Description = task.Description;
            //    dbtask.RemindDate = task.RemindDate;
            //    dbtask.DueDate = task.DueDate;
            //    dbtask.MyDayDate = task.MyDayDate;
            //    dbtask.Notes = task.Notes;
            //    dbtask.IsCompleted = task.IsCompleted;
            //    dbtask.IsImportant = task.IsImportant;
            //    _Db.SaveChanges();
            //}
        }
        public bool Save()
        {
            return (_Db.SaveChanges() >= 0);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
        }
    }
}
