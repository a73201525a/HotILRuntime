using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TipPanel : ILUIBase
{
    class TipItem
    {
        public GameObject gameObject;
        public float stayTime;
        public RectTransform transform;
        public string tipText;
        Text text;
        public bool Finsh;
        public TipItem(GameObject obj, string tipText, float stayTime = 1f)
        {
            this.gameObject = obj;
            this.tipText = tipText;
            this.stayTime = stayTime;
            Finsh = false;
            transform = gameObject.transform.GetComponent<RectTransform>();
            text = gameObject.transform.GetComponentInChildren<Text>();
            text.enabled = false;
            ContentSizeFitter contentSizeFitter = gameObject.transform.GetComponent<ContentSizeFitter>();
            contentSizeFitter.SetLayoutHorizontal();
        }

        public void Init(Transform messageParent, Vector3 point, float stayTime = 1f)
        {
            gameObject.transform.parent = messageParent;
            transform.anchoredPosition = point;
            text.enabled = true;
            text.text = tipText;
            this.stayTime = stayTime;
            gameObject.SetActive(true);
            Finsh = false;
        }

        public void DoMove()
        {
            gameObject.transform.DOLocalMoveY(800, 1f).OnComplete(Finish);
        }
        public void Finish()
        {


            gameObject.SetActive(false);
            Finsh = true;
        }
    }
    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        //监听事件
        switch (tmpMsg.msgID)
        {
            case (ushort)ILUIEventPanel.TipPanel:
                {
                    Init();
                    OnShow();
                }
                break;
            case (ushort)ILUIEvent.ShowTipText://传入显示文本内容
                {
                    TipMsg tipMsg = (TipMsg)tmpMsg;

                    AddTip(tipMsg.TipConnet);
                }
                break;
            default:
                break;
        }
    }


    public override void Awake()
    {
        Name = "TipPanel";
        //注册消息
        msgIds = new ushort[]
        {
            (ushort)ILUIEventPanel.TipPanel,
            (ushort)ILUIEvent.ShowTipText,//显示提示文本
        };
        RegistSelf(this, msgIds);


    }

    GameObject tipText;
    Transform messageParent;
    Vector3 StartPoint = new Vector3(100, 0, 0);
    //消息队列
    Queue<TipItem> messageQueue;

    List<TipItem> tipingList;

    List<TipItem> poolItem;


    public override void Init()
    {
        base.Init();
        messageQueue = new Queue<TipItem>();
        tipingList = new List<TipItem>();
        poolItem = new List<TipItem>();
        tipText = transform.Find("TipBg").gameObject;
        messageParent = transform.Find("Message");
        ILMonoBehaviour iLMono = transform.gameObject.AddComponent<ILMonoBehaviour>();
        iLMono.OnUpdate += Update;

    }

    //显示tiptext内容
    public void AddTip(string connet)
    {
        if (poolItem.Count > 0)
        {
            for (int i = 0; i < poolItem.Count; i++)
            {
                if (poolItem[i].Finsh)
                {
                    poolItem[i].tipText = connet;
                    messageQueue.Enqueue(poolItem[i]);
                    return;
                }
            }
        }
        GameObject tmp = GameObject.Instantiate(tipText);
        TipItem tmpTiem = new TipItem(tmp, connet);
        messageQueue.Enqueue(tmpTiem);
        poolItem.Add(tmpTiem);
    }
    public void Update()
    {
        if (messageQueue.Count > 0)
        {
            TipItem tip = messageQueue.Dequeue();
            tip.Init(messageParent, StartPoint);
            for (int i = 0; i < tipingList.Count; i++)
            {
                tipingList[i].transform.anchoredPosition = new Vector2(StartPoint.x, StartPoint.y + (50 * (tipingList.Count - i)));
            }
            tipingList.Add(tip);
        }
        for (int i = 0; i < tipingList.Count; i++)
        {
            tipingList[i].stayTime -= Time.deltaTime;
            if (tipingList[i].stayTime <= 0)
            {
                tipingList[i].DoMove();
                tipingList.RemoveAt(i);
            }
        }
        //DoAnimation();
    }
    public override void OnShow()
    {

    }
    public override void OnHide()
    {

    }
}
