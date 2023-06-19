using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Engine.Core
{
    public class Layer
    {
        public readonly static List<Layer> All = new List<Layer>()
        {
            new Layer("Default", 0)
        };

        public Layer this[int id]
        {
            get { return All[id]; }
            set 
            {
                ChangeLayer(id, value.Name, value.index);
            }
        }

        private string name;
        private int id;
        private int index;

        /// <summary>
        /// The layer's unique name
        /// </summary>
        public string Name { get => name; }
        /// <summary>
        /// The layer's unique id for the layer listing
        /// </summary>
        public int ID { get => id; }
        /// <summary>
        /// The layer's unique index indicating its position (that's neither its identification nor its position in the list).
        /// </summary>
        public int Index { get => index; }

        public Layer(string name, int index)
        {
            Instantiate(name, index, null);
        }

        public Layer(string name, int id, int index)
        {
            Instantiate(name, index, id);
        }

        private static void Reidenticate()
        {
            for (int i = 0; i < All.Count; i++)
            {
                if (All[i].id != i) All[i].id = i;
            }
        }

        private static bool Validate(string name, int index, int? Id, out Exception e)
        {
            e = null;
            if (name == null) e = new ArgumentNullException("NULL is not a name.");
            foreach (Layer l in All)
            {
                if (l.Name == name) e = new ArgumentException("The layer's name must be unique.");
                if (Id != null && l.id == Id) e = new ArgumentException("The layer's ID must be unique.");
                if (l.index == index) e = new ArgumentException("The layer's ID must be unique.");
            }
            return e == null;
        }

        private static void Instantiate(string name, int index, int? Id)
        {
            if (Validate(name, index, Id, out Exception exception))
            {
                Layer layer = new Layer(name, index);
                if (Id == null) All.Add(layer);
                else
                {
                    All.Insert((int)Id, layer);
                    Reidenticate();
                }
            }
            else throw exception;
        }

        public static void ChangeLayer(int Id, string name, int? index)
        {
            Layer l = All[Id];

            int i;
            if (index == null) i = l.index;
            else i = (int)index;

            string n;
            if (name == null) n = l.Name;
            else n = name;

            if (Validate(name, i, null, out Exception e))
            {
                l.name = n;
                l.index = i;
            }
            else throw e;
        }
    }
}
