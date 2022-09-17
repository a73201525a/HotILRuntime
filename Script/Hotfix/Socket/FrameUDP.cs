using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UPDEvent
{

}

public class FrameUDP:ILUIBase 
{
    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        switch (tmpMsg.msgID)
        {
            case (ushort)0:
                {


                }
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        //注册消息
        msgIds = new ushort[]
        {

        };
        RegistSelf(this, msgIds);

    }

    void Start()
    {


    }
    void Update()
    {

    }
}
