using UnityEngine;

namespace _Main.Scripts._Base.HelperFunctions
{
    public static class TrailColorChanger
    {
        public static void AddTrailColor(Renderer renderer, TrailRenderer trail)
        {
            var gradient = new Gradient();
            const byte col = 0;

            var materials = renderer.materials;
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(materials[col].color, 0.0f),
                    new GradientColorKey(materials[col].color, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.5f, 1.0f) }
            );
            trail.colorGradient = gradient;
        }
    }
}