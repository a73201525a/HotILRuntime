using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.IO;

//子弹阵营
public enum BulletCampType
{
    PlayerBullet,//玩家子弹
    MonsterBullet,//怪物子弹
}
//public enum BulletType
//{
//    bomb,//炸弹 （抛物线）
//    bullet,//子弹
//}
public class Bullet : NPCBase
{
    ILMonoBehaviour mono;
    public bool isRuning;
    public AttackData attackData;


    public BulletCampType bulletCampType;//阵营子弹

    public GameBuffType buffType;//buff类型

    public float buffTriggerProbability;//buff触发概率

    public Transform transform;
    private Vector3 position;       // 起始位置
    private Vector3 target;           // 目标位置
    private GameObject boomEf;//爆炸效果
    private BulletType bulletType;//子弹类型


    public Transform tragetTrans;//追踪目标

    public BulletJson data;//参数数据

    public float speed = 1f;        // 实际用于速度计算的速度值
    public float dataSpeed = 0;//速度参数

    private AnimationCurveTool curve;//曲线速率控制器

    private double destroyTime;//自动销毁时间

    private float curveTime = 0;//曲线时间轴


    // Rigidbody rigidbody;
    /// <summary>
    /// 创建子弹
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="objEf"></param>
    /// <param name="data"></param>
    /// <param name="tmpTarget">目标位置</param>
    /// <param name="curPos">开火点</param>
    /// <param name="bulletCampType">子弹阵营归属</param>
    public Bullet(GameObject obj, GameObject objEf,
        BulletJson bulletData, Vector3 tmpTarget,
        Vector3 curPos, BulletCampType bulletCampType, AttackData attackData, Transform tragetTrans)
    {

        // rigidbody = obj.GetComponent<Rigidbody>();

        //obj.GetComponent<Rigidbody>().solverIterations = 255;    

        //obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward*5.0f , ForceMode.VelocityChange);
        // obj.name = bulletCampType.ToString();

        mono = obj.AddComponent<ILMonoBehaviour>();
        mono.EnterTrigger += EnterTrigger;
        mono.OnUpdate += Update;
        boomEf = objEf;
        transform = obj.transform;

        this.attackData = attackData;
        this.tragetTrans = tragetTrans;

        Init(bulletData, tmpTarget, curPos, bulletCampType);

        isRuning = true;

    }

    internal void Init(BulletJson bulletData, Vector3 tmpTarget, Vector3 curPos, BulletCampType bulletCampType)
    {
        //lary
        this.bulletCampType = bulletCampType;
        data = bulletData;
        ////给子弹的属性赋值
        height = data.AnglesVertical;
        dataSpeed = (float)data.Speed;
        ////给子弹的属性赋值
        ///
        destroyTime = 0;

        bulletType = data.bulletType;

        target = tmpTarget;// target.position;

        position = curPos;//

        Fire(position, target);

        if (data.areaType == SkillAreaType.OuterCircle_InnerCircle)
        {
            target.y = position.y;
            dirNom = target - position;

        }
        else if (data.areaType == SkillAreaType.None)
        {
            target.y = position.y;
            dirNom = position - target;
        }
        else
        {
            dirNom = target;
        }


        if (data.IsInfiniteSpeed)
        {
            //obj.SetActive(false);
            //speed = 999;
        }

        curve = transform.GetComponent<AnimationCurveTool>();

    }
    internal void Rest(BulletJson data, Vector3 tmpTarget, Vector3 curPos, BulletCampType bulletCampType)
    {

        Init(data, tmpTarget, curPos, bulletCampType);
        transform.gameObject.SetActive(true);
        boomEf.SetActive(false);
        isRuning = true;
    }


