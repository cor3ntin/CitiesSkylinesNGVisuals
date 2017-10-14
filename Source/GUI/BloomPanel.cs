using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.PostProcessing;

namespace CitiesSkylinesNGVisuals
{
    /**
     * Enables and configures the bloom and lensflare effect using the
     * already added Bloom class of Main Camera.
     */
    class BloomPanel : IEffectMenu
    {


        private BloomModel m_model;
        /**
         * Create a new bloom effect menu with or without an existing config.
         */
        public BloomPanel (BloomModel model)
        {
            m_model = model;
        }

        public void onGUI (float x, float y)
        {
            if (GUI.Button (new Rect (x, y, 75, 20), "Default")) {
                m_model.Reset ();
            }

            /*if (GUI.Button (new Rect (x + 75, y, 75, 20), "Medium")) {
                m_activeConfig = BloomModel.BloomSettings.
            }

            if (GUI.Button (new Rect (x + 150, y, 75, 20), "High")) {
                m_activeConfig = BloomConfig.getHighPreset ();
            }

            if (GUI.Button (new Rect (x + 225, y, 75, 20), "Load")) {
                load ();
            }*/

            y += 25;
            m_model.enabled = GUI.Toggle (new Rect (x, y, 200.0f, 20.0f), m_model.enabled, "Enable Bloom Effect");
            y += 25;

            var settings = m_model.settings;

            settings.bloom.intensity = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 3.0f, "Intensity", settings.bloom.intensity);
            y += 25;

            settings.bloom.threshold = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 3.0f, "Threshold", settings.bloom.threshold);
            y += 25;

            settings.bloom.softKnee = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 1f, "Smoothness", settings.bloom.softKnee);
            y += 25;

            settings.bloom.radius = GUIUtils.drawSliderWithLabel (x, y, 0.0f, 2.0f, "Radius", settings.bloom.radius);
            y += 25;

            settings.bloom.antiFlicker = GUI.Toggle (new Rect (x, y, 200.0f, 20.0f), settings.bloom.antiFlicker, "Anti Flicker");

            m_model.settings = settings;
        }
    }
}