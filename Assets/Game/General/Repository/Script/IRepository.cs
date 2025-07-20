using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public interface IRepository<T>
    {
        void Add(T item);
        bool Remove(T item);
        T Get();
        List<T> GetAll();
        bool RemoveAll();
    }
}
