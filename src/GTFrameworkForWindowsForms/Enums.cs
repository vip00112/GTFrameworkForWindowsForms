using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFrameworkForWindowsForms
{
    public enum StyleTheme
    {
        Dark, Light, User
    }

    public enum StyleColor
    {
        White, Black, Red, Green, Blue, User
    }

    public enum MoveResizeAction
    {
        None,

        Move,

        ResizeLeft,
        ResizeTop,
        ResizeRight,
        ResizeBottom,

        ResizeTopLeft,
        ResizeTopRight,
        ResizeBottomRight,
        ResizeBottomLeft
    }
}
