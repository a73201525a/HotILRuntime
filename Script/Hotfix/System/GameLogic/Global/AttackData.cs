using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击所需要的数据
/// </summary>
public class AttackData
{
    public int Attack;//攻击


    public float AddAttack = 1;//临时攻 
    public float AddDefence = 1;//临时防

    //public int[] elementAttack;


    public int Vertigo;//眩晕
    public int DeformationSheep;//变羊
    public int Disarm;//压制
    public int Frozen;//冻结

    public int Repel;//击退

    public CreatBallConditions[] creatBallConditions;//创建球的条件

    public int ballID;//当前弹球的ID

    public AttackData(int Attack)
    {
        this.Attack = Attack;
      //  this.elementAttack = element;
        //this.Repel = Repel;
        //=================测试buff==========
      //  this.Vertigo = Vertigo;
        DeformationSheep = Vertigo;//变羊
        Disarm = Vertigo;//压制
        Frozen = Vertigo;//冻结

        //===================================

    }


}


