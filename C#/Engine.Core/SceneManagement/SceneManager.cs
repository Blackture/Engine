using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.SceneManagement
{
    public class SceneManager
    {
        public static SceneManager Instance;
        private List<Scene> scenes = new List<Scene>();
        public Scene ActiveScene;

        public SceneManager() 
        {
            Instance = Instance ?? this;
            ActiveScene = null;
        }

        public void AddScene(Scene scene)
        {
            if (ActiveScene == null) ActiveScene = scene;
            scenes.Add(scene);
        }
    }
}
