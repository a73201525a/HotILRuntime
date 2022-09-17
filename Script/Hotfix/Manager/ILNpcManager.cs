using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILNpcManager : ILManagerBase
{
   
    private static ILNpcManager instance;
    public static ILNpcManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ILNpcManager();
            }
            return instance;
        }
    }

    //public void Awake()
    //{
    //    Instance = this;
    //}

    /// <summary>
    /// 执行对应模块事件
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(ILMsgBase msg)
    {
        if (msg.GetManager() == ILManagerID.NpcManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }
    public async void SendAwaitMsg(ILMsgBase msg)
    {
        await System.Threading.Tasks.Task.Delay(100);
        if (msg.GetManager() == ILManagerID.NpcManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }
    public void Initialize()
    {

        PlayerController player = new PlayerController();
        player.Awake();
      
        //Player play = new Player();
        //play.Awake();



    }
}
