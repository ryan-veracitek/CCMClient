using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CCMClient
{
	/// <summary>
	/// Summary description for message.
	/// </summary>
	public class message : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		public System.Windows.Forms.RichTextBox messageText;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label version;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.RichTextBox updatesBox;
		public Form1 papa;
		public bool sicurrent = false;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		public System.Windows.Forms.RichTextBox sysLogBox;
		public System.Windows.Forms.RichTextBox appLogBox;
		public System.Windows.Forms.PictureBox logoBox;
		public System.Windows.Forms.PictureBox logoBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public message(Form1 pop)
		{
			//
			// Required for Windows Form Designer support
			//
			papa = pop;
			InitializeComponent();
			System.Drawing.Bitmap Img = new System.Drawing.Bitmap(Form1.controlPath + "\\setup\\bigLogo.gif");
			logoBox.Height = Img.Height;
			logoBox.Width = Img.Width;
			logoBox.Image = Img;
			this.label2.Text = Form1.copyRight;
			this.label1.Text = Form1.programTitle;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.logoBox2 = new System.Windows.Forms.PictureBox();
			this.messageText = new System.Windows.Forms.RichTextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.sysLogBox = new System.Windows.Forms.RichTextBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.appLogBox = new System.Windows.Forms.RichTextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.updatesBox = new System.Windows.Forms.RichTextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.logoBox = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.version = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(384, 440);
			this.tabControl1.TabIndex = 1;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
			this.tabPage1.Controls.Add(this.logoBox2);
			this.tabPage1.Controls.Add(this.messageText);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(376, 414);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "System";
			// 
			// logoBox2
			// 
			this.logoBox2.Location = new System.Drawing.Point(132, 167);
			this.logoBox2.Name = "logoBox2";
			this.logoBox2.Size = new System.Drawing.Size(112, 80);
			this.logoBox2.TabIndex = 2;
			this.logoBox2.TabStop = false;
			// 
			// messageText
			// 
			this.messageText.BackColor = System.Drawing.SystemColors.Window;
			this.messageText.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.messageText.Location = new System.Drawing.Point(0, 0);
			this.messageText.Name = "messageText";
			this.messageText.ReadOnly = true;
			this.messageText.Size = new System.Drawing.Size(376, 416);
			this.messageText.TabIndex = 1;
			this.messageText.Text = "Loading...";
			this.messageText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.messageText_MouseUp);
			this.messageText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messageText_KeyUp);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.sysLogBox);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(376, 414);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "System Log";
			// 
			// sysLogBox
			// 
			this.sysLogBox.BackColor = System.Drawing.SystemColors.Window;
			this.sysLogBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.sysLogBox.Location = new System.Drawing.Point(0, -1);
			this.sysLogBox.Name = "sysLogBox";
			this.sysLogBox.ReadOnly = true;
			this.sysLogBox.Size = new System.Drawing.Size(376, 416);
			this.sysLogBox.TabIndex = 3;
			this.sysLogBox.Text = "Loading...";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.appLogBox);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(376, 414);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Application Log";
			// 
			// appLogBox
			// 
			this.appLogBox.BackColor = System.Drawing.SystemColors.Window;
			this.appLogBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.appLogBox.Location = new System.Drawing.Point(0, -1);
			this.appLogBox.Name = "appLogBox";
			this.appLogBox.ReadOnly = true;
			this.appLogBox.Size = new System.Drawing.Size(376, 416);
			this.appLogBox.TabIndex = 3;
			this.appLogBox.Text = "Loading...";
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Window;
			this.tabPage2.Controls.Add(this.updatesBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(376, 414);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Updates/Messages";
			// 
			// updatesBox
			// 
			this.updatesBox.BackColor = System.Drawing.SystemColors.Window;
			this.updatesBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.updatesBox.Location = new System.Drawing.Point(0, -1);
			this.updatesBox.Name = "updatesBox";
			this.updatesBox.ReadOnly = true;
			this.updatesBox.Size = new System.Drawing.Size(376, 416);
			this.updatesBox.TabIndex = 2;
			this.updatesBox.Text = "Loading...";
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.Window;
			this.tabPage3.Controls.Add(this.logoBox);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Controls.Add(this.version);
			this.tabPage3.Controls.Add(this.label1);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(376, 414);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "About";
			// 
			// logoBox
			// 
			this.logoBox.Location = new System.Drawing.Point(132, 167);
			this.logoBox.Name = "logoBox";
			this.logoBox.Size = new System.Drawing.Size(112, 80);
			this.logoBox.TabIndex = 4;
			this.logoBox.TabStop = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 400);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(368, 16);
			this.label2.TabIndex = 3;
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// version
			// 
			this.version.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.version.Location = new System.Drawing.Point(8, 80);
			this.version.Name = "version";
			this.version.Size = new System.Drawing.Size(360, 23);
			this.version.TabIndex = 2;
			this.version.Text = "Version Info";
			this.version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(82)), ((System.Byte)(135)), ((System.Byte)(177)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(360, 72);
			this.label1.TabIndex = 0;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.menuItem1.Text = "&Refresh";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// message
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(386, 442);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "message";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Information";
			this.Load += new System.EventHandler(this.message_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Refresh();
			//MessageBox.Show("HERE: " + tabControl1.SelectedIndex);
			if (tabControl1.SelectedIndex == 0 && !sicurrent)
			{
				sicurrent = true;
				papa.si.updateMessageWindowThread(this);				
			}
			if (tabControl1.SelectedIndex == 1)
			{
				papa.si.updateUpdatesThread(this);
			}
			if (tabControl1.SelectedIndex == 2)
			{
				papa.si.updateAppLogThread(this);
			}
			if (tabControl1.SelectedIndex == 3)
			{
				papa.si.updateSysLogThread(this);
			}
		}

		private void message_Load(object sender, System.EventArgs e)
		{
			papa.si.updateMessageWindowThread(this);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			refreshWindow();
		}
		private void refreshWindow()
		{
			messageText.Rtf="{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fcharset0 Arial;}}\n{\\colortbl ;\\red0\\green128\\blue0;}\n\\viewkind4\\uc1\\pard\\cf1\\b\\f0\\fs30 Loading System Information... please wait.\\cf0\\b0\\par\n}";
			logoBox2.Visible = true;
			papa.si.updateMessageWindowThread(this);
		}
		private void messageText_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) 
				this.contextMenu1.Show(this.messageText,new System.Drawing.Point(e.X,e.Y));
		}

		private void messageText_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.F5)
				refreshWindow();
		}

	}
}
