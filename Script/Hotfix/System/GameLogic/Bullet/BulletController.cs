using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class BulletController : NPCBase
{
    private Dictionary<string, Object> BulletModel;
    private Dictionary<string, Object> BulletEfModel;

    private List<Bullet> BulletPool;

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        //�����¼�
        switch (tmpMsg.msgID)
        {
            case (ushort)ILNpcEvent.LoadBullet:
                {
                    ILHunkAssetResBack hunk = (ILHunkAssetResBack)tmpMsg;
                    if (!BulletModel.ContainsKey(hunk.value.name))
                    {
                        BulletModel.Add(hunk.value.name, hunk.value);
                    }
                }
                break;
            case (ushort)ILNpcEvent.LoadBulletEf:
                {
                    ILHunkAssetResBack hunk = (ILHunkAssetResBack)tmpMsg;
                    if (!BulletEfModel.ContainsKey(hunk.value.name))
                    {
                        BulletEfModel.Add(hunk.value.name, hunk.value);
                    }
                    BulletMsg msg = curBullermsg.Dequeue();
                    CreatBullet(msg.data, msg.tmpTarget, msg.point, msg.type, msg.attackData, msg.targetTransForm);
                }
                break;


            case (ushort)ILNpcEvent.CreartBullet:
                {
                    BulletMsg bulletMsg = (BulletMsg)tmpMsg;
                    curBullermsg.Enqueue(bulletMsg);
                    CreatBullet(bulletMsg.data, bulletMsg.tmpTarget, bulletMsg.point, bulletMsg.type, bulletMsg.attackData, bulletMsg.targetTransForm);
                }
                break;

            case (ushort)ILNpcEvent.UnLoadAllBullet:
                {
                    UnLoad();
                    OnDestroy();
                }
                break;
        }
    }
    public void Awake()
    {
        //ע����Ϣ
        msgIds = new ushort[]
        {
             (ushort)ILNpcEvent.LoadBullet,
             (ushort)ILNpcEvent.LoadBulletEf,
             (ushort)ILNpcEvent.CreartBullet,
             (ushort)ILNpcEvent.BulletInit,
             (ushort)ILNpcEvent.UnLoadAllBullet,
        };
        RegistSelf(this, msgIds);
        BulletPool = new List<Bullet>();
        Init();
    }

    /// <summary>
    /// ��ʼ���ӵ�
    /// </summary>
    /// <param name="data">����id����</param>
    public void Init()
    {
        BulletModel = new Dictionary<string, Object>();
        BulletEfModel = new Dictionary<string, Object>();

        ///��ǰ����bundle
        ILHunkAssetRes bullit = new ILHunkAssetRes("scenesone", "bullet", (ushort)ILAssetEvent.GetResObject);
        SendMsg(bullit);
    }

    /// <summary>
    /// �˳�ս����ж��ս�������е��ӵ���Դ
    /// </summary>
    public void UnLoad()
    {
        ILHunkAssetRes res1 = new ILHunkAssetRes("scenesone", "bullet", (ushort)ILAssetEvent.ReleaseBundleObj);
        SendMsg(res1);

        ILHunkAssetRes res2 = new ILHunkAssetRes("scenesone", "bullet", (ushort)ILAssetEvent.ReleaseSingleBundle);
        SendMsg(res2);

        BulletModel.Clear();
        BulletEfModel.Clear();
        BulletPool.Clear();
    }


    //��ǰ���ڴ������ӵ�
    Queue<BulletMsg> curBullermsg = new Queue<BulletMsg>();

    /// <summary>
    /// �ӵ�
    /// </summary>
    /// <param name="data"></param>
    /// <param name="tmpTarget">Ŀ���</param>
    /// <param name="point1">�����</param>
    /// <param name="type">�ӵ���Ӫ</param>
    /// /// <param name="tf">׷��Ŀ��</param>
    /// /// /// <param name="attackData">��������</param>
    internal void CreatBullet(BulletJson data, Vector3 tmpTarget, Vector3 point1, BulletCampType type, AttackData attackData, Transform tf = null)
    {
        if (!BulletModel.ContainsKey(data.Model))
        {

            ILHunkAssetRes bullit = new ILHunkAssetRes(true, (ushort)ILAssetEvent.GetResObject, "scenesone", "bullet", data.Model, (ushort)ILNpcEvent.LoadBullet);
            SendMsg(bullit);

            ILHunkAssetRes bullitEf = new ILHunkAssetRes(true, (ushort)ILAssetEvent.GetResObject, "scenesone", "bullet", data.BombEf, (ushort)ILNpcEvent.LoadBulletEf);
            SendMsg(bullitEf);

            return;

        }

        for (int i = 0; i < BulletPool.Count; i++)
        {

            if (BulletPool[i].isRuning == false && data.Model == BulletPool[i].data.Model && data.BombEf == BulletPool[i].data.BombEf)
            {
                BulletPool[i].Rest(data, tmpTarget, point1, type);

                return;
            }
        }

        Bullet bullet = new Bullet((GameObject)GameObject.Instantiate(BulletModel[data.Model]), (GameObject)GameObject.Instantiate(BulletEfModel[data.BombEf]), data, tmpTarget, point1, type, attackData, tf);
        BulletPool.Add(bullet);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        BulletModel.Clear();
        BulletEfModel.Clear();
        ILHunkAssetRes bullit = new ILHunkAssetRes( "scenesone", "bullet", (ushort)ILAssetEvent.ReleaseSingleBundle);
        SendMsg(bullit);

    }
}
