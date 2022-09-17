using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google.Protobuf;
using DG.Tweening;

public class Person
{
    public string Name;
    public int Age;
}
public class MainOthers : AssetBase
{
    public override void ProcessEvent(MsgBase tmpMsg)
    {
        switch (tmpMsg.msgID)
        {
            case (ushort)UIEventHao.Load:
                {
                    HunkAssetResBack tmp = (HunkAssetResBack)tmpMsg;
                    GameObject obj = (GameObject)tmp.value;
                    GameObject.Instantiate(obj);
                }
                break;
            case (ushort)UIEventHao.Load1:
                {

                }
                break;
        }
    }

    void Awake()
    {
        //注册消息
        msgIds = new ushort[]
        {
            (ushort)UIEventHao.Load,
        };
        //  RegistSelf(this, msgIds);

        //gameObject.AddComponent<NativeResLoader>();
        //gameObject.AddComponent<ILoaderManager>();
        //gameObject.AddComponent<TcpSocket>();

        //ProtoConfig.Init(); 初始化protobuf
        //LoadNode[,] a = new LoadNode[10,10];
        //Debug.Log(a[1, 2]);
        //Debug.Log(Application.dataPath + "/Art/TableData/ShopData");
        //TableBase<ShopTable>.Initialize(Application.dataPath + "/Art/TableData/ShopData");//初始化配置表


        Debug.Log("2222222222");
        //BulletData data = new BulletData();
        //data.AnglesHorizontal = 60;
        //BulletData p =  GameObject.Find("GameObject").GetComponent<BulletData>();
        //BulletJson jsonobj = new BulletJson();

        //jsonobj.MaxLevel = p.MaxLevel;
        //string json = LitJson.JsonMapper.ToJson(jsonobj);
        //Debug.Log(json);
        //BulletJson a = LitJson.JsonMapper.ToObject<BulletJson>(json);
        //Debug.Log(a.MaxLevel);
    }

    public RectTransform obj1;//hero
    public RectTransform obj2;//monster

    void Test()
    {
        Vector2 heroToMonster = obj2.anchoredPosition - obj1.anchoredPosition;
        //怪物在hero右侧

        Debug.Log("right:" + obj1.right);
        Debug.Log("forward:" + obj1.forward);
        Debug.Log(Vector2.Dot(obj1.right, heroToMonster.normalized));
    }

    private void Start()
    {
        //HunkAssetRes load = new HunkAssetRes(true, (ushort)AssetEvent.HunkRes, "scenesone", "Load", "test", (ushort)UIEventHao.Load);
        //SendMsg(load);


        //TCPConnectMsg tmp = new TCPConnectMsg((ushort)TCPEvent.TcpConnect, "192.168.3.16", 18034);
        //SendMsg(tmp);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
            //TestProto proto = new TestProto();
            //proto.Id = 1;
            //proto.Name = "111";
            //proto.Str.Add("aaa");
            //proto.Str.Add("bbb");
            //proto.Str.Add("bbb");
            //proto.Str.Add("b啥啥啥bb");
            //proto.Str.Add("bb都是是收到b");
            //proto.Str.Add("bb收到收到收到收到是多少多少b");
            //proto.Str.Add("bbb");
            //proto.Str.Add("bbb");
            //proto.Str.Add("bbb");
            //proto.Str.Add("bbb");
            //proto.Str.Add("bbb");
            //proto.Str.Add("bbb");
            //BufferEntity msgData = new BufferEntity((ushort)1001, 0, proto);
            //byte[] tmp =   ProtoBufTool.Serialize(msgData);

            //TestProto t = TestProto.Parser.ParseFrom(ProtoBufTool.Deserialize(tmp));

            //Debug.Log(t.Id);
            //Debug.Log(t.Name);
            //Debug.Log(t.Str[1]);
            //byte[] test = proto.ToByteArray();
            //BufferEntity msgData = new BufferEntity((ushort)1001, 0, proto);
            //byte[] tmpBuffer = ProtoBufTool.Serialize(msgData);
            //Debug.Log(tmpBuffer.Length);
            //Debug.Log(test.Length);


            //TCPMsg msg = new TCPMsg((ushort)TCPEvent.TcpSendMsg, proto, ProtoMsgType.MsgTestCs);
            //SendMsg(msg);

            //Debug.Log(ShopTable.Instance.Get(1));
            //Debug.Log(ShopTable.Instance.data[1].is_system);


        }

    }
}

public class LoadNode : MsgBase
{
    public string sceneName;

    public string bundleName;

    public string resName;

    public ushort backMsgId;

    public bool isSingle;

    public LoadNode(ushort tmpMsg, string scenceName, string bundleName, string res, bool single, ushort backid)
    {
        this.msgID = tmpMsg;

        this.sceneName = scenceName;

        this.bundleName = bundleName;

        this.resName = res;

        this.isSingle = single;

        this.backMsgId = backid;

    }
}

