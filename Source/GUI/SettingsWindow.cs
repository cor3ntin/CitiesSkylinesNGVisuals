using System;

using UnityEngine;

//using UnityStandardAssets.CinematicEffects;
using System.Collections.Generic;

namespace CitiesSkylinesNGVisuals
{
    /**
     * The menu that is currently active.
     */
    enum MenuType
    {
        Bloom = 0,
        AntiAliasing,
        AmbientOcclusion,
        Global,
        SunShafts,
    }

    class SettingsWindow : MonoBehaviour
    {

        private MenuType m_activeMenu = MenuType.AmbientOcclusion;
        private Dictionary<MenuType, IEffectMenu> m_menus = new Dictionary<MenuType, IEffectMenu> ();

        private bool m_active = false;
        private bool m_toggle = false;
        private bool m_dragging = false;



        private float m_lastMouseX;
        private float m_lastMouseY;

        CSNGVisuals m_csngv;
        Settings m_settings;

        private string m_toogleKeyString;

        public void Start ()
        {
            m_csngv = Camera.main.gameObject.GetComponent<CSNGVisuals> ();
            m_settings = m_csngv.getSettings ();

            m_menus.Add (MenuType.AmbientOcclusion, new AmbientOcclusionPanel (m_settings.aoModel));
            m_menus.Add (MenuType.AntiAliasing, new AntiAliasingPanel (m_settings.aaModel));
            m_menus.Add (MenuType.Bloom, new BloomPanel (m_settings.bloomModel));
            m_menus.Add (MenuType.SunShafts, new SunShaftsPanel (m_settings.sunshaftsSettings));

        }

        public void OnGUI ()
        {
            if (!m_active) {
                return;
            }

            float x = m_settings.menuPositionX;
            float y = m_settings.menuPositionY;

            GUI.Box (new Rect (x, y, 400, 500), "");

            if (GUI.Button (new Rect (x, y, 380, 20), CSNGVisualsMod.ModName + CSNGVisualsMod.Version)) {
                m_dragging = true;
            }

            if (GUI.Button (new Rect (x + 380, y, 20, 20), "X")) {
                m_active = false;
            }

            x += 10;
            y += 25;
            if (GUI.Button (new Rect (x, y, 60, 20), "Global")) {
                m_activeMenu = MenuType.Global;
            }

            if (GUI.Button (new Rect (x + 60, y, 60, 20), "Bloom")) {
                m_activeMenu = MenuType.Bloom;
            }

            if (GUI.Button (new Rect (x + 120, y, 60, 20), "AA")) {
                m_activeMenu = MenuType.AntiAliasing;
            }

            if (GUI.Button (new Rect (x + 180, y, 60, 20), "SSAO")) {
                m_activeMenu = MenuType.AmbientOcclusion;
            }

            if (GUI.Button (new Rect (x + 240, y, 80, 20), "SunShafts")) {
                m_activeMenu = MenuType.SunShafts;
            }

            y += 40;

            // Handle the Global case as it is rendered here.
            if (m_activeMenu == MenuType.Global) {

                var grainEffect = m_settings.grainModel.settings;
                var intensity = grainEffect.intensity;
                if (!m_settings.grainModel.enabled) {
                    intensity = 0;
                }

                intensity = GUIUtils.drawSliderWithLabel (x, y, 0, 1f, "Grain", intensity);
                m_settings.grainModel.enabled = intensity > 0.1f;
                grainEffect.intensity = intensity;
                m_settings.grainModel.settings = grainEffect;


                y += 50;

                m_toogleKeyString = ((KeyCode)(m_settings.toggleUIKey)).ToString ();

                GUI.Label (new Rect (x, y, 200, 20), "Toggle Key: ");
                m_toogleKeyString = GUI.TextArea (new Rect (x + 150, y, 150, 20), m_toogleKeyString);
                try {
                    m_settings.toggleUIKey = (int)Enum.Parse (typeof (KeyCode), m_toogleKeyString);
                } catch (Exception) {
                    // Silently ignore this exception, because it just means that it cannot be converted to a keycode.
                    m_settings.toggleUIKey = (int)KeyCode.F9;
                    //m_toogleKeyString = ((KeyCode)(m_settings.toggleUIKey)).ToString ();
                }


            } else {
                m_menus [m_activeMenu].onGUI (x, y);
            }

            try {

                m_csngv.applySettings ();
            } catch (Exception ex) {
                Utils.log (ex.Message);
                m_activeMenu = MenuType.Global;
            }
        }

        public void Update ()
        {

            if (Input.GetKeyDown ((KeyCode)(m_settings.toggleUIKey))) {
                if (!m_toggle) {
                    m_active = !m_active;
                }
                m_toggle = true;

                if (!m_active) {
                    m_csngv.SaveConfig ();
                }
            }

            if (Input.GetKeyUp ((KeyCode)(m_settings.toggleUIKey))) {
                m_toggle = false;
            }

            var resizeRect = new Rect (m_settings.menuPositionX, m_settings.menuPositionY, 320, 20);
            var mouse = Input.mousePosition;
            mouse.y = Screen.height - mouse.y;

            if (m_dragging) {
                if (!Input.GetMouseButton (0)) {
                    m_dragging = false;
                } else {
                    m_settings.menuPositionX = mouse.x - m_lastMouseX;
                    m_settings.menuPositionY = mouse.y - m_lastMouseY;
                }
            } else {
                if (Input.GetMouseButton (0) && resizeRect.Contains (mouse)) {
                    m_dragging = true;
                    m_lastMouseX = mouse.x - m_settings.menuPositionX;
                    m_lastMouseY = mouse.y - m_settings.menuPositionY;
                }
            }
        }

        public void OnDestroy ()
        {
            m_csngv.SaveConfig ();
        }

        public void OnMouseUp ()
        {
            m_dragging = false;
        }
    }
}
