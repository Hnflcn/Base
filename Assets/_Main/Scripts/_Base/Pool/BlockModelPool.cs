namespace _Main.Scripts._Base.Pool
{
    using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Main.Scripts.Pool
{
	public enum ModelType
	{
		Hole,
		Screw,
		Card
	}
	public class BlockModelPool : MonoBehaviour
	{
		[Inject] private DiContainer _container;
		[Serializable]
		public class Pool
		{
			[Tooltip("Give a tag to the pool for calling it")]
			public ModelType Tag ;
			[Tooltip("The prefab to be pooled")]
			public GameObject Prefab;
			[Tooltip("The size (count) of the pool")]
			public int Size;
		}

		[SerializeField] private List<Pool> Pools = new List<Pool>();
		private Dictionary<ModelType, Queue<GameObject>> PoolDictionary = new Dictionary<ModelType, Queue<GameObject>>();

		private void Awake()
		{
			InitPool();
		}

		private void InitPool()
		{
			foreach (Pool pool in Pools)
				AddToPool(pool.Tag, pool.Prefab, pool.Size);
		}

		public GameObject Spawn(ModelType poolTag, Vector3 position)
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

			GameObject obj = PoolDictionary[poolTag].Dequeue();
			obj.SetActive(true);
			obj.transform.position = position;
			return obj;
		}

		public GameObject Spawn(ModelType poolTag, Vector3 position, Quaternion rotation)
		{if (!PoolDictionary.ContainsKey(poolTag))
			{
				Debug.LogWarning("Pool with tag " + poolTag + " doesn't exist.");
				return null;
			}

			if (PoolDictionary[poolTag].Count == 0)
			{
				Debug.LogWarning("No object available in pool " + poolTag);
				return null;
			}

			GameObject obj = PoolDictionary[poolTag].Dequeue(); 
			obj.SetActive(true);
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj;
		}

		public GameObject Spawn(ModelType poolTag, Transform parent)
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

			GameObject obj = PoolDictionary[poolTag].Dequeue(); 
			obj.SetActive(true);
			obj.transform.SetParent(parent);
			return obj;
		}

		public GameObject Spawn(ModelType poolTag, Vector3 position, Transform parent)
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

			GameObject obj = PoolDictionary[poolTag].Dequeue(); 
			obj.SetActive(true);
			obj.transform.position = position;
			obj.transform.SetParent(parent);
			return obj;
		}

		public GameObject Spawn(ModelType poolTag, Vector3 position, Quaternion rotation, Transform parent)
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

			GameObject obj = PoolDictionary[poolTag].Dequeue(); 
			obj.SetActive(true);
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			obj.transform.SetParent(parent);
			return obj;
		}
		
		
		public void Despawn(ModelType poolTag, GameObject obj)
		{
			if (!PoolDictionary.ContainsKey(poolTag) || obj == null)
			{
				Debug.LogWarning("Invalid pool tag or object.");
				return;
			}

			obj.SetActive(false);
			obj.transform.SetParent(transform); 
			PoolDictionary[poolTag].Enqueue(obj);
		}

		private GameObject SpawnFromPool(ModelType poolTag)
		{
			if (!PoolDictionary.ContainsKey(poolTag)) return null;

			GameObject obj = PoolDictionary[poolTag].Dequeue();
			obj.SetActive(true);
			PoolDictionary[poolTag].Enqueue(obj);
			return obj;
		}

		public void AddToPool(ModelType poolTag, GameObject prefab, int count)
		{
			if (PoolDictionary.ContainsKey(poolTag))
			{
				Debug.LogWarning(gameObject.name + ": \"" + poolTag + "\" Tag has already exists! Skipped.");
				return;
			}

			Queue<GameObject> queue = new Queue<GameObject>();
			for (int i = 0; i < count; i++)
			{
				var obj = _container.InstantiatePrefab(prefab, transform);
				obj.SetActive(false);
				queue.Enqueue(obj);
			}

			PoolDictionary.Add(poolTag, queue);
		}
		
		
	}
}
}