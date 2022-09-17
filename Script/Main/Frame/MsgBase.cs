using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBase 
{
    public ushort msgID;

    public ManagerID GetManager()
    {
        int tmpID = msgID / FrameTools.msgSpan;

        return (ManagerID)(tmpID * FrameTools.msgSpan);
    }
 
    public MsgBase(ushort tmpMsg)
    {
        msgID = tmpMsg;
    }

    public MsgBase()
    {
        msgID = 0;
    }
}

public class MsgTransform : MsgBase
{
    public Transform value;

  
    public MsgTransform(ushort tmpMsg, Transform tmpTrans):base(tmpMsg)
    {
        this.msgID = tmpMsg;

        this.value = tmpTrans;
    }
}
