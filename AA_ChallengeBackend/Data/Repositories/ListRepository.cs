using Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class ListRepository : IListRepository
    {
        public void AddList(string userId, TList list)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(TList list)
        {
            throw new NotImplementedException();
        }

        public bool Exist(int idList)
        {
            throw new NotImplementedException();
        }

        public TList GetList(string userId, int idList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TList> GetLists(string userId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateList(TList list)
        {
            throw new NotImplementedException();
        }
    }
}
