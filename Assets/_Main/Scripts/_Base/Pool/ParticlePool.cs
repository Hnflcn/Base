using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts._Base.Pool.Types;
using _Main.Scripts.Pool;
using UnityEngine;

namespace _Main.Scripts._Base.Pool
{
    public class ParticlePool : BasePool<ParticleType, ParticleSystem>
    {
        protected override void InitPool()
        {
            base.InitPool();

            foreach (var pool in Pools)
            {
                if (pool.AutoDeactivate)
                {
                    foreach (var ps in PoolDictionary[pool.Tag])
                    {
                        if (!ps.main.loop)
                        {
                            StartCoroutine(Deactivate(ps));
                        }
                    }
                }
            }
        }

        private IEnumerator Deactivate(ParticleSystem particle)
        {
            float delay = .5f;
            while (particle.isPlaying)
            {
                yield return new WaitForSecondsRealtime(delay);
                if (particle.isStopped)
                    particle.gameObject.SetActive(false);

                delay = Mathf.Pow(2, delay);
            }
        }
    }
}