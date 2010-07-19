using System;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using HANDLE = System.IntPtr;
using System.Management;

namespace CCMClient
{		
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	///

	public enum SND
	{		
		SND_SYNC         = 0x0000  ,/* play synchronously (default) */
		SND_ASYNC        = 0x0001 , /* play asynchronously */
		SND_NODEFAULT    = 0x0002 , /* silence (!default) if sound not found */
		SND_MEMORY       = 0x0004 , /* pszSound points to a memory file */
		SND_LOOP         = 0x0008 , /* loop the sound until next sndPlaySound */
		SND_NOSTOP       = 0x0010 , /* don't stop any currently playing sound */
		SND_NOWAIT       = 0x00002000, /* don't wait if the driver is busy */
		SND_ALIAS        = 0x00010000 ,/* name is a registry alias */
		SND_ALIAS_ID     = 0x00110000, /* alias is a pre d ID */
		SND_FILENAME     = 0x00020000, /* name is file name */
		SND_RESOURCE     = 0x00040004, /* name is resource name or atom */
		SND_PURGE        = 0x0040,  /* purge non-static events for task */
		SND_APPLICATION  = 0x0080 /* look for application specific association */
	}

	public class Form1 : System.Windows.Forms.Form
	{
        public int hpPrintCounter = 0;
		private System.Windows.Forms.Label label1;
		private CCMClient.DDE.DDEListener ddeListener1;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.TextBox userInfo;
		private System.Windows.Forms.Button confirmButton;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Timer hideMeTimer;
		private System.Windows.Forms.Timer showMeTimer;
		private System.Windows.Forms.Button noButton;
		private System.Windows.Forms.Label label2;
		public  string s="";		
		private System.IO.FileSystemWatcher fileSystemWatcher1;		
		public static string filePath = "";
		public static string disableFileName = "popControlDisable.txt";
		public static string aliasFileName = "setup//alias.txt";
		public static string configFileName = "setup//configuration.txt";
        public static string lastFileError = "";
		public string updateHistory = "";
		public string applicationLog = "";
		public string systemLog = "";
		public string frsLog = "";
		public string directoryLog = "";
		public string fileReadStat = "";
		public string[] waitingUpdates = new string[2];
		public bool forceHide = false;
		private int pthow = 0;
		public static string alarmFile = "alarm.wav";
		private static System.Diagnostics.EventLog eventLog1  = new System.Diagnostics.EventLog();

		private CCMClient.NotifyIconEx notifyIcon1;
		private System.Windows.Forms.ContextMenu contextMenu1;

		public static string reportFileName = "popControlReport.txt";
		public static string versionCheckFileName = "popControlVersionCheck.txt";
		public static string installedSoftwareFileName = "popControlReportSoftware.txt";
		
		public static string alarmFileName = "popControlAlarm.txt";
		public static string pushFileName = "popControlPush.txt";
		public static string messageFileName = "m" + System.Net.Dns.GetHostName() + ".txt";
		public static string defFileName = "setup\\fileCheckDefinition.txt";
		public static string cver = "07192010";
		public string[] dotnetVersionStandard;
		public string[] osVersionStandard;
		public string[] osSPStandard;
		public string[] refreshRateMin;
		public string[] memoryMin;
		ArrayList senders = new ArrayList();
		ArrayList messages = new ArrayList();
		ArrayList timeStamps = new ArrayList();
        public int conTry = 0;
        public int messageIndex = -1;
		public int messageCount = 0;
		public static string controlPath = "\\\\ccm\\ccm";
        public static string localControlPath = "C:\\CCM";
		public static string programName = "CCMClient";
		public string serverList;
        public ArrayList illegalPrograms = new ArrayList();
        public ArrayList allowedPrograms = new ArrayList();
		public DateTime startTime;
        public System.IntPtr psWindow = (System.IntPtr) null;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.Timer autoHideTimer;
		private System.Windows.Forms.Timer fileWatcherFixer;
		public System.Windows.Forms.MenuItem autoUpdate;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem debugMode;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.TextBox sendToOld;
		private System.Windows.Forms.TextBox message;
		private System.Windows.Forms.Button sendButton;
		private System.IO.FileSystemWatcher fileSystemWatcher2;
		public double isInvalidated = 0;
		public double invalidateForHowLong = 5;
		private System.Windows.Forms.Label toLabel;
		private System.Windows.Forms.Timer messageTimer;
		public string lastSentTo = "";
		public string lastSender = "";
		private System.Windows.Forms.LinkLabel replyLink;
		public int offset = 75;
		public int messageTimerCount = 0;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.TrackBar refreshSpeed;
		private System.Windows.Forms.Label slowLabel;
		private System.Windows.Forms.Label fastLabel;
		public systemInventory si;
		public bool rKeyDown = false;
		private System.Windows.Forms.LinkLabel prevLink;
		private System.Windows.Forms.LinkLabel nextLink;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem sendFileItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		public bool connected=true;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		public string fileTransferName="";
		public string sendFileTo="";
		public bool sendingFile = false;
		public static string currentUser = "";
		public static bool loggedIn = false;
		public static bool lastLoggedIn = false;
		public static bool loginChecked = false;
		public bool gotMessage = false;
		private System.Windows.Forms.Label timeStamp;
		private System.Windows.Forms.MenuItem menuItem13;
		public System.Windows.Forms.MenuItem disablePops;
		public System.Windows.Forms.MenuItem enterSend;
		public System.Windows.Forms.MenuItem doNotDisturb;
		private System.Windows.Forms.MenuItem setPopSpeed;
		public System.Windows.Forms.MenuItem autoHide;
		public System.Windows.Forms.MenuItem playSound;
		private System.Windows.Forms.Timer closeProgramTimer;
        public static int powerDownTimeOut = 120;
        public static int powerDownMaxFail = 2;
        public static bool ASDinternalDisable = false;
        public static int powerDownTimer = -1;
		public string appToClose = "";
		public bool kill = false;
		private System.Windows.Forms.MenuItem autoCloseInOrder;
		public string ddmess = "Sorry, my workstation is set to \"Do Not Disturb\". I'm probably not available right now.";
        public ArrayList lastProcessList = new ArrayList();

        
		public static string orgName = "VeraciTek.com";
		public static string copyRight = "2009 " + orgName;
		public static string programTitle = "Corporate Computer Manager Client";
		public static string logTo = "CCM Logger";
		public static string DDEServer = "OFF";
		public static string database = "OFF";
		public static string notifyText = programTitle + "\n" + System.Net.Dns.GetHostName().ToUpper() + "\nVersion "+cver;// + System.DateTime.Now;
        public static string[] guListArray;
		
		public static System.Drawing.Icon iconNorm; //= new System.Drawing.Icon(controlPath + "\\setup\\logoNorm.ico");
		public static System.Drawing.Icon iconDND; //= new System.Drawing.Icon(controlPath + "\\setup\\logoDND.ico");
		private System.Windows.Forms.Timer trayIconCheck;
		private System.Windows.Forms.Timer invalidatedCheck;
		private System.Windows.Forms.MenuItem restartButton;
        private ComboBox sendTo;
        private MenuItem menuItem2;
		public static System.Drawing.Icon iconDisco; //= new System.Drawing.Icon(controlPath + "\\setup\\logoDisco.ico");

        public static int lastPower = powerDownMaxFail;
        public static bool lastAskMe = false;
        private System.Windows.Forms.Timer pdTimer;
        private MenuItem disableASD;
        private System.Windows.Forms.Timer processChecker;
        private MenuItem menuItem11;
        private LinkLabel linkLabel1;
        private PictureBox backgroundBox;
        private MenuItem showPrintMessages;
        private MenuItem askMeSD;
        private Button skipButton;
        private MenuItem hibernateInstead;
        public static bool powerOutBalloon = false;
        private MenuItem dasdwh;
        public DateTime lastTime = System.DateTime.Now;
        public DateTime newTime = System.DateTime.Now;
        public bool wasDASDChecked = false;
        public bool skipButtonClicked = false;
        public int fixTimeCount = 0;
        public bool timeNeedsChanged = false;
        public DateTime lastSync = new DateTime();
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
		    
            if (!System.IO.File.Exists(localControlPath + "\\setup\\logoNorm.ico"))
                debugMessage("CCM Crashed because " + localControlPath + "\\setup\\logoNorm.ico was not available. :-(");

            iconNorm = new System.Drawing.Icon(localControlPath + "\\setup\\logoNorm.ico");
            iconDND = new System.Drawing.Icon(localControlPath + "\\setup\\logoDND.ico");
            iconDisco = new System.Drawing.Icon(localControlPath + "\\setup\\logoDisco.ico");
			//MessageBox.Show(controlPath);
			filePath = Form1.controlPath;
			loadConfiguration();				
			//MessageBox.Show(programTitle);
			//iconNorm = new System.Drawing.Icon(controlPath + "\\setup\\logoNorm.ico");
			//iconDND = new System.Drawing.Icon(controlPath + "\\setup\\logoDND.ico");
			//iconDisco = new System.Drawing.Icon(controlPath + "\\setup\\logoDisco.ico");		
			notifyText = programTitle + "\n" + System.Net.Dns.GetHostName().ToUpper() + "\nVersion "+cver;// + System.DateTime.Now;
			InitializeComponent();
            if (System.IO.Directory.Exists(controlPath))
            {
                this.fileSystemWatcher1.Path = controlPath;
                this.fileSystemWatcher2.Path = controlPath + "\\messages";
            }
            else
            {
                fileSystemWatcher1_Broken(fileSystemWatcher1, new System.IO.ErrorEventArgs(new Exception()));
            }
			//this.fileSystemWatcher1.Path = controlPath;
			//this.fileSystemWatcher2.Path = controlPath;
			this.label2.Text = copyRight;		
			waitingUpdates[0] = "";
			waitingUpdates[1] = "";
            int startfnum = 0;
            if (connected)
            {
                while (System.IO.File.Exists(localControlPath + "\\setup\\backgroundActive" + startfnum + ".bmp"))
                {
                    try
                    {
                        System.IO.File.Delete(localControlPath + "\\setup\\backgroundActive" + (startfnum) + ".bmp");
                    }
                    catch (Exception e)
                    {
                        //do nothing this is just cleanup
                    }
                    startfnum++;
                }
                startfnum = 0;
                while (System.IO.File.Exists(localControlPath + "\\setup\\backgroundActive" + startfnum + ".bmp"))
                    startfnum++;
                System.IO.File.Move(localControlPath + "\\setup\\background.bmp", localControlPath + "\\setup\\backgroundActive" + startfnum + ".bmp");
            }
            else
            {
                startfnum = 0;
                while (!System.IO.File.Exists(localControlPath + "\\setup\\backgroundActive" + startfnum + ".bmp") && (startfnum < 30))
                {
                    startfnum++;
                }
            }
        	si = new systemInventory(this);
            if (startfnum < 30)
            {
                System.Drawing.Bitmap Img = new System.Drawing.Bitmap(localControlPath + "\\setup\\backgroundActive" + startfnum + ".bmp", true);
               // MessageBox.Show(localControlPath + "\\setup\\backgroundActive" + startfnum + ".bmp");
                //The color at Pixel(2,2) is rendered as transparent for the complete background. 
                //if (si.getColorDepth() > 17000000)
                //Img.MakeTransparent(Img.GetPixel(2,2));
                Img.MakeTransparent(Color.Blue);
//                MessageBox.Show(Img.GetPixel(8, 8).R.ToString() + "-" + Img.GetPixel(8, 8).G.ToString() + "-" + Img.GetPixel(8, 8).B.ToString());
                backgroundBox.BackgroundImage = Img;
                //this.BackgroundImage = Img;
                //this.TransparencyKey = Img.GetPixel(8, 8);
                this.TransparencyKey = Color.Blue;
                /*
                this.BackgroundImage = Img;
                this.TransparencyKey = Img.GetPixel(2, 2); */
                this.AllowTransparency = true;
            }
           // doExecute("%windir%\\w32tm.exe", "/resync", 1);
            syncTime("Synchronizing time on start up.",true);
            

			startTime = System.DateTime.Now;
			this.notifyIcon1.Text = notifyText;
			const int GWL_EXSTYLE = -20;
			const int WS_EX_TOOLWINDOW = 0x00000080;
			//const int WS_EX_APPWINDOW = 0x00040000;
			int windowStyle = GetWindowLong(GWL_EXSTYLE);				
			SetWindowLong(GWL_EXSTYLE, windowStyle | WS_EX_TOOLWINDOW);
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			//filePath = this.fileSystemWatcher1.Path;
			this.fileSystemWatcher1.Error += new System.IO.ErrorEventHandler(this.fileSystemWatcher1_Broken);
			this.components = new System.ComponentModel.Container();
			if (DDEServer.ToUpper().CompareTo("ON") == 0)
			{
				this.ddeListener1 = new CCMClient.DDE.DDEListener(this.components);
				// 
				// ddeListener1
				// 
				this.ddeListener1.ActionName = "Phone_Number";
				this.ddeListener1.AppName = "CCMClient";
				this.ddeListener1.OnDDEExecute += new CCMClient.DDE.DDEExecuteEventHandler(this.ddeListener1_OnDDEExecute);
			}
			// 
			this.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width-200-offset,Screen.PrimaryScreen.Bounds.Height); //removed -210
			//this.hideMeTimer.Enabled = true;
			this.Size = new System.Drawing.Size(200,200);
			int setting = 0;
			if ( si.strToInt(getRegVal("popSpeed").ToString()) > 0)
				setting = si.strToInt(getRegVal("popSpeed").ToString());	
			if (setting < 1)
				setting = 80;
            if (si.strToInt(getRegVal("powerDownTimeout").ToString()) > 0)
                powerDownTimeOut = si.strToInt(getRegVal("powerDownTimeout").ToString());
            else
            {
                if (isserver(System.Environment.MachineName))
                    powerDownTimeOut = 11100;
                setRegVal("powerDownTimeout", powerDownTimeOut.ToString());
            }
            
            if (si.strToInt(getRegVal("powerDownMaxFail").ToString()) > 0)
                powerDownMaxFail = si.strToInt(getRegVal("powerDownMaxFail").ToString());
            else
            {
                if (isserver(System.Environment.MachineName))
                    powerDownMaxFail = 2;
                setRegVal("powerDownMaxFail", powerDownMaxFail.ToString());
            }
            
            
            if (powerDownTimeOut >= (trayIconCheck.Interval / 1000))
                pdTimer.Interval = powerDownTimeOut;
            else
                powerDownTimeOut = (trayIconCheck.Interval / 1000)+1;
			this.refreshSpeed.Value = setting;
			if (getRegVal("enterSend").ToString().CompareTo("True") == 0)
				this.enterSend.Checked = true;	
			if (getRegVal("playSound").ToString().CompareTo("True") == 0)
				this.playSound.Checked = true;	
			if (getRegVal("doNotDisturb").ToString().CompareTo("True") == 0)
			{
				this.doNotDisturb.Checked = true;					
				this.notifyIcon1.Icon = Form1.iconDND;	
			}
			if (getRegVal("autoHide").ToString().CompareTo("True") == 0)
				this.autoHide.Checked = true;
			if (getRegVal("disablePops").ToString().CompareTo("True") == 0)
				this.disablePops.Checked = true;
            if (getRegVal("disableAutoShutDown").ToString().CompareTo("True") == 0)
                this.disableASD.Checked = true;
            wasDASDChecked = this.disableASD.Checked;
            if (getRegVal("askMeSD").ToString().CompareTo("True") == 0)
                this.askMeSD.Checked = true;
            if (getRegVal("hibernateInstead").ToString().CompareTo("True") == 0)
                this.hibernateInstead.Checked = true;
            if (getRegVal("dasdwh").ToString().CompareTo("True") == 0)
                this.dasdwh.Checked = true;
            
