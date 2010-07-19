using System;
using System.Diagnostics;

namespace CCMClient
{
	/// <summary>
	/// Summary description for eventLog.
	/// </summary>
	public class eventLog
	{
		public string listLogs()
		{
			String retVal = "";
			EventLog[] ea = EventLog.GetEventLogs(".");
			for (int i=0; i < ea.Length; i++)
				retVal += " [" + ea[i].Log + "] ";
			return retVal;
		}
		public string viewLog(string log,int maxNum)
		{
			String retVal = "";			
			//if(EventLog.SourceExists(log))
			try 
			{
				EventLog aLog = new EventLog();					
				aLog.Log = log;
				aLog.MachineName = ".";
				//foreach (EventLogEntry entry in aLog.Entries) 
				EventLogEntry entry;
				for(int i=aLog.Entries.Count-1;(i>0 && (i > aLog.Entries.Count-1 - maxNum));i--)
				{
					entry = aLog.Entries[i];
					retVal += " " + entry.TimeGenerated.ToString().Replace(":","#C#") + ": " + entry.EntryType + " - " + entry.Source + "\n" + entry.Message+"\n";
				}
				aLog.Close();
			}
			catch (Exception e){
				
			}
			return retVal;
		}
		public eventLog()
		{			
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
