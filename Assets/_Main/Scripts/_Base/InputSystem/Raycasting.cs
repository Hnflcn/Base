using UnityEngine;

namespace _Main.Scripts._Base.InputSystem
{
    public static class RayCasting
    {
        public static T ReturnRayObject<T>(LayerMask layerMask, int distanceMax) where T : Component
        {
            T  rayObject = null;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, distanceMax, layerMask)) return null;
            if (!hit.collider.TryGetComponent(out T type))
                return null;
            rayObject = type;
            return rayObject;
        }
    }
    
    public static class SphereCasting
    {
        public static T ReturnSphereCastObject<T>(LayerMask layerMask, float radius, int distanceMax) where T : Component
        {
            T rayObject = null;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.SphereCast(ray, radius, out var hit, distanceMax, layerMask)) return null;

            if (!hit.collider.TryGetComponent(out T type))
                return null;

            rayObject = type;
            return rayObject;
        }
    }

}