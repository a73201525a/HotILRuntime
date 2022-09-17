using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : ManagerBase
{
    public static NPCManager Instance = null;

    private Dictionary<string, GameObject> sonMembers = new Dictionary<string, GameObject>();


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

    public void RegistGameObject(string name, GameObject obj)
    {
        if (!sonMembers.ContainsKey(name))
        {
            sonMembers.Add(name, obj);
        }
    }
    public void UnRegistGameObject(string name)
    {
        if (sonMembers.ContainsKey(name))
        {
            sonMembers.Remove(name);
        }
    }
}
