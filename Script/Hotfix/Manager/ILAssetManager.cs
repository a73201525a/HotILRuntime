using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILAssetManager : ILManagerBase
{
    //public static ILAssetManager Instance = null;

    private static ILAssetManager instance;
    public static ILAssetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ILAssetManager();
            }
            return instance;
        }
    }

    //public void Awake()
    //{
    //    Instance = this;
    //}

    /// <summary>
    /// ִ�ж�Ӧģ���¼�
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(ILMsgBase msg)
    {
        if (msg.GetManager() == ILManagerID.AssetManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }




}
