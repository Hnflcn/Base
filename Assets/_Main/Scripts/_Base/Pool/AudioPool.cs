using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Main.Scripts.Pool
{
	public enum AudioType
	{
		Success,
		Fail,
		Selection,
		Move,
		Match,
		OpenSlot,
		Button
	}
	public class AudioPool : MonoBehaviour
	{
		[Inject] private DiContainer _container;
		
		[Serializable]
		public class Pool
		{
			public AudioType Tag;
			public GameObject Prefab;
			public int Size;
		}

		[SerializeField] private List<Pool> Pools = new List<Pool>();
		private Dictionary<AudioType, Queue<AudioSource>> PoolDictionary = new Dictionary<AudioType, Queue<AudioSource>>();

		private void Awake()
		{
			InitPool();
		}

		private void InitPool()
		{
			foreach (Pool pool in Pools)
				AddToPool(pool.Tag, pool.Prefab, pool.Size);
		}

		public AudioSource Spawn(AudioType poolTag)
		{
			AudioSource particle = SpawnFromPool(poolTag);
			return particle;
		}


		private AudioSource SpawnFromPool(AudioType poolTag)
		{
			if (!PoolDictionary.ContainsKey(poolTag))
			{
				Debug.Log("\"" + poolTag + "\" tag doesn't exist!");
				return null;
			}

			AudioSource particle = PoolDictionary[poolTag].Dequeue();
			particle.gameObject.SetActive(true);
			particle.Play();

			PoolDictionary[poolTag].Enqueue(particle);

			return particle;
		}


		public void AddToPool(AudioType poolTag, GameObject prefab, int count)
		{
			if (PoolDictionary.ContainsKey(poolTag))
			{
				Debug.LogWarning(gameObject.name + ": \"" + poolTag + "\" Tag has already exists! Skipped.");
				return;
			}

			Queue<AudioSource> queue = new Queue<AudioSource>();
			for (int i = 0; i < count; i++)
			{
				var obj = _container.InstantiatePrefab(prefab, transform);
				obj.SetActive(false);
				obj.TryGetComponent(out AudioSource audio);
				queue.Enqueue(audio);
			}

			PoolDictionary.Add(poolTag, queue);
		}
		public void Despawn(AudioType poolTag, AudioSource obj)
		{
			if (!PoolDictionary.ContainsKey(poolTag))
			{
				Debug.LogWarning("Pool with tag " + poolTag + " doesn't exist.");
				return;
			}

			obj.gameObject.SetActive(false);
			obj.transform.SetParent(transform); 
			PoolDictionary[poolTag].Enqueue(obj);
		}
	}
}