using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase
{
    public static UIManager  Instance = null;

    //存储UI控件引用，方便调用
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
        if (msg.GetManager() == ManagerID.UIManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            MsgCenter.Instance.SendToMsg(msg);
        }
    }

    public void RegistGameObject(string name,GameObject obj)
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

    public GameObject GetGameObject(string name)
    {
        if (!sonMembers.ContainsKey(name))
        {
            Debug.LogError("not find gameobject name===" + name);
            return null;
        }
        return sonMembers[name];
    }
}