    private void Update()
    {
        //根据时间曲线来计算速度变化
        curveTime += Time.deltaTime;
        float scaleValue = curve.curve.Evaluate(curveTime);

        // Debug.Log("scaleValue::" + scaleValue);
        //Debug.Log("dataSpeed::" + dataSpeed);
        speed = dataSpeed * scaleValue;
        //  Debug.Log("bulletType::" + bulletType);
        switch (bulletType)
        {
            case BulletType.bullet:
                {
                    // Debug.Log("232323");
                    UpdateLine();
                }
                break;
            case BulletType.bomb:
                {
                    if (_isFiring)
                    {
                        UpdateArrow();
                    }
                }
                break;
            case BulletType.trace:
                {
                    UpdateTrace();
                }
                break;
            default:
                break;
        }

        destroyTime += Time.deltaTime;
        if (destroyTime > data.DestoryTime)
        {
            Destroy();
        }
    }
    /// <summary>
    /// 爆炸事件
    /// </summary>
    public async void BombEvent()
    {
        isRuning = false;
        await Task.Delay(data.BombWaitTime);
        transform.gameObject.SetActive(false);
        boomEf.transform.position = transform.position;
        boomEf.SetActive(true);
    }
    #region  编辑器变量

    //抛物线变量=======================================
    public float gravitySpeed = 0;//重力加速度
    public float gravity = -1;//重力
    public double height = 3;       // 箭的最大高度 
                                    //抛物线变量================end=======================




    #endregion


    #region  直线运动计算

    Vector3 dirNom;//运动方向

    void UpdateLine()
    {
        //  Debug.Log("dirNom.normalized:" + dirNom.normalized);
        //rigidbody.IsSleeping();
        //rigidbody.solverIterations = 255;
        transform.position += dirNom.normalized * speed * Time.deltaTime;
        Turn(dirNom);

    }

    Vector3 moveDirectory;
    void Turn(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.x, dir.z);

        angle = angle * Mathf.Rad2Deg;

        moveDirectory.y = angle;

        Quaternion lookQuaternion = Quaternion.Euler(moveDirectory);


        float turnSpeed = Mathf.Lerp(100, 400, speed);


