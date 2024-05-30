using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts._Base.FeedBacks
{
    public class WaveAnimation : MonoBehaviour
    {
        public float duration = 1f; 
        public float delayBetweenElements = 0.05f;
        
        public Task StartWaveAnimation(GameObject[,] gridMatrix, CancellationToken cancellationToken)
        {
            var rows = gridMatrix.GetLength(0);
            var cols = gridMatrix.GetLength(1);

            var tcs = new TaskCompletionSource<bool>();
            var remainingAnimations = rows * cols;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        tcs.SetCanceled();
                        return tcs.Task;
                    }

                    var delay = (i + j) * delayBetweenElements;
                    if (gridMatrix[i, j] != null)
                        gridMatrix[i, j].transform.DOLocalMoveY(1f, duration).SetLoops(2, LoopType.Yoyo)
                            .SetDelay(delay)
                            .OnComplete(() =>
                            {
                                if (cancellationToken.IsCancellationRequested)
                                {
                                    tcs.TrySetCanceled();
                                    return;
                                }

                                remainingAnimations--;
                                if (remainingAnimations <= 0)
                                {
                                    tcs.TrySetResult(true);
                                }
                            });
                    else
                    {
                        remainingAnimations--;
                    }
                }
            }

            if (remainingAnimations == 0)
            {
                tcs.TrySetResult(true);
            }

            return tcs.Task;
        }
    }
}