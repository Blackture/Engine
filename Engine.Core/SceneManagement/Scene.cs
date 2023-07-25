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
        public int SceneId { get { return sceneId; } }

        public Scene(int sceneId)
        {
            this.sceneId = sceneId;
        }

        /// <summary>
        /// Gets the scene's global coordinate system.
        /// TODO: GCS2 in case it's 2D, and the not implemented GCS2 class is implemented.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
    }
}
