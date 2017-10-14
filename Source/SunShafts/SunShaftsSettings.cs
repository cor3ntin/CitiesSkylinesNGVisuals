using System;
using UnityEngine;
namespace CitiesSkylinesNGVisuals
{
    [Serializable]
    public class SunShaftsSettings
    {
        public Color sunColor = Color.white;
        public Color sunThreshold = new Color (0.15f, 0.15f, 0.15f, 1f);
        public float sunShaftBlurRadius = 2.5f;
        public float sunShaftIntensity = 1.15f;
        public float maxRadius = 0.75f;
        public bool enabled = false;
        public float height = 1f;
    }
}
