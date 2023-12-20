using System.Collections.Generic;

namespace MRF.Dictionary
{
    [System.Serializable]
    public class SerializableDictionary<T, U>
    {
        public List<DictionaryTuple<T, U>> InitialList = new List<DictionaryTuple<T, U>>();
        public Dictionary<T, U> Dictionary = new Dictionary<T, U>();

        public void InstantiateDictionary()
        {
            Dictionary.Clear();
            foreach (var element in InitialList)
            {
                Dictionary.Add(element.key, element.value);
            }
        }

        public bool TryGetValue(T key, ref U value)
        {
            if (Dictionary.ContainsKey(key))
            {
                value = Dictionary[key];
                return true;
            }
            return false;
        }
    }

    [System.Serializable]
    public class DictionaryTuple<T, U>
    {
        public T key;
        public U value;
    }
}