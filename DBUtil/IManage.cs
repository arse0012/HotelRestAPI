using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public interface IManage<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        bool Post(T guest);
        bool Put(int id, T guest);
        bool Delete(int id);
    }
}