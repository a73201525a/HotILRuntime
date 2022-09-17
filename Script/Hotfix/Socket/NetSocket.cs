using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.Text;

public class NetSocket
{
    public delegate void CallBackNormal(bool sucess, ErrorSocket error, string exception);

    // public delegate void CallBackSend(bool sucess, ErrorSocket error, string exception);

    public delegate void CallBackRecv(bool sucess, ErrorSocket error, string exception, byte[] byteMessage, string strMessage);

    // public delegate void CallBackDisConnent(bool sucess, ErrorSocket error, string exception);

    private CallBackNormal callBackConnect;

    private CallBackNormal callBackSend;

    private CallBackNormal callBackDisconnect;

    private CallBackRecv callBackRecv;

    public enum ErrorSocket
    {
        Sucess = 0,
        TimeOut,
        SocketNull,
        SocketUnConnect,

        ConnectSucess,
        ConnectUnSucessUnKnow,
        ConnectError,

        SendSucess,
        SendUnSucessUnKown,
        RecvUnSucessUnKown,

        DisConnectSucess,
        DisConnectUnKown
    }

    public ErrorSocket errorSocket;

    private Socket clientSocket;

    private string addressIp;

    private ushort port;

    SocketBuffer recvBuffer;

    byte[] recvBuf;

    public NetSocket()
    {
        recvBuffer = new SocketBuffer(6, RecvMsgOver);
        recvBuf = new byte[1024];
    }



    #region Connect

    public bool IsConnect()
    {
        if (clientSocket != null && clientSocket.Connected)
        {
            return true;
        }
        return false;
    }

    public void AsyncConnect(string ip, ushort port, CallBackNormal tmpConnectBack, CallBackRecv tmpRecvBack)
    {
        errorSocket = ErrorSocket.Sucess;

        this.callBackConnect = tmpConnectBack;

        this.callBackRecv = tmpRecvBack;

        if (clientSocket != null && clientSocket.Connected)
        {
            this.callBackConnect(false, ErrorSocket.ConnectError, "connect repeat");
        }
        else if (clientSocket == null || !clientSocket.Connected)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAdress = IPAddress.Parse(ip);

            IPEndPoint endPoint = new IPEndPoint(ipAdress, port);
 
            IAsyncResult connect = clientSocket.BeginConnect(endPoint, ConnectCallBack, clientSocket);

            if (!WriteDot(connect))
            {
                this.callBackConnect(false, errorSocket, "连接超时");
            }
        }

    }

    private void ConnectCallBack(IAsyncResult asconnect)
    {
        try
        {
            clientSocket.EndConnect(asconnect);

            if (clientSocket.Connected == false)
            {
                errorSocket = ErrorSocket.ConnectUnSucessUnKnow;

                this.callBackConnect(false, errorSocket, "连接失败");

                return;
            }
            else
            {
                //连接成功
                errorSocket = ErrorSocket.ConnectSucess;
                this.callBackConnect(true, errorSocket, "连接成功");
                Receive();
            }
        }
        catch (Exception ec)
        {
            errorSocket = ErrorSocket.ConnectError;
            this.callBackConnect(false, errorSocket, ec.ToString());
        }
    }


    #endregion

    #region Recv

    public void Receive()
    {
        if (clientSocket != null && clientSocket.Connected)
        {
            IAsyncResult recv = clientSocket.BeginReceive(recvBuf, 0, recvBuf.Length, SocketFlags.None, ReciveCallBack, clientSocket);

            //if (!WriteDot(recv))
            //{
            //    callBackRecv(false, ErrorSocket.RecvUnSucessUnKown, "recv false", null, "");
            //}
        }
    }

    private void ReciveCallBack(IAsyncResult ar)
    {
     
        try
        {
           
            if (!clientSocket.Connected)
            {
                this.callBackRecv(false, ErrorSocket.RecvUnSucessUnKown, "连接断开", null, "");
                return;
            }

            int length = clientSocket.EndReceive(ar);

            if (length == 0)
            {
                return;
            }

            recvBuffer.RecvByte(recvBuf, length);
        }
        catch (Exception ec)
        {

            callBackRecv(false, ErrorSocket.RecvUnSucessUnKown, ec.ToString(), null, "");
        }
        Receive();
    }

    #endregion

    #region RecvMsgOver

    public void RecvMsgOver(byte[] allByte)
    {
        callBackRecv(true, ErrorSocket.Sucess, "", allByte, "recv back sucess");
    }

    #endregion

    #region send message
    public void SendCallBack(IAsyncResult ar)
    {
        try
        {
            int byteSend = clientSocket.EndSend(ar);
            if (byteSend > 0)
            {
                callBackSend(true, ErrorSocket.SendSucess, "send  sucess");
            }
            else
            {
                callBackSend(false, ErrorSocket.SendUnSucessUnKown, "SendUnSucessUnKown");
            }
        }
        catch (Exception ec)
        {
            callBackSend(false, ErrorSocket.SendUnSucessUnKown, ec.ToString());

        }


    }

    public void AsynSend(byte[] sendBuffer, CallBackNormal tmpSendBack)
    {
        errorSocket = ErrorSocket.Sucess;

        this.callBackSend = tmpSendBack;

        if (clientSocket == null)
        {
            this.callBackSend(false, ErrorSocket.SocketNull, "socket null");
            return;
        }
        else if (!clientSocket.Connected)
        {
            this.callBackSend(false, ErrorSocket.SocketUnConnect, "未连接");
        }
        else
        {
            IAsyncResult asySend = clientSocket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, SendCallBack, clientSocket);
            if (!WriteDot(asySend))
            {
                callBackSend(false, ErrorSocket.TimeOut, "send failed");
            }
        }
    }
    #endregion

    #region TimeOut check

    bool WriteDot(IAsyncResult ar)
    {
        int index = 0;
        while (ar.IsCompleted == false)
        {
            index++;
            if (index > 20)
            {
                errorSocket = ErrorSocket.TimeOut;
                return false;
            }
            Thread.Sleep(100);
        }
        return true;
    }


    #endregion

    #region  Disconnect

    public void DisconnectCallBack(IAsyncResult ar)
    {
        try
        {
            clientSocket.EndDisconnect(ar);

            clientSocket.Close();

            clientSocket = null;

            this.callBackDisconnect(true, ErrorSocket.DisConnectSucess, "DisConnectSucess");
        }
        catch (Exception ec)
        {
            this.callBackDisconnect(true, ErrorSocket.DisConnectUnKown, ec.ToString());

        }
    }

    public void AsyncDisconnect(CallBackNormal tmpDisconnectBack)
    {
        try
        {
            errorSocket = ErrorSocket.Sucess;

            this.callBackDisconnect = tmpDisconnectBack;

            if (clientSocket == null)
            {
                errorSocket = ErrorSocket.DisConnectUnKown;
                this.callBackDisconnect(false, errorSocket, "client is null");
            }
            else if (!clientSocket.Connected)
            {
                errorSocket = ErrorSocket.DisConnectUnKown;
                this.callBackDisconnect(false, errorSocket, "client is Unconnent");
            }
            else
            {
                IAsyncResult ar = clientSocket.BeginDisconnect(false, DisconnectCallBack, clientSocket);

                if (!WriteDot(ar))
                {
                    callBackDisconnect(false, ErrorSocket.DisConnectUnKown, "disconnect filed");
                }
            }
        }
        catch (Exception)
        {

            callBackDisconnect(false, ErrorSocket.DisConnectUnKown, "disconnect filed");
        }
    }


    #endregion
}
