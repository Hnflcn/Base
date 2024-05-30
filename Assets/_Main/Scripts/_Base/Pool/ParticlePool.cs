using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Main.Scripts.Pool
{
	public enum ParticleType
	{
		Success,
		FullBox,
		RightMove
	}
	public class ParticlePool : MonoBehaviour
	{
		[Inject] private DiContainer _container;
		[Serializable]
		public class Pool
		{
			[Tooltip("Give a tag to the pool for calling it")]
			public ParticleType Tag;
			[Tooltip("Prefab of the Particle to be pooled")]
			public GameObject Prefab;
			[Tooltip("The size (count) of the pool")]
			public int Size;
			[Tooltip("Whether the Particle deactivates itself after finished playing")]
			public bool AutoDeactivate;
		}

		[SerializeField] private List<Pool> Pools = new List<Pool>();
		private Dictionary<ParticleType, Queue<ParticleSystem>> PoolDictionary = new Dictionary<ParticleType, Queue<ParticleSystem>>();

		private void Awake()
		{
			InitPool();
		}

		private void InitPool()
		{
			foreach (Pool pool in Pools)
				AddToPool(pool.Tag, pool.Prefab, pool.Size);
		}

		public ParticleSystem Spawn(ParticleType poolTag, Vector3 position)
		{
			ParticleSystem particle = SpawnFromPool(poolTag);

			particle.transform.position = position;
			return particle;
		}

		public ParticleSystem Spawn(ParticleType poolTag, Vector3 position, Quaternion rotation)
		{
			ParticleSystem particle = SpawnFromPool(poolTag);

			particle.transform.position = position;
			particle.transform.rotation = rotation;
			return particle;
		}

		public ParticleSystem Spawn(ParticleType poolTag, Transform parent)
		{
			ParticleSystem particle = SpawnFromPool(poolTag);

			particle.transform.SetParent(parent);
			particle.transform.localPosition = Vector3.zero;
			particle.transform.forward = parent.forward;
			return particle;
		}

		public ParticleSystem Spawn(ParticleType poolTag, Vector3 position, Transform parent)
		{
			ParticleSystem particle = SpawnFromPool(poolTag);

			particle.transform.position = position;
			particle.transform.forward = parent.forward;
			particle.transform.SetParent(parent);
			return particle;
		}
		public ParticleSystem Spawn(ParticleType poolTag, Vector3 position, Quaternion rotation, Transform parent)
		{
			ParticleSystem particle = SpawnFromPool(poolTag);

			particle.transform.position = position;
			particle.transform.rotation = rotation;
			particle.transform.SetParent(parent);
			return particle;
		}

		private ParticleSystem SpawnFromPool(ParticleType poolTag)
		{
			if (!PoolDictionary.ContainsKey(poolTag))
			{
				Debug.Log("\"" + poolTag + "\" tag doesn't exist!");
				return null;
			}

			ParticleSystem particle = PoolDictionary[poolTag].Dequeue();
			particle.gameObject.SetActive(true);
			particle.Play();

			PoolDictionary[poolTag].Enqueue(particle);

			int index = PoolDictionary.Keys.ToList().IndexOf(poolTag);
			if (Pools[index].AutoDeactivate && !particle.main.loop)
				StartCoroutine(Deactivate(particle));

			return particle;
		}

		private IEnumerator Deactivate(ParticleSystem particle)
		{
			float delay = .5f;
			while (particle.isPlaying)
			{
				yield return new WaitForSeconds(delay);
				if (particle.isStopped)
					particle.gameObject.SetActive(false);

				delay = Mathf.Pow(2, delay);
			}
		}
		public void Despawn(ParticleType poolTag, ParticleSystem obj)
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

		public void AddToPool(ParticleType poolTag, GameObject prefab, int count)
		{
			if (PoolDictionary.ContainsKey(poolTag))
			{
				Debug.LogWarning(gameObject.name + ": \"" + poolTag + "\" Tag has already exists! Skipped.");
				return;
			}

			Queue<ParticleSystem> queue = new Queue<ParticleSystem>();
			for (int i = 0; i < count; i++)
			{
				var obj = _container.InstantiatePrefab(prefab, transform);
				obj.SetActive(false);
				queue.Enqueue(obj.GetComponent<ParticleSystem>());
			}

			PoolDictionary.Add(poolTag, queue);
		}
	}
}