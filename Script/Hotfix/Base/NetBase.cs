﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetBase : ILMonoBase
{
    public ushort[] msgIds;

    public void RegistSelf(ILMonoBase mono, params ushort[] msgs)
    {
        NetManager.Instance.RegistMsg(mono, msgs);
    }

    public void UnRegistSelf(ILMonoBase mono, params ushort[] msgs)
    {
        NetManager.Instance.UnRegistMsg(mono, msgs);
    }

    /// <summary>
    /// 调用其他脚本功能
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(ILMsgBase msg)
    {
        NetManager.Instance.SendMsg(msg);
    }

    public override void ProcessEvent(ILMsgBase tmpMsg)
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
