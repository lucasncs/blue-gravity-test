using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetCollection
{
    public abstract class AssetCollection<T> : ScriptableObject, IEnumerable<T> where T : Object
    {
        [SerializeField] protected T[] _collection;

        public T this[int index] => GetAt(index);
        public int Length => _collection.Length;

        public T GetAt(int index)
        {
            return _collection[index];
        }

        public bool TryGetAt(int index, out T value)
        {
            if (index >= Length || index < 0)
            {
                value = null;
                return false;
            }

            value = GetAt(index);
            return true;
        }

        public T Find(Func<T, bool> predicate)
        {
            return _collection.FirstOrDefault(predicate);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnValidate()
        {
            if (Length == 0) return;

            for (int i = 0; i < Length; i++)
            {
                if (_collection[i] == null)
                {
                    Debug.LogError($"Object missing or not set on \"{name}\"", this);
                    break;
                }
            }
        }
    }
}