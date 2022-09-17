using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Net;

public class UDPSocket
{
    public delegate void UDPScoketDeletagte(byte[] pBuf, int dwCount, string tmpIp, ushort tmpPort);

    UDPScoketDeletagte udpDelegate;

    IPEndPoint udpIp;

    Socket udpSocket;

    byte[] recvData;

    Thread recvThread;

    public bool BindSocket(ushort port, int bufferLength, UDPScoketDeletagte tmpDelegate)
    {
        udpIp = new IPEndPoint(IPAddress.Any, port);

        UDPConnect();

        udpDelegate = tmpDelegate;

        recvData = new byte[bufferLength];

        recvThread = new Thread(RecvDataThread);

        recvThread.Start();

        return true;
    }

    public void UDPConnect()
    {
        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        udpSocket.Bind(udpIp);
    }
    bool isRunding = true;
    public void RecvDataThread()
    {
        while (isRunding)
        {
            if (udpSocket == null || udpSocket.Available < 1)
            {
                Thread.Sleep(100);

                continue;
            }

            lock (this)
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

                EndPoint remote = (EndPoint)sender;

                int myCount = udpSocket.ReceiveFrom(recvData, ref remote);

                if (udpDelegate != null)
                {
                    udpDelegate(recvData, myCount, remote.AddressFamily.ToString(),(ushort)sender.Port);
                }
            }
        }
    }

    public int SendData(string ip,byte[] data,ushort uport)
    {
        IPEndPoint sendToIp = new IPEndPoint(IPAddress.Parse(ip), uport);

        if (!udpSocket.Connected)
        {
            UDPConnect();
        }

        int mySend = udpSocket.SendTo(data, data.Length, SocketFlags.None, sendToIp);

        return mySend;
    }
}
