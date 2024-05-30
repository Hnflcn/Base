using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Main.Scripts.Pool
{
	public enum ObjeType
	{
		Grid,
		Zone,
		Block
	}

	public class ObjectPool<T> : MonoBehaviour
	{
		[Inject] private DiContainer _container;
		[Serializable]
		public class PoolInner
		{
			public T Tag ;
			public GameObject Prefab;
			public int Size;
		}

		[SerializeField] private List<PoolInner> Pools = new List<PoolInner>();
		private Dictionary<T, Queue<GameObject>> PoolDictionary = new Dictionary<T, Queue<GameObject>>();

		private void Awake()
		{
			InitPool();
		}

		private void InitPool()
		{
			foreach (PoolInner pool in Pools)
				AddToPool(pool.Tag, pool.Prefab, pool.Size);
		}

		public GameObject Spawn(T poolTag, Vector3 position)
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

		public GameObject Spawn(T poolTag, Vector3 position, Quaternion rotation)
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

		public GameObject Spawn(T poolTag, Transform parent)
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

		public GameObject Spawn(T poolTag, Vector3 position, Transform parent)
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

		public GameObject Spawn(T poolTag, Vector3 position, Quaternion rotation, Transform parent)
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
		
		
		public void Despawn(T poolTag, GameObject obj)
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

		private GameObject SpawnFromPool(T poolTag)
		{
			if (!PoolDictionary.ContainsKey(poolTag)) return null;

			GameObject obj = PoolDictionary[poolTag].Dequeue();
			obj.SetActive(true);
			PoolDictionary[poolTag].Enqueue(obj);
			return obj;
		}

		public void AddToPool(T poolTag, GameObject prefab, int count)
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