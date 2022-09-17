using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;
using System;
using System.Threading.Tasks;

public class ILUIBase : ILMonoBase
{
    public ushort[] msgIds;

    public Transform transform;

    public string Name;

    public virtual void Awake()
    {

    }
    public virtual void OnShow()
    {
       
    }

    public virtual void OnHide()
    {

    }

    public virtual void OnDestory()
    {
        if (msgIds != null)
        {
            UnRegistSelf(this, msgIds);

        }
        transform = null;
    }

    public string GetPanelName()
    {
        return Name;
    }


    public virtual void Init()
    {
        transform = ILUIManager.sonMembers[Name].Obj.transform;
        transform.localScale = Vector3.one;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

    }

    public void RegistSelf(ILMonoBase mono, params ushort[] msgs)
    {
        ILUIManager.Instance.RegistMsg(mono, msgs);
    }

    public void UnRegistSelf(ILMonoBase mono, params ushort[] msgs)
    {
        ILUIManager.Instance.UnRegistMsg(mono, msgs);
    }

    /// <summary>
    /// 调用其他脚本功能
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(ILMsgBase msg)
    {
        ILUIManager.Instance.SendMsg(msg);
    }

    public void SendMsg(ushort msgid)
    {
        ILMsgBase msg = new ILMsgBase(msgid);
        ILUIManager.Instance.SendMsg(msg);
    }

    public void SendAwaitMsg(ILMsgBase msg)
    {
        ILUIManager.Instance.SendAwaitMsg(msg);
    }

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {

    }
}
