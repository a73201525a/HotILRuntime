using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;
using UnityEngine.UI;

public class EventCSClass : ILUIBase
{
    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        //监听事件
        switch (tmpMsg.msgID)
        {
            case (ushort)ILUIEventPanel.LoadPanel:
                {
                   
                }
                break;
            default:
                break;
        }
    }


    public override void Awake()
    {
        Name = "MainScenePanel";
        //注册消息
        msgIds = new ushort[]
        {
            (ushort)ILUIEventPanel.LoadPanel,
        };
        RegistSelf(this, msgIds);


    }


    public override void Init()
    {
        base.Init();

        transform.Find("BackGround/CloseBtn").GetComponent<Button>().onClick.AddListener(CloseClick);

    }


    private void CloseClick()
    {
        ILUIManager.HideView(Name);
    }

    public override void OnShow()
    {

    }

    public override void OnHide()
    {

    }
}
