using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using HPlus.Forms;

namespace HPlus
{
    public partial class MainInterface : Form
    {
        //Fields
        private IconButton currentBtn;
        private IconButton currentIcon;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public MainInterface()
        {

            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelSideMenuItems.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(24, 161, 251);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(192, 30, 36);
        }

        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                DisableIcon();
                //Button 
                //Alternative BackCoor 64, 62, 138
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.Overlay;
                //currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(50, 58, 78);
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void ActivateIcon(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableIcon();
                DisableButton();
                //Button
                currentIcon = (IconButton)senderBtn;
                currentIcon.BackColor = Color.FromArgb(37, 36, 81);
                currentIcon.ForeColor = color;
                currentIcon.IconColor = color;

            }
        }

        private void DisableIcon()
        {
            if (currentIcon != null)
            {
                currentIcon.BackColor = Color.FromArgb(44, 50, 68);
                currentIcon.ForeColor = Color.White;
                currentIcon.IconColor = Color.White;
                leftBorderBtn.Visible = false;
            }
        }


        //Open Child form on MainForm
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //open one form at a time
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDisplayChildForm.Controls.Add(childForm);
            panelDisplayChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //lblTitleChildForm.Text = childForm.Text;
        }


        private void Reset()
        {
            DisableButton();
            DisableIcon();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Home";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTopMost_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panelTopMenu_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }
        #region Open Child Forms
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.ChartColumn;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Dashboard";
            OpenChildForm(new Dashboard());
        }

        private void iconDashboard_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Dashboard";
            OpenChildForm(new Dashboard());
        }

        private void btnInPatients_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.PeopleCarry;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage Inpatients";
            rjDropdownMenuInPatient.Show(btnInPatients, btnInPatients.Width, 0);
            //OpenChildForm(new ManageInpatient());
        }

        private void iconInPatients_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage Inpatients";
            rjDropdownMenuInPatient.Show(btnInPatients, btnInPatients.Width, 0);
            //OpenChildForm(new ManageInpatient());
        }

        private void btnOutPatients_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.AddressBook;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage Outpatients";
            rjDropdownMenuOutPatients.Show(btnOutPatients, btnOutPatients.Width, 0);
        }

        private void iconOutPatients_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage Outpatients";
            rjDropdownMenuOutPatients.Show(btnOutPatients, btnOutPatients.Width, 0);
        }

        private void btnChannelingServices_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.CircleNodes;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage Channeling Services";
            rjDropdownMenuChannelingServices.Show(btnChannelingServices, btnChannelingServices.Width, 0);
        }

        private void iconChannelingServices_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage Channeling Services";
            rjDropdownMenuChannelingServices.Show(btnChannelingServices, btnChannelingServices.Width, 0);
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.CircleNodes;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage All Payments";
            rjDropdownMenuPayments.Show(btnPayments, btnPayments.Width, 0);
        }

        private void iconPayments_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage All Payments";
            rjDropdownMenuPayments.Show(btnPayments, btnPayments.Width, 0);
        }

        private void btnHplusMaintenance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.HouseLock;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Hospital Maintance Services";
            rjDropdownMenuHPlusMaintenance.Show(btnHplusMaintenance, btnHplusMaintenance.Width, 0);
        }

        private void iconHplusMaintenance_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Hospital Maintance Services";
            rjDropdownMenuHPlusMaintenance.Show(btnHplusMaintenance, btnHplusMaintenance.Width, 0);
        }

        private void btnMasterSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.ScrewdriverWrench;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Hospital Services Management";
            rjDropdownMenuMasterSettings.Show(btnMasterSettings, btnMasterSettings.Width, 0);
        }

        private void iconMasterSettings_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Hospital Services Management";
            rjDropdownMenuMasterSettings.Show(btnMasterSettings, btnMasterSettings.Width, 0);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.BookOpen;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "View All Services Reports";
            rjDropdownMenuReports.Show(btnReports, btnReports.Width, 0);
        }

        private void iconReports_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "View All Services Reports";
            rjDropdownMenuReports.Show(btnReports, btnReports.Width, 0);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.UserLock;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage User's Account Profile";
            rjDropdownMenuUserAccounts.Show(btnUsers, btnUsers.Width, 0);
        }

        private void iconUsers_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Manage User's Account Profile";
            rjDropdownMenuUserAccounts.Show(btnUsers, btnUsers.Width, 0);
        }

        private void btnutilities_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = IconChar.UserLock;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Hplus Utilities";
            rjDropdownMenuTools.Show(btnutilities, btnutilities.Width, 0);
        }

        private void iconUtilities_Click(object sender, EventArgs e)
        {
            ActivateIcon(sender, RGBColors.color1);
            iconCurrentChildForm.IconChar = currentIcon.IconChar;
            iconCurrentChildForm.IconColor = Color.FromArgb(192, 30, 36);
            lblTitleChildForm.Text = "Hplus Utilities";
            rjDropdownMenuTools.Show(btnutilities, btnutilities.Width, 0);
        }
        #endregion

        private void MainInterface_Load(object sender, EventArgs e)
        {
            //rjDropdownMenuInPatient.IsMainMenu = true;
            //rjDropdownMenuInPatient.PrimaryColor = Color.FromArgb();
        }

        private void doctorschannelingScheduleReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
