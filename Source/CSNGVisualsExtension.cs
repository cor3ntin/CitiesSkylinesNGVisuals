using System;
using ICities;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace CitiesSkylinesNGVisuals {

public class CSNGVisualsExtension : ILoadingExtension {

    public void OnLevelLoaded(LoadMode mode) {
        if (Camera.main?.gameObject == null) {
            Utils.log("No camera");
            return;
        }
        Utils.log("Adding CSNGVisuals");
        Camera.main.gameObject.AddComponent<CSNGVisuals>();
              
    }

    public void OnCreated(ILoading i) {
    }

    public void OnLevelUnloading() {
        if (Camera.main?.gameObject != null) {
            Component csngv = Camera.main.gameObject.GetComponent("CSNGVisuals");
            if (csngv != null) {
                Component.Destroy(csngv);
                Utils.log("CSNGVisuals destroyed");
            }
            
        }
    }

    public void OnReleased() {
    }


}
}

