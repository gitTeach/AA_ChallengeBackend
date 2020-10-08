using Data.DbModels;
using Domain.Models.Response;
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

        public IEnumerable<TaskDetailResponse> GetTasksForUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var query = from t in _Db.TTask
                        join l in _Db.TList
                        on t.IdList equals l.Id
                        where l.UserId == userId
                        select new TaskDetailResponse
                        {
                            Id = t.Id,
                            IdList = t.IdList,
                            Description = t.Description,
                            Notes = t.Notes,
                            DueDate = t.DueDate,
                            RemindDate = t.RemindDate,
                            IsCompleted = t.IsCompleted,
                            IsImportant = t.IsImportant,
                            MyDayDate = t.MyDayDate,
                            ListName = l.Name,
                            ListDescription = l.Description,
                            BDueDate = t.DueDate.ToShortDateString() == DateTime.Now.ToShortDateString() ? true : false,
                            BMyDayDate = (t.MyDayDate.GetValueOrDefault().ToShortDateString() ) == DateTime.Now.ToShortDateString() ? true : false
                        };
            return query.ToList();

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
