using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToolBox.Forms {
    public partial class FormBasic : Form {
        bool isLoading = false;
        public bool IsLoading {
            get { return isLoading; }
            set {
                isLoading = value;

                panel1.Visible = isLoading;
                this.UseWaitCursor = isLoading;

            }
        }

        public FormBasic() {
            InitializeComponent();
        }

        public void showWarning(string msg) {
            MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void showError(string msg) {
            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormBasic_Load(object sender, EventArgs e) {

        }
    }
}
