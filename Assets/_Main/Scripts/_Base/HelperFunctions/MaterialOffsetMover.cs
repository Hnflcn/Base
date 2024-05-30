using UnityEngine;

namespace _Main.Scripts._Base.HelperFunctions
{
    public class MaterialOffsetMover : MonoBehaviour
    {
        [SerializeField] private Material materialOffset;
        [SerializeField] private float offsetX = 5f;
        [SerializeField] private bool isActive;

       //private void Update()
       //{
       //    if (isActive)
       //        ChangeMaterialOffset();
       //}
        
        private void ChangeMaterialOffset()
        {
            var offset = materialOffset.mainTextureOffset;
            offset.x -= offsetX * Time.deltaTime;

            materialOffset.mainTextureOffset = offset;
        }

        
    }
}