using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class IAdventureManager : ILManagerBase
{
    
    //缓存事件队列
    public static Queue<ILMsgBase> MsgQueue = new Queue<ILMsgBase>();
    private static IAdventureManager instance;
    public static IAdventureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new IAdventureManager();
                GameObject obj = new GameObject("IAdventureManager");
                ILMonoBehaviour mono = obj.AddComponent<ILMonoBehaviour>();
                mono.DontDestoryObj(obj);
                mono.OnUpdate += Update;
            }
            return instance;
        }
    }
    static void Update()
    {
        if (MsgQueue.Count > 0)
        {
            ILMsgBase msg = MsgQueue.Dequeue();
            Instance.SendMsg(msg);
        }
    }
    
    public void SendMsg(ILMsgBase msg)
    {
        if (msg.GetManager() == ILManagerID.AdventureManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }
    /// <summary>
    /// 下一帧在执行事件
    /// </summary>
    /// <param name="msg"></param>
    public async void SendAwaitMsg(ILMsgBase msg)
    {
         await  Task.Delay(100);
        if (msg.GetManager() == ILManagerID.AdventureManager)
        {
            MsgQueue.Enqueue(msg);
            ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }
    
   
    public void Initialize()
    {
      
       
    }
  
 
}
