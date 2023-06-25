using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Matrix3x3
    {
        public List<Vector3> rows = new List<Vector3>()
        {
            Vector3.Zero,
            Vector3.Zero,
            Vector3.Zero
        };

        public Matrix3x3(List<Vector3> rows)
        {
            if (rows.Count > 3 || rows.Count <= 0) throw new ArgumentException("Too less or too many rows.");
            this.rows = rows;
        }
        
        private bool ValidateIndex(int r, int c)
        {
            return (r >= 0 && r < 3 && c >= 0 && c < 3);
        }

        public float GetValue(int r, int c)
        {
            if (ValidateIndex(r, c))
            {
                return rows[r][c];
            }
            else throw new ArgumentOutOfRangeException("That's a non-existing index");
        }
    }
}
