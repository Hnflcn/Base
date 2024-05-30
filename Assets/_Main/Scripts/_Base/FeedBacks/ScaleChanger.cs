using System;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts._Base.FeedBacks
{
    public class ScaleChanger : MonoBehaviour
    {
        public void AnimateScaleUpDown(Transform trans, float scaleF, float duration, Action? onComplete = null)
        {
            var fScale = trans.localScale;
            trans.DOScale(fScale * scaleF, duration).OnComplete(() =>
            {
                trans.DOScale(fScale, duration).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            });
        }
        
    }
}