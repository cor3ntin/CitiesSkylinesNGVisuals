using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

namespace CitiesSkylinesNGVisuals
{
    class AmbientOcclusionPanel : IEffectMenu
    {
        AmbientOcclusionModel m_model = null;

        public AmbientOcclusionPanel (AmbientOcclusionModel model)
        {
            m_model = model;
        }

        public void disable ()
        {
            m_model.enabled = false;
        }

        public void onGUI (float x, float y)
        {
            AmbientOcclusionModel.Settings settings = m_model.settings;

            if (GUI.Button (new Rect (x, y, 75, 20), "Default")) {
                m_model.settings = AmbientOcclusionModel.Settings.defaultSettings;
            }

            y += 25;

            m_model.enabled = GUI.Toggle (new Rect (x, y, 200.0f, 20.0f), m_model.enabled, "enable ambient occlusion");
            y += 25;

            settings.intensity = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 4.0f, "Intensity", m_model.settings.intensity);
            y += 25;

            settings.radius = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 2.0f, "Radius", settings.radius);
            y += 25;

            settings.sampleCount = (AmbientOcclusionModel.SampleCount)GUIUtils.drawIntSliderWithLabel (x, y, 0, (int)AmbientOcclusionModel.SampleCount.High, "Sample Count: ", settings.sampleCount.ToString (), (int)settings.sampleCount);
            y += 25;

            // m_config.settings.source = (OcclusionSource)PPFXUtility.drawIntSliderWithLabel(x, y, 0, 1, "Occlusion Source: ", m_activeConfig.getOcclusionSourceLabel(), (int)m_config.settings.source);
            // y += 25;

            settings.downsampling = GUI.Toggle (new Rect (x, y, 200.0f, 20.0f), settings.downsampling, "Enable downsampling");
            y += 25;

            m_model.settings = settings;
        }
    }
}
