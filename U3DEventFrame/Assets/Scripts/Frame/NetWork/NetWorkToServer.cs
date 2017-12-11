using UnityEngine;

using System.Collections.Generic;

using System.Threading;

using U3DEventFrame;

//CopyRigtht by Lic 2016
//All rights received


public class NetWorkToServer
{
    private Queue<MsgBase> receiveMsgPool = new Queue<MsgBase>();
    private Queue<MsgBase> sendMsgPool = new Queue<MsgBase>();
    private NetSocket clientSocket;
    private Thread sendThread;



    private NetSocket.NormalNetCallBack m_callBackNormal;
    private NetSocket.NormalNetCallBack m_callBackConnect;
    private NetSocket.NormalNetCallBack m_callBackSend;
    private NetSocket.ReceiveCallBack   m_callBackReceive;
    private NetSocket.NormalNetCallBack m_callBackDisConnect;



    //public NetWorkToServer(string ip, ushort port)
    //{
    //    clientSocket = new NetSocket();
    //    clientSocket.AsyncConnect(ip, port, ConnectCallBack, ReceiveCallBack);
    //    //clientSocket.AsyncDisconnect(DisConnectCallBack);
    //}


    public NetWorkToServer(string ip, ushort port, NetSocket.NormalNetCallBack connectFaled, NetSocket.NormalNetCallBack  sendFailed, NetSocket.ReceiveCallBack recvFailed, NetSocket.NormalNetCallBack disConnect )
    {

        this.m_callBackConnect = connectFaled;

        this.m_callBackSend = sendFailed;

        m_callBackReceive = recvFailed;

        m_callBackDisConnect = disConnect;

        clientSocket = new NetSocket();
        clientSocket.AsyncConnect(ip, port, ConnectCallBack, SendCallBack,ReceiveCallBack,DisConnectCallBack);
        //clientSocket.AsyncDisconnect(DisConnectCallBack);
    }


    public void Disconnect()
    {
        clientSocket.AsyncDisconnect();
    }

    #region  网络方法回调


    private void ConnectCallBack(bool isSuccess, ErrorSockets error, string expection)
    {
        if (isSuccess)
        {
            sendThread = new Thread(LoopSending);
            sendThread.Start();


        }
        else
        {
            this.m_callBackConnect(isSuccess, error, expection);
        }


       

    }
    private void ReceiveCallBack(bool isSuccess, ErrorSockets error, string expection, byte[] byteData, string strMsg)
    {
        if (isSuccess)
        {
            ReceiveMsgToNetMsg(byteData);
        }
        else
        {

         //  Debug.Log(error + expection);
            m_callBackReceive(isSuccess,error,expection,byteData,strMsg);
        }

       // Debug.Log(error + expection);
    }
    private void SendCallBack(bool isSuccess, ErrorSockets error, string expection)
    {
        if (!isSuccess)
        {
            m_callBackSend(isSuccess,error,expection);
        }
    



        //Debug.Log("send =="+error + expection);
        //Debug.Log(isSuccess);


    }

    private void DisConnectCallBack(bool isSuccess, ErrorSockets error, string expection)
    {
        if (isSuccess)
        {
            sendThread.Abort();

        }
        else
        {
            this.m_callBackDisConnect(isSuccess, error, expection);
           //Debug.Log(error + expection);
        }

      //  Debug.Log(error + expection);
    }


    #endregion

    #region 其他方法


    public void PutSendMessageToPool(MsgBase msg)
    {
        lock (sendMsgPool)
        {
            sendMsgPool.Enqueue(msg);
        }
    }

    public void Update()
    {
        if (receiveMsgPool != null && receiveMsgPool.Count > 0)
        {
     
       

                while (receiveMsgPool.Count > 0)
                {
                    AnalyseData(receiveMsgPool.Dequeue());
                }

    
        }
    }

    ///TODO :ADD MORE

    /// <summary>
    /// 发送到上层
    /// </summary>
    /// <param name="msg"></param>
    private void AnalyseData(MsgBase msg)
    {
        MsgCenter.instance.SendToMsg(msg);
    }
    /// <summary>
    /// 转换为网络数据,并加入队列
    /// </summary>
    /// <param name="data"></param>
    private void ReceiveMsgToNetMsg(byte[] data)
    {
        MsgBase tmp = new MsgBase(data);

        //Debug.Log("socket    recv 1111==" + tmp.msgId);

        receiveMsgPool.Enqueue(tmp);
    }

    private void LoopSending()
    {
        while (clientSocket != null && clientSocket.isConnect())
        {
            lock (sendMsgPool)
            {
                while (sendMsgPool.Count > 0)
                {
                    MsgBase msg = sendMsgPool.Dequeue();
                    clientSocket.AsyncSend(msg.GetNetBytes());
                }
            }
            Thread.Sleep(100);
        }
    }


    #endregion
}
