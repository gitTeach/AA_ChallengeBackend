using Data.DbModels;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }
        public void AddList(string userId, TList list)
        {
            _listRepository.AddList(userId, list);
        }

        public void DeleteList(TList list)
        {
            _listRepository.DeleteList(list);
        }

        public bool Exist(int idList)
        {
            return _listRepository.Exist(idList);
        }

        public TList GetList(string userId, int idList)
        {
            return _listRepository.GetList(userId, idList);
        }

        public IEnumerable<TList> GetLists(string userId)
        {
            return _listRepository.GetLists(userId);
        }

        public void UpdateList(TList list)
        {
            _listRepository.UpdateList(list);
        }
    }
}
