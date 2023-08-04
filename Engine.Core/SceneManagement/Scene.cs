using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;

namespace Engine.Core.SceneManagement
{
    public class Scene
    {
        public string Name;
        private int sceneId;
        private SceneType type;

        private Scene3D reference3D;

        public int SceneId { get { return sceneId; } }
        public SceneType Type { get { return type; } }

        public Scene(int sceneId, SceneType type)
        {
            this.sceneId = sceneId;
            this.type = type;
        }

        /// <summary>
        /// Gets the scene's global coordinate system.
        /// TODO: Overload method for GCS2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns wether there is GCS3 or not</returns>
        public bool GetGlobalCoordinateSystem(out GCS3 gcs)
        {
            gcs = null;
            bool res = false;
            if (reference3D != null && Type != SceneType.Scene2D)
            {
                res = true;
                gcs = reference3D.GlobalCoordinateSystem;
            } 
            return res;
        }
    }
}
