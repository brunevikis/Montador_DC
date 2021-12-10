using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolBox.Forms {
    public static class ControlExtensions {

        static PictureBox LoadingImage {
            get {
                return new PictureBox() {
                    Image = ToolBox.Properties.Resources.ajax_loader,
                    SizeMode = PictureBoxSizeMode.AutoSize
                };
            }
        }

        static PictureBox ErrorImage {
            get {
                return new PictureBox() {
                    Image = ToolBox.Properties.Resources.error,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 16,
                    Height = 16
                };
            }
        }

        public static void EnterLoadingState(this Control control) {
            var image = LoadingImage;
            image.Name = "loadingImage_" + control.Name;
            image.Location = control.Location;
            control.Parent.Controls.Add(
                image);
            image.BringToFront();
            control.Enabled = false;
            control.Parent.Refresh();
        }
        public static void ExitLoadingState(this Control control) {
            var image = control.Parent.Controls["loadingImage_" + control.Name];
            control.Parent.Controls.Remove(image);
            image.Dispose();
            image = null;
            control.Enabled = true;
        }

        public static void SetErrorState(this Control control, Exception ex) {
            var image = ErrorImage;
            image.Name = "errorImage_" + control.Name;
            image.Location = control.Location;
            image.Cursor = Cursors.Help;

            image.Click += (object sender, EventArgs e) => Messages.Error(control.FindForm(), ex.Message, ex, true);

            control.Parent.Controls.Add(
                image);
            image.BringToFront();
            control.Enabled = false;
            control.Parent.Refresh();

        }

        
    }
}
