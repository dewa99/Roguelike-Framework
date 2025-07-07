using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public interface IRepository<T>
    {
        void Add();
        bool Remove();
        T Get();
        List<T> GetAll();
        bool RemoveAll();
    }
}
