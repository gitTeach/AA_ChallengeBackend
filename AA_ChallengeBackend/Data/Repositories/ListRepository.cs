using Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class ListRepository : IListRepository
    {

        private readonly DB_Context _Db;

        public ListRepository(DB_Context db)
        {
            _Db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddList(string userId, TList list)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            
            list.UserId = userId;
            _Db.TList.Add(list);
            _Db.SaveChanges();
        }

        public void DeleteList(TList list)
        {
            _Db.TList.Remove(list);
            _Db.SaveChanges();
        }

        public bool Exist(int idList)
        {
            if (idList == 0)
            {
                throw new ArgumentNullException(nameof(idList));
            }

            return _Db.TList.Any(x=> x.Id == idList);
        }

        public TList GetList(string userId, int idList)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (idList == 0)
            {
                throw new ArgumentNullException(nameof(idList));
            }

            return _Db.TList.Where(x => x.Id == idList && x.UserId == userId).FirstOrDefault();
        }

        public IEnumerable<TList> GetLists(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _Db.TList.Where(x => x.UserId == userId).OrderBy(x => x.Name).ToList();
        }
        public void UpdateList(TList list)
        {
            if (list.Id == 0)
            {
                throw new ArgumentNullException(nameof(list.Id));
            }

            var dblist = _Db.TList.Where(x => x.Id == list.Id).FirstOrDefault();

            if (dblist != null)
            {
                dblist.Description = list.Description;
                dblist.Name = list.Name;
                _Db.SaveChanges();
            }


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
