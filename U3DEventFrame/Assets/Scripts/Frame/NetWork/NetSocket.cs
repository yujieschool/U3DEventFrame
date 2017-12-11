using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;

//CopyRigtht by Lic 2016
//All rights received


namespace U3DEventFrame
{
    public enum ErrorSockets
    {
        Success = 0,

        ConnectTimeOut,
        ConnectDisconnect,
        MutiConnect,
        ConnectUnknowError,


        RecvTimeOut,
        RecvClientDisconnect,
        RecvUnkownError,

        SendTimeOut,
        SendZeroByte,
        SendError,
        SendUnkownError,

        DisconnectSocketIsNull,
        DisconnectSocketHasDisconnect,
        DisConnectionTimeOut,
        DisConnectionUnknow
    }

    public class NetSocket
    {
        public delegate void NormalNetCallBack(bool isSuccess, ErrorSockets error, string expection);
        //public delegate void SendCallBack(bool isSuccess, ErrorSockets error, string expection);
        public delegate void ReceiveCallBack(bool isSuccess, ErrorSockets error, string expection, byte[] byteData, string strMsg);
        //public delegate void DisConnectCallBack(bool isSuccess, ErrorSockets error, string expection);


      //  private NormalNetCallBack m_callBackNormal;
        private NormalNetCallBack m_callBackConnect;
        private NormalNetCallBack m_callBackSend;
        private ReceiveCallBack m_callBackReceive;
        private NormalNetCallBack m_callBackDisConnect;

      //  private ErrorSockets m_error;
        private Socket clientSocket = null;
      //  private string m_IP;
     //   private ushort m_Port;
        private byte[] DataBuffer;
        /// <summary>
        /// 粘包拆包
        /// </summary>
        private SocketBuffer recBuffer;

        public NetSocket()
        {
            recBuffer = new SocketBuffer(7, HandlePacketComplete);
            DataBuffer = new byte[1024];

        }


        #region 网络请求



