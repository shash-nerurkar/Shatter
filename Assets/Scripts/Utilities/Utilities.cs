using UnityEngine;

namespace Scripts.Utilities
{
    public static partial class MiscUtils
    {
        /// <summary>
        /// Instantiates a prefab from a given path and sets its parent to a given transform.
        /// Optionally sets the name of the instantiated object and its sibling index.
        /// </summary>
        /// <param name="path">The path of the prefab to instantiate.</param>
        /// <param name="parent">The parent transform of the instantiated object.</param>
        /// <param name="name">The name of the instantiated object. If null, the name is not changed.</param>
        /// <param name="siblingIndex">The sibling index of the instantiated object. If null, the sibling index is not changed.</param>
        /// <typeparam name="T">The type of the prefab to instantiate.</typeparam>
        /// <returns>The instantiated object if successful, null otherwise.</returns>
        public static T InstantiatePrefab<T>(string path, Transform parent, string name = null, int? siblingIndex = null) where T : Object
        {
            T prefab = Resources.Load<T>(path);
            if (prefab == null) 
                return null;

            T instance = Object.Instantiate(prefab, parent);
            
            GameObject gameobject = instance as GameObject;
            if(gameobject == null) 
            {
                Component component = instance as Component;
                if(component == null)
                {
                    Object.Destroy(instance);
                    return null;
                }
                
                gameobject = component.gameObject;
            }
            
            if (name != null)
                gameobject.name = name;

            if (siblingIndex.HasValue)
                gameobject.transform.SetSiblingIndex(siblingIndex.Value);

            return instance;
        }

        /// <summary>
        /// Instantiates an empty game object with a given name and parent transform, and optionally sets its sibling index.
        /// </summary>
        /// <param name="parent">The parent transform of the instantiated game object.</param>
        /// <param name="name">The name of the instantiated game object. If null, the name is not changed.</param>
        /// <param name="siblingIndex">The sibling index of the instantiated game object. If null, the sibling index is not changed.</param>
        /// <returns>The instantiated game object if successful, null otherwise.</returns>
        public static GameObject InstantiateEmpty(Transform parent, string name = null, int? siblingIndex = null)
        {
            GameObject go = new(name ?? "GameObject");
            
            go.transform.SetParent(parent);

            if (siblingIndex.HasValue)
                go.transform.SetSiblingIndex(siblingIndex.Value);

            return go;
        }
    }
}