        transform.rotation = Quaternion.RotateTowards
           (transform.rotation, lookQuaternion, turnSpeed * Time.deltaTime);
    }



    #endregion


    #region 抛物线计算

    // 所有变量

    public float rotateX = 90;      // 箭的最大X軸旋轉角度
    private Vector3 _startPos, _stopPos, _curPos;   // 起始位置，目標位置，當前位置
    private float _angleToStop;     // 從起始點到目標點的角度
    private float _startHeight, _stopHeight;    // 起始高度，結束高度
    private bool _isFiring = false;     //判斷箭是否正在移動
    private float _totalDistance, _curDistance; // 總距離， 當前距離
    private Vector3 _curRotation; // 當前的旋轉角度

    // 发射函数，只要调用这一个函数就可以发射
    public void Fire(Vector3 start, Vector3 stop)
    {
        _startPos = start;
        _stopPos = stop;
        _angleToStop = GetAngleToStop(start, stop); // 計算 起始位置 到 目標位置的角度
        _startHeight = start.y;
        _stopHeight = stop.y;
        _curDistance = 0;

        // 計算總距離
        Vector3 v = _stopPos - _startPos;
        _totalDistance = Mathf.Sqrt(v.x * v.x + v.z * v.z);

        // 設置當前位置
        transform.position = start;
        _curPos = start;

        // 設置當前X，Y軸的旋轉角度
        Vector3 rotation = transform.eulerAngles;
        if (rotateX > 0)
        {
            rotation.x = -rotateX;
        }
        rotation.y = _angleToStop;

        transform.eulerAngles = rotation;
        _curRotation = rotation;

        // 設置判斷爲發射狀態，讓Update函數能夠更新
        _isFiring = true;
    }

    // 計算 起始位置 到 目標位置的角度
    private float GetAngleToStop(Vector3 startPos, Vector3 stopPos)
    {
        stopPos.x -= startPos.x;
        stopPos.z -= startPos.z;

        float deltaAngle = 0;
        if (stopPos.x == 0 && stopPos.z == 0)
        {
            return 0;
        }
        else if (stopPos.x > 0 && stopPos.z > 0)
        {
            deltaAngle = 0;
        }
        else if (stopPos.x > 0 && stopPos.z == 0)
        {
            return 90;
        }
        else if (stopPos.x > 0 && stopPos.z < 0)
        {
            deltaAngle = 180;
        }
        else if (stopPos.x == 0 && stopPos.z < 0)
        {
            return 180;
        }
        else if (stopPos.x < 0 && stopPos.z < 0)
        {
            deltaAngle = -180;
        }
        else if (stopPos.x < 0 && stopPos.z == 0)
        {
            return -90;
        }
        else if (stopPos.x < 0 && stopPos.z > 0)
        {
            deltaAngle = 0;
        }

        float angle = Mathf.Atan(stopPos.x / stopPos.z) * Mathf.Rad2Deg + deltaAngle;
        return angle;
    }

    // 更新箭到下一個位置
    private void SetNextStep()
    {
        // 計算X,Z軸移動向量，然後再把它們乘移動距離，這樣就能移動到下一個位置
        float deltaX = Mathf.Sin(_angleToStop * Mathf.Deg2Rad);
        float deltaZ = Mathf.Cos(_angleToStop * Mathf.Deg2Rad);
        float deltaDistance = _curDistance / _totalDistance;
        float l;
        if (deltaDistance > 0.5f)
        {
            l = (_totalDistance / (1 / speed) + gravitySpeed) * Time.deltaTime;//计算移动速度保持匀速
        }
        else
        {
            l = ((_totalDistance / (1 / speed))) * Time.deltaTime;//计算移动速度保持匀速
        }

        _curPos.x += deltaX * l;
        _curPos.z += deltaZ * l;

        // 增加當前距離，用來判斷是否到達終點了
        _curDistance += l;

        /************************************************/
        // 計算出當前的高度
        // 這個是一元二次方程(ax^2 + bx)，大家都知道它是一條拋物線的方程，也是弓箭軌道最重要的地方。
        // 我會在下面跟大家詳解如果運用簡單的一元二次方程來做弓箭的拋物線效果
        /************************************************/
        //float a = -2;
        float b = _totalDistance;
        float apex = _totalDistance / 2;
        float deltaHeight = (float)(1 / ((-apex) * (apex - _totalDistance) / height));
        //float deltaDistance = _curDistance / _totalDistance;

        float h = deltaDistance * (_stopHeight - _startHeight) + _startHeight;
        h += deltaHeight * (gravity * (_curDistance * _curDistance) + b * _curDistance);
        _curPos.y = h;

        // 更新當前箭的位置
        transform.position = _curPos;

        // 旋轉X軸
        if (rotateX > 0)
        {
            _curRotation.x = -rotateX * (1 + -2 * deltaDistance);
            transform.eulerAngles = _curRotation;
        }
    }

    // 判斷是否到達
    private bool IsArrived()
    {
        return _curDistance * Mathf.Abs(gravity) >= _totalDistance;

        // 
        //
    }

    private void UpdateArrow()
    {
        SetNextStep();

        // 如果到達了目標地點就取消發射狀態
        if (IsArrived())
        {
            _isFiring = false;
            BombEvent();
        }
    }
    #endregion


    #region 子弹碰撞事件

    void Destroy()
    {
        // mono.OnUpdate -= Update;
        transform.gameObject.SetActive(false);

        isRuning = false;
    }

    /// <summary>
    /// 子弹触发检测
    /// </summary>
    /// <param name="go"></param>
    /// <param name="other"></param>

    void EnterTrigger(GameObject go, Collider other)
    {
        BulletMsg msg = null;
        if (bulletCampType == BulletCampType.PlayerBullet)
        {
            if (other.gameObject.layer == Utility.MonsterLayer) msg = new BulletMsg((ushort)ILNpcEvent.MonsterByAttack, other.gameObject, attackData);
            else return;
        }
        else if (bulletCampType == BulletCampType.MonsterBullet)
        {
            if (other.gameObject.layer == Utility.PlayerLayer) msg = new BulletMsg((ushort)ILNpcEvent.PlayerByAttack, int.Parse(other.gameObject.name), attackData);
            else return;
        }

        SendMsg(msg);
        BombEvent();


    }
    #region 许梦然

    #region 追踪计算

    /// <summary>
    /// 追踪物体位置
    /// </summary>

    /// <summary>
    /// 更新追踪物体 
    /// </summary>
    private void UpdateTrace()
    {
        dirNom = (tragetTrans.position - transform.position).normalized;
        transform.position += dirNom * speed * Time.deltaTime;
    }

    #endregion
    #endregion


    #endregion
}

