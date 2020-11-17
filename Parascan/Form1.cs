using Parascan.Biz;
using Parascan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parascan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Scanner.INIT();
                Scanner.SCAN(@"C:\Users\geo\source\repos\Paradigm20", "testProject");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("scan complete", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //MessageBox.Show(Scanner.GetFileId(3,
                //    @"C:\Users\geo\source\repos\Paradigm20\.git\logs\refs\remotes\origin\master").ToString(),
                //    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                 
           

        }
    }
}
