using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinBreakBricks
{
    static public class Vector
    {
        static public Vector2 NormalizeVector(Vector2 vector)
        {
            float length = vector.Length();
            if (length > 0)
            {
                return vector / length;
            }
            else
            {
                return Vector2.Zero; // Return a zero vector if the original vector is already a zero vector
            }
        }
    }
}
