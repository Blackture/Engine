using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MediaColor = System.Windows.Media.Color;

namespace Engine.WPF
{
    public class Color
    {
        private MediaColor color;

        private void SetColor(string hexValue, float alpha)
        {
            if (!IsValidColorString(hexValue)) return;
            color = (MediaColor)ColorConverter.ConvertFromString(hexValue);
            color.ScA = alpha;
        }

        private bool IsValidColorString(string hexValue)
        {
            try
            {
                var color = (MediaColor)ColorConverter.ConvertFromString(hexValue);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
