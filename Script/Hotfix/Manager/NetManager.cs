using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : ILManagerBase
{
    private static NetManager instance;
    public static NetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetManager();
            }
            return instance;
        }
    }

    //private void Awake()
    //{
    //    Instance = this;
    //}

    /// <summary>
    /// 执行对应模块事件
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(ILMsgBase msg)
    {
        if (msg.GetManager() == ILManagerID.NetManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }
}
