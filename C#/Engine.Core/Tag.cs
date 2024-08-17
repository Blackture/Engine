using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Tag
    {
        public static List<Tag> Tags;

        private string name;
        private int index;
        private bool locked;

        public string Name
        {
            get { return name; }
            set { SetName(value); }
        }

        static Tag()
        {
            Tags = new List<Tag>();
            new Tag("Camera", true);
            new Tag("LightSource", true);
        }

        public Tag(string name)
        {
            this.name = name;
            Tags.Add(this);
            index = Tags.Count - 1;
            locked = false;
        }

        private Tag(string name, bool locked)
        {
            this.name = name;
            Tags.Add(this);
            index = Tags.Count - 1;
            this.locked = locked;
        }

        public void SetName(string name)
        { 
            this.name = name;
            Tags[index] = name;
        }
        public static void SetName(string name, int index)
        {
            if (index >= 0 && index < Tags.Count)
            {
                Tags[index] = name;
            }
        }
        public static void RemoveTag(Tag tag)
        {
            if (!tag.locked)
            {
                Tags.Remove(tag);
            }
        }

        public static implicit operator int(Tag t) 
        {
            return Tags.IndexOf(t.name);
        }
        public static implicit operator string(Tag t)
        {
            return t.name;
        }
        public static implicit operator Tag(string s)
        {
            return Tags.Find(t => t.Name == s);
        }
        public static implicit operator Tag(int i)
        {
            return Tags[i];
        }
    }
}