            if (getRegVal("autoUpdate").ToString().CompareTo("True") == 0)
				this.autoUpdate.Checked = true;		
			if (getRegVal("autoCloseInOrder").ToString().CompareTo("True") == 0)
				this.autoCloseInOrder.Checked = true;					
			if (getRegVal("debugMode").ToString().CompareTo("True") == 0)
				this.debugMode.Checked = true;	
			getCurrentUser();
			string holder = "m" + currentUser.ToUpper().Replace("VeraciTek\\","").ToLower() + ".txt";

			System.IO.FileInfo messageFile = new System.IO.FileInfo(filePath + "\\messages\\" + messageFileName);
			try 
			{
				if (messageFile.Exists)
					if (messageFile.Length > 0)
						checkMessage(messageFileName);
					else
						messageFile.Delete();                 
			}
			catch (Exception e)
			{
				debugMessage("Delete of message file failed. First Branch.");
			}
			
			System.IO.FileInfo messageFile2 = new System.IO.FileInfo(filePath + "\\messages\\" + holder);
			try
			{
				if (messageFile2.Exists)
					if (messageFile2.Length > 0)
						checkMessage(holder);
					else
						messageFile2.Delete();
			}
			catch (Exception e)
			{
				debugMessage("Delete of message file failed. Second Branch.");
			}
            loadIllegalPrograms();
            loadAllowedPrograms();
            processChecker.Enabled = true;
			System.GC.WaitForPendingFinalizers();
			System.GC.Collect();
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			Form1.debugMessage("Dispose has been called!");
			if( disposing )
			{
				this.notifyIcon1.Dispose();
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		/*	protected override void WndProc (ref Message m)
			{			
				//SetStyle(ControlStyles.EnableNotifyMessage, true);
				const int WM_QUERYENDSESSION = 0x11;
				const int WM_ENDSESSION = 0x16;
				if (m.Msg == WM_ENDSESSION || m.Msg == WM_QUERYENDSESSION)
				{
					Form1.debugMessage("Shut Down Message Received!");
					m.Result = IntPtr.Zero;	
				}						
				base.WndProc(ref m);
			} */
		
		[DllImport("winmm.dll", EntryPoint="PlaySound",CharSet=CharSet.Auto)]
		private static extern int PlaySound(String pszSound, int hmod, int falgs);

        [DllImport("user32.dll")]
        public static extern int FindWindowEx(int hWndParent, int hWndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        public const int WM_COMMAND = 0x0112;
        public const int WM_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        static extern ulong GetWindowLongA(IntPtr hWnd, int nIndex);
        static readonly int GWL_STYLE = -16;
        static readonly ulong WS_VISIBLE = 0x10000000L;
        static readonly ulong WS_BORDER = 0x00800000L;
        static readonly ulong TARGETWINDOW = WS_BORDER | WS_VISIBLE;
        
        
        [DllImport("user32")]
		internal static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        
		[DllImport("user32.dll")]
		public static extern bool InvalidateRect(IntPtr hwnd, IntPtr lpRect, int bErase); 
		[DllImport("user32.dll")]
		internal static extern void ReleaseDC(IntPtr dc);
		[DllImport("user32.dll")]
		static extern int SetWindowLong(
			IntPtr window,
			int index,
			int value);

		[DllImport("user32.dll")]
		static extern int GetWindowLong(
			IntPtr window,
			int index);
		public int GetWindowLong(int index)
		{
			return GetWindowLong(this.Handle, index);
		}
		public static void Application_ThreadException(object sender,System.Threading.ThreadExceptionEventArgs e)
		{
			debugMessage(e.Exception.Message);
			eLog(e.Exception.Message + "\n" + e.Exception.Source + "\n" + e.Exception.StackTrace,System.Diagnostics.EventLogEntryType.Error);			
			Application.Exit();
		}
		public int SetWindowLong(int index, int value)
		{
			return SetWindowLong(this.Handle, index, value);
		}

        static void eLog(string what)
        {
            eLog(what, System.Diagnostics.EventLogEntryType.Information);
        }
		static void eLog(string what, System.Diagnostics.EventLogEntryType etype)
		{		
			if (!System.Diagnostics.EventLog.SourceExists("VeraciTek Workstation Starter")) 
			{         
				System.Diagnostics.EventLog.CreateEventSource("VeraciTek Workstation Starter","VeraciTek Workstation Logger");
			}			
			eventLog1.Log = logTo;
			eventLog1.Source = programTitle;
            
			//eventLog1.Source = "Debug Message";
			eventLog1.WriteEntry(what,etype); 
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.userInfo = new System.Windows.Forms.TextBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.hideMeTimer = new System.Windows.Forms.Timer(this.components);
            this.showMeTimer = new System.Windows.Forms.Timer(this.components);
            this.noButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.sendFileItem = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.playSound = new System.Windows.Forms.MenuItem();
            this.doNotDisturb = new System.Windows.Forms.MenuItem();
            this.enterSend = new System.Windows.Forms.MenuItem();
            this.setPopSpeed = new System.Windows.Forms.MenuItem();
            this.autoHide = new System.Windows.Forms.MenuItem();
            this.disablePops = new System.Windows.Forms.MenuItem();
            this.autoUpdate = new System.Windows.Forms.MenuItem();
            this.autoCloseInOrder = new System.Windows.Forms.MenuItem();
            this.dasdwh = new System.Windows.Forms.MenuItem();
            this.hibernateInstead = new System.Windows.Forms.MenuItem();
            this.askMeSD = new System.Windows.Forms.MenuItem();
            this.disableASD = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.showPrintMessages = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.debugMode = new System.Windows.Forms.MenuItem();
            this.restartButton = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.autoHideTimer = new System.Windows.Forms.Timer(this.components);
            this.fileWatcherFixer = new System.Windows.Forms.Timer(this.components);
            this.sendToOld = new System.Windows.Forms.TextBox();
            this.message = new System.Windows.Forms.TextBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.fileSystemWatcher2 = new System.IO.FileSystemWatcher();
            this.messageTimer = new System.Windows.Forms.Timer(this.components);
            this.replyLink = new System.Windows.Forms.LinkLabel();
            this.refreshSpeed = new System.Windows.Forms.TrackBar();
            this.slowLabel = new System.Windows.Forms.Label();
            this.fastLabel = new System.Windows.Forms.Label();
            this.prevLink = new System.Windows.Forms.LinkLabel();
            this.nextLink = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timeStamp = new System.Windows.Forms.Label();
            this.closeProgramTimer = new System.Windows.Forms.Timer(this.components);
            this.trayIconCheck = new System.Windows.Forms.Timer(this.components);
            this.invalidatedCheck = new System.Windows.Forms.Timer(this.components);
            this.sendTo = new System.Windows.Forms.ComboBox();
            this.pdTimer = new System.Windows.Forms.Timer(this.components);
            this.processChecker = new System.Windows.Forms.Timer(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.backgroundBox = new System.Windows.Forms.PictureBox();
            this.skipButton = new System.Windows.Forms.Button();
            this.notifyIcon1 = new CCMClient.NotifyIconEx();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.refreshSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 8);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.Window;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.CloseButton.ForeColor = System.Drawing.Color.IndianRed;
            this.CloseButton.Location = new System.Drawing.Point(168, 17);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(18, 16);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // userInfo
            // 
            this.userInfo.AllowDrop = true;
            this.userInfo.BackColor = System.Drawing.SystemColors.Window;
            this.userInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.userInfo.ForeColor = System.Drawing.Color.Navy;
            this.userInfo.Location = new System.Drawing.Point(16, 40);
            this.userInfo.Multiline = true;
            this.userInfo.Name = "userInfo";
            this.userInfo.ReadOnly = true;
            this.userInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.userInfo.Size = new System.Drawing.Size(172, 88);
            this.userInfo.TabIndex = 2;
            this.userInfo.Text = "Loading";
            this.userInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Window;
            this.confirmButton.Enabled = false;
            this.confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.confirmButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.confirmButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.confirmButton.Location = new System.Drawing.Point(48, 128);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(48, 16);
            this.confirmButton.TabIndex = 3;
            this.confirmButton.Text = "Yes";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // hideMeTimer
            // 
            this.hideMeTimer.Tick += new System.EventHandler(this.hideMeTimer_Tick);
            // 
            // showMeTimer
            // 
            this.showMeTimer.Tick += new System.EventHandler(this.showMeTimer_Tick);
            // 
            // noButton
            // 
            this.noButton.BackColor = System.Drawing.SystemColors.Window;
            this.noButton.Enabled = false;
            this.noButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.noButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.noButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.noButton.Location = new System.Drawing.Point(104, 128);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(48, 16);
            this.noButton.TabIndex = 4;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = false;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.label2.Location = new System.Drawing.Point(47, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 10);
            this.label2.TabIndex = 5;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.Filter = "popControl*.txt";
            this.fileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.FileName;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.menuItem12,
            this.sendFileItem,
            this.menuItem8,
            this.playSound,
            this.doNotDisturb,
            this.enterSend,
            this.setPopSpeed,
            this.autoHide,
            this.disablePops,
            this.autoUpdate,
            this.autoCloseInOrder,
            this.dasdwh,
            this.hibernateInstead,
            this.askMeSD,
            this.disableASD,
            this.menuItem11,
            this.showPrintMessages,
            this.menuItem7,
            this.menuItem13,
            this.menuItem1,
            this.menuItem3,
            this.menuItem6,
            this.menuItem4,
            this.menuItem5,
            this.debugMode,
            this.restartButton,
            this.menuItem10,
            this.menuItem2});
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "&Send Message";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click_1);
            // 
            // menuItem12
            // 
            this.menuItem12.Enabled = false;
            this.menuItem12.Index = 1;
            this.menuItem12.Text = "&View Message(s)";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // sendFileItem
            // 
            this.sendFileItem.Index = 2;
            this.sendFileItem.Text = "Send &File";
            this.sendFileItem.Click += new System.EventHandler(this.sendFileItem_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.Text = "-";
            // 
            // playSound
            // 
            this.playSound.Index = 4;
            this.playSound.Text = "P&lay Message Sound";
            this.playSound.Click += new System.EventHandler(this.playSound_Click);
            // 
            // doNotDisturb
            // 
            this.doNotDisturb.Index = 5;
            this.doNotDisturb.Text = "Do &Not Disturb";
            this.doNotDisturb.Click += new System.EventHandler(this.doNotDisturb_Click);
            // 
            // enterSend
            // 
            this.enterSend.Index = 6;
            this.enterSend.Text = "&Enter = Send";
            this.enterSend.Visible = false;
            this.enterSend.Click += new System.EventHandler(this.enterSend_Click);
            // 
            // setPopSpeed
            // 
            this.setPopSpeed.Index = 7;
            this.setPopSpeed.Text = "Set &Pop Speed";
            this.setPopSpeed.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // autoHide
            // 
            this.autoHide.Index = 8;
            this.autoHide.Text = "A&uto Hide Popups";
            this.autoHide.Visible = false;
            this.autoHide.Click += new System.EventHandler(this.autoHide_Click);
            // 
            // disablePops
            // 
            this.disablePops.Index = 9;
            this.disablePops.Text = "&Disable Pops";
            this.disablePops.Visible = false;
            this.disablePops.Click += new System.EventHandler(this.disablePops_Click);
            // 
            // autoUpdate
            // 
            this.autoUpdate.Index = 10;
            this.autoUpdate.Text = "A&uto Update";
            this.autoUpdate.Visible = false;
            this.autoUpdate.Click += new System.EventHandler(this.autoUpdate_Click);
            // 
            // autoCloseInOrder
            // 
            this.autoCloseInOrder.Index = 11;
            this.autoCloseInOrder.Text = "Disable AutoClose";
            this.autoCloseInOrder.Visible = false;
            this.autoCloseInOrder.Click += new System.EventHandler(this.autoCloseInOrder_Click);
            // 
            // dasdwh
            // 
            this.dasdwh.Index = 12;
            this.dasdwh.Text = "Disable ASD during work hours.";
            this.dasdwh.Click += new System.EventHandler(this.dasdwh_Click);
            // 
            // hibernateInstead
            // 
            this.hibernateInstead.Index = 13;
            this.hibernateInstead.Text = "Hi&bernate Instead";
            this.hibernateInstead.Click += new System.EventHandler(this.hibernateInstead_Click);
            // 
            // askMeSD
            // 
            this.askMeSD.Index = 14;
            this.askMeSD.Text = "Ask &Me B4 Shutdown ";
            this.askMeSD.Click += new System.EventHandler(this.askMeSD_Click);
            // 
            // disableASD
            // 
            this.disableASD.Index = 15;
            this.disableASD.Text = "Disable &Auto Shutdown";
            this.disableASD.Click += new System.EventHandler(this.disableASD_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 16;
            this.menuItem11.Text = "Disable E&xec Prevention";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click_1);
            // 
            // showPrintMessages
            // 
            this.showPrintMessages.Index = 17;
            this.showPrintMessages.Text = "Show Print Messages";
            this.showPrintMessages.Click += new System.EventHandler(this.showPrintMessages_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 18;
            this.menuItem7.Text = "-";
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 19;
            this.menuItem13.Text = "&Today\'s Scripture";
            this.menuItem13.Click += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 20;
            this.menuItem1.Text = "Corporate Home Page";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 21;
            this.menuItem3.Text = "&Help Desk";
            this.menuItem3.Visible = false;
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 22;
            this.menuItem6.Text = "Tut&orial";
            this.menuItem6.Visible = false;
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click_1);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 23;
            this.menuItem4.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 24;
            this.menuItem5.Text = "System &Information";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // debugMode
            // 
            this.debugMode.Index = 25;
            this.debugMode.Text = "Debug Mode";
            this.debugMode.Visible = false;
            this.debugMode.Click += new System.EventHandler(this.debugMode_Click);
            // 
            // restartButton
            // 
            this.restartButton.DefaultItem = true;
            this.restartButton.Index = 26;
            this.restartButton.Text = "&Restart this Program";
            this.restartButton.Visible = false;
            this.restartButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 27;
            this.menuItem10.Text = "Test";
            this.menuItem10.Visible = false;
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 28;
            this.menuItem2.Text = "Test Connections";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // autoHideTimer
            // 
            this.autoHideTimer.Interval = 10000;
            this.autoHideTimer.Tick += new System.EventHandler(this.autoHideTimer_Tick);
            // 
            // fileWatcherFixer
            // 
            this.fileWatcherFixer.Interval = 1000;
            this.fileWatcherFixer.Tick += new System.EventHandler(this.fileWatcherFixer_Tick);
            // 
            // sendToOld
            // 
            this.sendToOld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.sendToOld.Location = new System.Drawing.Point(48, 35);
            this.sendToOld.Name = "sendToOld";
            this.sendToOld.Size = new System.Drawing.Size(136, 20);
            this.sendToOld.TabIndex = 0;
            this.sendToOld.TabStop = false;
            this.sendToOld.Visible = false;
            // 
            // message
            // 
            this.message.BackColor = System.Drawing.SystemColors.Window;
            this.message.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.message.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.message.Location = new System.Drawing.Point(16, 56);
            this.message.MaxLength = 0;
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.message.Size = new System.Drawing.Size(174, 72);
            this.message.TabIndex = 7;
            this.message.Visible = false;
            this.message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.message_KeyDown);
            this.message.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.message_KeyPress);
            // 
            // toLabel
            // 
            this.toLabel.BackColor = System.Drawing.SystemColors.Window;
            this.toLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toLabel.Location = new System.Drawing.Point(16, 35);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(32, 16);
            this.toLabel.TabIndex = 8;
            this.toLabel.Text = "To:";
            this.toLabel.Visible = false;
            // 
            // sendButton
            // 
            this.sendButton.BackColor = System.Drawing.SystemColors.Window;
            this.sendButton.Enabled = false;
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.sendButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.sendButton.Location = new System.Drawing.Point(72, 128);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(50, 18);
            this.sendButton.TabIndex = 8;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Visible = false;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // fileSystemWatcher2
            // 
            this.fileSystemWatcher2.EnableRaisingEvents = true;
            this.fileSystemWatcher2.Filter = "m*.txt";
            this.fileSystemWatcher2.NotifyFilter = System.IO.NotifyFilters.FileName;
            this.fileSystemWatcher2.SynchronizingObject = this;
            this.fileSystemWatcher2.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher2_Renamed);
            this.fileSystemWatcher2.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher2_Created);
            this.fileSystemWatcher2.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher2_Created);
            // 
            // messageTimer
            // 
            this.messageTimer.Interval = 500;
            this.messageTimer.Tick += new System.EventHandler(this.messageTimer_Tick);
            // 
            // replyLink
            // 
            this.replyLink.BackColor = System.Drawing.SystemColors.Window;
            this.replyLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replyLink.LinkColor = System.Drawing.Color.Green;
            this.replyLink.Location = new System.Drawing.Point(148, 136);
            this.replyLink.Name = "replyLink";
            this.replyLink.Size = new System.Drawing.Size(40, 16);
            this.replyLink.TabIndex = 10;
            this.replyLink.TabStop = true;
            this.replyLink.Text = "Reply";
            this.replyLink.Visible = false;
            this.replyLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // refreshSpeed
            // 
            this.refreshSpeed.BackColor = System.Drawing.SystemColors.Window;
            this.refreshSpeed.Location = new System.Drawing.Point(24, 56);
            this.refreshSpeed.Maximum = 210;
            this.refreshSpeed.Minimum = 1;
            this.refreshSpeed.Name = "refreshSpeed";
            this.refreshSpeed.Size = new System.Drawing.Size(152, 45);
            this.refreshSpeed.TabIndex = 11;
            this.refreshSpeed.Value = 1;
            this.refreshSpeed.Visible = false;
            // 
            // slowLabel
            // 
            this.slowLabel.BackColor = System.Drawing.SystemColors.Window;
            this.slowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slowLabel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.slowLabel.Location = new System.Drawing.Point(24, 88);
            this.slowLabel.Name = "slowLabel";
            this.slowLabel.Size = new System.Drawing.Size(40, 23);
            this.slowLabel.TabIndex = 12;
            this.slowLabel.Text = "Slower";
            this.slowLabel.Visible = false;
            // 
            // fastLabel
            // 
            this.fastLabel.BackColor = System.Drawing.SystemColors.Window;
            this.fastLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastLabel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fastLabel.Location = new System.Drawing.Point(136, 88);
            this.fastLabel.Name = "fastLabel";
            this.fastLabel.Size = new System.Drawing.Size(40, 23);
            this.fastLabel.TabIndex = 13;
            this.fastLabel.Text = "Faster";
            this.fastLabel.Visible = false;
            // 
            // prevLink
            // 
            this.prevLink.BackColor = System.Drawing.SystemColors.Window;
            this.prevLink.LinkColor = System.Drawing.Color.Green;
            this.prevLink.Location = new System.Drawing.Point(38, 136);
            this.prevLink.Name = "prevLink";
            this.prevLink.Size = new System.Drawing.Size(17, 16);
            this.prevLink.TabIndex = 14;
            this.prevLink.TabStop = true;
            this.prevLink.Text = "<<";
            this.prevLink.Visible = false;
            this.prevLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.prevLink_LinkClicked);
            // 
            // nextLink
            // 
            this.nextLink.BackColor = System.Drawing.SystemColors.Window;
            this.nextLink.LinkColor = System.Drawing.Color.Green;
            this.nextLink.Location = new System.Drawing.Point(55, 136);
            this.nextLink.Name = "nextLink";
            this.nextLink.Size = new System.Drawing.Size(17, 16);
            this.nextLink.TabIndex = 15;
            this.nextLink.TabStop = true;
            this.nextLink.Text = ">>";
            this.nextLink.Visible = false;
            this.nextLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.nextLink_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.InitialDirectory = "C:";
            this.openFileDialog1.Title = "Choose a file to send.";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.InitialDirectory = "C:";
            this.saveFileDialog1.Title = "File Destination";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // timeStamp
            // 
            this.timeStamp.BackColor = System.Drawing.Color.White;
            this.timeStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeStamp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.timeStamp.Location = new System.Drawing.Point(16, 16);
            this.timeStamp.Name = "timeStamp";
            this.timeStamp.Size = new System.Drawing.Size(144, 16);
            this.timeStamp.TabIndex = 17;
            this.timeStamp.Text = "test 1 2 3";
            this.timeStamp.Visible = false;
            // 
            // closeProgramTimer
            // 
            this.closeProgramTimer.Interval = 10000;
            this.closeProgramTimer.Tick += new System.EventHandler(this.closeProgramTimer_Tick);
            // 
            // trayIconCheck
            // 
            this.trayIconCheck.Enabled = true;
            this.trayIconCheck.Interval = 30000;
            this.trayIconCheck.Tick += new System.EventHandler(this.trayIconCheck_Tick);
            // 
            // invalidatedCheck
            // 
            this.invalidatedCheck.Enabled = true;
            this.invalidatedCheck.Interval = 1000;
            this.invalidatedCheck.Tick += new System.EventHandler(this.invalidatedCheck_Tick);
            // 
            // sendTo
            // 
            this.sendTo.AllowDrop = true;
            this.sendTo.FormattingEnabled = true;
            this.sendTo.Location = new System.Drawing.Point(48, 34);
            this.sendTo.Name = "sendTo";
            this.sendTo.Size = new System.Drawing.Size(140, 21);
            this.sendTo.TabIndex = 6;
            // 
            // pdTimer
            // 
            this.pdTimer.Interval = 120000;
            this.pdTimer.Tick += new System.EventHandler(this.pdTimer_Tick);
            // 
            // processChecker
            // 
            this.processChecker.Interval = 3000;
            this.processChecker.Tick += new System.EventHandler(this.processChecker_Tick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.BackColor = System.Drawing.SystemColors.Window;
            this.linkLabel1.LinkColor = System.Drawing.Color.Green;
            this.linkLabel1.Location = new System.Drawing.Point(70, 119);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(57, 17);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Try Again";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // backgroundBox
            // 
            this.backgroundBox.BackColor = System.Drawing.Color.Blue;
            this.backgroundBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundBox.Location = new System.Drawing.Point(0, 0);
            this.backgroundBox.Name = "backgroundBox";
            this.backgroundBox.Size = new System.Drawing.Size(200, 21);
            this.backgroundBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.backgroundBox.TabIndex = 19;
            this.backgroundBox.TabStop = false;
            // 
            // skipButton
            // 
            this.skipButton.BackColor = System.Drawing.SystemColors.Window;
            this.skipButton.Enabled = false;
            this.skipButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.skipButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.skipButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.skipButton.Location = new System.Drawing.Point(34, 34);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(126, 37);
            this.skipButton.TabIndex = 20;
            this.skipButton.Text = "Click Here to Skip Pending Shutdown";
            this.skipButton.UseVisualStyleBackColor = false;
            this.skipButton.Visible = false;
            this.skipButton.Click += new System.EventHandler(this.skipButton_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenu = this.contextMenu1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Loading...";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.BalloonClick += new System.EventHandler(this.notifyIcon1_BalloonClick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(200, 21);
            this.ControlBox = false;
            this.Controls.Add(this.skipButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.sendTo);
            this.Controls.Add(this.timeStamp);
            this.Controls.Add(this.nextLink);
            this.Controls.Add(this.prevLink);
            this.Controls.Add(this.fastLabel);
            this.Controls.Add(this.slowLabel);
            this.Controls.Add(this.refreshSpeed);
            this.Controls.Add(this.message);
            this.Controls.Add(this.sendToOld);
            this.Controls.Add(this.userInfo);
            this.Controls.Add(this.replyLink);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backgroundBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CCMClient";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.refreshSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		[DllImport("advapi32")]
		static extern bool OpenProcessToken(
			HANDLE ProcessHandle, // handle to process
			int DesiredAccess, // desired access to process
			ref IntPtr TokenHandle // handle to open access token
			);

		[DllImport("kernel32")]
		static extern bool CloseHandle(HANDLE handle);

		public static string getCurrentUser()
		{
			string retVal = System.Environment.MachineName;
            if (retVal.ToLower() == "k2")
                return retVal;
            if (retVal.ToLower() == "ks")
                return "simon";             
			int i=0;
			loggedIn = false;
			if (!loginChecked)
				lastLoggedIn = false;
			System.Security.Principal.WindowsIdentity wi;
			try 
			{
				System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("explorer");
				if (process.Length > 0)
				{
					if (process[i].ProcessName.ToLower().CompareTo("explorer") == 0)
					{
						int access = 0X00000008;
						HANDLE logonToken = IntPtr.Zero;
						if (OpenProcessToken(process[i].Handle,access,ref logonToken))
						{
							wi = new System.Security.Principal.WindowsIdentity(logonToken); 
							retVal = wi.Name;					
							loggedIn = true;
							if (!loginChecked)
								lastLoggedIn = true;
						}
					}
				}
			}
			catch (Exception e) {}
            
			currentUser = retVal;
			loginChecked = true;
			return retVal;		
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
        {
           // MessageBox.Show("starting!!!");
            if (System.Environment.HasShutdownStarted) 
                Application.Exit();
            bool dropOut = false;
			int lineNum = 0;	
			try 
			{
             //   MessageBox.Show("s");
                string tcp = getRegVal("clientControlPath").ToString();
                if (tcp != "0" && tcp.Length > 0)
                    controlPath = tcp;
               // if (System.Environment.CommandLine.ToString().ToLower().Contains(controlPath.ToLower())){
                if (!System.Environment.CommandLine.ToString().ToLower().Contains((localControlPath + "\\CCMClient.exe").ToLower()))
                {
                   // MessageBox.Show("[" + System.Environment.CommandLine.ToString().ToLower() + "] [" + (localControlPath + "\\CCMClient.exe").ToLower()) + "]";
                    if (!System.IO.Directory.Exists(localControlPath))
                        System.IO.Directory.CreateDirectory(localControlPath);
                    if (!System.IO.Directory.Exists(localControlPath + "\\setup"))
                        System.IO.Directory.CreateDirectory(localControlPath + "\\setup");

                    // MessageBox.Show("2");
                    if (System.IO.File.Exists(localControlPath + "\\CCMClient.exe"))
                    {
                     //   MessageBox.Show(controlPath + "\\setup\\logoNorm.ico");
                        if (System.IO.File.GetLastWriteTime(localControlPath + "\\CCMClient.exe") != System.IO.File.GetLastWriteTime(System.Environment.CommandLine.Replace("\"", "")))
                        {
                            System.IO.File.Delete(localControlPath + "\\CCMClient.exe");
                            System.IO.File.Copy(System.Environment.CommandLine.Replace("\"", ""), System.IO.Path.GetFullPath("C:\\CCM\\CCMClient.exe"));                        
                            //MessageBox.Show("Making Copy. " + System.IO.File.GetLastWriteTime("C:\\CCM\\CCMClient.exe").ToString() + " " + System.IO.File.GetLastWriteTime(System.Environment.CommandLine.Replace("\"", "")));
                            // Thread.Sleep(5000);
                        }
                        int lino = 1219;
                        try
                        {
                            lino = 1222;
                            if (System.IO.File.Exists(localControlPath + "\\restarter.MOVED"))
                                System.IO.File.Delete(localControlPath + "\\restarter.MOVED");
                            lino = 1225;
                            if (System.IO.File.Exists(localControlPath + "\\restarter.exe"))
                                System.IO.File.Move(localControlPath + "\\restarter.exe", System.IO.Path.GetFullPath(localControlPath + "\\restarter.MOVED"));
                            lino = 1228;
                            if (!System.IO.File.Exists(localControlPath + "\\restarter.exe"))
                                System.IO.File.Copy(controlPath + "\\restarter.exe", System.IO.Path.GetFullPath(localControlPath + "\\restarter.exe"));
                            lino = 1231;
                        }
                        catch (Exception copyE)
                        {
                            eLog("Line: " + lino + "\n" + copyE.Message, System.Diagnostics.EventLogEntryType.Warning);
                        }
                    }
                    else
                    {
                        System.IO.File.Copy(System.Environment.CommandLine.Replace("\"", ""), System.IO.Path.GetFullPath("C:\\CCM\\CCMClient.exe"));
                        System.IO.File.Copy(System.Environment.CommandLine.Replace("\"", "").ToLower().Replace("\\ccmclient.exe", "\\" + defFileName), System.IO.Path.GetFullPath("C:\\CCM\\setup\\fileCheckDefinition.txt"));
                    }
                    if (System.IO.File.Exists(localControlPath + "\\CCMClient.exe"))
                    {
                        doExecute(localControlPath + "\\CCMClient.exe");
                        eLog("Recursively executing. - This should only happen once.", System.Diagnostics.EventLogEntryType.Information);
                        //doExecute();
                        //MessageBox.Show("SWITCH!");
                        dropOut = true;
                    }
                    // else
                    // MessageBox.Show("OOOPS!");
                }
                else
                {
                    if (System.IO.File.Exists(controlPath + "\\setup\\logoNorm.ico"))
                    {
                        //MessageBox.Show(controlPath + "\\setup\\" + defFileName);
                        try
                        {
                            System.IO.File.Delete(localControlPath + "\\setup\\fileCheckDefinition.txt");
                            System.IO.File.Copy(controlPath + "\\" + defFileName, System.IO.Path.GetFullPath(localControlPath + "\\setup\\fileCheckDefinition.txt"));
                            System.IO.File.Delete(localControlPath + "\\setup\\logoNorm.ico");
                            System.IO.File.Copy(controlPath + "\\setup\\logoNorm.ico", System.IO.Path.GetFullPath(localControlPath + "\\setup\\logoNorm.ico"));
                            System.IO.File.Delete(localControlPath + "\\setup\\logoDND.ico");
                            System.IO.File.Copy(controlPath + "\\setup\\logoDND.ico", System.IO.Path.GetFullPath(localControlPath + "\\setup\\logoDND.ico"));
                            System.IO.File.Delete(localControlPath + "\\setup\\logoDisco.ico");
                            System.IO.File.Copy(controlPath + "\\setup\\logoDisco.ico", System.IO.Path.GetFullPath(localControlPath + "\\setup\\logoDisco.ico"));
                            System.IO.File.Delete(localControlPath + "\\setup\\logoDisco.ico");
                            System.IO.File.Copy(controlPath + "\\setup\\logoDisco.ico", System.IO.Path.GetFullPath(localControlPath + "\\setup\\logoDisco.ico"));
                            System.IO.File.Delete(localControlPath + "\\setup\\background.bmp");
                            System.IO.File.Copy(controlPath + "\\setup\\background.bmp", System.IO.Path.GetFullPath(localControlPath + "\\setup\\background.bmp"));
                        }
                        catch (Exception fce)
                        {
                            eLog(fce.Message, System.Diagnostics.EventLogEntryType.Warning);
                        }

                    }
                }
                if (!dropOut)
                {
                    Thread.Sleep(5000);
                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcesses();
                    lineNum = 1;
                    int cnt = 0;
                    lineNum = 2;
                    //System.Diagnostics.Process[] _process = System.Diagnostics.Process.GetProcessesByName("explorer");
                    //System.Security.Principal.WindowsIdentity wi = new System.Security.Principal.WindowsIdentity(process[0].Handle); 
                    getCurrentUser();
                    lineNum = 3;
                    for (int i = 0; i < process.Length; i++)
                    {
                        if (process[i].ProcessName.ToLower().CompareTo(programName.ToLower()) == 0)
                            cnt++;
                    }
                    lineNum = 4;
                    if (System.IO.File.Exists(filePath + "\\" + disableFileName) || (cnt > 1))
                    {
                        if (cnt < 2)
                        {
                            MessageBox.Show("Sorry, this application is currently disabled for maintenance. [" + filePath + "\\" + disableFileName + "]");
                            doExecute(localControlPath + "\\restarter.exe", controlPath + "\\" + programName + ".exe T");
                        }
                        Application.Exit();
                    }
                    else
                    {
                        lineNum = 5;
                        Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                        lineNum = 6;
                        Application.Run(new Form1());
                        lineNum = 7;
                    }
                }	
			}
			catch (Exception e)
			{
                lineNum = 1313;
                eLog(e.ToString() + "\n\n" + "Code POS: " + lineNum, System.Diagnostics.EventLogEntryType.Error);
			}
		}
		public static bool isAppRunning (string which)
		{
			if (getRegVal("debugMode").ToString().CompareTo("True") == 0)
			{
				string retVal = "";
				System.Diagnostics.Process[] processescheck = System.Diagnostics.Process.GetProcesses();
				foreach (System.Diagnostics.Process proc in processescheck)
					retVal += proc.ToString()+"\n";
				debugMessage("Process List: " + retVal);
			}
			int cnt = 0;
			System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(which);			
			if (processes.Length > 0)
			{
				debugMessage(which + " is running.");
				return true;
			}
			else
			{
				debugMessage(which + " is not running.");
				return false;
			}
		}

		public bool closeApp (string which)
		{			
			int cnt = 0;
			

			System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(which);
			debugMessage("Found " + processes.Length + " " + which + " process(es) to close.");
			foreach (System.Diagnostics.Process proc in processes)
			{				
				System.Diagnostics.Process tempProc=System.Diagnostics.Process.GetProcessById(proc.Id);
				debugMessage("line 1327 trying to close process #" + proc.Id);
				tempProc.CloseMainWindow();	
				cnt ++;
			}

			if (kill)
				foreach (System.Diagnostics.Process proc in processes)
				{				
					System.Diagnostics.Process tempProc=System.Diagnostics.Process.GetProcessById(proc.Id);
					debugMessage("line 1336 trying to KILL process #" + proc.Id);
					tempProc.Kill();					
				}
			kill = false;
			if (cnt > 0)
				return true;
			else
				return false;
		}

		private void ddeListener1_OnDDEExecute(object Sender, string[] Commands)
		{
			s="";
			foreach (string s2 in Commands)
			{
				s+=s2;
			}
			//MessageBox.Show("1" + s);
			string[] sarr=s.Split(new char[]{','});
			//System.Decimal test = System.Decimal.Parse(sarr[1]);

			if ((sarr[1].Length < 10) || disablePops.Checked) // || (test < 1))
			{
				//do nothing when number is too small or pops disabled.
			}
			else 
			{
				//this.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width-200,Screen.PrimaryScreen.Bounds.Height-200);
	
				this.userInfo.Text = "Incoming call from: "+sarr[1];	
				debugMessage("DDE Call: "+sarr[1]);
				
				string myString = sarr[1];
				myString = myString.Split("@".ToCharArray())[0];
				myString = myString.Split(":".ToCharArray())[1];
				if (myString.Length < 10)
					myString = sarr[1];
				s = myString;
				sarr[1] = myString; 

				debugMessage("DDE Call Changed to: "+myString);
				this.userInfo.Text = dataTool.searchNumber(Convert.ToDouble(sarr[1]));
				if (this.userInfo.Text.IndexOf("Match") < 1)
				{
					clearWindow();

					showMeTimer.Enabled = true;
					hideMeTimer.Enabled = false;
					autoHideTimer.Enabled = true;				
				}
				this.confirmButton.Visible = false;
				this.noButton.Visible = false;
				if (this.userInfo.Text.IndexOf("(I)") > 0)
				{
					this.userInfo.Text += "\nWould you like to search InOrder?";
					this.confirmButton.Enabled = true;
					this.noButton.Enabled = true;	
					this.confirmButton.Visible = true;
					this.noButton.Visible = true;
				}				
				else
					this.forceHide = true;
				this.Invalidate(true);
				this.CloseButton.Refresh();
				this.label2.Refresh();
				//				this.Size = new System.Drawing.Size(200,200);
				//				MessageBox.Show();
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Sending Test Message...","CCM - Message");
			ddeListener1.sAppName = "CCMClient";
			ddeListener1.sTopic = "Phone_Number";
			ddeListener1.sVal = "2, 7196509644";
			ddeListener1.initiate();
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			setRegVal("popSpeed",refreshSpeed.Value.ToString());
            linkLabel1.Visible = false;
			noButton_Click(sender,e);
			this.sendingFile = false;
			clearWindow();
		}

		private void confirmButton_Click(object sender, System.EventArgs e)
		{
			this.userInfo.Text = "Sending request to InOrder, Please wait...";
			this.Refresh();
			if (!showMeTimer.Enabled)
				hideMeTimer.Enabled = true;
			ddeListener1.sAppName = "InOrder";
			ddeListener1.sTopic = "Phone_Number";
			debugMessage("DDE Call to InOrder to search for: " + s);
			ddeListener1.sVal = "-2, " + s;
			ddeListener1.initiate();			
			this.confirmButton.Enabled = false;
			this.noButton.Enabled = false;			
		}

		private void hideMeTimer_Tick(object sender, System.EventArgs e)
		{
            debugMessage("hideMeTimer_Tick");
			int moveby = 0;
			moveby = (1+(this.refreshSpeed.Value));	
			if ((this.Location.Y + moveby) > Screen.PrimaryScreen.Bounds.Height)
				moveby = Screen.PrimaryScreen.Bounds.Height - this.Location.Y;
			if (this.Location.Y < Screen.PrimaryScreen.Bounds.Height)
				this.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width-200-offset,this.Location.Y+moveby);
			else
				hideMeTimer.Enabled = false;
            debugMessage("hideMeTimer Done Tick");
		}

		private void showMeTimer_Tick(object sender, System.EventArgs e)
		{
            debugMessage("showMeTimer_Tick");
			int moveby = 0;
			moveby = (1+(this.refreshSpeed.Value));
			if ((this.Location.Y - moveby) < Screen.PrimaryScreen.Bounds.Height-210)
				moveby = this.Location.Y-(Screen.PrimaryScreen.Bounds.Height-210);
			if (this.Location.Y > Screen.PrimaryScreen.Bounds.Height-210)
				this.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width-200-offset,this.Location.Y-moveby);
			else 
			{
				showMeTimer.Enabled = false;
				userInfo.Text = userInfo.Text.Replace("\n","\r\n");
				this.Refresh();
			}
            debugMessage("showMeTimer Done Tick");
		}

		private void noButton_Click(object sender, System.EventArgs e)
		{
			this.confirmButton.Enabled = false;
			this.noButton.Enabled = false;
			this.userInfo.Text = "\r\n\r\n\r\n:-)";
			s = "";
			if (!showMeTimer.Enabled)
				hideMeTimer.Enabled = true;			
		}

		private void leave()
		{
			//MessageBox.Show("Sorry, the IBS Screen pop\napplication has been disabled\nfor maintenance. This should\nonly take a few minutes.");
            doExecute(localControlPath + "\\restarter.exe", controlPath + "\\" + programName + ".exe T");
			this.notifyIcon1.Visible = false;
			this.Dispose();
			this.Close();
			Application.Exit();
		}

		public static void doExecute(string what)
		{
			doExecute(what,"",0);
		}
        public static void doExecute(string what,string args)
        {
            doExecute(what, args, 0);
        }
		private static void doExecute(string what,string args,int windowType)
		{
            what = what.Replace("%windir%", System.Environment.SystemDirectory);
			if (what.CompareTo("%clientRestart%") == 0)
			{
                doExecute(localControlPath + "\\restarter.exe", controlPath + "\\" + programName + ".exe T");				
				Application.Exit();		
			}
			else
				if (what.CompareTo("%clientExit%") == 0)
			{
				//doExecute("\\\\isaac\\ibsWorkstation\\restarter.exe","\\\\isaac\\ibsWorkstation\\IBSWorkstation.exe T");				
				Application.Exit();		
			}
			else
				if (what.CompareTo("%playSound%") == 0)
			{
							
				Form1.PlaySound(args,0,(int) (SND.SND_ASYNC | SND.SND_ALIAS | SND.SND_NOWAIT));
			}
			else
			{			
				if (what.IndexOf("%") != 0)
				{
					System.Diagnostics.Process p = new System.Diagnostics.Process();
					p.StartInfo.RedirectStandardOutput=false;
					p.StartInfo.FileName=what;
					p.StartInfo.WorkingDirectory=what.Substring(0,what.Replace("/","\\").LastIndexOf("\\"));
					p.StartInfo.Arguments=args.Replace("%computerName%",System.Net.Dns.GetHostName()).Replace("%homeDirectory%",systemInventory.homeDirectory).Replace("%userName%",currentUser.ToUpper().Replace("VeraciTek\\",""));
					p.StartInfo.UseShellExecute=true;
                    if (windowType == 1)
                        p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
					try 
					{
						debugMessage("Executing: " + what + "\nPath =" + p.StartInfo.WorkingDirectory);
						p.Start();			
						debugMessage("Done Executing: " + what);
					}
					catch (Exception e)
					{
						if (what.Length > 0)
						{
							string mbox = "There was a problem finding or executing: " + p.StartInfo.FileName;
							if (p.StartInfo.Arguments.Length > 0)
								mbox += " with the parameters: " + p.StartInfo.Arguments;
							mbox +=  "\nAdmin Info:\n" + e.Message;		
							debugMessage(mbox);
						}

					}
					p.Dispose(); 
				}
			}
		}


        public static string readTextFile(string name)
        {
            string results = "";
            System.IO.FileInfo dataFile;
            System.IO.StreamReader textStream;
            dataFile = new System.IO.FileInfo(name);
            try
            {
                textStream = dataFile.OpenText();
                results += textStream.ReadToEnd();
                textStream.Close();
            }
            catch (Exception e) { }
            return results;
        }

        public string getProperties(string name, string property)
        {
            return getProperties(name, property.Split(','));
        }
        public string getProperty(string name, string property)
        {
            string[] par = new string[1];
            par[0] = property;
            return getProperties(name, par);
        }
        public string getProperties(string name, string[] property)
        {
            string tempFile = readTextFile(name);
            string tempStr = "";
            string retVal = "";
            string sep = "";
            for (int i = 0; i < property.Length; i++)
            {
                if (tempFile.IndexOf(property[i]) > 0)
                    tempStr = tempFile.Substring(tempFile.IndexOf(property[i]) + property[i].Length);
                else
                    tempStr = "N/A \n";
                if (tempStr.IndexOf("\n") > 0)
                    tempStr = tempStr.Substring(0, tempStr.IndexOf("\n") - 1);
                //MessageBox.Show(name + "-" + tempStr + "-" + property[i] + "\n");
                retVal += sep + tempStr.Replace(",", "#COM#");
                sep = ",";
            }
            return retVal;
        }

        public string prepName(string name)
        {
            string[] holder =name.Split('\\');
            string hold = "";
            hold = holder[0];
            if (holder.Length > 1)
                hold = holder[1];
            return hold.Trim().ToUpper();
        }

        public void popList()
        {

            System.DateTime lr = getLastRefresh();
            guListArray = System.IO.Directory.GetFiles(controlPath + "\\reports","*_report.txt");
            ArrayList guListOnline = new ArrayList();
            ArrayList guListOffline = new ArrayList();
            string lastholder = "";
            //Array.Sort(guListArray);
            sendTo.Items.Clear();
            
            for (int i = 0; i < guListArray.Length; i++)
                if (System.IO.Directory.GetLastWriteTime(guListArray[i].Trim()) >= lr)                    
                    guListOnline.Add(prepName(getProperties(guListArray[i].Trim(), "User: ")));
            guListOnline.Sort();
            for (int i = 0; i < guListArray.Length; i++)
                if (System.IO.Directory.GetLastWriteTime(guListArray[i].Trim()) < lr)
                    guListOffline.Add(prepName(getProperties(guListArray[i].Trim(), "User: ")));
            guListOffline.Sort();

            for (int i = 0; i < guListOnline.Count; i++)
                if (guListOnline[i].ToString().CompareTo(lastholder) != 0)
                {
                    sendTo.Items.Add(guListOnline[i]);
                    lastholder = guListOnline[i].ToString();
                }
            sendTo.Items.Add("        ---offline---    ");
            lastholder = "";
            for (int i = 0; i < guListOffline.Count; i++)
                if (guListOffline[i].ToString().CompareTo(lastholder) != 0)
                {
                    sendTo.Items.Add(guListOffline[i]);
                    lastholder = guListOffline[i].ToString();
                }
        }
        public System.DateTime getLastRefresh()
        {
            return System.IO.Directory.GetLastWriteTime(controlPath + "\\popControlReport.txt");
        }

        public void loadIllegalPrograms(){
            string f;
            bool beg = false;
            string[] temp;
            f = systemInventory.readTextFile(localControlPath + "/setup/fileCheckDefinition.txt");
           // eLog(f);
			string[] resAr;
			resAr = f.Split("\n".ToCharArray());
            string tempHolder = "";
			for (int i=0;i<resAr.Length;i++)
			{
            //    eLog(resAr[i]);
				if (resAr[i].ToLower().Trim().CompareTo("<illegalprograms>") == 0)
					beg = true;
				if (beg) 
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("names") == 0)
					{
                        tempHolder = temp[1].Trim().Replace(";", ",").ToLower();
                      //  eLog(tempHolder);
                        temp = tempHolder.Split(",".ToCharArray());
                        for (int g = 0; g < temp.Length; g++)
                        {
//                            eLog(temp[g]);
                            illegalPrograms.Add(temp[g]);
                        }
						beg = false;
						i = resAr.Length;
					}
				}
			}
            f = "Illegal Programs Loaded:";
            foreach (string prog in illegalPrograms)
                f += "\n" + prog;
            eLog(f);
        }

        public void loadAllowedPrograms()
        {
            string f;
            bool beg = false;
            string[] temp;
            f = systemInventory.readTextFile(localControlPath + "/setup/fileCheckDefinition.txt");
            // eLog(f);
            string[] resAr;
            resAr = f.Split("\n".ToCharArray());
            string tempHolder = "";
            for (int i = 0; i < resAr.Length; i++)
            {
                //    eLog(resAr[i]);
                if (resAr[i].ToLower().Trim().CompareTo("<allowedprograms>") == 0)
                    beg = true;
                if (beg)
                {
                    temp = resAr[i].Split("=".ToCharArray());
                    if (temp[0].ToLower().Trim().CompareTo("names") == 0)
                    {
                        tempHolder = temp[1].Trim().Replace(";", ",").ToLower();
                        //  eLog(tempHolder);
                        temp = tempHolder.Split(",".ToCharArray());
                        for (int g = 0; g < temp.Length; g++)
                        {
                            //                            eLog(temp[g]);
                            allowedPrograms.Add(temp[g]);
                        }
                        beg = false;
                        i = resAr.Length;
                    }
                }
            }
            f = "Allowed Programs Loaded:";
            foreach (string prog in allowedPrograms)
                f += "\n" + prog;
            eLog(f);
        }
		public bool isserver(string compName) 
		{
			string f;
			bool beg = false;
			string[] temp;
            f = systemInventory.readTextFile(localControlPath + "/setup/fileCheckDefinition.txt");
			string[] resAr;
			resAr = f.Split("\n".ToCharArray());	
			for (int i=0;i<resAr.Length;i++)
			{
				if (resAr[i].ToLower().Trim().CompareTo("<servers>") == 0)
					beg = true;
				if (beg) 
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("names") == 0)
					{
						serverList = "," + temp[1].Trim().Replace(";",",").ToLower() + ",";
						beg = false;
						i = resAr.Length;
					}
				}
			}
			return (serverList.IndexOf("," + compName.ToLower() + ",") > -1);
		}
		
		private void doPushThread()
		{
			doPushThread(0);
		}
		public void doPushThread(int how)
		{	
			this.pthow = how;
			System.Threading.ThreadStart mt = new System.Threading.ThreadStart(doPush);
			System.Threading.Thread thread1 = new System.Threading.Thread( mt ) ;
			Form1.debugMessage("Starting doPush (" + pthow + ") Thread...");
			thread1.Start() ;
			Form1.debugMessage("... Done Starting doPush (" + pthow + ")Thread.");
		}
		private void doPush()
		{
			doPush(pthow);
		}
		private void doPush(int how)
		{
			doPush(how,"");
		}

		private string toZero(string val)
		{
			if (val.Length < 1)
				return "0";
			else
				return val;
		}

        private void doIdentify(string say)
        {
            doIdentify(say, "25,5,Yellow,0,5,1");
        }

        private void doIdentify(string say, string args)
        {
            bool doingIt = false;
            IntPtr deskDC = GetWindowDC(IntPtr.Zero);
            try
            {
                if (deskDC != null)
                {
                    Graphics dc = Graphics.FromHdc(deskDC);
                    if (dc != null)
                    {
                        doingIt = true;
                        //System.Drawing.Drawing2D.GraphicsState gs=dc.Save();						
                        //Size size = new Size(1000, 1000);
                        //Image image = new Bitmap(size.Width, size.Height);
                        //Graphics dc = Graphics.FromImage(image);
                        string[] arg = args.Split(',');
                        float fontSize = float.Parse(toZero(arg[0]));
                        float stayFor = float.Parse(toZero(arg[1]));
                        float locationx = 0;
                        float locationy = 0;
                        float locationu = 0;
                        float locationu2 = 0;
                        string fcolor = "Yellow";
                        if (arg.Length >= 3)
                            fcolor = arg[2];
                        if (arg.Length >= 4)
                            locationx = float.Parse(toZero(arg[3]));
                        if (arg.Length >= 5)
                            locationy = float.Parse(toZero(arg[4]));
                        if (arg.Length >= 6)
                            locationu = float.Parse(toZero(arg[5]));
                        if (arg.Length >= 7)
                            locationu2 = float.Parse(toZero(arg[6]));

                        if (fontSize < 5)
                            fontSize = 50;
                        Brush blackBrush = Brushes.Black;
                        Type brushType = typeof(Brushes);

                        Brush theBrush = (Brush)brushType.InvokeMember(fcolor,
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.GetProperty,
                            null, null, new object[] { });

                        Font arialFont = new Font("Arial", fontSize, System.Drawing.FontStyle.Bold);

                        float fsw = dc.MeasureString(say, arialFont).Width;
                        float fsh = dc.MeasureString(say, arialFont).Height;

                        if (locationu > 0)
                            locationx = Screen.PrimaryScreen.Bounds.Width / 2 - ((locationu * fsw) / 2);
                        if (locationu2 > 0)
                            locationy = Screen.PrimaryScreen.Bounds.Height / 2 - ((locationu2 * fsh) / 2);



                        dc.DrawString(say, arialFont, theBrush, locationx, locationy);
                        int i = 0;
                        System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("winlogon");
                        if (process.Length > 0)
                        {
                            if (process[i].ProcessName.ToLower().CompareTo("winlogon") == 0)
                            {
                                int access = 0X00000008;
                                HANDLE loginHandle = IntPtr.Zero;
                                if (OpenProcessToken(process[i].Handle, access, ref loginHandle))
                                {
                                    IntPtr loginDC = GetDC(loginHandle);
                                    Graphics ldc = Graphics.FromHdc(loginDC);
                                    ldc.DrawString(say, arialFont, theBrush, locationx, locationy);
                                }
                            }
                        }

                        //this.clearText.Enabled = true;
                        invalidateForHowLong = stayFor;
                        isInvalidated = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                        //dc.ReleaseHdc(deskDC);
                        //InvalidateRect(IntPtr.Zero,IntPtr.Zero,1);
                        dc.Dispose();
                    }
                }
            }
            catch (Exception ie)
            {
                eLog("Tried to write to screen but couldn't get a handle. - Line 1864 Form1.cs");
            }
            if (!doingIt)
                eLog("Tried to write to screen but couldn't get a handle. - Line 1824 Form1.cs");
            ReleaseDC(deskDC);
        }
		private void doPush(int how,string fname)
		{
			string results="";
			string[] resAr;
			bool isMe = true;
            string theFileName = pushFileName;
			string extraPath = "";
			debugMessage("-Entering doPush");
			if (how == 1)
			{
                theFileName = fname;
				extraPath = "\\messages";
			}
			try 
			{
				//System.IO.FileStream myFile = System.IO.File.Open(filePath + extraPath + "\\" + theFileName,System.IO.FileMode.Open,System.IO.FileAccess.Read,System.IO.FileShare.Read);
				//for (int i=0;i<myFile.Length;i++)
				results += systemInventory.readTextFile(filePath + extraPath + "\\" + theFileName);
				//myFile.Close();
			}
			catch (Exception e)
			{
				debugMessage("There was a problem receiving the package. The package file was opened by an application that won't share.");
			}
           // MessageBox.Show(theFileName);
			//MessageBox.Show(results);
			resAr = results.Split("\n".ToCharArray());
			string[] temp;
			string[] temp2;
			string file = "";
			string args = "";
			string notice = "";
			string es="";
			string message = "";
			string title = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
			string dnsName = System.Net.Dns.GetHostName().ToLower()	;
			string cu = getCurrentUser().ToUpper().Replace("VeraciTek\\","").ToLower();
            string[] cua = getCurrentUser().ToUpper().ToLower().Split('\\');
            if (cua.Length > 1)
                cu = cua[1];
			bool foundMe = false;
            bool isMeViaMachineName = false;
			for (int i=0;i<resAr.Length && isMe;i++)
			{
				//MessageBox.Show(resAr[i]);
				temp = resAr[i].Split("=".ToCharArray());
				//MessageBox.Show(temp.Length + "");
				if (temp.Length > 1)
				{
					if (temp[0].ToLower().Trim() == "users")
						if (temp[1].ToLower().Trim() != "all" && temp[1].ToLower().Trim() != "allservers")
						{							
							//MessageBox.Show(temp[0] +"-"+ temp[1]);
							temp2 = temp[1].Split(",".ToCharArray());
							for (int b=0;b<temp2.Length && !foundMe;b++)
                                if (temp2[b].ToLower().Trim() == dnsName || temp2[b].ToLower().Trim() == cu)
                                {
                                    if (temp2[b].ToLower().Trim() == dnsName)
                                        isMeViaMachineName = true;
                                    foundMe = true;
                                }
							if (!foundMe)
								isMe = false;
						}
						else
						{
							//MessageBox.Show("gotit");
							isMe = false;
							if (temp[1].ToLower().Trim() == "all")
                                if (!isserver(System.Environment.MachineName))
                                {
                                    isMe = true;
                                    isMeViaMachineName = true;
                                }
							if (temp[1].ToLower().Trim() == "allservers")
                                if (isserver(System.Environment.MachineName))
                                {
                                    isMe = true;
                                    isMeViaMachineName = true;
                                }
		
						}
					if (temp[0].ToLower().Trim() == "file")
					{
						file = temp[1].Trim();
						for (int c=2;c < temp.Length; c++)
							file += "=" + temp[c];
					}
					if (temp[0].ToLower().Trim() == "arguments")
						args = temp[1].Trim();
					
					if (temp[0].ToLower().Trim() == "notice")
						notice = temp[1].Trim();					

					if (temp[0].ToLower().Trim() == "kill")
						if (temp[1].Trim().ToLower() == "true")
							kill = true;

					if (temp[0].ToLower().Trim() == "message")
					{
						for (int m=1;m<temp.Length;m++)
						{
							message +=  es + temp[m].Replace("{nl}","\r\n");
							es = "=";
						}
						es="";
						message = message.Trim();
						message = message.Replace("\\n","\n");
					}
					if (temp[0].ToLower().Trim() == "title")
					{
						title = temp[1].Trim();		
					}
					//MessageBox.Show(temp[0] +"-"+ temp[1]);
			
				}
			 		
			}
			if (isMe)
			{
                file = file.Replace("%windir%", System.Environment.SystemDirectory);
             //   MessageBox.Show(file);
				if (file.CompareTo("%sendFile%") == 0)
				{
					if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show(this,title + " is trying to send you a file. Would you like to accept it?","File Transfer",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Question,System.Windows.Forms.MessageBoxDefaultButton.Button1))
					{
						//MessageBox.Show("getting file: " + filePath + "\\messages\\fileTransfer\\" + message);
						this.fileTransferName = filePath + "\\messages\\fileTransfer\\" + message;
						saveFileDialog1.FileName = message;
						saveFileDialog1.ShowDialog();
					}			
				}
				else
					if (file.CompareTo("%showPopup%") == 0)
				{
					gotMessage = true;
					this.messageTimer.Enabled=false;
					if (!this.doNotDisturb.Checked) 
					{
						hideMeTimer.Enabled = false;
						showMeTimer.Enabled = true;
						debugMessage("Showing popup! " + title + " - " + message);
					}
					this.noButton.Visible=false;		
					this.confirmButton.Visible=false;
					clearWindow();				
					messageCount++;
					messageIndex = messageCount - 1;
					if (how == 1 || how == 2)
					{
						this.replyLink.Visible=true;
						if (messageCount > 1)
							this.prevLink.Visible=true;
						this.timeStamp.Visible=true;
						senders.Add(title);
						messages.Add(message);						
						this.timeStamps.Add(System.IO.File.GetLastWriteTime(filePath + extraPath + "\\" + theFileName));
						this.timeStamp.Text=System.IO.File.GetLastWriteTime(filePath + extraPath + "\\" + theFileName).ToString();
						//MessageBox.Show(filePath + extraPath + "\\" + theFileName);
					}						
					this.userInfo.Text = title + ": " + message.Replace("{nl}","\r\n");
					this.menuItem12.Enabled = true;						
					if (this.playSound.Checked)
						playMessageSound();
					if (this.doNotDisturb.Checked && (message != ddmess)  && title.ToLower().Trim() != dnsName.ToLower().Trim() && title.ToLower().Trim()!=cu) 
						sendMessage(title,ddmess);					
					this.lastSender = title;
                    bool doit = true;
                    for (int i = 0; i < sendTo.Items.Count && doit; i++)
                        if (sendTo.Items[i].ToString().ToUpper() == lastSender.ToUpper())
                            doit = false;
                    if (doit)
                        sendTo.Items.Add(lastSender);
					this.Refresh();					
				}
				else
					if (!autoUpdate.Checked && isMeViaMachineName)
					if (message.Length > 0)					
						//	if (file.CompareTo("%showPopup%") != 0)
						MessageBox.Show(message.Replace("\\",""),"VeraciTek Workstation - " + title,System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.ServiceNotification);					
				if (file.CompareTo("%enableSpecialFunctions%") == 0)
				{	
					this.debugMode.Visible = true;
					this.restartButton.Visible = true;
				}
				else
				if (file.CompareTo("%enableSendMessage%") == 0)
					{	
					this.menuItem9.Enabled = true;
					this.sendFileItem.Enabled = true;
					}
				else
					if (file.CompareTo("%identify%") == 0)
				{
                    string u = getCurrentUser();
                    if (u.Split('\\').Length > 1)
                        u = u.Split('\\')[1];

                    u = u + " on " + System.Net.Dns.GetHostName().ToUpper();
                    doIdentify(u);									
				}
				else				
					if (file.CompareTo("%disableSendMessage%") == 0)
					{
						this.menuItem9.Enabled = false;
						this.sendFileItem.Enabled = false;
					}
				else
					if (file.CompareTo("%autoHide%") == 0)
					autoHide_Click();
				else
					if (file.CompareTo("%autoUpdate%") == 0)
					autoUpdate_Click();
				else
					if (file.CompareTo("%autoCloseInOrder%") == 0)
					autoCloseInOrder_Click();
				else
					if (file.CompareTo("%disablePops%") == 0)
					disablePops_Click();
                else
                    if (file.CompareTo("%disableASD%") == 0)
                        disableASD_Click();	
				else
					if (file.CompareTo("%enterSend%") == 0)
					enterSend_Click();
				else
					if (file.CompareTo("%debugMode%") == 0)
					debugMode_Click();	
				else
					if (file.CompareTo("%closeApp%") == 0)
					if (notice.Length < 1)
						closeApp(args.Split(',')[0]);
					else
					{
						int timeOut = 10;
						if (args.Split(',').Length > 1)
							if (si.strToInt(args.Split(',')[1]) > 0)
								timeOut = si.strToInt(args.Split(',')[1]);
						if (isAppRunning(args.Split(',')[0]) && !this.autoCloseInOrder.Checked)
						{
							notifyIcon1.ShowBalloon(notice.Split('~')[0],notice.Split('~')[1],CCMClient.NotifyIconEx.NotifyInfoFlags.Info,timeOut*1000);
							this.appToClose = args.Split(',')[0];
							this.closeProgramTimer.Interval = timeOut*1000;
							this.closeProgramTimer.Enabled = true;					
						}
						//this.showMeTimer.Enabled = true;
						//MessageBox.Show("Enabled timer with: " + timeOut);
					}
				else
					if (notice.Length > 0 && isMeViaMachineName)
				{
					waitingUpdates[0] = file;
					waitingUpdates[1] = args;
					//MessageBox.Show(notice);
					if (notice.Split('~').Length > 1)
					{
						notifyIcon1.ShowBalloon(notice.Split('~')[0],notice.Split('~')[1],CCMClient.NotifyIconEx.NotifyInfoFlags.Info,9999999);
						debugMessage("I showed the balloon: " + notice.Split('~')[0] + " - " + notice.Split('~')[1]);
						notifyIcon1.Icon = notifyIcon1.Icon;
					}
					else
						debugMessage("Invalid Package Syntax: Notice was used but was not split by '~' between the title and text.");
				}
				else
                    if (isMeViaMachineName)
                    {
                        doExecute(file, args);
                        if (title.Length > 23)
                            title = title.Substring(0, 23);
                        if (file.IndexOf("%") != 0)
                            this.notifyIcon1.Text = notifyText + "\n-" + title;
                        updateHistory += "\n" + title + ":" + message;
                    }
			}
		}

		private void messageTimer_Tick(object sender, System.EventArgs e)
		{
            debugMessage("messageTimer_Tick");
			int messageTimeLimit = 15;
            bool makeLinkVisible = false;
			if (System.IO.File.Exists(filePath+"\\messages\\m"+lastSentTo+".txt") && (this.messageTimerCount > messageTimeLimit))
			{
				this.userInfo.Text = "Message was unsuccessful. This could mean several things:\r\n1. A mistyped user name.\r\n2. Their computer is off.\r\n3. Their computer is not configured to receive messages.";
                makeLinkVisible = true;
				updateHistory += " " + this.userInfo.Text + ")";
				messageTimer.Enabled=false;
				messageTimerCount = 0;
			}
			else
			{	
	
				if (!System.IO.File.Exists(filePath+"\\messages\\m"+lastSentTo+".txt"))
				{
					if (!gotMessage)
					{
						this.userInfo.Text = "Message was successfully sent!";
						messageTimer.Enabled=false;
						if (!showMeTimer.Enabled)
							hideMeTimer.Enabled = true;
					}
					else 
					{
						gotMessage = false;
					}
					updateHistory += " " + this.userInfo.Text + ")";
					this.message.Text = "";						
					messageTimerCount = 0;				
					
				}				
				else
					messageTimerCount++;
			}
			clearWindow();
            if (makeLinkVisible)
                linkLabel1.Visible = true;
            debugMessage("messageTimer done Tick");
		}

		private void loadConfiguration()
		{
			string results = "";
			fileReadStat = "";
			bool gAlias = false;
			try
			{
				results = systemInventory.readTextFile(filePath + "\\" + configFileName,true);
				if (results.Length < 1)
					fileReadStat = "Configuration File Load Failed, but system did not report an error.\n";
			}
			catch (Exception e)
			{
				fileReadStat = "Configuration File Load Failed: "+e.Message+"\n";
			}
			string[] resAr;
			string [] temp;
			resAr = results.Split("\n".ToCharArray());		
			//MessageBox.Show(fileReadStat);
			for (int i=0;i<resAr.Length;i++)
			{
				temp = resAr[i].Split("=".ToCharArray());
				if (gAlias)
				{
					if (temp[0].ToLower().Trim().CompareTo("copyright") == 0)
						copyRight = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("programtitle") == 0)
						programTitle = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("logto") == 0)
						logTo = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("ddeserver") == 0)
						DDEServer = temp[1].Trim();	
					if (temp[0].ToLower().Trim().CompareTo("database") == 0)
						database = temp[1].Trim();		
					if (temp[0].ToLower().Trim().CompareTo("orgname") == 0)
						orgName = temp[1].Trim();	
				}
				if (temp[0].ToLower().Trim() == "<sitestrings>")
					gAlias = true;			
				if (temp[0].ToLower().Trim() == "</sitestrings>")
				{
					gAlias = false;			
				}
			}			
		}

		private string getAlias(string who)
		{
			string results = "";
			bool gAlias = false;
			string aName = "";
			string uNames = "";
			fileReadStat = "";
			try 
			{
				results = systemInventory.readTextFile(filePath + "\\" + aliasFileName);
				if (results.Length < 1)
					fileReadStat = "Definition File Load Failed, but system did not report an error.\n";
			}
			catch (Exception e)
			{
				fileReadStat = "Definition File Load Failed: "+e.Message+"\n";
			}
			string[] resAr;
			string [] temp;
			resAr = results.Split("\n".ToCharArray());		
			for (int i=0;i<resAr.Length;i++)
			{
				temp = resAr[i].Split("=".ToCharArray());
				if (gAlias)
				{
					if (temp[0].ToLower().Trim().CompareTo("name") == 0)
						aName = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("users") == 0)
						uNames = temp[1].Trim();
					
				}
				if (temp[0].ToLower().Trim() == "<alias>")
					gAlias = true;			
				if (temp[0].ToLower().Trim() == "</alias>")
				{
					if (who.ToLower().CompareTo(aName.ToLower()) == 0)
						return uNames;
					gAlias = false;			
				}
			}
			return "";
		}
		private void clearWindow()
		{
			this.sendButton.Visible=false;
			this.replyLink.Visible=false;
			this.prevLink.Visible=false;
			this.nextLink.Visible=false;
			this.sendButton.Enabled=false;
			this.sendTo.Visible=false;
			this.confirmButton.Visible=false;
			this.noButton.Visible=false;
			this.userInfo.Visible=true;
			this.message.Visible=false;
			this.toLabel.Visible=false;
			this.refreshSpeed.Visible=false;
			this.fastLabel.Visible=false;
			this.slowLabel.Visible=false;
			this.timeStamp.Visible=false;
            this.linkLabel1.Visible = false;
            this.skipButton.Visible = false;
			this.Refresh();		
		}
		private void sendFile(string sendTo,string fileName)
		{
			bool sentTo = false;
			if (sendTo.Length < 1)
			{
				sendTo = this.sendTo.Text;
				sentTo = true;
			}
			sendTo = sendTo.Trim();
			sendTo = fixUpSendTo(sendTo);			
			string message = "<PACKAGE>\nusers=" + sendTo  + "\nfile=%sendFile%\nmessage=" + fileName + "\ntitle=" + System.Net.Dns.GetHostName() + "\n" + "</PACKAGE>";			
			try 
			{
				System.IO.File.Delete(filePath+"\\messages\\m"+sendTo+".txt");
			}
			catch (Exception e){}
			System.IO.FileStream myFile = System.IO.File.Create(filePath+"\\messages\\m"+sendTo+".txt");
			byte[] myBA = System.Text.Encoding.Default.GetBytes(message.Replace("\n","\r\n"));	
			myFile.Write(myBA,0,myBA.Length);
			myFile.Close();
			
		}

        private void sendMessageAliased(string sendTo, string messageText)
        {
            this.sendTo.Text = sendTo;
            this.message.Text = messageText;
            sendMessage();
        }

		private void sendMessage()
		{
            this.sendTo.Text = this.sendTo.Text.Trim().Replace(";",",");
			string list = "";
			string[] resAr;
            string sentFrom = System.Net.Dns.GetHostName();
            if (Form1.getCurrentUser().Split("\\".ToCharArray()).Length > 1)
                sentFrom = Form1.getCurrentUser().Split("\\".ToCharArray())[1];
			list = getAlias(this.sendTo.Text.Trim());
			updateHistory += "\n"+ sentFrom + ":" + this.message.Text.Replace("\n"," ") + " (to: " + this.sendTo.Text + " - ";
			if (list.Length > 0)
			{
				resAr = list.Split(",".ToCharArray());
				for (int i=0;i<resAr.Length;i++)
					sendMessage(resAr[i],"");
				this.userInfo.Text = "Message sent to\r\naliased user(s)!";
				this.message.Text = "";
				clearWindow();
                CloseButton_Click(null, null);
			}
			else
				if (this.sendTo.Text.IndexOf(",") > 0)
			{
				resAr = this.sendTo.Text.Split(",".ToCharArray());
				for (int i=0;i<resAr.Length;i++)
					sendMessage(resAr[i],"");
				this.userInfo.Text = "Message sent to multiple users. Receipt is not confirmed.";
				this.message.Text = "";
				clearWindow();
                CloseButton_Click(null, null);
			}
			else
				sendMessage("","");					
		}
		private void sendMessage(string sendTo,string messageText)
		{
			gotMessage = false;
			string mtext;
            if (messageText.Length > 0)                         
                mtext = messageText.Replace("\r\n", "{nl}");
            else
                mtext = this.message.Text.Replace("\r\n", "{nl}");
          	bool sentTo = false;
			if (sendTo.Length < 1)
			{
				sendTo = this.sendTo.Text;
				sentTo = true;
			}
			sendTo = sendTo.Trim();
			sendTo = fixUpSendTo(sendTo);			
			lastSentTo = sendTo; //this.sendTo.Text.Trim();
            string sentFrom = System.Net.Dns.GetHostName();
            if (Form1.getCurrentUser().Split("\\".ToCharArray()).Length > 1)
                sentFrom = Form1.getCurrentUser().Split("\\".ToCharArray())[1];
            string message = "<PACKAGE>\nusers=" + sendTo + "\nfile=%showPopup%\nmessage=" + mtext + "\ntitle=" + sentFrom + "\n" + "</PACKAGE>";			
			try 
			{
				System.IO.File.Delete(filePath+"\\messages\\m"+sendTo+".txt");
			}
			catch (Exception e){}			
			System.IO.FileStream myFile = System.IO.File.Create(filePath+"\\messages\\m"+sendTo+".txt");
			byte[] myBA = System.Text.Encoding.Default.GetBytes(message.Replace("\n","\r\n"));	
			myFile.Write(myBA,0,myBA.Length);
			myFile.Close();
			if (sentTo)
				this.messageTimer.Enabled=true;	

			  			
		}
		private void sendButton_Click()
		{
			this.userInfo.Text="Sending Message...";
            bool doit = true;
            for (int i =0; i < sendTo.Items.Count && doit; i++)
                if (sendTo.Items[i].ToString().ToUpper() == this.sendTo.Text.ToUpper())
                    doit = false;
            if (doit)
                sendTo.Items.Add(this.sendTo.Text);
            
			sendMessage();
		}
		private string fixUpSendTo(string val)
		{
			val = val.Trim();			
			string newOne = val[0].ToString();
			bool found = false;
			if (val.Trim().IndexOf(" ",0,val.Trim().Length) > 0)
			{
				for (int i=1;i < val.Length;i++)
				{
					if (found)
						newOne += val[i].ToString();
					if (val[i].ToString().CompareTo(" ") == 0)
						if (val[i+1].ToString().CompareTo(" ") != 0)
							found = true;
				}
				return newOne;
			}
			else
				return val;			
		}
		private void sendButton_Click(object sender, System.EventArgs e)
		{
			//if (sendTo.Text.Trim().IndexOf(" ",0,sendTo.Text.Trim().Length) > 0)		  
            if (sendTo.Text.Length > 0)
            {
                if (!sendingFile)
                    sendButton_Click();
                else
                {
                    sendFileTo = this.sendTo.Text;
                    if (!showMeTimer.Enabled)
                        hideMeTimer.Enabled = true;
                    this.openFileDialog1.ShowDialog();
                    sendingFile = false;
                }
            }
		}	
		private bool checkForMessage(string fname)
		{
			string contents = systemInventory.readTextFile(filePath + "\\" + pushFileName);
			//MessageBox.Show(contents);
			if (contents.IndexOf("%showPopup%") > 0 || contents.IndexOf("%closeApp%") > 0 )
				return true;
			else
				return false;
		}
		private void checkMessage(string Name)
		{
			debugMessage("Check Message " + Name);
			string holder="";
			if (Name.ToLower().Equals(disableFileName.ToLower()))
				leave();
			else
				if (Name.ToLower().Equals(reportFileName.ToLower()))
				si.doReport();
			else
				if (Name.ToLower().Equals(installedSoftwareFileName.ToLower()))
				//doAlarm();
				si.doSoftwareReport();
			else
				if (Name.ToLower().Equals(alarmFileName.ToLower()))
				//doAlarm();
				Name.ToLower();
			else 
				if (Name.ToLower().Equals(pushFileName.ToLower()))
				if (!checkForMessage(Name))
					doPushThread();
				else 
					doPush(2);
			else
				if (Name.ToLower().Equals(versionCheckFileName.ToLower()))
				si.doVersionCheck();
			else 
			{
                getCurrentUser();				
				holder = "m" + currentUser.ToUpper().Replace("VeraciTek\\","").ToLower() + ".txt";
                string cu = "";
                string[] cua = getCurrentUser().ToUpper().ToLower().Split('\\');
                cu = cua[0];
                if (cua.Length > 1)
                    cu = cua[1];
                holder = "m" + cu.ToLower() + ".txt"; 
                System.IO.FileInfo messageFile = new System.IO.FileInfo(filePath + "\\messages\\" + Name);
				if (Name.ToLower().Equals(messageFileName.ToLower()) || Name.ToLower().Equals(holder))
				{
					try 
					{
						doPush(1,Name);
//						System.IO.File.Delete(filePath + "\\messages\\" + Name);
						messageFile.Delete();                        
					}
					catch (Exception e)
					{
						debugMessage("MESSAGE ERROR - A general exception was detected when processing the message file. CCM should recover.");
					}
				}
				//else
				//	MessageBox.Show("here " + Name + "------" + holder);
			}

		}
		private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			checkMessage(e.Name);
		}

		private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
		{
			checkMessage(e.Name);		
		}

		private void fileSystemWatcher2_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			checkMessage(e.Name);
		}

		private void fileSystemWatcher2_Renamed(object sender, System.IO.RenamedEventArgs e)
		{
			checkMessage(e.Name);
		}
		private void menuItem9_Click_1(object sender, System.EventArgs e)
		{
            this.skipButton.Visible = false;
			this.noButton.Visible=false;		
			this.confirmButton.Visible=false;
			this.sendButton.Visible=true;
			this.sendButton.Enabled=true;
			this.sendTo.Visible=true;
			this.userInfo.Visible=false;
			this.message.Visible=true;
			this.toLabel.Visible=true;
			this.Refresh();
			showMeTimer.Enabled = true;		
			hideMeTimer.Enabled = false;
			this.sendTo.Focus();
		}

		private void fileSystemWatcher1_Broken(object sender, System.IO.ErrorEventArgs e)
		{
			if (connected)
			{
				connected = false;
				fileWatcherFixer.Enabled = true;
				debugMessage("Making Icon1 = DISCO");				
				this.notifyIcon1.Icon = Form1.iconDisco;
                conTry = 0;                
				this.menuItem9.Enabled = false;
				this.sendFileItem.Enabled = false;
			}
		}		
		private void fileWatcherFixer_Tick(object sender, System.EventArgs e)
		{
            debugMessage("fileWatcherFixer_Tick");
			try 
			{
                if (System.IO.File.Exists(filePath + "\\setup\\networkReset.txt") && !connected)
                {
                    if (!connected)
                    {
                       // syncTime("Synchronizing time on reconnect.");                        
                    }
                    connected = true;
                    fileWatcherFixer.Enabled = false;
                    this.fileSystemWatcher1.Dispose();
                    this.fileSystemWatcher1 = new System.IO.FileSystemWatcher(filePath, "popControl*.txt");
                    this.fileSystemWatcher1.EnableRaisingEvents = true;
                    this.fileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.FileName;
                    this.fileSystemWatcher1.SynchronizingObject = this;
                    this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
                    this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
                    this.fileSystemWatcher1.Error += new System.IO.ErrorEventHandler(this.fileSystemWatcher1_Broken);
                    this.fileSystemWatcher2.Dispose();
                    this.fileSystemWatcher2 = new System.IO.FileSystemWatcher(filePath + "\\messages", "m*.txt");
                    this.fileSystemWatcher2.EnableRaisingEvents = true;
                    this.fileSystemWatcher2.NotifyFilter = System.IO.NotifyFilters.FileName;
                    this.fileSystemWatcher2.SynchronizingObject = this;
                    this.fileSystemWatcher2.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher2_Renamed);
                    this.fileSystemWatcher2.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher2_Created);
                    this.fileSystemWatcher2.Error += new System.IO.ErrorEventHandler(this.fileSystemWatcher1_Broken);
                    debugMessage("Making Icon1 = NORM");
                    this.notifyIcon1.Icon = Form1.iconNorm;
                    this.menuItem9.Enabled = true;
                    this.sendFileItem.Enabled = true;
                    if (System.IO.File.Exists(filePath + "\\messages\\" + messageFileName))
                        checkMessage(messageFileName);
                    if (conTry > 9)
                    {
                        notifyIcon1.ShowBalloon("VeraciTek Connection Restored", "You are now connected again!", CCMClient.NotifyIconEx.NotifyInfoFlags.Info, 15000);
                        syncTime("Synchronizing time on reconnect.");
                    }
                    this.conTry = 0;
                }
                else
                {
                    conTry++;
                    if (!connected && conTry == 10)
                    {
                        notifyIcon1.ShowBalloon("VeraciTek Connection Problem", "Click here to troubleshoot. You may safely ignore this message if you are not connected to the VeraciTek LAN. Program execution monitoring is still working to protect you from known thumb drive viruses and bad programs.", CCMClient.NotifyIconEx.NotifyInfoFlags.Info, 15000);                        
                    }
                }
			}
			catch (Exception e1){}
            debugMessage("fileWatcherFixer done Tick");
		}	

		private void ExitButton_Click(object sender, System.EventArgs e)
		{
			doRestart("Exit Button Click: Line 2570");
		}
		
		public void doRestart(string fromWhere)
		{
            int lineNum = 2574;
            eLog("doRestart was called from " + fromWhere + ".\nCode POS: " + lineNum);
            doExecute(localControlPath + "\\restarter.exe", controlPath + "\\" + programName + ".exe T");
			this.Dispose();
			this.Close();
			Application.Exit();
		}
		
		

		public static void debugMessage(string message)
		{
			if (getRegVal("debugMode").ToString().CompareTo("True") == 0)
			{
				//MessageBox.Show(message);
				Form1.eLog(message);
			}
		}
		
		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			//MessageBox.Show(getVersionInfo(),"VeraciTek Workstation - System Info");
			message messageWindow = new message(this);
			messageWindow.version.Text = "Version " + cver;
			messageWindow.logoBox2.Height = messageWindow.logoBox.Image.Height;
			messageWindow.logoBox2.Width = messageWindow.logoBox.Image.Width;
			messageWindow.logoBox2.Image = messageWindow.logoBox.Image;
			messageWindow.messageText.Rtf="{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fcharset0 Arial;}}\n{\\colortbl ;\\red0\\green128\\blue0;}\n\\viewkind4\\uc1\\pard\\cf1\\b\\f0\\fs30 Loading System Information... please wait.\\cf0\\b0\\par\n}";
			messageWindow.Icon = this.Icon;
			si.updateUpdatesThread(messageWindow);
			messageWindow.Show();			
			messageWindow.Refresh();					    						
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			doExecute("http://www.veracitek.com");			
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			doExecute("http://www.veracitek.com/helpDesk/");			
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.notifyIcon1.Visible = false;			
		}

		private void enterSend_Click()
		{
			if (enterSend.Checked)
				enterSend.Checked = false;
			else
				enterSend.Checked = true;
			setRegVal("enterSend",enterSend.Checked.ToString());
		}
		private void enterSend_Click(object sender, System.EventArgs e)
		{
			enterSend_Click();
		}
		
		private void autoHide_Click()
		{
			if (autoHide.Checked)
				autoHide.Checked = false;
			else
				autoHide.Checked = true;
			setRegVal("autoHide",autoHide.Checked.ToString());
		}

		private void autoHide_Click(object sender, System.EventArgs e)
		{
			autoHide_Click();
		}

		private void autoHideTimer_Tick(object sender, System.EventArgs e)
		{
			if (autoHide.Checked || forceHide)
			{
				debugMessage("Auto Hide Called");	
				noButton_Click(sender,e);
			}
			forceHide = false;
			autoHideTimer.Enabled = false;
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			this.fileSystemWatcher1.Path = filePath;	
		}
		
		private void autoUpdate_Click()
		{
			if (autoUpdate.Checked)
				autoUpdate.Checked = false;
			else
				autoUpdate.Checked = true;
			setRegVal("autoUpdate",autoUpdate.Checked.ToString());
		}

		private void autoUpdate_Click(object sender, System.EventArgs e)
		{
			autoUpdate_Click();
		}

		private void autoCloseInOrder_Click()
		{
			if (autoCloseInOrder.Checked)
				autoCloseInOrder.Checked = false;
			else
				autoCloseInOrder.Checked = true;
			setRegVal("autoCloseInOrder",autoCloseInOrder.Checked.ToString());
		}
		
		private void autoCloseInOrder_Click(object sender, System.EventArgs e)
		{
			autoCloseInOrder_Click();
		}

        private void disableASD_Click(object sender, EventArgs e)
        {
            disableASD_Click();
        }
        private void disableASD_Click()
        {
            if (disableASD.Checked)
                disableASD.Checked = false;
            else
                disableASD.Checked = true;
            setRegVal("disableAutoShutDown", disableASD.Checked.ToString());
            wasDASDChecked = disableASD.Checked;
        }
		private void disablePops_Click()
		{
			if (disablePops.Checked)
				disablePops.Checked = false;
			else
				disablePops.Checked = true;
			setRegVal("disablePops",disablePops.Checked.ToString());
		}
		private void disablePops_Click(object sender, System.EventArgs e)
		{
			disablePops_Click();			
		}


		public static void setRegVal(string name, string val)
		{			
			Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software",true);
			Microsoft.Win32.RegistryKey newkey = key.CreateSubKey(orgName);
			newkey.SetValue(name, val);
		}
		public static Object getRegVal(string name)
		{
			return getRegVal(name,"");
		}
	
		public static Object getRegVal(string name,string specified)
		{	
			Object retVal = null;
			Microsoft.Win32.RegistryKey pRegKey = Microsoft.Win32.Registry.LocalMachine;			
			if (specified.Length < 1)
				pRegKey = pRegKey.OpenSubKey("Software\\" + orgName);
			else
				pRegKey = pRegKey.OpenSubKey(specified);
			if (pRegKey != null)
				retVal = pRegKey.GetValue(name);
			if (retVal == null)
				retVal = "0";
			return retVal;
		}
		private void debugMode_Click()
		{
			if (debugMode.Checked)
				debugMode.Checked = false;
			else
				debugMode.Checked = true;
			setRegVal("debugMode",debugMode.Checked.ToString());
		}
		private void debugMode_Click(object sender, System.EventArgs e)
		{
			debugMode_Click();			
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(dataTool.searchNumber(1817407387),"VeraciTek Workstation - Message");
		}

		private void menuItem6_Click_1(object sender, System.EventArgs e)
		{
			doExecute("http://wits/dotnet/corporate/helpDesk/clientHelp.aspx");			
		}

		private void message_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//MessageBox.Show("HERE:"+message.Lines.Length);
		}

		private void message_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode.Equals(System.Windows.Forms.Keys.Return) && enterSend.Checked)
				sendButton_Click();
		}


		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			clearWindow();
			this.fastLabel.Visible=true;
			this.slowLabel.Visible=true;
			this.refreshSpeed.Visible=true;
			
			this.hideMeTimer.Enabled=false;
			this.showMeTimer.Enabled=true;
			this.Invalidate(true);
			this.Refresh();
		}

		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
            if (this.doNotDisturb.Checked)
                this.doNotDisturb_Click();
            else
                if (this.appToClose.Length > 0 && this.closeProgramTimer.Enabled == true)
                {
                    this.appToClose = "";
                    this.closeProgramTimer.Enabled = false;
                }
                else
                    menuItem9_Click_1(sender, e);
                //doExecute("", "http://www.veracitek.com 500 600 1");
			//menuItem9_Click_1(sender,e);
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{			
			//SetStyle(ControlStyles.EnableNotifyMessage, true);
            if (connected)
            {
                si.doSoftwareReport();
                si.doReport();
            }
		}

		private void Form1_Click(object sender, System.EventArgs e)
		{
			this.Refresh();
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void Form1_MouseEnter(object sender, System.EventArgs e)
		{
			this.Refresh();
		}

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string tempText = "";
            if (e.KeyCode == System.Windows.Forms.Keys.R)
                rKeyDown = true;
            if ((e.Control) && (!e.Alt) && (!e.Shift))
            {// only CTRL key was down
                if (e.KeyCode == Keys.F1)
                {
                    tempText = message.Text;
                    message.Text = "\r\n\r\n             ...searching...";
                    message.Refresh();
                    popList();
                    message.Text = tempText;
                    message.Refresh();
                }
            }
        }
  

		private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Escape)
				noButton_Click(sender,e);
			if (e.KeyCode == System.Windows.Forms.Keys.R)
				rKeyDown = false;
		}

		private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) 
				if (rKeyDown)
					doExecute("http://www.veracitek.com");
			
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.timeStamp.Visible = false;
			this.sendTo.Text = lastSender;
			this.menuItem9_Click_1(sender,e);
			this.message.Focus();			
		}
		private void goToMessage(int dir)
		{
			if (messageCount > 0)
			{
				clearWindow();
				if ((messageIndex+dir) < 0)
					messageIndex = messageCount - 1;
				else
					messageIndex = messageIndex + dir;
				this.replyLink.Visible=true;
				if (messageIndex > 0 && messageCount > 1)
					this.prevLink.Visible=true;
				if (messageIndex < (messageCount - 1))
					this.nextLink.Visible=true;
				this.lastSender = senders[messageIndex].ToString();
				this.timeStamp.Visible=true;
				this.userInfo.Text = senders[messageIndex].ToString() + ": " + messages[messageIndex].ToString();
				this.timeStamp.Text = timeStamps[messageIndex].ToString();
				this.Refresh();			
			}
		}
		private void prevLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			goToMessage(-1);
		}

		private void nextLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			goToMessage(1);
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			hideMeTimer.Enabled = false;
			showMeTimer.Enabled = true;
			goToMessage(0);
		}

		private void sendFileItem_Click(object sender, System.EventArgs e)
		{
			sendingFile = true;
			this.noButton.Visible=false;		
			this.confirmButton.Visible=false;
			this.sendButton.Visible=true;
			this.sendButton.Enabled=true;
			this.sendTo.Visible=true;
			this.userInfo.Visible=false;
			this.message.Visible=false;
			this.toLabel.Visible=true;
			this.Refresh();
			showMeTimer.Enabled = true;		
			hideMeTimer.Enabled = false;
			this.sendTo.Focus();			
		}

		private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string fname = openFileDialog1.FileName;
			fname = fname.Substring(fname.LastIndexOf("\\")+1);
			//MessageBox.Show(fname);
			
			System.IO.File.Copy(openFileDialog1.FileName,filePath + "\\messages\\fileTransfer\\" + fname,true);
			sendFile(this.sendFileTo,fname);
		}

		private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			System.IO.File.Copy(this.fileTransferName,saveFileDialog1.FileName,true);
			if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show(this,"File saved. Do you want to open it?","File Transfer",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Question,System.Windows.Forms.MessageBoxDefaultButton.Button1))
				doExecute(saveFileDialog1.FileName);
		}
		private void playSound_Click()
		{
			if (playSound.Checked)
				playSound.Checked = false;
			else
				playSound.Checked = true;
			setRegVal("playSound",playSound.Checked.ToString());
		}
		private void playSound_Click(object sender, System.EventArgs e)
		{
			playSound_Click();

		}

		private void doNotDisturb_Click()
		{
			if (doNotDisturb.Checked)
			{
				doNotDisturb.Checked = false;
				this.notifyIcon1.Icon = Form1.iconNorm;	
			}
			else 
			{
				doNotDisturb.Checked = true;				
				this.notifyIcon1.Icon = Form1.iconDND;		
			}
			setRegVal("doNotDisturb",doNotDisturb.Checked.ToString());
		}

		private void doNotDisturb_Click(object sender, System.EventArgs e)
		{
			doNotDisturb_Click();

		}

        public void playMessageSound()
        {
              System.Media.SystemSounds.Asterisk.Play();
              //MessageBox.Show("tried to play sound");
            /*
            Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.Users;
            Microsoft.Win32.RegistryKey rkey1 = rkey.OpenSubKey(SID.ShowUserSID(Form1.currentUser) + "\\AppEvents\\Schemes\\Apps\\.Default\\New Chat Notification\\.Current");
            string fileName = "";
            if (rkey1 != null)
            {
                try
                {
                    fileName = rkey1.GetValue("").ToString();
                    if (fileName.Length < 1)
                    {
                        rkey1 = rkey.OpenSubKey("AppEvents\\Schemes\\Apps\\.Default\\New Chat Notification\\.Default");
                        fileName = rkey1.GetValue("").ToString();
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message); 
                    debugMessage(e.Message);
                }
            }
            else
            {
                rkey1 = rkey.OpenSubKey(".DEFAULT\\AppEvents\\Schemes\\Apps\\.Default\\.Default\\.Current");
                try
                {
                    fileName = rkey1.GetValue("").ToString();
                }
                catch (Exception e) { }
            }
            //MessageBox.Show(fileName);
            if (fileName.Length > 0 && System.IO.File.Exists(fileName))
                Form1.PlaySound(fileName, 0, (int)(SND.SND_ASYNC | SND.SND_ALIAS | SND.SND_NOWAIT));

            MessageBox.Show(fileName); */
        }

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			try 
			{	
				notifyIcon1.ShowBalloon("Test Title","Test Message",CCMClient.NotifyIconEx.NotifyInfoFlags.Info,10);
				//balloon myBalloon = new balloon();
				//NotifyBalloonDemo.NotifyIcon notifyIcon = new NotifyBalloonDemo.NotifyIcon();	
				//myBalloon.ShowBalloon(1, "My Title", "My Text", 15000);				
				//NotifyIconEx notifyIcon = new NotifyIconEx();
				//notifyIcon.ShowBalloon("Test Title","Test Message",NotifyIconEx.NotifyInfoFlags.Info,10);
				//notifyIcon1.Icon = notifyIcon1.Icon;
			}
			catch (Exception e2) 
			{
				MessageBox.Show(e2.Message,"VeraciTek Workstation - Exception Message 2");
				debugMessage(e2.Message);
			}
			//this.notifyIcon3.Icon = this.notifyIcon1.Icon;
			//this.notifyIcon1.Icon = notifyIcon2.Icon;
			//this.notifyIcon2.Icon = this.notifyIcon3.Icon;			
			//playMessageSound();	
		}

		private void notifyIcon1_BalloonClick(object sender, System.EventArgs e)
		{
            if (powerOutBalloon)
                powerOutBalloon = false;
			if (this.appToClose.Length > 0 && this.closeProgramTimer.Enabled == true)
			{
				this.closeProgramTimer.Enabled = false;
				this.appToClose = "";
				kill = false;
				
			}
			if (waitingUpdates[0].Length > 0)
			{
				doExecute(waitingUpdates[0],waitingUpdates[1]);
				waitingUpdates[0] = "";
			}
            if (!connected)
            {
                connections con = new connections(this);
                con.Show();
                con.Refresh();
            }
		}

		private void closeProgramTimer_Tick(object sender, System.EventArgs e)
		{
			//MessageBox.Show("HERE! " + this.appToClose.Length);
			if (this.appToClose.Length > 0)
			{				
				if (closeApp(appToClose))
					notifyIcon1.ShowBalloon("Application Notice","The application: " + appToClose + " was closed.",CCMClient.NotifyIconEx.NotifyInfoFlags.Info,30000);
				appToClose = "";
			}
			closeProgramTimer.Enabled = false;
		}

        private bool checkPower()
        {
            threadPing pinger = new threadPing();
            string result = pinger.doPing("10.10.1.15", 500);
            string result2 = "-1";
            //MessageBox.Show(result);
            if (result != "-1")
                return true;
            else {
                result2 = pinger.doPing("10.10.1.222", 500);
                if (result2 != "-1")
                    return false;
                else
                    return true;
                }            
        }

        private void askMeShow()
        {
            this.noButton.Visible = false;
            this.confirmButton.Visible = false;
            this.sendButton.Visible = false;
            this.sendButton.Enabled = false;
            this.sendTo.Visible = false;
            this.userInfo.Visible = false;
            this.message.Visible = false;
            this.toLabel.Visible = false;
            this.Refresh();
            this.skipButton.Visible = true;
            this.showMeTimer.Enabled = true;
            this.hideMeTimer.Enabled = false;
            this.skipButton.Enabled = true;
            this.skipButton.Focus();            
        }

        public void syncTime(string why)
        {
            syncTime(why, false);
        }

        public void syncTime(string why,bool force)
        {
            TimeSpan ts = DateTime.Now - lastSync;

            if (ts.Minutes > 1)
            {
                if (ts.Minutes < 5)
                    lastTime = DateTime.Now;
                lastSync = DateTime.Now;
                doExecute("%windir%\\w32tm.exe", "/resync", 1);
                eLog(why);
            }
            
        }

		private void trayIconCheck_Tick(object sender, System.EventArgs e)
		{
			//MessageBox.Show("hi");
			//notifyIcon1.Visible = false;			
			//notifyIcon1.Visible = true;
			//fileSystemWatcher1.
            bool power = true;
            TimeSpan ts = new TimeSpan();
            TimeSpan tsNew = new TimeSpan();
            ts = System.DateTime.Now - lastTime;
            
            //MessageBox.Show("Comparing: " + System.DateTime.Now.ToString() + " to " + lastTime.ToString() + " resulted in: " + Math.Abs(ts.Hours) + " hours difference.");
        

            if (Math.Abs(ts.Hours) > 2 && (fixTimeCount < 1 || timeNeedsChanged))
            {
                if (timeNeedsChanged)
                {
                    tsNew = System.DateTime.Now - newTime;
                    if (Math.Abs(tsNew.Hours) > 2)
                    {
                        timeNeedsChanged = false;
                        newTime = System.DateTime.Now;
                    }
                    fixTimeCount = 0;
                }
                else
                {
                    newTime = System.DateTime.Now;
                    syncTime("A time difference of " + ts.Hours.ToString() + " hours was detected. Synchronizing time.");
                    fixTimeCount = 5;
                    timeNeedsChanged = true;
                }
            }
      

            if (!timeNeedsChanged)
                lastTime = System.DateTime.Now;
            
            fixTimeCount--;
            if (Math.Abs(ts.Hours) == 0)
                fixTimeCount = 0;

            if (!wasDASDChecked && dasdwh.Checked && System.DateTime.Now.DayOfWeek != DayOfWeek.Saturday && System.DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                if (System.DateTime.Now.Hour > 6 && System.DateTime.Now.Hour < 17)
                {
                    ASDinternalDisable = true;
                    /* disableASD.Checked = true;
                    if (!disableASD.Text.Contains("(auto set)"))
                        disableASD.Text = disableASD.Text + " (auto set)"; */
                }
                else
                    if (!skipButtonClicked)
                    {
                        ASDinternalDisable = false;
                        /* disableASD.Checked = false;
                        if (!disableASD.Text.Contains("(auto set)"))
                            disableASD.Text = disableASD.Text.Replace(" (auto set)", ""); */
                    }
            }
            if (pdTimer.Enabled)
                powerDownTimer-=(trayIconCheck.Interval/1000);
            if (connected)
                power = checkPower();
            if (power)
            {
                if (pdTimer.Enabled)
                    skipButton_Click();
                powerOutBalloon = false;
                pdTimer.Enabled = false;
                trayIconCheck.Interval = 30000;
                lastAskMe = false;
                ASDinternalDisable = false;
                //disableASD.Checked = wasDASDChecked;
                lastPower = powerDownMaxFail;
                skipButtonClicked = false;
            }
            if (!((lastPower > 0) || power || !connected))
            {
                
                if (!powerOutBalloon)
                {
                    pdTimer.Interval = powerDownTimeOut * 1000;
                    
                    string dasd = "";
                    string lastWarning = getRegVal("powerDownWarning","0").ToString();
                    bool nagBlock = false;
                    
                    if (lastWarning.Length > 5)
                    {
                        System.TimeSpan lastBug = System.DateTime.Parse(lastWarning).Subtract(System.DateTime.Now);                    
                        if (lastBug.Minutes < 30)
                            nagBlock = true;
                    }
                    if (!nagBlock && !disableASD.Checked && (!ASDinternalDisable || (askMeSD.Checked && !lastAskMe)))
                    {
                        if (askMeSD.Checked)
                        {
                            ASDinternalDisable = false;
                            //disableASD.Checked = false;
                            lastAskMe = true;
                            askMeShow();
                        }
                        if (!pdTimer.Enabled)
                            powerDownTimer = (pdTimer.Interval / 1000);
                        if (powerDownTimer > 600)
                            dasd = "If power is not restored automatic shutdown will commence in " + (powerDownTimer / 60) + " minute(s).";
                        else
                            dasd = "If power is not restored automatic shutdown will commence in " + (powerDownTimer) + " second(s).";
                        pdTimer.Enabled = true;
                        trayIconCheck.Interval = 10000;
                    }
                    notifyIcon1.ShowBalloon("POWER IS OUT!", "POWER IS OUT! SAVE YOUR WORK NOW! Your computer may shut off at any moment. "+ dasd, CCMClient.NotifyIconEx.NotifyInfoFlags.Info, 15000);
                    lastPower = powerDownMaxFail;
                    powerOutBalloon = true;
                    setRegVal("powerDownWarning", System.DateTime.Now.ToString()); 
                }
            }
            else
                if (!power)
                    lastPower -= 1; // powerDownMaxFail - !power;
            getCurrentUser();
			if (Form1.loggedIn && !Form1.lastLoggedIn)
				this.doRestart("Form1.loggedIn && !Form1.lastLoggedIn: line 3130");			
			System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcesses();				
			bool doIt = true;
			for (int i=0;i<process.Length && doIt;i++)
				if (process[i].ProcessName.ToLower().CompareTo(programName.ToLower()) == 0)
				{
					doIt = false;
					//MessageBox.Show((process[i].WorkingSet) + " - " + process[i].PeakVirtualMemorySize);
					//if (process[i].WorkingSet64 < 18000000)
					//	this.doRestart("process[i].WorkingSet64 < 18000000: line 3139");	
				}
        //Uncomment below to enable the header bar notification about power going off.
            /*   if (pdTimer.Enabled && (powerDownTimer < pdTimer.Interval))
                if (!disableASD.Checked)
                    if (powerDownTimer < 600)
                        doIdentify(powerDownTimer + " second(s) remaining until automatic shut down.");
                    else
                        doIdentify((powerDownTimer / 60) + " minute(s) remaining until automatic shut down.");
                else
                    doIdentify(""); */

		}

		private void invalidatedCheck_Tick(object sender, System.EventArgs e)
		{	
			
			if (isInvalidated > 0 && ((DateTime.UtcNow - new DateTime(1970,1,1,0,0,0)).TotalSeconds - isInvalidated) > invalidateForHowLong)
			{
				InvalidateRect(IntPtr.Zero,IntPtr.Zero,1);
				isInvalidated = 0;
			}
		}

		public string[] SplitByString(string testString, string split) 
		{ 
			int offset = 0; 
			int index = 0; 
			int[] offsets = new int[testString.Length + 1];  

			while(index < testString.Length) 
			{ 
				int indexOf = testString.IndexOf(split, index); 
				if ( indexOf != -1 )  
				{ 
					offsets[offset++] = indexOf; 
					index = (indexOf + split.Length); 
				} 
				else 
				{ 
					index = testString.Length; 
				}
			}

			string[] final = new string[offset+1]; 
			if (offset == 0 ) 
			{ 
				final[0] = testString; 
			} 
			else 
			{ 
				offset--; 
				final[0] = testString.Substring(0, offsets[0]); 
				for(int i = 0; i < offset; i++) 
				{ 
					final[i + 1] = testString.Substring(offsets[i] + split.Length, offsets[i+1] - offsets[i] - split.Length); 
				} 
				final[offset + 1] = testString.Substring(offsets[offset] + split.Length);
			} 
			return final; 
		}

        private void menuItem2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(getVersionInfo(),"VeraciTek Workstation - System Info");
            connections con = new connections(this);
            con.Show();
            con.Refresh();
        }

        private void pdTimer_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("shutdown");

            if (!checkPower() && !disableASD.Checked && !ASDinternalDisable)
            {
                doIdentify("Shutting down because the power is out.");                
                if (!hibernateInstead.Checked)
                    doExecute("%windir%\\shutdown.exe", "-s -t 30 -c \"The power has gone out and this computer is shutting down. Save all of your work immediately! Contact HelpDesk if you think this is in error.\"");
                else
                    doExecute("%windir%\\rundll32.exe", "powrprof.dll,SetSuspendState Hibernate");                    
                eLog("Shutdown initiated by power down.", System.Diagnostics.EventLogEntryType.Information);
                pdTimer.Enabled = false;
            }
            else
            {
                //doIdentify("Shutdown was cancelled. Either power has returned or you have disabled auto shutdown.");
                eLog("Shutdown aborted.", System.Diagnostics.EventLogEntryType.Information);
                pdTimer.Enabled = false;
            }
                
        }

        private string getCommandLineByProcessID(int processID)
        {
            string cl = "";
            using (ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + processID))
            {
                foreach (ManagementObject mo in mos.Get())
                {
                    cl += mo["CommandLine"];
                }
            }
            return cl;
        }

        private bool checkArgs(int processID){
            string cl = getCommandLineByProcessID(processID);            
            foreach (string invalid in illegalPrograms)
                if (cl.ToLower().Contains(invalid.ToLower()))
                    return true;
            return false;
        }
        private bool notInAllowed(string fn){
            foreach (string prog in allowedPrograms){
                if (fn.ToLower().Contains(prog.ToLower()))
                {                    
                    return false;
                }
                //MessageBox.Show(fn.ToLower() + " --> " + prog);
            }
            return true;
            
        }
        private void processChecker_Tick(object sender, EventArgs e)
        {
            	System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcesses();				
			    bool doIt = true;
                string fn = "";
                string em = "";
                string newName = "";
                bool moveFile = true;
                int howFar = 0;

                System.IntPtr handle = (System.IntPtr)FindWindowEx(0, 0, "#32770", "PS Monitor");
                if (handle == psWindow)
                    handle = (System.IntPtr)FindWindowEx((int)psWindow, 0, "#32770", "PS Monitor");
                if ((GetWindowLongA(handle, GWL_STYLE) & TARGETWINDOW) == TARGETWINDOW)
                    {
                        SendMessage((int)handle, WM_COMMAND, WM_CLOSE, 0);
                        hpPrintCounter++;                        
                        if(showPrintMessages.Checked)
                            notifyIcon1.ShowBalloon("HP Lab Printer Count", hpPrintCounter.ToString() + " prints handled by PS Monitor. Annoying notification has been disabled via CCM.", CCMClient.NotifyIconEx.NotifyInfoFlags.Info, 15000);
                        if (handle != null)
                            if ((int)handle > 0)
                                psWindow = handle;
                    }

                for (int i = 0; i < process.Length && doIt; i++)
                {
                    if (!lastProcessList.Contains(process[i].Id) && !menuItem11.Checked)
                    try
                    {
                        howFar = 1;
                        //howFar = process[i].Id;
                        try
                        {
                            fn = process[i].MainModule.FileName.ToLower();            
                        }
                        catch (Exception ze)
                        {
                            //must ignore local service processes on vista cause vista is mean.
                            fn = "c:\\dunno";
                        }
                        howFar = 2;
                        if (checkArgs(process[i].Id) || illegalPrograms.Contains(fn.Split(new char[] { '\\' })[fn.Split(new char[] { '\\' }).Length - 1]) || notInAllowed(fn))
                        {
                            moveFile = false;
                            howFar = 3;
                            if (illegalPrograms.Contains(fn.Split(new char[] { '\\' })[fn.Split(new char[] { '\\' }).Length - 1]))
                                moveFile = true;
                            fn = "Attempt to run: " + process[i].MainModule.FileName.ToString().Replace("\\n", "\\N") + " by " + currentUser.ToString() + " was terminated on " + System.Net.Dns.GetHostName().ToString() + ". It was opened with this command line which may be disallowed: " + getCommandLineByProcessID(process[i].Id).ToString().Replace("\\n", "\\N") + ".";
                            em = process[i].MainModule.FileName + " is running from an unauthorized location or is an illegal program and has been terminated. Download or installation of programs without permission is a violation of the VeraciTek ICT Acceptable Use Policy. If you are trying to run an important program please request assistance via the Help Desk.";
                            howFar = 4;

                            newName = process[i].MainModule.FileName.ToLower();
                            process[i].Kill();
                            if (moveFile)
                            {
                                System.IO.FileInfo theFile = new System.IO.FileInfo(newName);
                                try
                                {
                                    process[i].WaitForExit(5000);
                                    newName = System.IO.Path.GetFullPath(newName) + ".BADperVeraciTek";
                                    if (System.IO.File.Exists(newName))
                                        System.IO.File.Delete(newName);
                                    theFile.MoveTo(newName);
                                    fn += " A rename of the file to: " + newName + " has been attempted.";
                                    em += " A rename of the file to: " + newName + " has been attempted.";
                                }
                                catch (Exception fre)
                                {
                                    eLog(fre.Source + "\n" + fre.Message + "\n\n" + theFile.FullName + "--->" + newName);
                                    fn += " Rename failed.";
                                    em += " Rename failed.";
                                }
                            }
                            howFar = 5;
                            if (lastFileError.CompareTo(fn) != 0)
                            {
                                eLog(fn, System.Diagnostics.EventLogEntryType.Warning);
                                sendMessageAliased("ict", fn);
                                MessageBox.Show(em);                                
                                lastFileError = fn;
                            }
                        }
                        else
                        {
                            lastProcessList.Add(process[i].Id);
                            howFar = 6;
                        }
                    }
                    catch (Exception pe)
                    {
                        if (!pe.Message.Contains("Unable to enumerate the process modules."))
                            eLog(pe.Source + " got this far: " + howFar + "\n\n" + pe.Message + "\n\n" + pe.StackTrace);                        
                        //do nothing - system and system idle fail here.
                    }
                }           
                
        }
        public void setMenuItem11(bool which)
        {
            menuItem11.Checked = which;
           // MessageBox.Show(menuItem11.Checked.ToString());
        }

        public bool getMenuItem11()
        {
           return menuItem11.Checked;
        }

        private void menuItem11_Click_1(object sender, EventArgs e)
        {
            /*
            string Prompt = "Enter Password"; 
            string Title = "Password"; 
            string Default = "";
            if (!menuItem11.Checked)
            {
                Int32 XPos = ((SystemInformation.WorkingArea.Width / 2) - 200);
                Int32 YPos = ((SystemInformation.WorkingArea.Height / 2) - 100);
                String Result = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Title, Default, XPos, YPos);
                if (Result == "allow it")
                {
                    menuItem11.Checked = true;
                }
            }
            else
            {
                menuItem11.Checked = false;
            }
            */
            dialogForm diag = new dialogForm(this);
            diag.Show();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Visible = false;
            menuItem9_Click_1(sender, e);
        }

        private void showPrintMessages_Click(object sender, EventArgs e)
        {
            showPrintMessages.Checked = !showPrintMessages.Checked;
        }

        private void askMeSD_Click(object sender, EventArgs e)
        {
            askMeSD_Click();
        }

        private void askMeSD_Click()
        {
            if (askMeSD.Checked)
                askMeSD.Checked = false;
            else
                askMeSD.Checked = true;
            setRegVal("askMeSD", askMeSD.Checked.ToString());
        }
        private void skipButton_Click()
        {
            clearWindow();
            doExecute("%windir%\\shutdown.exe", "-a");
            trayIconCheck.Interval = 30000;
            showMeTimer.Enabled = false;
            hideMeTimer.Enabled = true;
            //            disableASD.Checked = true;
            ASDinternalDisable = true;
            skipButtonClicked = true;
        }
        private void skipButton_Click(object sender, EventArgs e)
        {
            skipButton_Click();
        }

        private void hibernateInstead_Click(object sender, EventArgs e)
        {
            hibernateInstead_Click();
        }
        private void hibernateInstead_Click()
        {
            MessageBox.Show("The hibernate option will only work if your computer is configured to support hibernation. There will be no final 30 second warning timer for this option.");
            if (hibernateInstead.Checked)
                hibernateInstead.Checked = false;
            else
                hibernateInstead.Checked = true;
            setRegVal("hibernateInstead", hibernateInstead.Checked.ToString());
        }

        private void dasdwh_Click(object sender, EventArgs e)
        {
            dasdwh_Click();
        }
        private void dasdwh_Click()
        {
            wasDASDChecked = disableASD.Checked;
            if (dasdwh.Checked)
                dasdwh.Checked = false;
            else
                dasdwh.Checked = true;
            setRegVal("dasdwh", dasdwh.Checked.ToString());
        }

    }
}
