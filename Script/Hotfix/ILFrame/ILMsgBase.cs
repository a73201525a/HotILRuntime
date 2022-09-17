using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ILMsgBase
{
    public ushort msgID;


    public ILManagerID GetManager()
    {
        int tmpID = msgID / ILFrameTools.msgSpan;

        return (ILManagerID)(tmpID * ILFrameTools.msgSpan);
    }

    public ILMsgBase(ushort tmpMsg)
    {
        msgID = tmpMsg;
    }

    public ILMsgBase()
    {
        msgID = 0;
    }
}

public class MsgTransform : ILMsgBase
{
    public Transform value;


    public MsgTransform(ushort tmpMsg, Transform tmpTrans) : base(tmpMsg)
    {
        this.msgID = tmpMsg;

        this.value = tmpTrans;
    }
}
