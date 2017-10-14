using System;
using UnityEngine;
namespace CitiesSkylinesNGVisuals
{
    public class GUIUtils
    {
        public static float drawSliderWithLabel (float x, float y, float min, float max, String label, float configValue)
        {
            GUI.Label (new Rect (x, y, 200, 20), label);
            GUI.Label (new Rect (x + 150, y, 30, 20), configValue.ToString ());

            float newValue = GUI.HorizontalSlider (new Rect (x + 200, y + 5, 100, 20), configValue, min, max);

            /*if (newValue == configValue)
            {
                // The value has not changed, but might edit the text field.
                string value = GUI.TextField(new Rect(x + 150, y, 30, 20), configValue.ToString());

                // Read the value from the text field
                if (float.TryParse(value, out newValue))
                {
                    // Check if new value lies with the ranges
                    newValue = Math.Max(newValue, min);
                    newValue = Math.Min(newValue, max);
                }
            }
            else
            {
                // Just show the new value
                string value = GUI.TextField(new Rect(x + 150, y, 30, 20), configValue.ToString());
            }*/

            return newValue;
        }

        public static int drawIntSliderWithLabel (float x, float y, int min, int max, String label, int configValue)
        {
            GUI.Label (new Rect (x, y, 200, 20), label);
            GUI.Label (new Rect (x + 150, y, 200, 20), configValue.ToString ());

            return (int)GUI.HorizontalSlider (new Rect (x + 200, y + 5, 100, 20), configValue, min, max);
        }

        public static int drawIntSliderWithLabel (float x, float y, int min, int max, String label, String valueLabel, int configValue)
        {
            GUI.Label (new Rect (x, y, 100, 20), label);
            GUI.Label (new Rect (x + 150, y, 100, 20), valueLabel);

            return (int)GUI.HorizontalSlider (new Rect (x + 200, y + 5, 100, 20), configValue, min, max);
        }
    }
}
