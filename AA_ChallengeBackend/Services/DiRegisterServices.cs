using Data;
using System;
using System.Collections.Generic;
using System.Text;
using Utils.DInjection;

namespace Services
{
    public class DiRegisterServices
    {
        public static List<IClassType> GetServiceList()
        {
            var list = new List<IClassType>();
            list.AddRange(DiRegisterData.GetDataList());
            list.Add(new ClassType<IListService, ListService>());
            list.Add(new ClassType<ITaskService, TaskService>());
            return list;
        }
    }
}
