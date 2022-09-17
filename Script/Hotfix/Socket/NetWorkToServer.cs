using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class NetWorkToServer
{
    private Queue<NetMsgBase> recvMsgPool = null;

    private Queue<NetMsgBase> sendMsgPool = null;

    NetSocket clientSocket;

    Thread sendThread;

    public ILMonoBehaviour mono;

    public NetWorkToServer(string ip, ushort port)
    {
        recvMsgPool = new Queue<NetMsgBase>();

        sendMsgPool = new Queue<NetMsgBase>();

        clientSocket = new NetSocket();

        clientSocket.AsyncConnect(ip, port, AsysnConnectCallBack, AsysnRecvCallBack);

        GameObject obj = new GameObject();
        obj.name = "NetWork";
        mono = obj.AddComponent<ILMonoBehaviour>();
        mono.ILOnApplicationQuit += OnApplicationQuit;

    }

    void OnApplicationQuit()
    {
         //sendThread.Abort();
         if(clientSocket != null)
        clientSocket = null;
    }

    void AsysnConnectCallBack(bool sucess, NetSocket.ErrorSocket tmpError, string exception)
    {
        if (sucess)
        {
            sendThread = new Thread(LoopSendMsg);

            sendThread.Start();

            Debuger.Log("Connect sucess");
        }
        else
        {
            Debuger.Log(exception);
        }
    }

    #region send

    public void PutSendMsgToPool(NetMsgBase msg)
    {
        lock (sendMsgPool)
        {
            sendMsgPool.Enqueue(msg);
            Debuger.Log(string.Format("<color=#ff0000>{0}</color>", "发送消息 id：  " + msg.msgID + "  Count: " + msg.GetNetBytes().Length));
        }
    }

    void CallBackSend(bool sucess, NetSocket.ErrorSocket tmpError, string exception)
    {
        //发送成功
        if (sucess)
        {

        }
        else
        {

        }
    }


    void LoopSendMsg()
    {
        while (clientSocket != null && clientSocket.IsConnect())
        {
            lock (sendMsgPool)
            {
                while (sendMsgPool.Count > 0)
                {
                    NetMsgBase tmpBody = sendMsgPool.Dequeue();

                    clientSocket.AsynSend(tmpBody.GetNetBytes(), CallBackSend);


                }
            }
            Debug.Log("sssss");
            Thread.Sleep(100);
        }
    }

    #endregion


    #region recv
    void AsysnRecvCallBack(bool sucess, NetSocket.ErrorSocket error, string exception, byte[] byteMessage, string strMessage)
    {
        if (sucess)
        {
            PutRecvMsgToPool(byteMessage);
        }
        else
        {
            //处理错误信息
        }
    }

    void PutRecvMsgToPool(byte[] recMsg)
    {
        NetMsgBase tmp = new NetMsgBase(recMsg);

        recvMsgPool.Enqueue(tmp);

    }

    public void Update()
    {
        if (recvMsgPool != null)
        {
            while (recvMsgPool.Count > 0)
            {
                NetMsgBase tmp = recvMsgPool.Dequeue();

                AnalyseData(tmp);
            }
        }
    }

    void AnalyseData(NetMsgBase msg)
    {
        //消息派发
        Debuger.Log(string.Format("<color=#ff0000>{0}</color>", "收到消息 id：  " + msg.msgID + "  Count: " + msg.GetNetBytes().Length));
        ILMsgCenter.Instance.SendToMsg(msg);
    }

    #endregion


    #region Disconnect

    void CallBackDisconnect(bool sucess, NetSocket.ErrorSocket tmpError, string exception)
    {
        if (sucess)
        {
            sendThread.Abort();
        }
        else
        {
            Debug.Log("exception:" + exception);
        }

    }

    public void Disconnect()
    {
        if (clientSocket != null && clientSocket.IsConnect())
        {
            clientSocket.AsyncDisconnect(CallBackDisconnect);
        }
    }

    #endregion




}
