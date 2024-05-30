using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _Main.Scripts._Base.GridSystem.SO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Main.Scripts._Base.GridSystem
{
    public abstract class GridManager : MonoBehaviour
    {
        [SerializeField] protected LevelGridData levelGridData;
        
        public float offsetGridX;
        public float offsetGridY;
        public float offsetGridZoneX;
        public float offsetGridZoneY;
        
        protected CancellationTokenSource CancellationTokenSource;

        public async UniTask Initialization()
        {
            await Generate();
        }

        private async UniTask Generate()
        {
            CancellationTokenSource = new CancellationTokenSource();
            var token = CancellationTokenSource.Token;

            
            await Generating();
    
            try
            {
                //  await FeedbackController.Instance.advanceFeedbacks.StartWaveAnimation(blockParentMatris, token);
                if (token.IsCancellationRequested) return;

                await Task.Delay(1000, token).ConfigureAwait(false);
                if (token.IsCancellationRequested) return;
            }
            catch (TaskCanceledException)
            {
            }
        }
        private void OnApplicationQuit()
        {
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
            }
        }

        protected void Shuffle<T>(IList<T> list)
        {
            var rng = new System.Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }



        protected abstract UniTask Generating();



    }
}