using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : ManagerBase
{
    public static AssetManager Instance = null;

 

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 执行对应模块事件
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(MsgBase msg)
    {
        if (msg.GetManager() == ManagerID.AssetManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            MsgCenter.Instance.SendToMsg(msg);
        }
    }
}
