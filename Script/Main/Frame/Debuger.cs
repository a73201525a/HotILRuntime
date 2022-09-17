using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Debuger
{
    static bool EnableLog = true;

    //static UDPSocket udpSocket = null;

    //static UDPSocket UDPSocket
    //{
    //    get
    //    {
    //        if (udpSocket == null)
    //        {
    //            udpSocket = new UDPSocket();
    //            udpSocket.BindSocket(18001, 1024, null);
    //        }
    //        return udpSocket;
    //    }


    //}

    public static void Log(object message, Object context)
    {
        if (EnableLog)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor||Application.platform==RuntimePlatform.OSXEditor)
            {
                Debug.Log(message, context);
            }
            else
            {
                //byte[] data = Encoding.Default.GetBytes(message.ToString());
                //udpSocket.SendData("0.0.0.0", data, 18001);
            }
        }
       
    }

    public static void Log(object message)
    {
        if (EnableLog)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                Debug.Log(message);
            }
            else
            {
                //byte[] data = Encoding.Default.GetBytes(message.ToString());
                //udpSocket.SendData("0.0.0.0", data, 18001);
            }
        }
       
    }


}
