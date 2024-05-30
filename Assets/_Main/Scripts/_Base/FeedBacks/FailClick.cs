using System;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts._Base.FeedBacks
{
    public class FailClick : MonoBehaviour
    {
        private bool _isCantClick; 
        
        public void FailColor(Renderer renderer, int loopCount, Color color, Action? onComplete = null)
        {
            if (_isCantClick) return;
            _isCantClick = true;
            StartColorChangeSequence(renderer,loopCount, color, onComplete);
        }
        
        private void StartColorChangeSequence(Renderer renderer,  int yoyoLoops, Color color, Action? onComplete = null, float highlightDuration = 0.1f)
        {
            Tween colorTween = renderer.material.DOColor(color, highlightDuration);
            colorTween.SetLoops(yoyoLoops, LoopType.Yoyo);
            colorTween.OnComplete(() => {
                _isCantClick = false;
                onComplete?.Invoke();
            });
        }

       
    }
}