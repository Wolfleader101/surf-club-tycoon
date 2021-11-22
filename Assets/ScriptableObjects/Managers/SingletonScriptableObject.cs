using UnityEngine;

namespace ScriptableObjects.Managers
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] assets = Resources.FindObjectsOfTypeAll<T>();
                    if (assets == null || assets.Length < 1)
                    {
                        throw new System.Exception("Could not find any singleton scriptable object instances");
                    }

                    if (assets.Length > 1)
                    {
                        Debug.LogWarning("Multiple instances of the singleton scriptable object found");
                    }

                    _instance = assets[0];
                    _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
                }

                return _instance;
            }
        }
    }
}