using System;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts._Base.FeedBacks
{
    public class Shake : MonoBehaviour
    {
        private Vector3 _baseStrength = Vector3.right;
        
        public void AddShakeRotation(Transform trans, Vector3? strength = null, float duration = .2f,
            int vibrato = 50, Action doCompl = null, float randomNess = 90F)
        {
            Vector3 actualStrength = strength ?? _baseStrength;
            trans.DOShakeRotation(duration, actualStrength, vibrato, randomNess).OnComplete(() =>
            {
                doCompl?.Invoke();
            });
        }
        
        public void AddShakePosition(Transform trans,Vector3? strength = null, float duration = .2f, int vibrato = 30,Action? doCompl = null,
            float randomNess = 90F)
        {
            Vector3 actualStrength = strength ?? _baseStrength;
            trans.DOShakePosition(duration, Vector3.right, vibrato, randomNess).OnComplete(()=>
            {
                doCompl?.Invoke();
            });
        }

        public void AddShakeScale(Transform trans,   Vector3? strength = null, float duration = .2f, int vibrato = 30, Action? doCompl = null,
            float randomNess = 90F)
        {
            Vector3 actualStrength = strength ?? _baseStrength;
            trans.DOShakeScale(duration, actualStrength, vibrato, randomNess).OnComplete(()=>
            {
                doCompl?.Invoke();
            });
        }
    }
}