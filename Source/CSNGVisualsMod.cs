using System;
using ICities;
using ColossalFramework.Plugins;
using System.Reflection;
using UnityEngine;



namespace CitiesSkylinesNGVisuals
{

    public class CSNGVisualsMod : IUserMod
    {

        public static string ModName = "PostProcessFX";
        public static string Version = "4.0";

        public string Name {
            get { return ModName; }
        }

        public string Description {
            get { return "Collection of Visuals Effects and Rendering Options"; }
        }

        public static string ModPath ()
        {
            return PluginManager.instance.FindPluginInfo (Assembly.GetAssembly (typeof (CSNGVisualsMod))).modPath;
        }


        public static AssetBundle assetsBundle = null;



        public void OnEnabled ()
        {
            string modPath = ModPath ();
            string bundleName = null;
            switch (Application.platform) {
            case RuntimePlatform.WindowsPlayer:
                bundleName = "ngv_windows.bundle";
                break;
            case RuntimePlatform.LinuxPlayer:
                bundleName = "ngv_linux.bundle";
                break;
            case RuntimePlatform.OSXPlayer:
                bundleName = "ngv_osx.bundle";
                break;
            }
            if (bundleName == null) {
                //throw new BrokenAssetException("Unsupported OS");
            }

            string uri = modPath.Replace ("\\", "/") + "/Resources/" + bundleName;


            assetsBundle = AssetBundle.LoadFromFile (uri);

            if (assetsBundle == null) {
                throw new BrokenAssetException ("Asset bundle [" + uri + "] could not be loaded");
            }


        }

        public void OnDisabled ()
        {
            if (assetsBundle) {
                assetsBundle.Unload (true);
                AssetBundle.Destroy (assetsBundle);
                Utils.log ("Disabled");
            }
        }
    }
}