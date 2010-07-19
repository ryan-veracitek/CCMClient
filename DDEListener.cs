using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using CCMClient.Win32;
using System.Runtime.InteropServices;

namespace CCMClient.DDE
{
	/// <summary>
	/// Listen for DDE Execute Messages.
	/// </summary>
	public class DDEListener : System.ComponentModel.Component,IDisposable
	{
		private System.ComponentModel.Container components = null;
		//this class inherits Windows.Forms.NativeWindow and provides an Event for message processing
		protected DummyWindowWithMessages m_Window=new DummyWindowWithMessages();
		protected string m_AppName;
		protected string m_ActionName;
		public string sAppName;
		public string sTopic;
		public string sVal;
		public bool sDDEHandled = true;
		public IntPtr theMessage;
		public DDEListener(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			m_Window.ProcessMessage+=new MessageEventHandler(MessageEvent);
		}

		public DDEListener()
		{
			InitializeComponent();
			if (!DesignMode)
			{
				m_Window.ProcessMessage+=new MessageEventHandler(MessageEvent);
			}
		}

		public DDEListener(string AppName,string ActionName) : this()
		{
			m_AppName=AppName;
			m_ActionName=ActionName;
		}

		public new void Dispose()
		{
			m_Window.ProcessMessage-=new MessageEventHandler(MessageEvent);
		}

		/// <summary>
		/// Event is fired after WM_DDEExecute
		/// </summary>
		public event DDEExecuteEventHandler OnDDEExecute;

		/// <summary>
		/// The Application Name to listen for
		/// </summary>
		public string AppName
		{
			get{return m_AppName;}
			set{m_AppName=value;}
		}

		/// <summary>
		/// The Action Name to listen for
		/// </summary>
		public string ActionName
		{
			get{return m_ActionName;}
			set{m_ActionName=value;}
		}

	#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		protected bool isInitiated=false;

		/// <summary>
		/// Processing the Messages
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="m"></param>
		/// <param name="Handled"></param>
		/// 
		public void initiate () 
		{
			sDDEHandled = false;
			ushort a1=Win32.Kernel32.GlobalAddAtom(Marshal.StringToHGlobalAnsi(sAppName));
			ushort a2=Win32.Kernel32.GlobalAddAtom(Marshal.StringToHGlobalAnsi(sTopic));			
			IntPtr po=Win32.User32.PackDDElParam((int)Msgs.WM_DDE_INITIATE,(IntPtr)a1,(IntPtr)a2);
			Win32.User32.SendMessage((IntPtr)(-1),(int)Msgs.WM_DDE_INITIATE,m_Window.Handle,po);
		}

		public void doExec(IntPtr winHan)
		{	
			//	MessageBox.Show("HERE: " + sVal);
			IntPtr pV=Win32.Kernel32.GlobalLock(theMessage);
			Win32.User32.PostMessage(winHan,(int)Msgs.WM_DDE_EXECUTE,m_Window.Handle,pV);			
			Win32.Kernel32.GlobalUnlock(theMessage);
			sDDEHandled = true;
		}		

