using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FireType
{
    single,//单点
    repeating,//连发
    chargeSingle,//蓄力单点
    chargeRepeating,//蓄力连发
    throwing//投掷

}
public enum SkillAreaType
{
    None = 0,//无提示
    OuterCircle_InnerCube = 1,//箭头
    OuterCircle_InnerSector = 2,//扇形
    OuterCircle_InnerCircle = 3,//内圆
}

public enum BulletType
{
    bomb,//炸弹 （抛物线）
    bullet,//子弹
    trace,//追踪弹
}

public class BulletData :MonoBehaviour
{
    //枪械编号
    public int BulletName = 10001;
    //子弹携带效果
    public int BuffType = 8001;


    public FireType fireType;//开火类型
    public SkillAreaType areaType;//提示类型
    public BulletType bulletType;//子弹类型
    public string Model = "";
    public string BombEf = "";
    //=========
    public int MaxLevel = 1;//武器最高等级
    public int curLevel = 1;//当前等级
    //=======

    //=========抛物线弹道参数=============
    public double gravitySpeed = 0;//重力加速度
    public double gravity = -1;//重力
    public double height = 3;       // 箭的最大高度 
                                   //===============抛物线弹道参数=======================
    public double Speed = 1;        // 炮弹的速度

    public int BulletNumber = 1;//单次发射炮弹数量
    public int BulletTime = 1000;//单次发射炮弹时间(在时间内发射BulletNumber数量炮弹)(毫秒)
    public bool IsFireAverage = true;//不规则时间发射 还是 随机时间发射

    public double AnglesVertical = 3;//垂直角度(设置height)

    public int AnglesHorizontal = 60;//水平发射角度
    public double FireAnglesRange = 0;//发射区域范围

    public double FireCD = 0.5f;//发射延迟CD

    public double FirePointOffsetX;//开火点偏移量 左右
    public double FirePointOffsetY;//开火点偏移量 前后


    public double DestoryTime = 3f;//炮弹生命周期

   // public AnimationCurve bulletSpeed;//炮弹速度变化曲线

    public bool IsInfiniteSpeed;//是否瞬发

    public double BulletAttackMax;//炮弹伤害最大
    public double BulletAttackMin;//炮弹伤害最小
    public double BulletRangeAttackMax;//炮弹范围伤害最大
    public double BulletRangeAttackMin;//炮弹范围伤害最小

    public double BullerBombCD;//延迟爆炸CD

    public double Trace;//追踪系数(计算好路线后，偏移量)

    public double Rebound;//反弹衰减系数

    public bool IsPenetrate;//是否穿透

    public bool IsShake;//是否震动屏幕

    public int ShakeID;//震动效果ID

    public double ChargeTime;//蓄力时间

    public int WaitTime;//发射延迟时间

    public int BombWaitTime;//命中后爆炸延迟时间
   
}
