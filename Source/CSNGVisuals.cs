using System;
using System.IO;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityStandardAssets.ImageEffects;
using ColossalFramework.UI;
using System.Xml.Serialization;


namespace CitiesSkylinesNGVisuals
{

    public class CSNGVisuals : MonoBehaviour
    {

        private SettingsWindow m_window;
        private Settings m_settings = new Settings ();

        public void Start ()
        {

            PostProcessingBehaviour postProcessing = Camera.main.gameObject.GetComponent<PostProcessingBehaviour> ();

            if (postProcessing == null) {
                postProcessing = Camera.main.gameObject.AddComponent<PostProcessingBehaviour> ();
                if (!postProcessing.profile) {
                    postProcessing.profile = new PostProcessingProfile ();
                }
            }

            SunShafts sunshafts = Camera.main.GetComponent<SunShafts> ();
            if (sunshafts == null) {
                sunshafts = Camera.main.gameObject.AddComponent<SunShafts> ();
                sunshafts.sunTransform = new GameObject ().transform;
            }


            postProcessing.enabled = true;
            LoadConfig ();
            applySettings ();

            m_window = Camera.main.gameObject.GetComponent<SettingsWindow> ();
            if (m_window == null) {
                m_window = Camera.main.gameObject.AddComponent<SettingsWindow> ();
                Utils.log ("Window created");
            }


            /* var dof = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
             if (dof != null) {
                 Utils.log ("Disabling Vanilla DepthOfField");
                 dof.enabled = false;
             }*/

            var grain = Camera.main.GetComponent<ColossalFramework.FilmGrainEffect> ();
            if (grain != null) {
                Utils.log ("Disabling Vanilla Grain");
                grain.enabled = false;
            }
            var bloom = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ();
            if (bloom != null) {
                bloom.enabled = true;
            }

        }

        private string ConfigurtionFilePath ()
        {
            return CSNGVisualsMod.ModPath () + "/cities_skylines_ng_visuals.xml";
        }

        public void LoadConfig ()
        {
            Utils.log ("Loading Config");


            TextReader reader = null;
            try {
                reader = new StreamReader (ConfigurtionFilePath ());
                XmlSerializer xmlSerializer = new XmlSerializer (typeof (Settings));
                m_settings = (Settings)xmlSerializer.Deserialize (reader);
            } catch (Exception) {
                Utils.log ("Failed to load config " + ConfigurtionFilePath ());
                m_settings = new Settings ();
            } finally {
                if (reader != null) {
                    reader.Close ();
                }
            }
        }

        public void SaveConfig ()
        {
            Utils.log ("Saving Config " + ConfigurtionFilePath ());

            TextWriter writer = null;

            try {
                writer = new StreamWriter (ConfigurtionFilePath ());
                XmlSerializer xmlSerializer = new XmlSerializer (typeof (Settings));
                xmlSerializer.Serialize (writer, m_settings);
            } catch (Exception) {
                Utils.log ("Failed to save config " + ConfigurtionFilePath ());
            } finally {
                if (writer != null) {
                    writer.Close ();
                }
            }

        }

        public Settings getSettings ()
        {
            return m_settings;
        }

        public void applySettings ()
        {
            try {
                PostProcessingBehaviour postProcessing = Camera.main.gameObject.GetComponent<PostProcessingBehaviour> ();

                postProcessing.profile.ambientOcclusion.settings = m_settings.aoModel.settings;
                postProcessing.profile.ambientOcclusion.enabled = m_settings.aoModel.enabled;


                postProcessing.profile.antialiasing.settings = m_settings.aaModel.settings;
                postProcessing.profile.antialiasing.enabled = m_settings.aaModel.enabled;


                postProcessing.profile.bloom.settings = m_settings.bloomModel.settings;
                postProcessing.profile.bloom.enabled = m_settings.bloomModel.enabled;


                SunShafts sunshafts = Camera.main.gameObject.GetComponent<SunShafts> ();
                sunshafts.enabled = m_settings.sunshaftsSettings.enabled;
                sunshafts.sunShaftIntensity = m_settings.sunshaftsSettings.sunShaftIntensity;
                sunshafts.height = m_settings.sunshaftsSettings.height;
                sunshafts.sunShaftBlurRadius = m_settings.sunshaftsSettings.sunShaftBlurRadius;
                sunshafts.sunColor = m_settings.sunshaftsSettings.sunColor;
                sunshafts.sunThreshold = m_settings.sunshaftsSettings.sunThreshold;
                sunshafts.maxRadius = m_settings.sunshaftsSettings.maxRadius;



                postProcessing.profile.grain.settings = m_settings.grainModel.settings;
                postProcessing.profile.grain.enabled = m_settings.grainModel.enabled;


            } catch (Exception) {
                Utils.log ("Unable to enable post processing effects");
            }
        }


        public void Destroy ()
        {
            SaveConfig ();
            /*var dof = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
            if (dof != null) {
                dof.enabled = true;
            }*/
            var grain = Camera.main.GetComponent<ColossalFramework.FilmGrainEffect> ();
            if (grain != null) {
                grain.enabled = true;
            }

            var bloom = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ();
            if (bloom != null) {
                bloom.enabled = true;
            }

            PostProcessingBehaviour postProcessing = Camera.main.gameObject.GetComponent<PostProcessingBehaviour> ();
            if (postProcessing != null)
                UnityEngine.Object.Destroy (postProcessing);

            SunShafts sunshafts = Camera.main.gameObject.GetComponent<SunShafts> ();
            if (sunshafts != null) {
                UnityEngine.Object.Destroy (sunshafts);
            }

        }
    }
}

