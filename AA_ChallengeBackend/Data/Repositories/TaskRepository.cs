using Data.DbModels;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public TTask GetTask(int idTask, string userId = "")
        {
            if (idTask == 0)
            {
                throw new ArgumentNullException(nameof(idTask));
            }
            if (!string.IsNullOrEmpty(userId) || !string.IsNullOrWhiteSpace(userId))
            {
                return _Db.TTask.Where(x => x.Id == idTask && x.IdListNavigation.UserId == userId).FirstOrDefault();
            }
            else
            {
                return _Db.TTask.Where(x => x.Id == idTask).FirstOrDefault();
            }
                
        }

        public IEnumerable<TTask> GetTasks(int idList)
        {
            if (idList == 0)
            {
                throw new ArgumentNullException(nameof(idList));
            }

            return _Db.TTask.Where(x => x.IdList == idList).OrderBy(x => x.Description).ToList();
        }

        public IEnumerable<TaskDetailResponse> GetTasksDetail(string userId, int idList, string category)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }
            try
            {
                var data = _Db.TList
                       .Join(_Db.TTask,
                           l => l.Id,
                           t => t.IdList,
                           (l, t) => new { TList = l, TTask = t })
                       .Where (x=> x.TList.UserId == userId)
                       .AsEnumerable().Select(x => new TaskDetailResponse() {
                           Id = x.TTask.Id,
                           IdList = x.TTask.IdList,
                           Description = x.TTask.Description,
                           Notes = x.TTask.Notes,
                           DueDate = x.TTask.DueDate,
                           RemindDate = x.TTask.RemindDate,
                           IsCompleted = x.TTask.IsCompleted,
                           IsImportant = x.TTask.IsImportant,
                           MyDayDate = x.TTask.MyDayDate,
                           ListName = x.TList.Name,
                           ListDescription = x.TList.Description,
                           BDueDate = x.TTask.DueDate.ToShortDateString() == DateTime.Now.ToShortDateString() ? true : false,
                           BMyDayDate = x.TTask.MyDayDate.GetValueOrDefault().ToShortDateString() == DateTime.Now.ToShortDateString() ? true : false
                       });

                if(!string.IsNullOrEmpty(category) || !string.IsNullOrWhiteSpace(category))
                {
                    switch (category)
                    {
                        case "dueToday":
                            data = data.Where(x => x.DueDate.ToShortDateString() == DateTime.Now.ToShortDateString());
                            break;
                        case "plannedToday":
                            data = data.Where(x => x.MyDayDate.GetValueOrDefault().ToShortDateString() == DateTime.Now.ToShortDateString());
                            break;
                        case "isImportant":
                            data = data.Where(x => x.IsImportant == true);
                            break;
                        case "isCompleted":
                            data = data.Where(x => x.IsCompleted == true);
                            break;
                        default:
                            break;
                    }
                }

                if (idList != 0)
                {
                    data = data.Where(x => x.IdList == idList);
                }

                return data.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            
            //var query = from t in _Db.TTask
            //            join l in _Db.TList
            //            on t.IdList equals l.Id
            //            where l.UserId == userId
            //            select new TaskDetailResponse
            //            {
            //                Id = t.Id,
            //                IdList = t.IdList,
            //                Description = t.Description,
            //                Notes = t.Notes,
            //                DueDate = t.DueDate,
            //                RemindDate = t.RemindDate,
            //                IsCompleted = t.IsCompleted,
            //                IsImportant = t.IsImportant,
            //                MyDayDate = t.MyDayDate,
            //                ListName = l.Name,
            //                ListDescription = l.Description,
            //                BDueDate = t.DueDate.ToShortDateString() == DateTime.Now.ToShortDateString() ? true : false,
            //                BMyDayDate = (t.MyDayDate.GetValueOrDefault().ToShortDateString() ) == DateTime.Now.ToShortDateString() ? true : false
            //            };
            //if (idList != 0)
            //{
            //    query = query.Where(o => o.IdList == idList);
            //}
            //return query.ToList();
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

        public TaskOverallResponse GetTasksOverall(string userId, string category)
        {
            try
            {
                var queryData = from t in _Db.TTask
                                    join l in _Db.TList
                                    on t.IdList equals l.Id
                                    where l.UserId == userId
                                    select new { T = t, L = l };

                var tasksCompleted = queryData.Where(x => x.T.IsCompleted == true).Count();
                var tasksImportant = queryData.Where(x => x.T.IsImportant == true).Count();

                var tasksDueToday = _Db.TList    
                                   .Join(_Db.TTask, 
                                      l => l.Id,        
                                      t => t.IdList,  
                                      (l, t) => new { TList = l, TTask = t })
                                   .AsEnumerable()
                                   .Where( x=>x.TTask.DueDate.ToShortDateString() == DateTime.Now.ToShortDateString() && x.TList.UserId == userId).Count();
                
                var tasksPlannedForToday = _Db.TList
                                  .Join(_Db.TTask,
                                     l => l.Id,
                                     t => t.IdList,
                                     (l, t) => new { TList = l, TTask = t })
                                  .AsEnumerable()
                                  .Where(x => x.TTask.MyDayDate.GetValueOrDefault().ToShortDateString() == DateTime.Now.ToShortDateString() && x.TList.UserId == userId).Count();

                

                return new TaskOverallResponse
                {
                    TasksCompleted = tasksCompleted,
                    TasksDueToday = tasksDueToday,
                    TasksImportant = tasksImportant,
                    TasksPlannedForToday = tasksPlannedForToday
                };
            }
            catch (Exception ex)
            {   
                throw;
            }
        }
    }
}
