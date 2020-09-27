using Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IListRepository
    {
        IEnumerable<TList> GetLists(string userId);
        TList GetList(string userId, int idList);
        void AddList(string userId, TList list);
        void UpdateList(TList list);
        void DeleteList(TList list);
        bool Exist(int idList);
        bool Save();
    }
}