        public void AsyncConnect(string ip, ushort port, NormalNetCallBack connectBack, NormalNetCallBack sendBack, ReceiveCallBack callBackRecv  , NormalNetCallBack disConnect)
        {
            //   m_error = ErrorSockets.Success;




            this.m_callBackConnect = connectBack;
            if (clientSocket != null && clientSocket.Connected)
            {
                this.m_callBackConnect(false, ErrorSockets.MutiConnect, "重复连接");

                return;
            }


          
            this.m_callBackSend = sendBack;
            this.m_callBackReceive = callBackRecv;
            this.m_callBackDisConnect = disConnect;




            if (clientSocket == null || !clientSocket.Connected)
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress address = IPAddress.Parse(ip);
                IPEndPoint point = new IPEndPoint(address, port);
                IAsyncResult connectAr = clientSocket.BeginConnect(point, ConnectCallBack, clientSocket);
                if (!WriteDot(connectAr))
                {
                    connectBack(false, ErrorSockets.ConnectTimeOut, "连接超时");
                }
            }
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
                if (!clientSocket.Connected)
                {

                    m_callBackConnect(false, ErrorSockets.ConnectDisconnect, "连接服务器失败");
                    return;

                }
                else
                {
                    m_callBackConnect(true, ErrorSockets.Success, "连接成功");

                    AsyncReceive();
                    return;

                }
            }
            catch (Exception e)
            {

                m_callBackConnect(false, ErrorSockets.ConnectUnknowError, e.ToString());

              //  Debug.Log("connenct eror =" + e.ToString());
                // throw e;
            }
        }





        private void AsyncReceive()
        {
            if (clientSocket != null & clientSocket.Connected)
            {
               // Debug.Log("begin recv  11111111111");
                IAsyncResult rec = clientSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, RecceiveCallBack, clientSocket);
                //if (!WriteDot(rec))
                //{
                //    m_callBackReceive(false, ErrorSockets.RecvTimeOut, "超时", null, null);
                //}

            }
            else
            {
                m_callBackReceive(false, ErrorSockets.RecvClientDisconnect, "Disconnect", null, null);
            }
        }


        private void RecceiveCallBack(IAsyncResult ar)
        {
            try
            {
              //  Debug.Log("end recv ");
                if (!clientSocket.Connected)
                {
                    m_callBackReceive(false, ErrorSockets.RecvClientDisconnect, "接收时连接断开", null, null);
                    return;
                }

                int length = clientSocket.EndReceive(ar);
                if (length == 0)
                {
                    Debug.LogError(" RecceiveCallBack   数据长度为0");
                    return;
                }
                else
                {
                  //  Debug.Log("SOCKET  recv  lenght=="+length);
                    recBuffer.RecevByte(DataBuffer, length);
                }

            }
            catch (Exception e)
            {

                Debug.Log("recev eror =" + e.ToString());
                m_callBackReceive(false, ErrorSockets.RecvUnkownError, e.ToString(), null, null);
                // throw e;
            }
            //循环接收
            AsyncReceive();
        }





        public void AsyncSend(byte[] data)
        {
          //  m_error = ErrorSockets.Success;
         //   this.m_callBackSend = sendCallBack;
            if (data == null || data.Length == 0 || clientSocket == null)
            {
                m_callBackSend(false, ErrorSockets.SendError, "error");
            }
            if (clientSocket.Connected != true)
            {
                m_callBackSend(false, ErrorSockets.SendError, "未连接");

            }
            else
            {
                IAsyncResult send = clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallBack, clientSocket);


               // Debug.Log(" send.Length==" + data.Length);
                if (!WriteDot(send))
                {
                    m_callBackSend(false, ErrorSockets.SendTimeOut, "发送超时");
                }
            }
        }

        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                int byteSend = clientSocket.EndSend(ar);

               // Debug.Log("byteSend =="+ byteSend);
                if (byteSend > 0)
                {
                    m_callBackSend(true, ErrorSockets.Success, "发送成功");
                }
                else
                {
                    m_callBackSend(false, ErrorSockets.SendZeroByte, "发送异常");
                    return;


                }
            }
            catch (Exception e)
            {

                //Debug.Log("send eror ="+e.ToString());

                m_callBackSend(false, ErrorSockets.SendUnkownError, e.ToString());
            }



        }




        public void AsyncDisconnect()
        {
            try
            {
        
                if (clientSocket == null)
                {
                    m_callBackDisConnect(false, ErrorSockets.DisconnectSocketIsNull, "null");
                    return;
                }
                if (clientSocket.Connected != true)
                {
                    m_callBackDisConnect(false, ErrorSockets.DisconnectSocketHasDisconnect, "已经断开连接了");
                    return;

                }
                else
                {
                    IAsyncResult dis = clientSocket.BeginDisconnect(false, DisConnectCallBack, clientSocket);
                    if (!WriteDot(dis))
                    {
                        m_callBackDisConnect(false, ErrorSockets.DisConnectionTimeOut, "超时");

                    }
                }


            }
            catch (Exception e)
            {

                //Debug.Log("disconnect eror =" + e.ToString());
                m_callBackDisConnect(false, ErrorSockets.DisConnectionUnknow, e.ToString());
              //  throw e;
            }
        }

        private void DisConnectCallBack(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndDisconnect(ar);
                clientSocket.Close();
                clientSocket = null;
                m_callBackDisConnect(true, ErrorSockets.Success, "成功断开");
            }
            catch (Exception e)
            {

               // Debug.Log("disconnect back eror =" + e.ToString());
                m_callBackDisConnect(false, ErrorSockets.DisConnectionUnknow, e.ToString());
                // throw e;
            }
        }


        #endregion
        #region 网络回调





        #endregion

        #region Other
        /// <summary>
        /// TimeOutCheck,true 代表不超时
        /// </summary>
        /// <param name="ar"></param>
        /// <returns></returns>
        private bool WriteDot(IAsyncResult ar)
        {
            int i = 0;
            while (ar.IsCompleted == false)
            {
                i++;
                if (i > 30)
                {
                  
                    return false;
                }
                Thread.Sleep(100);
            }

            return true;
        }
        private void HandlePacketComplete(byte[] allData)
        {
            m_callBackReceive(true, ErrorSockets.Success, null, allData, "成功收到数据 长度：" + allData.Length);
        }
        public bool isConnect()
        {
            return clientSocket.Connected;
        }
        #endregion
    }

}