		protected void MessageEvent(object sender,ref Message m,ref bool Handled)
		{
			if ((m.Msg==(int)Win32.Msgs.WM_DDE_ACK))
			{
				if (!sDDEHandled)
				{
					//MessageBox.Show("Ack 1");
					//IntPtr pV=Win32.Kernel32.GlobalLock(m.LParam);			
					
					//MessageBox.Show(System.Runtime.InteropServices.Marshal.PtrToStringAnsi(theMessage));
					try 
					{
						theMessage = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(sVal);
					}
					catch (Exception ioexcept)
					{
						MessageBox.Show(ioexcept.Message + " - " + ioexcept.ToString());
					}
					//Win32.Kernel32.GlobalUnlock(m.LParam);
					//MessageBox.Show(System.Runtime.InteropServices.Marshal.PtrToStringAnsi(theMessage));
					doExec(m.WParam);
				}
				else
				{
					//MessageBox.Show("Ack with nothing to send.");
					Win32.User32.PostMessage(m.WParam,(int)Msgs.WM_DDE_TERMINATE,m_Window.Handle,(IntPtr)0);
				}
				Handled=true;
			}


			//A client wants to Initiate a DDE connection
			if ((m.Msg==(int)Win32.Msgs.WM_DDE_INITIATE))
			{
				System.Diagnostics.Debug.WriteLine("WM_DDE_INITIATE!");
				//Get the ATOMs for AppName and ActionName
				ushort a1=Win32.Kernel32.GlobalAddAtom(Marshal.StringToHGlobalAnsi(m_AppName));
				ushort a2=Win32.Kernel32.GlobalAddAtom(Marshal.StringToHGlobalAnsi(m_ActionName));
				
				//The LParam of the Message contains the ATOMs for AppName and ActionName
				ushort s1 = (ushort)(((uint)m.LParam) & 0xFFFF);
				ushort s2 = (ushort)((((uint)m.LParam) & 0xFFFF0000) >> 16);
				
				//Return when the ATOMs are not equal.
				if ((a1!=s1)||(a2!=s2)) return;

				//At this point we know that this application should answer, so we send
				//a WM_DDE_ACK message confirming the connection
				IntPtr po=Win32.User32.PackDDElParam((int)Msgs.WM_DDE_ACK,(IntPtr)a1,(IntPtr)a2);				
				Win32.User32.SendMessage(m.WParam,(int)Msgs.WM_DDE_ACK,m_Window.Handle,po);
				//Release ATOMs
				Win32.Kernel32.GlobalDeleteAtom(a1);
				Win32.Kernel32.GlobalDeleteAtom(a2);
				isInitiated=true;
				Handled=true;
			}

			//After the connection is established the Client should send a WM_DDE_EXECUTE message
			if ((m.Msg==(int)Win32.Msgs.WM_DDE_EXECUTE))
			{
				System.Diagnostics.Debug.WriteLine("WM_DDE_EXECUTE!");
				//prevent errors
				if (!isInitiated) return;
				//LParam contains the Execute string, so we must Lock the memory block passed and
				//read the string. The Marshal class provides helper functions
				IntPtr pV=Win32.Kernel32.GlobalLock(m.LParam);				
				//MessageBox.Show("1");
				//s3=System.Runtime.InteropServices.Marshal.P;				
				string s3="";
				s3=System.Runtime.InteropServices.Marshal.PtrToStringAnsi(pV); //HERE'S WHERE I CHANGED THE CONVERSION
				theMessage = pV;
				//MessageBox.Show("2" + theMessage + " " + pV + "-" + s3);
				Win32.Kernel32.GlobalUnlock(m.LParam);
				//After the message has been processed, a WM_DDE_ACK message is sent
				IntPtr lP=Win32.User32.PackDDElParam((int)Win32.Msgs.WM_DDE_ACK,(IntPtr)1,m.LParam);
//				Win32.User32.PostMessage(m.WParam,(int)Win32.Msgs.WM_DDE_ACK,m_Window.Handle,lP);				
				System.Diagnostics.Debug.WriteLine(s3);
				//MessageBox.Show("3"+s3);
				//now we split the string in Parts (every command should have [] around the text)
				//the client could send multiple commands
				string[] sarr=s3.Split(new char[]{'[',']'});		
				//MessageBox.Show("4");	
				if (sarr.GetUpperBound(0)>-1)
				{				
					//MessageBox.Show("5");
					//and fire the event, passing the array of strings
					if (OnDDEExecute!=null) OnDDEExecute(this,sarr);
				}
				Handled=true;
			}

			//After the WM_DDE_EXECUTE message the client should Terminate the connection
			if (m.Msg==(int)Win32.Msgs.WM_DDE_TERMINATE)
			{
				System.Diagnostics.Debug.WriteLine("WM_DDE_TERMINATE");
				if (!isInitiated) return;
				//Confirm termination
				Win32.User32.PostMessage(m.WParam,(int)Win32.Msgs.WM_DDE_TERMINATE,m_Window.Handle,(IntPtr)0);
				Handled=true;
				isInitiated=false;				
			}

			//if (!Handled && m.Msg != 28)
				//MessageBox.Show("HERE:"+m.Msg);
		}
	}

	public delegate void DDEExecuteEventHandler(object Sender, string[] Commands);
}
