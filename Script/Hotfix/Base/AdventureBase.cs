using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;
using UnityEngine.UI;

public class AdventureBase : ILMonoBase
{
    public ushort[] msgIds;
    //注册关卡
    public void RegisteSelf(ILMonoBase iLMono,params ushort[] msgs)
    {
        IAdventureManager.Instance.RegistMsg(iLMono, msgs);
    }
    //取消关卡注册
    public void UnRegistDelf(ILMonoBase iLMono,params ushort[] msgs )
    {
        IAdventureManager.Instance.UnRegistMsg(iLMono, msgs);
    }
    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        //监听事件
       
    }
    /// <summary>
    //消息携带者
    /// </summary>
    ILMsgBase msg;
    public void SendMsg(ILMsgBase msg)
    {
        IAdventureManager.Instance.SendMsg(msg);
    }
    /// <summary>
    /// 发送UI消息执行
    /// </summary>
    /// <param name="msgid"></param>
    public void SendMsg(ushort msgid)
    {
        if (msg == null)
        {
            msg = new ILMsgBase(msgid);
        }
        else
        {
            msg.msgID = msgid;
        }

        ILUIManager.Instance.SendMsg(msg);
    }
    /// <summary>
    /// 发送等待一帧消息
    /// </summary>
    /// <param name="msg"></param>
    public void SendAwaitMsg(ILMsgBase msg)
    {
        IAdventureManager.Instance.SendAwaitMsg(msg);
    }
    public void Destory(GameObject obj)
    {
        OnDestory();
        GameObject.Destroy(obj);
    }
    public virtual void OnDestory()
    {
        if (msgIds!=null)
        {
            UnRegistDelf(this, msgIds);
        }
    }
}
