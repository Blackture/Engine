using Engine.Core.Components;
using Engine.Core.Physics.CollisionDetection;
using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Engine.Core
{
    public class LCS3Element
    {
        private readonly int depth;
        private readonly LCS3 lcs;
        private readonly LCS3 parent;

        public int Depth => depth;
        public LCS3 Lcs => lcs;
        public LCS3 Parent => parent;
        public Guid GroupID;

        public LCS3Element(int depth, LCS3 lcs, LCS3 parent, Guid groupID)
        {
            this.depth = depth;
            this.lcs = lcs;
            this.parent = parent;
            GroupID = groupID;
        }

        public static implicit operator LCS3(LCS3Element element)
        {
            return element.Lcs;
        }
    }
}
