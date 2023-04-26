using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ViamazusSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] List<TKey> keys = new List<TKey>();
    [SerializeField] List<TValue> values = new List<TValue>();

    public void OnBeforeSerialize()
    {
        keys.Clear(); values.Clear();

        if (typeof(TKey).IsSubclassOf(typeof(UnityEngine.Object)) || typeof(TKey) == typeof(UnityEngine.Object))
        {
            foreach (KeyValuePair<TKey, TValue> pair in this.Where(pair => pair.Key != null))
            {
                keys.Add(pair.Key); values.Add(pair.Value);
            }
        }
        else
        {
			foreach (KeyValuePair<TKey, TValue> pair in this)
			{
				keys.Add(pair.Key); values.Add(pair.Value);
			}
		}
    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count) throw new System.Exception(string.Format("These data types are not serializable! There are {0} keys and {1} values"));

        for (int i = 0; i < keys.Count; i++) this.Add(keys[i], values[i]);
    }
}
