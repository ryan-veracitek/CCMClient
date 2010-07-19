using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CCMClient
{
    public partial class connections : Form
    {
        public Form1 parent;
        public string ipTT;
        public string nameTTT;
        public bool changeColorTT;
        public connections(Form1 pparent)
        {
            parent = pparent;
            InitializeComponent();
        }

        private void connections_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //MessageBox.Show("Map Drive Created");
        }
        private void processSite(string ip, string name)
        {
            processSite(ip, name, false);
        }

        private void jumpTo(object sender, EventArgs e)
        {
            Panel p = new Panel();
            Label l = new Label();
            PictureBox pic = new PictureBox();
            if (sender.GetType().Equals(p.GetType()))
            {
                p = (Panel)sender;
                Form1.doExecute("http://" + p.Tag);
            }
            if (sender.GetType().Equals(l.GetType()))
            {
                l = (Label)sender;
                Form1.doExecute("http://" + l.Tag);
            }
            if (sender.GetType().Equals(pic.GetType()))
            {
                pic = (PictureBox)sender;
                Form1.doExecute("http://" + pic.Tag);
            }
        }
        private void processSite2()
        {
            processSite2(ipTT, nameTTT, changeColorTT);
        }
        private void processSite2(string ip, string name, bool changeColor)
        {
            processSite(ip, name, changeColor);
        }

        private void processSiteNT(string ip, string name, bool changeColor)
        { 
            string result = "";
            threadPing pinger = new threadPing();
            try
            {
                result = pinger.doPing(ip,4000);
            //    MessageBox.Show(result);
                string name1 = "pic" + name;
                string name2 = "name" + name;
                string name3 = "panel" + name;
                PictureBox pb1 = null;
                Panel p1 = null;
                string tms = result;
                //MessageBox.Show(result);
                if (result != "-1")
                {
                    pb1 = (PictureBox)this.Controls.Find(name1, true)[0];
                    pb1.Image = picHolder.Image;
                    this.Controls.Find(name2, true)[0].Text = this.Controls.Find(name2, true)[0].Text + " (" + Int32.Parse(tms.Replace(".", "")) + "ms)\n(" + ip + ")";
                    this.Controls.Find(name1, true)[0].Tag = ip;
                    this.Controls.Find(name2, true)[0].Tag = ip;
                    this.Controls.Find(name3, true)[0].Tag = ip;
                    this.Controls.Find(name1, true)[0].DoubleClick += new System.EventHandler(this.jumpTo);
                    this.Controls.Find(name2, true)[0].DoubleClick += new System.EventHandler(this.jumpTo);
                    this.Controls.Find(name3, true)[0].DoubleClick += new System.EventHandler(this.jumpTo);
                    if (changeColor)
                        p1.BackColor = Color.Green;
                }
                else
                {
                    p1 = (Panel)this.Controls.Find(name3, true)[0];
                    this.Controls.Find(name2, true)[0].Text = this.Controls.Find(name2, true)[0].Text + "\n(" + ip + ")";
                    p1.BackColor = Color.Red;
                }
                //picPC
                //  picPC.Image = picHolder.Image; 
                this.toolStripProgressBar1.Increment(8);
                this.Refresh();
            }
            catch (Exception e)
            {
                Form1.debugMessage(e.Message);
            }
        }

        private void processSite(string ip, string name, bool changeColor)
        {
          //  ipTT = ip;
         //   nameTTT = name;            
         //   changeColorTT = changeColor;
           
           
            System.Threading.ThreadStart mt = delegate { processSiteNT(ip, name, changeColor); };
           // System.Threading.ThreadStart mt = new System.Threading.ThreadStart(processSite);
            System.Threading.Thread thread1 = new System.Threading.Thread(mt);
            Form1.debugMessage("Starting processSiteThread...");
            thread1.Start();
            Form1.debugMessage("... Done Starting processSite Thread.");
        }

        private void checkConnections()
        {
            string ip = "";
            ip = parent.si.getIPAddress();
           // System.Diagnostics.Process proc = new System.Diagnostics.Process();
           // proc.EnableRaisingEvents = false;
           // proc.StartInfo.FileName = "ping";
            processSite(ip, "PC",true);
            if (ip == "127.0.0.1")
                panelPC.BackColor = Color.Red;

            processSite("192.168.1.12", "Reception");
            
            //if (picReception.Image == picHolder.Image && picHall.Image == picHolder.Image)
            //    picStaff.Image = picHolder.Image;
            //else
            //    panelPrimaryStaff.BackColor = Color.Red;
            processSite("192.168.2.1", "ISP");
            processSite("192.168.1.2", "WiFi");
            processSite("192.168.1.3", "RemoteAccess");
            processSite("192.168.1.1", "Firewall");
            processSite("192.168.1.4", "FileServer");
            processSite("192.168.1.5", "UserFileServer");
            processSite("192.168.1.6", "ComputerLab");
            processSite("74.125.113.99", "Google");
            processSite("68.180.206.184", "Yahoo");
            processSite("67.215.65.132", "Microsoft"); //207.46.197.32
            processSite("192.168.1.7", "StaffPrint");
            processSite("192.168.1.8", "Staff");
            processSite("192.168.1.9", "web");
            processSite("192.168.1.10", "Admin");
            processSite("192.168.1.11", "PDC");
                   
            
            this.toolStripProgressBar1.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try { checkConnections(); }
            catch (Exception e2)
            {
                Form1.debugMessage(e2.Message);
            }
        }




    }
}