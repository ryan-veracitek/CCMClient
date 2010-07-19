using System;
using System.Data;
using System.Data.OleDb;

namespace CCMClient
{
	/// <summary>
	/// Summary description for dataTool.
	/// </summary>
	public class dataTool
	{
		public dataTool()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string searchNumber(double number)
		{
			DataSet ds;
			string q = "select * from InOrder_dataWarehouse.dbo.corePhones where phone = " + number + "  order by source";
			string retVal = "";
			string src = "";
			OleDbDataReader dr = null;
			OleDbConnection con;			
			ds = new DataSet();			
			con = new OleDbConnection("Provider=SQLOLEDB;DATA SOURCE=ira;Database=wits;Trusted_Connection=no;User ID=reportUser;Password=ruser");	
			con.Open();
			if (con.State != System.Data.ConnectionState.Open && Form1.getRegVal("debugMode").ToString().CompareTo("True") == 0)
				System.Windows.Forms.MessageBox.Show("Database connection failed!");
			OleDbCommand cmd = new OleDbCommand();		
			cmd.CommandText = q;
			cmd.Connection = con;
			//	cmd.Connection.Open();
			if (dr != null)
				if (!dr.IsClosed)
					dr.Close();		
			dr = cmd.ExecuteReader();	
			dr.Read();					
			try
			{
				if (dr["source"].ToString() == "0")
					src = "(I)";
				else
					src = "(M)";
				retVal = dr["name"].ToString() + " " + src + "\n";
				retVal += dr["city"].ToString();
				if (dr["city"].ToString().Trim().Length > 0 && (dr["state"].ToString().Trim().Length > 0 || dr["zip"].ToString().Trim().Length > 0))
					retVal += ", ";
				retVal += dr["state"].ToString() + " " + dr["zip"].ToString();
				if (dr["city"].ToString().Trim().Length > 0 || dr["state"].ToString().Trim().Length > 0 || dr["zip"].ToString().Trim().Length > 0)
				  retVal += "\n";
				//retVal += "Source: ";

			}
			catch (Exception e)
			{	
				if (Form1.getRegVal("debugMode").ToString().CompareTo("True") == 0)
				  System.Windows.Forms.MessageBox.Show(q + "\n" + e.Message);
				retVal = "No Match";
				//res = "ooops: " + what + "<hr>" + e.GetType() +  " - " + e.Message;
			}
			dr.Close();
			con.Close();
			cmd.Dispose();
			return retVal;
		}
	}
}
