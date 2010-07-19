using System; 
using System.Text; 
using System.Runtime.InteropServices; 
using System.Management; 
using System.Diagnostics; 
namespace CCMClient
{ 
	/// <summary> 
	/// Gets unique SID for give user (e.g.  "S-1-5-21-1692944411-1583482546-720635935-8985") 
	/// </summary> 
	public class SID
	{ 
		private static ManagementObjectSearcher query; 
		private static ManagementObjectCollection queryCollection; 

		public static string ShowUserSID(string username) 
		{ 
			// local scope 
			string[] unames = username.Split("\\".ToCharArray(),2);
			string d = "";
			string n = "";
			d = unames[0];			
			if (unames.Length < 2)
			{
				n = unames[0];
				d = "US_IBS";
			}
			else
				n = unames[1];
			ConnectionOptions co = new ConnectionOptions();
			//co.Username = username; 			
			ManagementScope msc = new ManagementScope ("\\root\\cimv2",co); 
			string queryString = "SELECT * FROM Win32_UserAccount where LocalAccount = false AND SIDType = 1 AND Domain = '" + d+ "' AND Name = '" + n + "'"; 
			//System.Windows.Forms.MessageBox.Show(queryString);
			SelectQuery q = new SelectQuery (queryString); 
			query = new ManagementObjectSearcher(msc, q); 
			queryCollection = query.Get(); 
			string res=String.Empty; 
			foreach( ManagementObject mo in queryCollection ) 
			{ 
				// there should be only one here! 
				res+= mo["SID"].ToString(); 
				//res+= mo["Name"]+"\n";
			} 
			return res; 

		} 
	} 
} 