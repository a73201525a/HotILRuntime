using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������Ҫ������
/// </summary>
public class AttackData
{
    public int Attack;//����


    public float AddAttack = 1;//��ʱ�� 
    public float AddDefence = 1;//��ʱ��

    //public int[] elementAttack;


    public int Vertigo;//ѣ��
    public int DeformationSheep;//����
    public int Disarm;//ѹ��
    public int Frozen;//����

    public int Repel;//����

    public CreatBallConditions[] creatBallConditions;//�����������

    public int ballID;//��ǰ�����ID

    public AttackData(int Attack)
    {
        this.Attack = Attack;
      //  this.elementAttack = element;
        //this.Repel = Repel;
        //=================����buff==========
      //  this.Vertigo = Vertigo;
        DeformationSheep = Vertigo;//����
        Disarm = Vertigo;//ѹ��
        Frozen = Vertigo;//����

        //===================================

    }


}


