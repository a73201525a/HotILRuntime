using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
    public double MaxHp;
    private double HP;
    public bool IsFractured;

    private void Start()
    {
        HP = MaxHp;
    }
    public void SetHP(double hp)
    {
        //Debug.LogError(HP);
        HP -= hp;
        CheckFractired();
    }

    void CheckFractired()
    {
        this.gameObject.GetComponent<HUDTool>().SetValue((float)(HP / MaxHp));
        if (IsFractured && HP <= 0)
        {
            //血量为0，墙体播放破碎动画
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<FracturedObject>().enabled = true;
        }
    }
}
