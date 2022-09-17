using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBase
{
    public ushort[] msgIds;

    public void RegistSelf(MonoBase mono, params ushort[] msgs)
    {
        UIManager.Instance.RegistMsg(mono, msgs);
    }

    public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
    {
        UIManager.Instance.UnRegistMsg(mono, msgs);
    }

    /// <summary>
    /// 调用其他脚本功能
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(MsgBase msg)
    {
        UIManager.Instance.SendMsg(msg);
    }

    public override void ProcessEvent(MsgBase tmpMsg)
    {
        
    }
    private void OnDestroy()
    {
        if (msgIds != null)
        {
            UnRegistSelf(this, msgIds);
        }
    }
}
