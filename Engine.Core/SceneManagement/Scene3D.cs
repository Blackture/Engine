using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.SceneManagement
{
    public class Scene3D : Scene
    {
        private GCS3 globalCoordinateSystem;
        public GCS3 GlobalCoordinateSystem { get { return globalCoordinateSystem; } }

        public Scene3D(int sceneId) : base(sceneId, SceneType.Scene3D) 
        {
            globalCoordinateSystem = new GCS3();
        }
    }
}
