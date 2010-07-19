using System;
using System.Runtime.InteropServices;

namespace CCMClient
{
	/// <summary>
	/// Summary description for systemInventory.
	/// </summary>	
	/// 
	public class systemInventory
	{
		public Form1 parent;
		public message mw;
		public static string homeDirectory;
		public int errorCount = 0;
		public systemInventory(Form1 pparent)
		{
			//
			// TODO: Add constructor logic here
			//
			parent = pparent;
			if (homeDirectory == null)
				homeDirectory = "";
			if (homeDirectory.Length < 1)
				homeDirectory = "C:\\";
			
		}
		public class displaySettings
		{
			public string refreshRate;
			public string colorDepth;
			public string height;
			public string width;
		}

		public struct OSVERSIONINFO 
		{
			public int dwOSVersionInfoSize;
			public int dwMajorVersion;
			public int dwMinorVersion;
			public int dwBuildNumber;
			public int dwPlatformId;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)]
			public string szCSDVersion;
		}

		[DllImport("kernel32.Dll")] 
		public static extern short GetVersionEx(
			ref OSVERSIONINFO o);

		public string doFileChecks()
		{ 
			string retVal = "";
			string results = "";
			parent.fileReadStat = "";
			/*
			retVal += "Internet Explorer: " + fileVersionChecker(System.Environment.SystemDirectory + "\\shdocvw.dll")+"\n";
			retVal += "InOrder: " + fileVersionChecker("C:\\inorder\\inorder.exe") + "\n";
			retVal += "Phone Client: " + fileVersionChecker("C:\\Program Files\\Interactive Intelligence\\clientA.exe");
			*/			
			try 
			{
				
				results = readTextFile(Form1.filePath + "\\" + Form1.defFileName);
				if (results.Length < 1)
					parent.fileReadStat = "Definition File Load: Failed, but system did not report an error.\n";
			}
			catch (Exception e)
			{
				parent.fileReadStat = "Definition File Load: Failed: "+e.Message+"\n";
			}
			string[] resAr;
			resAr = results.Split("\n".ToCharArray());			
			bool begin = false;
			bool wmiBegin = false;
			bool rkeyBegin = false;
			bool getOS = false;
			bool getdotnet = false;
			bool getRR = false;
			bool getM = false;
			string[] wmiMin=null;
			string title = "";
			string path = "";
			string product = "";
			string key = "";
			string patch = "";
			string sval = "";
			string search = "";
			string name = "";
			string wmiItem = "";
			string criteria = "";
			string divisor = "";
			string wmiDataType = "";
			string units = "";
			string wmiLocation = "";
			string location = "";
			string[] date = null;
			string[] version = null;
			string[] temp;
			bool required = false;
			string vresult = "";		
			int lineNumber = 0;
			Form1.debugMessage("Now checking files...");
			for (int i=0;i<resAr.Length;i++)
			{
				if (getRR)
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim() == "min")
						parent.refreshRateMin = temp[1].Split(",".ToCharArray());
					getRR = false;
				}
				if (getM)
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim() == "min")
						parent.memoryMin = temp[1].Split(",".ToCharArray());
					getM = false;
				}

				

				if (getdotnet)
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim() == "version")
						parent.dotnetVersionStandard = temp[1].Split(",".ToCharArray());
					getdotnet = false;
				}
				
				if (getOS)
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim() == "version")
						parent.osVersionStandard = temp[1].Split(",".ToCharArray());
									

					if (temp[0].ToLower().Trim() == "sp")
						parent.osSPStandard = temp[1].Split(",".ToCharArray());
					getOS = false;
		
				}
				if (begin)
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("required") == 0)
						if (temp[1].Trim()=="1")
							required=true;
					if (temp[0].ToLower().Trim().CompareTo("title") == 0)
						title = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("name") == 0)
						name = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("path") == 0)
						path = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("date") == 0)
						date = temp[1].Split(",".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("version")==0)
						version = temp[1].Split(",".ToCharArray());
				}
				if (wmiBegin)
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("wmiitem") == 0)
						wmiItem = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("criteria") == 0)
						criteria = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("divisor") == 0)
						divisor = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("wmidatatype") == 0)
						wmiDataType = temp[1].Trim();					
					if (temp[0].ToLower().Trim().CompareTo("units") == 0)
						units = temp[1].Trim();
					
					if (temp[0].ToLower().Trim().CompareTo("wmilocation") == 0)
						wmiLocation = temp[1].Trim();	
					if (temp[0].ToLower().Trim().CompareTo("min")==0)
						wmiMin = temp[1].Split(",".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("title") == 0)
						title = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("required") == 0)
						if (temp[1].Trim()=="1")
							required=true;
				}
				if (rkeyBegin)
				{
					temp = resAr[i].Split("=".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("path") == 0)
						path = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("sval") == 0)
						sval = temp[1].Trim();					
					if (temp[0].ToLower().Trim().CompareTo("product") == 0)
						product = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("key") == 0)
						key = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("location") == 0)
						location = temp[1].Trim();		
					if (temp[0].ToLower().Trim().CompareTo("patch") == 0)
						patch = temp[1].Trim();					
					if (temp[0].ToLower().Trim().CompareTo("version") == 0)
						version = temp[1].Split(",".ToCharArray());
					if (temp[0].ToLower().Trim().CompareTo("title") == 0)
						title = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("search") == 0)
						search = temp[1].Trim();
					if (temp[0].ToLower().Trim().CompareTo("required") == 0)
						if (temp[1].Trim()=="1")
							required=true;
				}
				if (resAr[i].ToLower().Trim().CompareTo("<dotnet>") == 0)
					getdotnet=true;
				if (resAr[i].ToLower().Trim().CompareTo("</dotnet>") == 0)
					getdotnet=false;
				if (resAr[i].ToLower().Trim().CompareTo("<refresh rate>") == 0)
					getRR=true;
				if (resAr[i].ToLower().Trim().CompareTo("</refresh rate>") == 0)
					getRR=false;
				if (resAr[i].ToLower().Trim().CompareTo("<memory>") == 0)
					getM=true;
				if (resAr[i].ToLower().Trim().CompareTo("</memory>") == 0)
					getM=false;
				if (resAr[i].ToLower().Trim().CompareTo("<os>") == 0)
					getOS=true;
				if (resAr[i].ToLower().Trim().CompareTo("</os>") == 0)
					getOS=false;
				if (resAr[i].ToLower().Trim().CompareTo("<rkey>") == 0)
					rkeyBegin = true;
				if (resAr[i].ToLower().Trim().CompareTo("</rkey>") == 0)
				{		
					rkeyBegin = false;	
					vresult = regCheck(path,product,key,patch,sval,version,location);
					if (search.Length > 0)
					{
						if ((vresult.ToLower().Trim().IndexOf(search.ToLower().Trim(),0,vresult.Length)) == -1)
						{
							vresult = "Search value not found";
							if (required)
							{
								errorCount++;
								vresult += " ---bad--- This is a required component.";
							}
						}
						else
							vresult = "Installed";
					}
					if (vresult.Length < 1)						
					{
						vresult = "Not Installed";
						if (required)
						{
							errorCount++;
							vresult += " ---bad--- This is a required component.";
						}
					}
					retVal += "\n"+ title + ": " + vresult;
					search = "";
					vresult = "";
					key = "";
					product = "";
					path = "";
					patch="";
					location="";
					version = null;
					sval="";
					required = false;
					title = "";
				}
				if (resAr[i].ToLower().Trim().CompareTo("<wmi>") == 0)
					wmiBegin = true;
				if (resAr[i].ToLower().Trim().CompareTo("</wmi>") == 0)
				{					
					wmiBegin = false;
					Form1.debugMessage("Checking WMI result..." + wmiDataType + "-" + wmiLocation+ "-" +wmiItem+ "-" +criteria+ "-" +divisor);	
					if (wmiItem.Length > 0 && wmiLocation.Length > 0)
						vresult = wmiQuery(wmiLocation,wmiItem,criteria,divisor);
					if (wmiDataType.CompareTo("DateTime") == 0)
					{
						Form1.debugMessage("Fixing DateTime on: " + vresult);	
						vresult = fixDateTime(vresult).ToString();
					}
					else
						Form1.debugMessage("WOOPS:" + wmiDataType);	
					if (wmiMin != null)
					if (wmiMin.Length > 0)
						try
						{
							vresult = vresult + units + checkMin(wmiItem,vresult,wmiMin);
						}
						catch (Exception e)
						{
							//MessageBox.Show(e.Message);
						}
					else
						vresult +=  units;
					if (required)
						if (vresult.Length < 1 || (vresult.CompareTo("Not Found") == 0))
						{
							errorCount++;
							vresult = "Not Found ---bad--- This is a required component";					
						}
					retVal += "\n"+ title + ": " + vresult;
					//fileReadStat += "Definition File: " + retVal + "\n";
					title = "";
					path = "";
					divisor="";
					units="";
					name = "";
					wmiItem = "";
					wmiDataType="";
					wmiLocation = "";
					criteria = "";
					wmiMin = null;
					version = null;
					date = null;
					required=false;
				}
				if (resAr[i].ToLower().Trim().CompareTo("<file>") == 0)
				{
					begin=true;
					//fileReadStat += "BON\n";
				}
				string sep = "\\";
				if (resAr[i].ToLower().Trim().CompareTo("</file>") == 0)
				{					
					begin = false;	
					if (name.IndexOf("*",0,name.Length)>-1)
					{
						name=getNewestFile(name,path.Replace("%windir%",System.Environment.SystemDirectory));
						path="";
						sep = "";
					}					
					vresult = fileVersionChecker(path.Replace("%windir%",System.Environment.SystemDirectory)+sep + name,true,version,date);					
					if (required)
						if (vresult.Length < 1 || (vresult.CompareTo("Not Found") == 0))
						{
							errorCount++;
							vresult = "Not Found ---bad--- This is a required component";					
						}
					retVal += "\n"+ title + ": " + vresult;
					//fileReadStat += "Definition File: " + retVal + "\n";
					title = "";
					path = "";
					name = "";
					version = null;
					date = null;
					required=false;
				}

				//retVal += resAr[i];
			}			
			return retVal;
		}
		
		public string fileVersionChecker(string tfile)
		{
			return fileVersionChecker(tfile,false,null,null);
		}
		
		public string getNewestFile(string name,string path)
		{
			string[] myfiles;
			string retVal = "";
			DateTime dt = new DateTime();
			DateTime dt2;
			int lineNum=0;
			string[] extensions = name.Split(',');
			string[] paths = path.Split(',');
			try 
			{
				lineNum=1;
				//System.Windows.Forms.MessageBox.Show(extensions.Length.ToString());
				for(int idx=0;idx<extensions.Length;idx++)
				{
					//System.Windows.Forms.MessageBox.Show(name + " - " + extensions[idx]);
					if (paths.Length == extensions.Length)
						path=paths[idx];
					if (System.IO.Directory.Exists(path))
					{
						lineNum=2;
						myfiles=System.IO.Directory.GetFiles(path,extensions[idx]);
						lineNum=3;
						//System.Windows.Forms.MessageBox.Show(path + " - " + extensions[idx] + " - " + myfiles.Length);
						if (myfiles.Length > 0)
							if (idx > 0)
							{
								if (System.IO.Directory.GetLastWriteTime(myfiles[0]) > dt)
								{
									dt = System.IO.Directory.GetLastWriteTime(myfiles[0]);
									name = myfiles[0];								
								}
							}
							else 
							{
								dt = System.IO.Directory.GetLastWriteTime(myfiles[0]);
								name = myfiles[0];								
							}					
						for(int i=1;i<myfiles.Length;i++)
						{
							dt2 = System.IO.Directory.GetLastWriteTime(myfiles[i]);
							if (dt2 > dt)
							{
								name = myfiles[i];
								//System.Windows.Forms.MessageBox.Show(name + " - " + extensions[idx]);
								dt = dt2;
							}								
						}			
					retVal = name; //.Replace(path,"");				
					//System.Windows.Forms.MessageBox.Show("USING THIS: " + name + " " + idx.ToString());
					}				
				}
			}
			catch (Exception e) 
			{ 
			//	System.Windows.Forms.MessageBox.Show("getNewestFile error: " + e.Message +"\n"+ e.StackTrace);
				Form1.debugMessage("getNewestFile error: " + e.Message +"\n"+ e.StackTrace);
				retVal = "";
			}
			//System.Windows.Forms.MessageBox.Show("USING THIS: " + retVal);
			return retVal;
		}
		public string fileVersionChecker(string tfile,bool check,string[] FileVersion,string[] Date)
		{	
			Form1.debugMessage("File Version Checker..." + tfile + " " + check);			
			string retVal = "";
			string fdate = "";
			try 
			{
				fdate = new System.IO.FileInfo(tfile).LastWriteTime.ToShortDateString() + "";
				retVal += System.Diagnostics.FileVersionInfo.GetVersionInfo(tfile).FileVersion + " - " + fdate ;
				if (check)
				{
					if (FileVersion != null)
						if (!checkVer(System.Diagnostics.FileVersionInfo.GetVersionInfo(tfile).FileVersion,FileVersion))
						{
							errorCount++;
							retVal += "\n---badVersion---\n-Yours:["+System.Diagnostics.FileVersionInfo.GetVersionInfo(tfile).FileVersion+"] Standard:["+showA(FileVersion)+"]";
						}
					if (Date != null)
						if (!checkVer(fdate,Date))
						{
							errorCount++;
							retVal += "\n---badDate---\n-Yours:["+fdate+"] Standard:["+showA(Date)+"]";					
						}
				}
				
			}
			catch (Exception e) { retVal += "Not Found"; }
			Form1.debugMessage("DONE! File Version Checker..." + tfile + " " + check);			
			return retVal;
		}
		public static string readTextFile(string name) 
		{
			return readTextFile(name,false);
		}

		public static string readTextFile(string name,bool forceDebugSkip)
		{
			string results = "";

			System.IO.FileInfo dataFile;							
			System.IO.StreamReader textStream;
			dataFile = new System.IO.FileInfo(name);
			textStream = dataFile.OpenText();
			if (!forceDebugSkip)
				Form1.debugMessage("Starting to read " + name + ".");
			results += textStream.ReadToEnd();
			if (!forceDebugSkip)
				Form1.debugMessage("Done reading " + name + ".\n-------\n" + results );
			textStream.Close();
			
			/* System.IO.FileStream myFile = System.IO.File.Open(name,System.IO.FileMode.Open,System.IO.FileAccess.Read,System.IO.FileShare.Read);
			for (int i=0;i<myFile.Length;i++)
				results += (char)myFile.ReadByte();
			myFile.Close();		*/
			
			return results;				
		}
		
		public string implode(string[] var, string sep)
		{
			string retVal = "";
			string spacer = "";
			for (int i=0;i<var.Length;i++)
			{
				retVal += spacer + var[i];
				spacer = sep + " ";
			}
			return retVal;
		}
		private String wmiQuery(string location, string item)
		{
			return wmiQuery(location,item,"","");
		}
		private String wmiQuery(string location, string item, string criteria,string divisor)
		{
			Form1.debugMessage("WMI Query..." + location + " " + item + " " + criteria + " " + divisor);			
			String retVal = "";									
			String tq = "";
			int loopCount = 0;
			int lineCount = 0;
			try
			{
				String sep = "";
				String temp = "";
				float mydiv = 0;
				float holder = 0;
				try
				{
					mydiv = float.Parse(divisor);
				}
				catch (Exception e){}

				tq = "Select * from " + location;
				if (criteria.Length > 0)
					tq += " where " + criteria.Replace(" is ","=");
				//MessageBox.Show(tq);
				System.Management.ManagementObjectCollection queryCollection = null; 
				Form1.debugMessage("WMI Query: " + tq);
				System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(tq) ;				
				Form1.debugMessage("Getting WMI Collection: " + tq);
				lineCount = 1;
				queryCollection = searcher.Get(); 
				lineCount = 2;
//				if (queryCollection.Count < 1)   //THIS MAKES THINGS BREAK :-(
//					Form1.debugMessage("Nothing Returned for Query: " + location + " " + item + " " + criteria + " " + divisor);
				lineCount = 3;
				foreach( System.Management.ManagementObject mo in queryCollection ) 
				{
					loopCount++;
					Form1.debugMessage("ITEM ... " + location + " " + item + " " + criteria + " " + divisor);
					if (mo[item] != null)
					{
						try
						{
							temp =  mo[item].ToString();
							Form1.debugMessage("Got Value:" + temp + "[" + location + " " + item + " " + criteria + " " + divisor + "]");
							//MessageBox.Show(sep + temp.Replace(":","%colon%"));
							if (mydiv != 0)
							{
								holder=0;
								try
								{
									holder = float.Parse(temp);
								}
								catch (Exception e){}
								holder = holder/mydiv;
								temp = System.Math.Round(holder,3).ToString();
							}
							if (temp.Length > 0)
								retVal += sep + temp; //.Replace(":","%colon%"); 							
						}
						catch (Exception e)
						{
							Form1.debugMessage("WMI Query Error 1 ("+loopCount+") ..." + e.Message + "\n" + location + " " + item + " " + criteria + " " + divisor + "\n" + e.Source + "\n" + e.StackTrace);
							retVal += sep + e.Message;
						}
					}
					else
						Form1.debugMessage("WMI Query returned a null. " + location + " " + item + " " + criteria + " " + divisor);
					lineCount = 4;
					sep = ", ";
					lineCount = 5;
				}		
			}
			catch (Exception e)
			{
				Form1.debugMessage("WMI Query Error 2("+loopCount+"-"+lineCount+") ..." + e.Message + "\n" + location + " " + item + " " + criteria + " " + divisor + "\n" + e.Source + "\n" + e.StackTrace);
				retVal += e.Message + "*" + tq;
			}
			Form1.debugMessage("Done WMI Query..." + location + " " + item + " " + criteria + " " + divisor);			
			return retVal;
		}

		private String getMemory()
		{
			return wmiQuery("Win32_ComputerSystem","totalphysicalmemory");
		}
		public float getColorDepth()
		{
			displaySettings[] ds = getScreenInfo();	
			//System.Windows.Forms.MessageBox.Show("-->" + ds.Length + ds[0].colorDepth);
			return this.strToFloat(ds[0].colorDepth);
		}
		public displaySettings[] getScreenInfo()
		{				
			int i = 0;			
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("select * from win32_videocontroller");
			foreach (System.Management.ManagementObject videocontroller in searcher.Get()) 
			{
				if (videocontroller["CurrentRefreshRate"] != null)
					i++;
			}
			displaySettings[] myDS = new displaySettings[i];			
			i=0;
			foreach (System.Management.ManagementObject videocontroller in searcher.Get()) 
			{
				if (videocontroller["CurrentRefreshRate"] != null)
				{
					myDS[i] = new displaySettings();
					myDS[i].colorDepth=""+videocontroller["CurrentNumberOfColors"];
					myDS[i].refreshRate=""+videocontroller["CurrentRefreshRate"];
					myDS[i].height=""+videocontroller["CurrentVerticalResolution"];
					myDS[i].width=""+videocontroller["CurrentHorizontalResolution"];							
					i++;
				}
			}
			return myDS;
		}
		public void updateAppLog()
		{
			updateAppLog(this.mw);
		}
		public void updateAppLog(message messageWindow)
		{
			string updates = "-------------APPLOG----------------\n" + parent.applicationLog;
			updates = updates.Replace("-------------APPLOG----------------","\\b\\cf0\\fs30 Application Log: \\b0\\cf0\\fs16");
			updates = updates.Replace("\n","\n\\b\\cf1");
			updates = updates.Replace(":"," : \\b0\\cf1");			
			updates = updates.Replace("\n","\\par\n");			
			updates = updates.Replace("Error","\\b\\cf2 Error \\b0\\cf1");		
			updates = updates.Replace("Information","\\b\\cf3 Information \\b0\\cf1");		
			updates = updates.Replace("Warning","\\b\\cf4 Warning \\b0\\cf1");		
			updates = updates.Replace("#C#",":");
			messageWindow.appLogBox.Rtf="{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fcharset0 Arial;}}\n{\\colortbl ;\\red0\\green0\\blue0;\\red255\\green0\\blue0;\\red0\\green255\\blue0;\\red0\\green0\\blue255;}\n\\viewkind4\\uc1\\pard\\cf1\\f0\\fs16 "+updates+"\\cf0\\b0\\par\n}";			
		}	

		public void updateSysLog()
		{
			updateSysLog(this.mw);
		}
		public void updateSysLog(message messageWindow)
		{
			string updates = "-------------SYSLOG----------------\n" + parent.systemLog;
			updates = updates.Replace("-------------SYSLOG----------------","\\b\\cf0\\fs30 System Log: \\b0\\cf0\\fs16");
			updates = updates.Replace("\n","\n\\b\\cf1");
			updates = updates.Replace(":"," : \\b0\\cf1");			
			updates = updates.Replace("\n","\\par\n");			
			updates = updates.Replace("Error","\\b\\cf2 Error \\b0\\cf1");		
			updates = updates.Replace("Information","\\b\\cf3 Information \\b0\\cf1");		
			updates = updates.Replace("Warning","\\b\\cf4 Warning \\b0\\cf1");		
			updates = updates.Replace("#C#",":");
			messageWindow.sysLogBox.Rtf="{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fcharset0 Arial;}}\n{\\colortbl ;\\red0\\green0\\blue0;\\red255\\green0\\blue0;\\red0\\green255\\blue0;\\red0\\green0\\blue255;}\n\\viewkind4\\uc1\\pard\\cf1\\f0\\fs16 "+updates+"\\cf0\\b0\\par\n}";
		}	


		public void updateUpdates()
		{
			updateUpdates(this.mw);
		}
		public void updateUpdates(message messageWindow)
		{
			string updates = "-------------UPDATE HISTORY----------------\n" + parent.updateHistory;
			updates = updates.Replace("-------------UPDATE HISTORY----------------","\\b\\cf0\\fs30 Updates & Messages Recieved: \\b0\\cf0\\fs16");
			updates = updates.Replace("\n","\n\\b\\cf1");
			updates = updates.Replace(":"," : \\b0\\cf1");			
			updates = updates.Replace("\n","\\par\n");			
			messageWindow.updatesBox.Rtf="{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fcharset0 Arial;}}\n{\\colortbl ;\\red0\\green0\\blue0;\\red255\\green0\\blue0;}\n\\viewkind4\\uc1\\pard\\cf1\\f0\\fs16 "+updates+"\\cf0\\b0\\par\n}";			
		}	
		public string showA(string[] mystring)
		{
			string retVal = "";
			string sep = "";
			for(int i=0;i<mystring.Length;i++)
			{
				retVal += sep + mystring[i].Trim();
				sep = ",";
			}
			return retVal;
		}
		public bool checkVer(string who, string[] what)
		{
            Form1.debugMessage("checkVerStart");
			bool checkIt = false;
			for (int i=0;i<what.Length;i++)
				if (who.ToString().ToLower().Trim() == what[i].ToLower().Trim())
					checkIt = true;
            Form1.debugMessage("checkVerStop");
			return checkIt;
		}
		private string getProcessor()
		{
			String retVal = "";
			Object val = Form1.getRegVal("VendorIdentifier","HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
			retVal = val.ToString();
			val = Form1.getRegVal("Identifier","HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
			retVal += " " + val.ToString();
			return retVal;
		}
		public string getInstalledSoftware()
		{
			return getInstalledSoftware(true);
		}
		public string getInstalledSoftware(bool type)
		{
			string retVal = "";
			string holder = "";
			string[] listOfSubKeys;	
			Microsoft.Win32.RegistryKey pRegKey2;
			string path = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\";
			Microsoft.Win32.RegistryKey pRegKey = Microsoft.Win32.Registry.LocalMachine;
			pRegKey = pRegKey.OpenSubKey(path);
			if (pRegKey != null)
			{
				listOfSubKeys = pRegKey.GetSubKeyNames();				
				for (int i=0;i<listOfSubKeys.Length;i++)
				{
				if (listOfSubKeys[i].StartsWith("{") ^ type)
				{
					pRegKey2 = pRegKey.OpenSubKey(listOfSubKeys[i]);
					holder = "";
					if (pRegKey2.GetValue("DisplayName") != null)
						holder = pRegKey2.GetValue("DisplayName").ToString();
					if (holder.Length < 1)
						holder = listOfSubKeys[i];
					retVal += holder;					
					if (pRegKey2.GetValue("DisplayVersion") != null)
						if (pRegKey2.GetValue("DisplayVersion").ToString().Length > 0)
							retVal += " - " + pRegKey2.GetValue("DisplayVersion");
					retVal += "\n";
				}
				}
				
			}			
			return retVal;
		}
		public string getVersionInfo()
		{
			string retVal = "";
			string fChecks = "";
			string spInfo = "";				
			string err = "";
			string[] stuff;
			errorCount = 0;
			try 
			{
				fChecks += doFileChecks();
			}
			catch (Exception e) {				
				Form1.debugMessage("FILE CHECK ERROR..." + e.Message + "\n" + e.Source + "\n" + e.StackTrace);
			}
			Form1.debugMessage("Now getting system info...");
			OSVERSIONINFO os = new OSVERSIONINFO();
			os.dwOSVersionInfoSize=Marshal.SizeOf(typeof(OSVERSIONINFO)); 
			GetVersionEx(ref os);
			if (os.szCSDVersion=="")					 
				spInfo = "No Service Pack Installed";
			else
				spInfo = os.szCSDVersion;
			OperatingSystem os2 = System.Environment.OSVersion;
			if (!checkVer(os2.ToString()+" ("+spInfo+")",parent.osVersionStandard))
			{
				errorCount++;
				err = "---bad version---\nYours:["+os2.ToString()+" ("+spInfo+")] Standard:["+showA(parent.osVersionStandard)+"]";					
			}

			//if (!checkVer(spInfo,osSPStandard))
			//err = "---bad service pack---\nYours:["+spInfo+"] Standard:["+showA(osSPStandard)+"]";		
			retVal += "Timestamp: " + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString() + "\n";
			retVal += "Windows: " + parent.SplitByString(os2.ToString(),"Service Pack")[0].ToString() + " (" + spInfo + ") "+err+"\n";			
			err="";
			Form1.debugMessage("Getting Processor Info...");
			retVal += "Processor: " + getProcessor() +"\n";
			err = "";	
			string holder="";
			//holder = getMemory();
			//if (strToInt(holder) < strToInt(memoryMin[0].Trim()))
			//	err = "---bad Memory Size---\nYours:["+holder+"] Standard Minimum:["+memoryMin[0].Trim()+"]";						
			//retVal += "Memory: " + holder + err + "\n";
			err = "";
			holder = "";
			retVal += "\nIBS Workstation: " + Form1.cver +"\n";
			Form1.debugMessage("Getting DotNet Version...");
			
			if (!checkVer(System.Environment.Version.ToString(),parent.dotnetVersionStandard))
			{
				errorCount++;
				err = "---bad version---\nYours:["+System.Environment.Version.ToString()+"] Standard:["+showA(parent.dotnetVersionStandard)+"]";			
			}
			retVal += "dotNet Version: " + System.Environment.Version.ToString()+err+"\n";
			err = "";
			retVal += "Auto Update: " + parent.autoUpdate.Checked.ToString() +"\n";
			retVal += "Enter = Send: " + parent.enterSend.Checked.ToString() +"\n";
			retVal += "Auto Hide: " + parent.autoHide.Checked.ToString() +"\n";
			retVal += "Disable Pops: " + parent.disablePops.Checked.ToString() +"\n";
			DateTime startTime = parent.startTime;						
			retVal += "Workstation Started: " + startTime.ToShortDateString() + " " + startTime.ToShortTimeString() + "\n";
			//if (System.Environment.CommandLine.ToString().ToLower().IndexOf("isaac\\ibsworkstation") < 1)
			//{
			//	errorCount++;
			//	err = "---bad source location---\nYours:["+System.Environment.CommandLine.ToString()+"] Standard:[\\\\isaac\\ibsWorkstation\\IBSWorkstation.exe]";			
			//}
            retVal += "Command Line: " + System.Environment.CommandLine.ToString() +err+"\n";
            retVal += "Path: " + Form1.controlPath + err + "\n";			
			err = "";
			retVal += "\nUser: " + Form1.getCurrentUser() +"\n";
			string ls = "Not Logged In";
			string sid = "";
			if (Form1.loggedIn)
				ls = "Logged In";
			try 
			{
				sid = Form1.getRegVal("ProfileImagePath","Software\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList\\" + SID.ShowUserSID(Form1.currentUser)).ToString();
				if (sid.Length > 1)
				{
					homeDirectory = sid;
					Form1.setRegVal("homeDirectory",sid);
				}
				else 
				{
					sid = Form1.getRegVal("homeDirectory").ToString();
					if (sid.Length > 1)
						homeDirectory = sid;
				}
				retVal += "Home Directory: " + homeDirectory + "\n";
			}
			catch (Exception e) 
				{ 
				Form1.debugMessage(e.Message + "\n" + Form1.currentUser); 			
				string ret = "";
				sid = "C:\\Documents and Settings\\" + Form1.currentUser.ToLower().Replace("us_ibs\\","");
				Form1.debugMessage("Trying " + sid);
				if (System.IO.Directory.Exists(sid)){
					ret = sid;
					}
				else 
				{
					string sid2 = sid + ".US_IBS";
					Form1.debugMessage("Trying " + sid2);
					if (System.IO.Directory.Exists(sid2))
						ret = sid2;
					else 
					{
					sid2 = sid + ".ibs.org";
					Form1.debugMessage("Trying " + sid2);
					if (System.IO.Directory.Exists(sid2))
						ret = sid2;
					else 
						{
						sid2 = sid + ".ibs";
						Form1.debugMessage("Trying " + sid2);
						if (System.IO.Directory.Exists(sid2))
							ret = sid2;
						else 
						{
							sid2 = sid + ".WINNT";
							Form1.debugMessage("Trying " + sid2);
							if (System.IO.Directory.Exists(sid2))
								ret = sid2;
							else 
							{
								ret = "C:\\";
								Form1.debugMessage("Gave up using: " + ret);
							}
						}
						}
					}
				}
				retVal += "Home Directory: " + ret + "\n";
				homeDirectory = ret;
				Form1.setRegVal("homeDirectory",ret);
				}
			retVal += "Login Status: " + ls +"\n";
			retVal += "Program Context: " + System.Environment.UserName +"\n";
			retVal += "Computer: " + System.Environment.MachineName +"\n";
			retVal += "Hostname: " + System.Net.Dns.GetHostName() + "\n";
			retVal += "IP Address: " + getIPAddress() + "\n";
			
			//retVal += "Last Reboot: " + ((float)((uint)System.Environment.TickCount)/1000/60/60/24) +" days ago.\n";
			//retVal += "Last Reboot: " + lastSystemBootUp.getSystemBootup().ToString() + "\n";
			//retVal += "Last Reboot (simple): " + wmiQuery("Win32_OperatingSystem","LastBootUpTime") + "\n";
			err="";
			displaySettings [] dps = getScreenInfo();
			//			if (rr.CompareTo(refreshRateMin[0]) > 1)
			string sep="";			
			for (int i=0;i<dps.Length;i++)
			{
				holder += sep + dps[i].refreshRate;
				sep = ",";
			}
			stuff = holder.Split(",".ToCharArray());
			for (int i=0;i<stuff.Length;i++)
				if (strToInt(stuff[i]) < strToInt(parent.refreshRateMin[0]))
				{
					errorCount++;
					err = "---bad Refresh Rate---\nYours:["+holder+"] Standard:["+parent.refreshRateMin[0].Trim()+"]";
				}
			retVal += "Refresh Rate: " + holder + err + "\n";
			retVal += "Screen Resolution: ";
			sep = "";
			for (int idx=0;idx<dps.Length;idx++)
			{
				retVal+= sep + dps[idx].width + "x" + dps[idx].height + "x" + dps[idx].colorDepth;
				sep = ",";
			}
			retVal += "\n";
			retVal += parent.fileReadStat;
			retVal += fChecks;
			retVal += "\nError Count: " + errorCount;
			Form1.debugMessage("Getting Update History...");
			eventLog eLog = new eventLog();			
			if (parent.updateHistory.Length > 0)
			{
				retVal += "\n-------------UPDATE HISTORY----------------";
				retVal += parent.updateHistory;
			}						
			//retVal += "\n-------------Application Log----------------\n";
			parent.applicationLog = eLog.viewLog("Application",300);
			//retVal += "\n-------------System Log----------------\n";
			parent.systemLog  = eLog.viewLog("System",300);

			parent.directoryLog  = eLog.viewLog("Directory Service",300);
			parent.frsLog  = eLog.viewLog("File Replication Service",300);
			
			Form1.debugMessage("Finishing Up...");
			return retVal;
		}	
		public string regCheck(string path, string product,string key,string patch,string sval,string[] version, string location)
		{
			Form1.debugMessage("Reg Key Searching..." + path + " " + product + " " + key + " " + patch + " " + sval);			
			string retVal = "";
			if (sval.Length < 1)
				sval = "InstalledDate";
			if (path.Length < 1)
				path = "Software\\Microsoft\\Updates\\";
			Microsoft.Win32.RegistryKey pRegKey;
			if (location.ToUpper().CompareTo("HKEY_CURRENT_USER") == 0)
				pRegKey = Microsoft.Win32.Registry.CurrentUser;
			else if (location.ToUpper().CompareTo("HKEY_USERS") == 0)
				pRegKey = Microsoft.Win32.Registry.Users;
			else if (location.ToUpper().CompareTo("HKEY_CURRENT_CONFIG") == 0)
				pRegKey = Microsoft.Win32.Registry.CurrentConfig;
			else if (location.ToUpper().CompareTo("HKEY_CLASSES_ROOT") == 0)
				pRegKey = Microsoft.Win32.Registry.ClassesRoot;
			else
				pRegKey = Microsoft.Win32.Registry.LocalMachine;
			
			pRegKey = pRegKey.OpenSubKey(path+product+"\\"+key+"\\"+patch);
			Form1.debugMessage("Getting..." +path+product+"\\"+key+"\\"+patch+ "--- " + sval);			
			if (pRegKey != null)
			{
				if (sval == "InstalledDate")
					retVal = pRegKey.GetValue("Description") + " - ";
			
				retVal += pRegKey.GetValue(sval);
				Form1.debugMessage("...Got : " + retVal);
				try 
				{
					if (version.Length > 0)
					{					
						bool matched = false;
						for(int j=0; j < version.Length; j++)
						{
							if (version[j].Trim() == pRegKey.GetValue(sval).ToString())
								matched = true;
						}
						if (!matched)
						{
							errorCount++;
							retVal += "---bad Version---\nYours: ["+pRegKey.GetValue(sval)+"] Standard: ["+implode(version,",")+"]";						
						}
					}
				}
				catch (Exception e)
				{
				}
				Form1.debugMessage("DONE! Reg Key Searching..." + path + " " + product + " " + key + " " + patch + " " + sval);						
				return retVal;
			}
			return "";
		}
		public int strToInt(string val)
			{
				int myInt = 0;
				try { myInt=int.Parse(val); } 
				catch(Exception e) { }
				return myInt;
			}
		public float strToFloat(string val)
		{
			float myInt = 0;
			try { myInt=float.Parse(val); } 
			catch(Exception e) { }
			return myInt;
		}
		public string checkMin(string item, string mine, string[] standard)
		{	
			string retVal = "";
			string[] holder = mine.Split(",".ToCharArray());
			for(int i=0; i < holder.Length; i++)
				for(int j=0; j < standard.Length; j++)
					if (strToFloat(holder[i]) < strToFloat(standard[j].Trim()))
					{
						errorCount++;
						retVal = "---bad "+item+"---\nYours:["+mine+"] Standard Minimum:["+standard[j].Trim()+"]";						
					}
			return retVal;

		}
		public void updateMessageWindow()
		{
			updateMessageWindow(this.mw);
		}	
		public void updateMessageWindow(message messageWindow)
		{
			String vi = "";
			Form1.debugMessage("Starting getVersionInfo() ...");
			vi = getVersionInfo();
			Form1.debugMessage("Done getVersionInfo() ...");			
			int loc = vi.IndexOf("-------------UPDATE HISTORY----------------");
			if (loc < 1 || loc > vi.Length)
				loc = vi.Length;			
			vi = vi.Substring(0,loc);
			vi = vi.Replace("\\","\\\\");
			vi = "\\b\\cf1" + vi;
			String[] hldr1 = vi.Split("\n".ToCharArray());
			vi = "";
			string rep = " :\\b0\\cf1 ";
			for (int x=0;x<hldr1.Length;x++)
			{
				
				string[] hldr = hldr1[x].Split(":".ToCharArray());		
				rep = "";
				for (int i=0;i<hldr.Length;i++)
				{
					if (hldr[i].Trim().Length > 0)
					{
						vi += rep + hldr[i];
						if (i == 0)
							rep = " :\\b0\\cf1 ";
						else
							rep = ":";
					}
				}
				vi += "\n";
			}
			//vi = vi.Replace(":"," :\\b0\\cf1 ");
			vi = vi.Replace("\n","\n\\b\\cf1");
			vi = vi.Replace("---bad","\\b\\cf2  - bad ");
			vi = vi.Replace("---\n","\\b0\\cf1 \n");			
			vi = vi.Replace("\n","\\par\n");			
			
			messageWindow.logoBox2.Visible=false;
			Form1.debugMessage("Setting Window Text ...");	
			messageWindow.messageText.Rtf="{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fcharset0 Arial;}}\n{\\colortbl ;\\red0\\green0\\blue0;\\red255\\green0\\blue0;}\n\\viewkind4\\uc1\\pard\\cf1\\f0\\fs16 "+vi+"\\cf0\\b0\\par\n}";
			Form1.debugMessage("Done Setting Window Text ...");				
		}
		public void doSoftwareReport()
		{
			//MessageBox.Show(System.Net.Dns.GetHostName());
			string ver = "Timestamp: " + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString() + "\n";
			OperatingSystem os2 = System.Environment.OSVersion;
			ver += os2.ToString()+ "\n";			
			
			ver += getInstalledSoftware();
			ver += "\n-MANAGED CODE\n" + getInstalledSoftware(false);
			try
			{
				System.IO.FileStream myFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_software.txt");			
				byte[] myBA = System.Text.Encoding.Default.GetBytes(ver.Replace("\n","\r\n"));	
				myFile.Write(myBA,0,myBA.Length);
				myFile.Close();
				//System.IO.File.Create(filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_report.txt").Close();
			}
			catch (Exception e){}
		}

		public void doReport()
		{
			//MessageBox.Show(System.Net.Dns.GetHostName());
			string ver = getVersionInfo();
			try
			{
				System.IO.FileStream myFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_report.txt");			
				System.IO.FileStream sysLogFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_sysLog.txt");			
				System.IO.FileStream appLogFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_appLog.txt");			
				System.IO.FileStream processLogFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_processLog.txt");			
				
				byte[] myBA = System.Text.Encoding.Default.GetBytes(ver.Replace("\n","\r\n"));	
				myFile.Write(myBA,0,myBA.Length);
				myFile.Close();
				myBA = System.Text.Encoding.Default.GetBytes(parent.systemLog.Replace("\n","\r\n").Replace("#C#",":"));	
				sysLogFile.Write(myBA,0,myBA.Length);
				sysLogFile.Close();
				myBA = System.Text.Encoding.Default.GetBytes(parent.applicationLog.Replace("\n","\r\n").Replace("#C#",":"));	
				appLogFile.Write(myBA,0,myBA.Length);
				appLogFile.Close();				
				
				if (parent.directoryLog.Length > 50)
				{
					System.IO.FileStream directoryLogFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_directoryLog.txt");			
					myBA = System.Text.Encoding.Default.GetBytes(parent.directoryLog.Replace("\n","\r\n").Replace("#C#",":"));	
					directoryLogFile.Write(myBA,0,myBA.Length);
					directoryLogFile.Close();			
				}
				if (parent.frsLog.Length > 0)
				{
					System.IO.FileStream frsLogFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_frsLog.txt");			
					myBA = System.Text.Encoding.Default.GetBytes(parent.frsLog.Replace("\n","\r\n").Replace("#C#",":"));	
					frsLogFile.Write(myBA,0,myBA.Length);
					frsLogFile.Close();	
				}
				string processLog = "";
				System.Diagnostics.Process[] processescheck = System.Diagnostics.Process.GetProcesses();
				processLog = processescheck.Length + "\r\n";
				foreach (System.Diagnostics.Process proc in processescheck)
					processLog += proc.ToString()+"\n";
				myBA = System.Text.Encoding.Default.GetBytes(processLog.Replace("\n","\r\n").Replace("#C#",":"));	
				processLogFile.Write(myBA,0,myBA.Length);
				processLogFile.Close();	
				
			}
			catch (Exception e){}
		}
		public string getIPAddress()
		{
			String retVal = "";
			String sep = "";
			System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName());
			System.Net.IPAddress[] addr = ipEntry.AddressList;
			for (int i = 0; i < addr.Length; i++)
			{
				retVal += sep + addr[i].ToString ();
				sep = ", ";
			}
			return retVal;
		}
		public void doVersionCheck()
		{
			//MessageBox.Show(System.Net.Dns.GetHostName());
			string ver = getVersionInfo();
			if (ver.IndexOf("---bad") > 0)
				try
				{
					System.IO.FileStream myFile = System.IO.File.Create(Form1.filePath+"\\reports\\"+System.Net.Dns.GetHostName()+"_versions.txt");
					byte[] myBA = System.Text.Encoding.Default.GetBytes(ver.Replace("\n","\r\n"));	
					myFile.Write(myBA,0,myBA.Length);
					myFile.Close();
				}
				catch (Exception e){}
		}

		public void updateSysLogThread(message messageWindow)
		{	
			this.mw = messageWindow;
			System.Threading.ThreadStart mt = new System.Threading.ThreadStart(updateSysLog);
			System.Threading.Thread thread1 = new System.Threading.Thread( mt ) ;
			Form1.debugMessage("Starting updateUpdates Thread...");
			thread1.Start() ;
			Form1.debugMessage("... Done Starting updateUpdates Thread.");
		}

		public void updateAppLogThread(message messageWindow)
		{	
			this.mw = messageWindow;
			System.Threading.ThreadStart mt = new System.Threading.ThreadStart(updateAppLog);
			System.Threading.Thread thread1 = new System.Threading.Thread( mt ) ;
			Form1.debugMessage("Starting updateUpdates Thread...");
			thread1.Start() ;
			Form1.debugMessage("... Done Starting updateUpdates Thread.");
		}

		

	public void updateUpdatesThread(message messageWindow)
	{	
		this.mw = messageWindow;
		System.Threading.ThreadStart mt = new System.Threading.ThreadStart(updateUpdates);
		System.Threading.Thread thread1 = new System.Threading.Thread( mt ) ;
		Form1.debugMessage("Starting updateUpdates Thread...");
		thread1.Start() ;
		Form1.debugMessage("... Done Starting updateUpdates Thread.");
	}

	public void doReportThread()
	{	
		System.Threading.ThreadStart mt = new System.Threading.ThreadStart(doReport);
		System.Threading.Thread thread1 = new System.Threading.Thread( mt ) ;
		Form1.debugMessage("Starting doReport Thread...");
		thread1.Start() ;
		Form1.debugMessage("... Done Starting doReport Thread.");
	}

	public void updateMessageWindowThread(message messageWindow)
	{			
		this.mw = messageWindow;
		System.Threading.ThreadStart mt = new System.Threading.ThreadStart(updateMessageWindow);
		System.Threading.Thread thread1 = new System.Threading.Thread( mt ) ;
		Form1.debugMessage("Starting updateMessageWindow Thread...");
		thread1.Start() ;
		Form1.debugMessage("... Done Starting updateMessageWindow Thread.");
	}

		public DateTime fixDateTime(string datetime)
		{
			string lastBootUpTime = datetime;
			DateTime lastBootUp = new System.DateTime();
			if(lastBootUpTime.Length >= 25)
			{
				int yy = int.Parse(lastBootUpTime.Substring(0, 4));
				int mo = int.Parse(lastBootUpTime.Substring(4, 2));
				int dd = int.Parse(lastBootUpTime.Substring(6, 2));
				int hr = int.Parse(lastBootUpTime.Substring(8, 2));
				int min = int.Parse(lastBootUpTime.Substring(10, 2));
				int sec = int.Parse(lastBootUpTime.Substring(12, 2));

				// Get zone-time offset (hours)
				// Includes "+" or "-" sign!!
				int timeShift =  
					int.Parse(lastBootUpTime.Substring(lastBootUpTime.Length-4, 4))/60;
						
				int bootHour = hr - timeShift;
				int localBootHour = bootHour;
				int localBootDay = dd;

				// Convert to local time. 
				if (bootHour < 0)
				{
					localBootHour = bootHour + 24;
					localBootDay = dd - 1;
				}
				else if (bootHour > 23)
				{
					localBootHour = bootHour - 24;
					localBootDay = dd + 1;
				}

				DateTime lastBootUpDate = 
					new DateTime(yy, mo, localBootDay, localBootHour, min, sec);

				// This "sixDigitNumb" SEEMS = 0 for Win2K; non-zero for Win2K3/XP 
				int sixDigitNumb = int.Parse(lastBootUpTime.Substring(15, 6));						

				lastBootUp = (sixDigitNumb == 0) ? lastBootUpDate : 
				lastBootUpDate.ToLocalTime();	
												
			}
			return lastBootUp;
		}

	}

}
