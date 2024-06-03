using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Main.Scripts.Pool
{
    public abstract class BasePool<TPoolKey, TPoolObject> : MonoBehaviour where TPoolObject : UnityEngine.Object
    {
        [Inject] protected DiContainer _container;

        [Serializable]
        public class Pool
        {
            [Tooltip("Give a tag to the pool for calling it")]
            public TPoolKey Tag;
            [Tooltip("The prefab to be pooled")]
            public GameObject Prefab;
            [Tooltip("The size (count) of the pool")]
            public int Size;
            [Tooltip("Whether the Particle deactivates itself after finished playing")]
            public bool AutoDeactivate;
        }

        [SerializeField] protected List<Pool> Pools = new List<Pool>();
        protected Dictionary<TPoolKey, Queue<TPoolObject>> PoolDictionary = new Dictionary<TPoolKey, Queue<TPoolObject>>();

        private void Awake()
        {
            InitPool();
        }

        protected virtual void InitPool()
        {
            foreach (Pool pool in Pools)
                AddToPool(pool.Tag, pool.Prefab, pool.Size);
        }

        public TPoolObject Spawn(TPoolKey poolTag, Vector3 position)
        {
            TPoolObject obj = SpawnFromPool(poolTag);
            if (obj == null) return null;

            if (obj is GameObject gameObject)
            {
                gameObject.transform.position = position;
            }

            return obj;
        }

        public TPoolObject Spawn(TPoolKey poolTag, Vector3 position, Quaternion rotation)
        {
            TPoolObject obj = SpawnFromPool(poolTag);
            if (obj == null) return null;

            if (obj is GameObject gameObject)
            {
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
            }

            return obj;
        }

        public TPoolObject Spawn(TPoolKey poolTag, Transform parent)
        {
            TPoolObject obj = SpawnFromPool(poolTag);
            if (obj == null) return null;

            if (obj is GameObject gameObject)
            {
                gameObject.transform.SetParent(parent);
            }

            return obj;
        }

        public TPoolObject Spawn(TPoolKey poolTag, Vector3 position, Transform parent)
        {
            TPoolObject obj = SpawnFromPool(poolTag);
            if (obj == null) return null;

            if (obj is GameObject gameObject)
            {
                gameObject.transform.position = position;
                gameObject.transform.SetParent(parent);
            }

            return obj;
        }

        public TPoolObject Spawn(TPoolKey poolTag, Vector3 position, Quaternion rotation, Transform parent)
        {
            TPoolObject obj = SpawnFromPool(poolTag);
            if (obj == null) return null;

            if (obj is GameObject gameObject)
            {
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
                gameObject.transform.SetParent(parent);
            }

            return obj;
        }

        public void Despawn(TPoolKey poolTag, TPoolObject obj)
        {
            if (!PoolDictionary.ContainsKey(poolTag) || obj == null)
            {
                Debug.LogWarning("Invalid pool tag or object.");
                return;
            }

            if (obj is GameObject gameObject)
            {
                gameObject.SetActive(false);
                gameObject.transform.SetParent(transform);
            }

            PoolDictionary[poolTag].Enqueue(obj);
        }

        private TPoolObject SpawnFromPool(TPoolKey poolTag)
        {
            if (!PoolDictionary.ContainsKey(poolTag))
            {
                Debug.LogWarning("Pool with tag " + poolTag + " doesn't exist.");
                return null;
            }

            if (PoolDictionary[poolTag].Count == 0)
            {
                Debug.LogWarning("No object available in pool " + poolTag);
                return null;
            }

            TPoolObject obj = PoolDictionary[poolTag].Dequeue();

            if (obj is GameObject gameObject)
            {
                gameObject.SetActive(true);
            }

            return obj;
        }

        public void AddToPool(TPoolKey poolTag, GameObject prefab, int count)
        {
            if (PoolDictionary.ContainsKey(poolTag))
            {
                Debug.LogWarning(gameObject.name + ": \"" + poolTag + "\" Tag already exists! Skipped.");
                return;
            }

            Queue<TPoolObject> queue = new Queue<TPoolObject>();
            for (int i = 0; i < count; i++)
            {
                var obj = _container.InstantiatePrefab(prefab, transform);

                if (obj is TPoolObject poolObject)
                {
                    if (obj is GameObject gameObject)
                    {
                        gameObject.SetActive(false);
                    }

                    queue.Enqueue(poolObject);
                }
                else
                {
                    Destroy(obj);
                    throw new InvalidOperationException($"The prefab does not contain a component of type {typeof(TPoolObject).Name}");
                }
            }

            PoolDictionary.Add(poolTag, queue);
        }
    }
}
