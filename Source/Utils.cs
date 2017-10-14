using System;
using UnityEngine;

namespace CitiesSkylinesNGVisuals {

public class Utils {

    public static void log(string str) {
        UnityEngine.Debug.Log(CSNGVisualsMod.ModName + " : " + str);
    }

    public static Shader LoadShader(string name) {
        log("Loading " + name);
        return CSNGVisualsMod.assetsBundle.LoadAsset<Shader>(name);
    }
}

}