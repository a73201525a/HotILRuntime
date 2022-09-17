using ProtoMsg;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoConfig
{
    //c client s server
    //key 消息ID  值 是对应的类型
    //客户端 发送 服务器 所有协议 
    private static Dictionary<int, Type> C2SPB = new Dictionary<int, Type>();
    //服务器 发送 客户端 所有协议
    private static Dictionary<int, Type> S2CPB = new Dictionary<int, Type>();

    //初始化 记得调用
    public static void Init()
    {

        foreach (ProtoMsgType item in Enum.GetValues(typeof(ProtoMsgType)))
        {
            string tmpStr = item.ToString();
            tmpStr = tmpStr.Substring(tmpStr.Length - 2);

            if (tmpStr == "Cs")
            {
                if (!C2SPB.ContainsKey((ushort)item)) AddC2SPB((ushort)item);
                else Debuger.Log("messageid repeat");
            }
            else if (tmpStr == "Sc")
            {
                if (!C2SPB.ContainsKey((ushort)item)) AddS2CPB((ushort)item);
                else Debuger.Log("messageid repeat");
            }
            else
            {
                if((ushort)item != 0)
                Debuger.Log("proto error item:"+ item);
            }
        }
     
       

    }

    static void AddC2SPB(ushort messageId)
    {
        //添加协议的方法
        C2SPB.Add(messageId, typeof(TestProto));
        //C2SPB.Add(messageId, typeof(TestProto2));
    }

    static void AddS2CPB(ushort messageId)
    {
        S2CPB.Add(messageId, typeof(TestProto));
    }

    //客户端发送给服务器 检查发送消息ID是否合法
    public static bool CheckC2SPB(int id)
    {
        //检查是否包含key的方法
        return C2SPB.ContainsKey(id);
    }

    //服务器发送给客户端 检查消息ID是否合法
    public static bool CheckS2CPB(int id)
    {
        return S2CPB.ContainsKey(id);
    }

}
