using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace CCMClient
{


    public class threadPing
    {
    
    private int pingsSent;
    public string txtResponse;
    AutoResetEvent resetEvent = new AutoResetEvent(false);
        public threadPing()
        {
        }
    public string doPing(string theHost)
    {
        return doPing(theHost, 1000); 
    }
    public string doPing(string theHost, int timeout)
    {
    System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
    // Create an event handler for ping complete
    pingSender.PingCompleted += new PingCompletedEventHandler(pingSender_Complete);
    // Create a buffer of 32 bytes of data to be transmitted.
    byte[] packetData = Encoding.ASCII.GetBytes("................................");
    // Jump though 50 routing nodes tops, and don't fragment the packet
    PingOptions packetOptions = new PingOptions(50, true);
    // Send the ping asynchronously
    PingReply rep = pingSender.Send(theHost, timeout, packetData, packetOptions);
    long retVal = -1;
    if (rep.Status == IPStatus.Success)
        retVal = rep.RoundtripTime;
    //pingSender.SendAsync(theHost, 4000, packetData, packetOptions, resetEvent);        
    return retVal.ToString();
    
    }


    private void pingSender_Complete(object sender, PingCompletedEventArgs e)
    {
    // If the operation was canceled, display a message to the user.
    if (e.Cancelled)
    {
        txtResponse += "Ping was canceled...\r\n";
        // The main thread can resume
        ((AutoResetEvent)e.UserState).Set();
    }
    else if (e.Error != null)
    {
        txtResponse += "An error occured: " + e.Error + "\r\n";
        // The main thread can resume
        ((AutoResetEvent)e.UserState).Set();
    }
    else
    {
        PingReply pingResponse = e.Reply;
        // Call the method that displays the ping results, and pass the information with it
        ShowPingResults(pingResponse);
    }

}

public void ShowPingResults(PingReply pingResponse)

{

    if (pingResponse == null)
    {
        // We got no response
        txtResponse += "No response from remote host";
        return;

    }

    else if (pingResponse.Status == IPStatus.Success)

    {
        // We got a response, let's see the statistics
        txtResponse += "Reply from " + pingResponse.Address.ToString() + ": bytes=" + pingResponse.Buffer.Length + " time=" + pingResponse.RoundtripTime + " TTL=" + pingResponse.Options.Ttl + "\r\n";

    }

    else

    {
        // The packet didn't get back as expected, explain why
        txtResponse += "Ping was unsuccessful: " + pingResponse.Status + "\r\n\r\n";
    }

    // Increase the counter so that we can keep track of the pings sent
        pingsSent++;
       
    }

   }



    public class SimplePing
    {
        public SimplePing()
        {
        }
        public string doPing(string theHost)
        {
            string retVal = "";
            try
            {
          
                byte[] data = new byte[1024];
                int recv;
                Socket host = new Socket(AddressFamily.InterNetwork, SocketType.Raw,
                           ProtocolType.Icmp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(theHost), 0);
                EndPoint ep = (EndPoint)iep;
                ICMP packet = new ICMP();

                packet.Type = 0x08;
                packet.Code = 0x00;
                packet.Checksum = 0;
                Buffer.BlockCopy(BitConverter.GetBytes((short)1), 0, packet.Message, 0, 2);
                Buffer.BlockCopy(BitConverter.GetBytes((short)1), 0, packet.Message, 2, 2);
                data = Encoding.ASCII.GetBytes("test packet");
                Buffer.BlockCopy(data, 0, packet.Message, 4, data.Length);
                packet.MessageSize = data.Length + 4;
                int packetsize = packet.MessageSize + 4;

                UInt16 chcksum = packet.getChecksum();
                packet.Checksum = chcksum;

                host.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
                System.DateTime time1 = System.DateTime.UtcNow;
                System.DateTime time2;
                host.SendTo(packet.getBytes(), packetsize, SocketFlags.None, iep);
                try
                {
                    data = new byte[1024];
                    recv = host.ReceiveFrom(data, ref ep);
                    time2 = System.DateTime.UtcNow;
                    //System.Windows.Forms.MessageBox.Show(ep.ToString());
                }
                catch (SocketException)
                {
                    //Console.WriteLine();
                    return "No response from remote host";
                }
                ICMP response = new ICMP(data, recv);
                //retVal += "response from: " + ep.ToString();
                string resp = "" + (time2 - time1);
                resp = resp.Split(':')[2] + "00000000";

                retVal += (resp.Substring(0, 6));
                //retVal += ("  Type " + response.Type);
                //retVal += ("  Code: " + response.Code);
                int Identifier = BitConverter.ToInt16(response.Message, 0);
                int Sequence = BitConverter.ToInt16(response.Message, 2);
                //retVal += ("  Identifier: " +  Identifier);
                //retVal += ("  Sequence: " + Sequence);
                string stringData = Encoding.ASCII.GetString(response.Message, 4, response.MessageSize - 4);
                //retVal += ("  data: " + stringData);
                if (stringData != "test packet")
                    return "No response from remote host";
                host.Close();
      
            }
            catch (Exception ze)
            {
                //do nothing
            }
            return retVal;
        }
    }

    class ICMP
    {
        public byte Type;
        public byte Code;
        public UInt16 Checksum;
        public int MessageSize;
        public byte[] Message = new byte[1024];

        public ICMP()
        {
        }

        public ICMP(byte[] data, int size)
        {
            Type = data[20];
            Code = data[21];
            Checksum = BitConverter.ToUInt16(data, 22);
            MessageSize = size - 24;
            Buffer.BlockCopy(data, 24, Message, 0, MessageSize);
        }

        public byte[] getBytes()
        {
            byte[] data = new byte[MessageSize + 9];
            Buffer.BlockCopy(BitConverter.GetBytes(Type), 0, data, 0, 1);
            Buffer.BlockCopy(BitConverter.GetBytes(Code), 0, data, 1, 1);
            Buffer.BlockCopy(BitConverter.GetBytes(Checksum), 0, data, 2, 2);
            Buffer.BlockCopy(Message, 0, data, 4, MessageSize);
            return data;
        }

        public UInt16 getChecksum()
        {
            UInt32 chcksm = 0;
            byte[] data = getBytes();
            int packetsize = MessageSize + 8;
            int index = 0;

            while (index < packetsize)
            {
                chcksm += Convert.ToUInt32(BitConverter.ToUInt16(data, index));
                index += 2;
            }
            chcksm = (chcksm >> 16) + (chcksm & 0xffff);
            chcksm += (chcksm >> 16);
            return (UInt16)(~chcksm);
        }
    }


}
