using System;
using UnityEngine.PostProcessing;

namespace CitiesSkylinesNGVisuals
{

    [Serializable]
    public class Settings
    {
        public float menuPositionX = 10.0f;
        public float menuPositionY = 100.0f;
        public int toggleUIKey = (int)UnityEngine.KeyCode.F9;

        public AmbientOcclusionModel aoModel = new AmbientOcclusionModel ();
        public AntialiasingModel aaModel = new AntialiasingModel ();
        public BloomModel bloomModel = new BloomModel ();
        public GrainModel grainModel = new GrainModel ();
        //public DepthOfFieldModel dofModel = new DepthOfFieldModel ();
        public SunShaftsSettings sunshaftsSettings = new SunShaftsSettings ();


        /* public Settings ()
         {
             DepthOfFieldModel.Settings dof = dofModel.settings;
             dof.aperture = 11.5f;
             dof.useCameraFov = true;
             dofModel.settings = dof;
         }*/
    }

}
