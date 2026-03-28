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
        public static T InstantiatePrefab<T>(
            string path, Transform parent, Quaternion? rotation = null, Vector3? position = null,
            string name = null, int? siblingIndex = null
        ) where T : Object
        {
            T prefab = Resources.Load<T>(path);
            if (prefab == null) 
                return null;

            return InstantiatePrefab<T, T>(prefab, parent, rotation, position, name, siblingIndex);
        }
        
        /// <summary>
        /// Instantiates a prefab from a given path and sets its parent to a given transform.
        /// Optionally sets the name of the instantiated object and its sibling index.
        /// </summary>
        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="parent">The parent transform of the instantiated object.</param>
        /// <param name="name">The name of the instantiated object. If null, the name is not changed.</param>
        /// <param name="siblingIndex">The sibling index of the instantiated object. If null, the sibling index is not changed.</param>
        /// <typeparam name="T1">The prefab type used.</typeparam>
        /// <typeparam name="T2">The type to instantiate.</typeparam>
        /// <returns>The instantiated object if successful, null otherwise.</returns>
        public static T2 InstantiatePrefab<T1, T2>(
            T1 prefab, Transform parent, Quaternion? rotation = null, Vector3? position = null,
            string name = null, int? siblingIndex = null
        ) where T1 : Object where T2 : Object
        {
            T1 instance = rotation.HasValue && position.HasValue 
                ? Object.Instantiate(prefab, (Vector3)position, (Quaternion)rotation, parent)
                : Object.Instantiate(prefab, parent);

            GameObject gameObject = instance as GameObject;
            if (gameObject == null)
            {
                Component component = instance as Component;
                if (component == null)
                {
                    Object.Destroy(instance);
                    return null;
                }
                gameObject = component.gameObject;
            }

            if (name != null)
                gameObject.name = name;
            
            if (siblingIndex.HasValue)
                gameObject.transform.SetSiblingIndex(siblingIndex.Value);

            T2 result = gameObject as T2 ?? gameObject.GetComponent<T2>();
            if (result == null)
            {
                Object.Destroy(gameObject);
                return null;
            }

            return result;
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