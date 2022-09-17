using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBase : MonoBase
{
    public ushort[] msgIds;

    public void RegistSelf(MonoBase mono, params ushort[] msgs)
    {
        AssetManager.Instance.RegistMsg(mono, msgs);
    }

    public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
    {
        AssetManager.Instance.UnRegistMsg(mono, msgs);
    }

    /// <summary>
    /// 调用其他脚本功能
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(MsgBase msg)
    {
        AssetManager.Instance.SendMsg(msg);
    }

    public override void ProcessEvent(MsgBase tmpMsg)
    {

    }
    private void OnDestroy()
    {
        if (msgIds != null && this != null)
        {
            UnRegistSelf(this, msgIds);
        }
    }
}
