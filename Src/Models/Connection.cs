using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace Ajax_Minimal.Models
{
    public class Connection
    {
        private TcpClient client;
        private BinaryWriter writer;
        private BinaryReader reader;
        private bool isConnected = false;

        public void Connect(string ip, int port)
        {
            if (!this.isConnected)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                this.client = new TcpClient();

                try
                {
                    while (!this.client.Connected)
                    {
                        this.client.Connect(ep);
                        this.isConnected = true;
                        this.writer = new BinaryWriter(this.client.GetStream());
                        this.reader = new BinaryReader(this.client.GetStream());
                    }
                    this.isConnected = true;
                }
                catch { }
            }
        }

        public void Close()
        {
            try
            {
                this.client.Close();
                this.isConnected = false;
            }
            catch { }
        }

        public double GetLon()
        {
            return this.GetData("/position/longitude-deg");
        }

        public double GetLat()
        {
            return this.GetData("/position/latitude-deg");
        }

        public double GetRudder()
        {
            return this.GetData("/controls/flight/rudder");
        }

        public double GetThrottle()
        {
            return this.GetData("/controls/engines/current-engine/throttle");
        }

        public double GetData(string path)
        {
            try
            {
                this.writer.Write(System.Text.Encoding.ASCII.GetBytes("get " + path + "\r\n"));
                char c;
                string input = "";
                while ((c = reader.ReadChar()) != '\n') { input += c; }

                int i = 0;
                while (!Char.IsDigit(input[i]))
                {
                    i++;
                }
                int start = i;
                bool isNeg = (input[start - 1] == '-');
                i++;
                while (Char.IsDigit(input[i]) || (input[i] == '.'))
                {
                    i++;
                }
                int end = i;

                string str = input.Substring(start, end - start);
                double result = double.Parse(str, System.Globalization.CultureInfo.InvariantCulture);

                if (isNeg)
                {
                    result = -result;
                }

                return result;
            }
            catch
            { return 0; }

        }


        // singleton
        private static Connection m_Instance = null;
        public static Connection Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Connection();
                }
                return m_Instance;
            }
        }
    }
}