
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utilities
{
    /// <summary>
    /// A serializable dictionary that can be used to store key-value pairs of any type.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [Serializable]
        public struct Pair
        {
            public TKey key;
            public TValue value;

            public static implicit operator KeyValuePair<TKey, TValue>(Pair pair)
            {
                return new KeyValuePair<TKey, TValue>(pair.key, pair.value);
            }

            public static implicit operator Pair(KeyValuePair<TKey, TValue> pair)
            {
                return new Pair
                {
                    key = pair.Key, 
                    value = pair.Value
                };
            }
        }

        [SerializeField] private List<Pair> entries = new();

        public SerializableDictionary() {}

        public SerializableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) {}

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            Clear();

            foreach (var entry in entries)
            {
                if (entry.key == null || ContainsKey(entry.key))
                {
                    Debug.LogWarning($"Skipping duplicate key '{entry.key}' in dictionary '{this.GetType().Name}'");
                    continue;
                }

                this[entry.key] = entry.value;
            }
        }
    }
}