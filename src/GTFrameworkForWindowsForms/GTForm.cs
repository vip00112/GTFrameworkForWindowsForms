using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTFrameworkForWindowsForms
{
    public class GTForm : Form, IStyle
    {
        private StyleTheme _styleTheme;
        private StyleColor _styleColor;
        private Color _styleThemeRGB;
        private Color _styleColorRGB;
        private Point _startLoc;
        private MoveResizeAction _moveResizeAction;

        #region Constructor
        public GTForm()
        {
            var style = ControlStyles.AllPaintingInWmPaint | 
                        ControlStyles.OptimizedDoubleBuffer | 
                        ControlStyles.ResizeRedraw | 
                        ControlStyles.UserPaint;
            SetStyle(style, true);

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
        }
        #endregion

        #region Properties
        public StyleTheme StyleTheme
        {
            get { return _styleTheme; }
            set
            {
                _styleTheme = value;
                StyleInvalidate();
            }
        }

        public StyleColor StyleColor
        {
            get { return _styleColor; }
            set
            {
                _styleColor = value;
                StyleInvalidate();
            }
        }

        public Color StyleThemeRGB
        {
            get
            {
                if (StyleTheme != StyleTheme.User)
                {
                    return ColorTable.GetBackColorNormal(StyleTheme);
                }

                return _styleThemeRGB;
            }
            set
            {
                if (StyleTheme != StyleTheme.User) return;

                _styleThemeRGB = value;
                StyleInvalidate();
            }
        }

        public Color StyleColorRGB
        {
            get
            {
                if (StyleColor != StyleColor.User)
                {
                    return ColorTable.GetStyleColor(StyleColor);
                }

                return _styleColorRGB;
            }
            set
            {
                if (StyleColor != StyleColor.User) return;

                _styleColorRGB = value;
                StyleInvalidate();
            }
        }

        public Rectangle ResizeRec
        {
            get
            {
                return new Rectangle(Width - 12, Height - 12, 12, 12);
            }
        }

        new public FormBorderStyle FormBorderStyle
        {
            get { return FormBorderStyle.None; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
        }
        #endregion

        #region Protected Method
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            StyleInvalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            MinimumSize = new Size(75, 75);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var themeBrush = new SolidBrush(StyleThemeRGB))
            using (var colorBrush = new SolidBrush(StyleColorRGB))
            using (var colorPen = new Pen(StyleColorRGB, 1))
            {
                e.Graphics.FillRectangle(themeBrush, ClientRectangle);
                if (StyleTheme == StyleTheme.Dark)
                {
                    e.Graphics.FillRectangle(colorBrush, 1, 17, Width - 2, 2);
                }
                else
                {
                    e.Graphics.FillRectangle(colorBrush, 0, 17, Width, 2);
                }

                var format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                // Buttons
                using (var font = new Font("Webdings", 9F))
                {
                    var closeRec = new Rectangle(Width - 16, 1, 15, 15);
                    //e.Graphics.FillRectangle(colorBrush, closeRec);
                    //e.Graphics.DrawString("r", font, themeBrush, closeRec, format);
                    e.Graphics.DrawString("r", font, colorBrush, closeRec, format);

                    var maximunRec = new Rectangle(Width - 16 - 16, 1, 15, 15);
                    //e.Graphics.FillRectangle(colorBrush, maximunRec);
                    if (WindowState == FormWindowState.Maximized)
                    {
                        //e.Graphics.DrawString("2", font, themeBrush, maximunRec, format);
                        e.Graphics.DrawString("2", font, colorBrush, maximunRec, format);
                    }
                    else
                    {
                        //e.Graphics.DrawString("1", font, themeBrush, maximunRec, format);
                        e.Graphics.DrawString("1", font, colorBrush, maximunRec, format);
                    }

                    var minimumRec = new Rectangle(Width - 16 - 16 - 16, 1, 15, 15);
                    //e.Graphics.FillRectangle(colorBrush, minimumRec);
                    //e.Graphics.DrawString("0", font, themeBrush, minimumRec, format);
                    e.Graphics.DrawString("0", font, colorBrush, minimumRec, format);
                }

                // Title
                var stringRec = new Rectangle(1, 1, Width - 16 - 16 - 16 - 2, 15);
                //e.Graphics.FillRectangle(colorBrush, stringRec);
                //e.Graphics.DrawString(Text, Font, themeBrush, stringRec, format);
                e.Graphics.DrawString(Text, Font, colorBrush, stringRec, format);

                // Resize
                for (int i = 1; i <= 4; i++)
                {
                    int value = i * 3;

                    if (StyleTheme == StyleTheme.Dark)
                    {
                        e.Graphics.DrawLine(colorPen, Width - value - 2, Height - 2, Width - 2, Height - value - 2);
                    }
                    else
                    {
                        e.Graphics.DrawLine(colorPen, Width - value, Height, Width, Height - value);
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _startLoc = e.Location;

            if (ResizeRec.Contains(e.Location)) _moveResizeAction = MoveResizeAction.ResizeBottomRight;
            else _moveResizeAction = MoveResizeAction.Move;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_moveResizeAction == MoveResizeAction.None)
            {
                if (ResizeRec.Contains(e.Location)) Cursor = Cursors.SizeNWSE;
                else Cursor = Cursors.Default;
                return;
            }

            Bounds = ControlMoveResizeUtil.CalcBounds(_moveResizeAction, this, _startLoc, e.Location);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _moveResizeAction = MoveResizeAction.None;
        }
        #endregion

        #region Public Method
        public void StyleInvalidate()
        {
            bool isDarkMode = StyleTheme == StyleTheme.Dark;
            WinAPI.SetTitleBarTheme(Handle, isDarkMode);

            if (FormBorderStyle == FormBorderStyle.None)
            {
                WinAPI.SetFormShadow(Handle);
            }

            Invalidate();
        }
        #endregion
    }
}
