using MDSYS.LocalMessageNotifier.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDSYS.LocalMessageNotifier.UI.Controls
{
    class UsersCheckedListBox : CheckedListBox
    {

        public Color ActiveColor { get; set; } = Color.Green;
        public Color DisConnectedColor { get; set; } = Color.Red;
        public Color DefaultColor { get; set; } = Color.Black;


        public UsersCheckedListBox()
        {

        }


        public UsersCheckedListBox(Color activeColor, Color disConnectedColor, Color defaultColor)
        {
            ActiveColor = activeColor;
            DisConnectedColor = disConnectedColor;
            DefaultColor = defaultColor;
        }


        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.DesignMode)
            {
                base.OnDrawItem(e);
            }
            else
            {
                bool isWindowsUser = this.Items[e.Index] is WindowsUser;
                var user = this.Items[e.Index] as WindowsUser;
                if (!isWindowsUser || user == null)
                {
                    base.OnDrawItem(e);
                    return;
                }
                Color textColor = user.Status switch
                {
                    "Active" => ActiveColor,
                    "Disconnected" => DisConnectedColor,
                    _ => DefaultColor,
                };

                //DrawItemEventArgs e2 = new DrawItemEventArgs
                //   (e.Graphics,
                //    e.Font,
                //    new Rectangle(e.Bounds.Location, e.Bounds.Size),
                //    e.Index,
                //    (e.State & DrawItemState.Focus) == DrawItemState.Focus ? DrawItemState.Default : DrawItemState.None, /* Remove 'selected' state so that the base.OnDrawItem doesn't obliterate the work we are doing here. */
                //    textColor,
                //    this.BackColor);
               
                
                DrawItemEventArgs e2 = new DrawItemEventArgs
                  (e.Graphics,
                   e.Font,
                   new Rectangle(e.Bounds.Location, e.Bounds.Size),
                   e.Index,
                   e.State, /* Remove 'selected' state so that the base.OnDrawItem doesn't obliterate the work we are doing here. */
                   textColor,
                   this.BackColor);

                base.OnDrawItem(e2);
                //if (!Enabled)
                //{
                //    using (Brush textBrush = new SolidBrush(textColor))
                //    {
                //        e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, textBrush, e.Bounds);
                //    }
                //}
               
            }
        }
       
    }
   
}

