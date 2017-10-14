using System;
using UnityEngine;
namespace CitiesSkylinesNGVisuals
{
    public class SunShaftsPanel : IEffectMenu
    {
        private SunShaftsSettings m_settings;

        public SunShaftsPanel (SunShaftsSettings settings)
        {
            m_settings = settings;
        }

        public void onGUI (float x, float y)
        {
            y += 25;
            m_settings.enabled = GUI.Toggle (new Rect (x, y, 200.0f, 20.0f), m_settings.enabled, "Enable Sun Shafts");
            y += 25;


            m_settings.sunShaftIntensity = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 6.0f, "Intensity", m_settings.sunShaftIntensity);
            y += 25;

            m_settings.height = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 2.0f, "Height", m_settings.height);
            y += 30;


            m_settings.sunColor.r = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 1.0f, "Red", m_settings.sunColor.r);
            y += 25;
            m_settings.sunColor.g = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 1.0f, "Green", m_settings.sunColor.g);
            y += 25;
            m_settings.sunColor.b = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 1.0f, "Blue", m_settings.sunColor.b);
            y += 25;
        }
    }
}