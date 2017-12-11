/*  		  
       
       
       
           AsyncUdpClient myclient;
    AsyncUdpClient.MYDelegate recvDelegate;

public void Recv(byte[]  buffer, long dwCount, string pszIP, long wPort)
{
    //接受到消息
}


void Start () 
{
         recvDelegate = new AsyncUdpClient.MYDelegate(Recv);

        myclient = new AsyncUdpClient();
        myclient.BindSocket(18001, recvDelegate, 1024);
        }
        
        
        */
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace U3DEventFrame
{
    // 异步UDP类
    public class AsyncUdpClient
    {


        #region  Read

        Socket m_newsock;
        IPEndPoint m_ip;

        IPEndPoint brodIp = null;

        Thread recvThread;

        byte[] recvData;

        public delegate void MYDelegate(byte[] pBuf, long dwCount, string pszIP, long wPort);

        MYDelegate RecvDelegate;
        public AsyncUdpClient()
        {

        }
        ~AsyncUdpClient()
        {
            //  UnityEngine.Debug.Log("123456");
            if (recvThread != null)
                recvThread.Abort();

            //对于不存在的IP地址，加入此行代码后，可以在指定时间内解除阻塞模式限制
            m_newsock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);


            m_newsock.Close();
        }


        private bool isRunning = true;



  
        #endregion


        public void RecvDataThread()
        {

            while (isRunning)
            {

                if (m_newsock == null || m_newsock.Available < 1)
                {

                    //  UnityEngine.Debug.Log("789123456");

                    Thread.Sleep(100);
                    continue;

                }

                lock (this)
                {
                    // UnityEngine.Debug.Log("123456");
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint Remote = (EndPoint)sender;



                    // byte[]  recvFrom=   new byte[1024];


                    int myCount = m_newsock.ReceiveFrom(recvData, ref Remote);

                    sender = (IPEndPoint)Remote;
                    if (myCount > 0 && RecvDelegate != null)
                        RecvDelegate(recvData, myCount, sender.Address.ToString(), sender.Port);


                }


            }

        }


        public string GetLocalIp()
        {
            string hostname = Dns.GetHostName();
            IPAddress[] localhost = Dns.GetHostAddresses(hostname);

            for (int i = 0; i < localhost.Length; i++)
            {

                int tmpFind = localhost[i].ToString().IndexOf("192");

                if (tmpFind != -1)
                {
                    IPAddress localaddr = localhost[i];
                    return localaddr.ToString();
                }
               // UnityEngine.Debug.Log("ip Adrress ==" + localhost[i]);
            }

            return null;
         
        }




        /// <summary>
        /// http://www.soaspx.com/dotnet/csharp/csharp_20120220_8610.html
        /// </summary>http://www.fengfly.com/plus/view-173114-1.html
        /// <param name="uRecvPort"></param>
        /// <param name="myDelegate"></param>
        /// <param name="iRecvBuf"></param>
        /// <returns></returns>

        public bool BindSocket(int uRecvPort, MYDelegate myDelegate, int iRecvBuf)
        {


            //得到本机IP，设置UDP端口号         
            m_ip = new IPEndPoint(IPAddress.Any, uRecvPort);


            Reconnect();

            //对于不存在的IP地址，加入此行代码后，可以在指定时间内解除阻塞模式限制
            // m_newsock.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName., 100);

            RecvDelegate = myDelegate;

            recvData = new byte[iRecvBuf];
            recvThread = new Thread(RecvDataThread);

            recvThread.IsBackground = false;
            recvThread.Start();


            return true;
        }

        public void Reconnect()
        {
            m_newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_newsock.Bind(m_ip);
        }



        public int SendByteData(string pszIP, byte[] data, int uPort)
        {
            IPEndPoint sendToIP = new IPEndPoint(IPAddress.Parse(pszIP), uPort);

            int mySend = m_newsock.SendTo(data, data.Length, SocketFlags.None, sendToIP);

            return mySend;
        }

        public int SendUnicodeData(string pszIP, int uPort, string pszBuf)
        {



            byte[] data = Encoding.Unicode.GetBytes(pszBuf);

            int mySend = SendByteData(pszIP, data, uPort);

            // UnityEngine.Debug.Log("mysend =" + mySend.ToString()+"port=="+uPort.ToString());
            return mySend;
        }


        public int SendData(string pszIP, int uPort, string pszBuf, int iBufLen)
        {

            //  if (m_sendToIP == null)


            //  UnityEngine.Debug.Log("ip=" + pszIP + "prot=" + uPort.ToString());
            //得到客户机IP




            byte[] data = Encoding.Default.GetBytes(pszBuf);

            int mySend = SendByteData(pszIP, data, uPort);

            // UnityEngine.Debug.Log("mysend =" + mySend.ToString()+"port=="+uPort.ToString());
            return mySend;
        }

        public int SendData(string pszIP, int uPort, string pszBuf)
        {

            //  if (m_sendToIP == null)


            //  UnityEngine.Debug.Log("ip=" + pszIP + "prot=" + uPort.ToString());
            //得到客户机IP




            byte[] data = Encoding.Default.GetBytes(pszBuf);

            int mySend = SendByteData(pszIP, data, uPort);

            // UnityEngine.Debug.Log("mysend =" + mySend.ToString()+"port=="+uPort.ToString());
            return mySend;
        }



        public void GenSocket()
        {

            m_newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_newsock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            m_newsock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
        }



        public int BroadCastMsg(int uport, Byte[] buffer)
        {

            if (brodIp == null)
            {
              
                brodIp = new IPEndPoint(IPAddress.Broadcast, uport);
                GenSocket();
            }

            //if (!m_newsock.Connected)
            //{
            //    GenSocket();
            //}

            int sendCount = m_newsock.SendTo(buffer, brodIp);

            return sendCount;
        }

    }


}


