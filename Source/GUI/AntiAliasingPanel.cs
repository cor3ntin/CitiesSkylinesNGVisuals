using System;

using UnityEngine;
using UnityEngine.PostProcessing;

namespace CitiesSkylinesNGVisuals
{
    /**
     * Enables and configures various anti aliasing post processing effects
     * by adding the required shaders and applying the config.
     */
    class AntiAliasingPanel : IEffectMenu
    {

        private AntialiasingModel m_model;

        // Shaders loaded from the asset bundle

        public AntiAliasingPanel (AntialiasingModel model)
        {
            m_model = model;
        }


        public void disable ()
        {

        }

        public void onGUI (float x, float y)
        {
            if (GUI.Button (new Rect (x, y, 75, 20), "Default")) {
                m_model.Reset ();
            }

            y += 25;

            int method = GUIUtils.drawIntSliderWithLabel (x, y, 0, 2, "Mode", getMethodLabel (getMethod ()), getMethod ());
            if (method == 0) {
                m_model.enabled = false;
                return;
            }

            y += 25;

            if (method == 1) {
                m_model.enabled = true;
                var settings = m_model.settings;
                settings.method = AntialiasingModel.Method.Fxaa;
                AntialiasingModel.FxaaPreset preset =
                                     (AntialiasingModel.FxaaPreset)GUIUtils.drawIntSliderWithLabel (x, y, 0, 4, "Quality", m_model.settings.fxaaSettings.preset.ToString (),
                                                                                                    (int)m_model.settings.fxaaSettings.preset);

                settings.fxaaSettings.preset = preset;
                m_model.settings = settings;
                return;
            }

            if (method == 2) {
                m_model.enabled = true;
                var settings = m_model.settings;
                settings.method = AntialiasingModel.Method.Taa;

                settings.taaSettings.jitterSpread = GUIUtils.drawSliderWithLabel (x, y, 0.1f, 1f, "Jitter Speed", settings.taaSettings.jitterSpread);
                y += 25;
                settings.taaSettings.sharpen = GUIUtils.drawSliderWithLabel (x, y, 0f, 3f, "Sharpening", settings.taaSettings.sharpen);
                y += 25;
                settings.taaSettings.stationaryBlending = GUIUtils.drawSliderWithLabel (x, y, 0f, 0.99f, "Stationary Blending", settings.taaSettings.stationaryBlending);
                y += 25;
                settings.taaSettings.motionBlending = GUIUtils.drawSliderWithLabel (x, y, 0f, 0.99f, "Motion Blending", settings.taaSettings.motionBlending);

                m_model.settings = settings;
            }
        }



        private int getMethod ()
        {
            if (!m_model.enabled)
                return 0;
            if (m_model.settings.method == AntialiasingModel.Method.Fxaa)
                return 1;
            return 2;
        }

        private string getMethodLabel (int method)
        {
            switch (method) {
            case 0: return "Disabled";
            case 1: return "FXAA";
            case 2: return "TAA";
            }
            return "";
        }
    }
}
