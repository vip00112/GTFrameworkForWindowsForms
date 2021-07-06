using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFrameworkForWindowsForms
{
    public static class ColorTable
    {
        public static Color GetBackColorNormal(StyleTheme theme)
        {
            switch (theme)
            {
                case StyleTheme.Dark:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                case StyleTheme.Light:
                    return Color.FromArgb(0xfa, 0xfa, 0xfa);
                default:
                    return GetBackColorNormal(StyleTheme.Dark);
            }
        }

        public static Color GetForeColorNormal(StyleTheme theme)
        {
            switch (theme)
            {
                case StyleTheme.Dark:
                    return Color.FromArgb(0xfa, 0xfa, 0xfa);
                case StyleTheme.Light:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                default:
                    return GetBackColorNormal(StyleTheme.Dark);
            }
        }

        public static Color GetStyleColor(StyleColor color)
        {
            switch (color)
            {
                case StyleColor.White:
                    return Color.FromArgb(0xfa, 0xfa, 0xfa);
                case StyleColor.Black:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                case StyleColor.Red:
                    return Color.FromArgb(255, 0, 0);
                case StyleColor.Green:
                    return Color.FromArgb(0, 255, 0);
                case StyleColor.Blue:
                    return Color.FromArgb(0, 0, 255);
                default:
                    return GetStyleColor(StyleColor.White);
            }
        }
    }
}
