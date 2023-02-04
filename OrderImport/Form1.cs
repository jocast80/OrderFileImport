using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderImport
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog oDlg = new System.Windows.Forms.OpenFileDialog();
            if (System.Windows.Forms.DialogResult.OK == oDlg.ShowDialog())
            {
                txtFilePath.Text = oDlg.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtFilePath.TextLength > 0)
                processFile(txtFilePath.Text);
        }

        private void processFile(string FileLocation)
        {
            Orders importOrders = new Orders();
            var result = importOrders.ParseFile(FileLocation);
            lstSummaryBox.Items.AddRange(result.ToArray());
        }
    }
}
