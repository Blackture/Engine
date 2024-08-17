using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Engine.WPF
{
    public class Theme
    {
        private Color backgroundColor;
        private Color foregroundColor;
        private Color textColor;
        private Color primary;
        private Color secondary;
        private Color success;
        private Color info;
        private Color warning;
        private Color error;

        public Color BackgroundColor => backgroundColor;
        public Color ForegroundColor => foregroundColor;
        public Color TextColor => textColor;
        public Color Primary => primary;
        public Color Secondary => secondary;
        public Color Success => success;
        public Color Info => info;
        public Color Warning => warning;
        public Color Error => error;

        /// <summary>
        /// Initialized the Theme with
        /// </summary>
        /// <param name="colors"></param>
        public Theme(string backgroundColor, string foregroundColor, string textColor, string primary, string secondary, string success, string info, string warning, string error) 
        {
            //this.backgroundColor = backgroundColor;
            //this.foregroundColor = foregroundColor;
            //this.textColor = textColor;
            //this.primary = primary;
            //this.secondary = secondary;
            //this.success = success;
            //this.info = info;
            //this.warning = warning;
            //this.error = error;
        }


    }
}
