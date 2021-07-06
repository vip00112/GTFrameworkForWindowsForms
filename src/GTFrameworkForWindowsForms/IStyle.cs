using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFrameworkForWindowsForms
{
    public interface IStyle
    {
        StyleTheme StyleTheme { get; set; }

        StyleColor StyleColor { get; set; }

        Color StyleThemeRGB { get; set; }

        Color StyleColorRGB { get; set; }

        void StyleInvalidate();
    }
}
