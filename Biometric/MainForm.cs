using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;


namespace Biometric
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadSizeResolution();
        }
        #region UI Style
        private void LoadSizeResolution()
        {
            if (Properties.Settings.Default.Size.Width == 0) Properties.Settings.Default.Upgrade();
            if (Properties.Settings.Default.Size.Width == 0 || Properties.Settings.Default.Size.Height == 0)
            {
                this.Size = new Size(1374, 749);
                this.WindowState = FormWindowState.Normal;
                this.Location = new Point(100, 70);
            }
            else
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                this.WindowState = Properties.Settings.Default.State;
                this.Location = Properties.Settings.Default.Location;
                this.Size = Properties.Settings.Default.Size;
                ToogleDarkMode(Properties.Settings.Default.IsDark);
            }
        }
        private void SaveSizeRosolution()
        {
            Properties.Settings.Default.State = this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Location = this.Location;
                Properties.Settings.Default.Size = this.Size;
            }
            else
            {
                Properties.Settings.Default.Location = this.RestoreBounds.Location;
                Properties.Settings.Default.Size = this.RestoreBounds.Size;
            }
            Properties.Settings.Default.IsDark = Configuration.IsDark;
            Properties.Settings.Default.Save();
        }
        private void ToogleDarkMode(Boolean isDark)
        {
            Configuration.IsDark = isDark;
            Configuration.UseImmersiveDarkMode(Handle, isDark);
            this.BackColor = Color.White;
            if (isDark) this.BackColor = Color.FromArgb(31, 31, 31);
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.WindowState = FormWindowState.Normal;
            }
            this.Refresh();
        }
        private void ToogleDarkModeButton_Click(object sender, EventArgs e)
        {
            ToogleDarkMode(!Configuration.IsDark);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSizeRosolution();
        }
        #endregion
    }
}
