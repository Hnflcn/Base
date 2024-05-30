using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts._Base.FeedBacks
{
    public class HighlightEffect: MonoBehaviour
    {
        public IEnumerator HighlightRandomObjects<T>(float highlightDuration, Color color, List<T> list, Action<T>? onComplete = null)
        {
            GameplayReferences.Instance.CanContinueGame = false;

            for (var i = 0; i < list.Count*2; i++)
            {
                var randomIndex = Random.Range(0, list.Count);

                var selectedObject = list[randomIndex];
             //   selectedObject.soundEffect.Play();    // sesi varsa oynat
             
             //------------- buraya objenin materyalini koyman gerekiyor, değişken olarak material ata-------
             var rend = new Renderer();
             var mate = rend.material;
             //----------------------------------------------------------------------------------------------
             
                StartCoroutine(HighlightObject(mate, color, highlightDuration));
                
                yield return new WaitForSeconds(highlightDuration);
            }

            yield return new WaitForSeconds(highlightDuration);
            
            var finalIndex = Random.Range(0, list.Count);
            var finalObject = list[finalIndex];
            
            onComplete?.Invoke(finalObject);
        }
        
        private IEnumerator HighlightObject(Material mat,Color color, float duration)
        {
            var changingMaterial = mat;
            var orjColor = changingMaterial.color;

            changingMaterial.color = color;
            yield return new WaitForSeconds(duration / 2);
            changingMaterial.color = orjColor;
        }


    }
}