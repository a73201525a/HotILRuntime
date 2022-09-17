using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletEf;
    void Start()
    {
        StartCoroutine(NotHotfixStart());
    }
    ILManager iL;
    IEnumerator NotHotfixStart()
    {
        iL = this.gameObject.AddComponent<ILManager>();

        yield return StartCoroutine(iL.GetLocalDllVersion());

        // SceneManager.LoadScene("Main");
        yield return StartCoroutine(iL.LoadHotfixDll());

    }
    //internal Bullet(GameObject obj, GameObject objEf,
    //  BulletJson bulletData, Vector3 tmpTarget,
    //  Vector3 curPos, BulletCampType bulletCampType, AttackData attackData, Transform tragetTrans)
    //{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BulletData data = GameObject.Find("BulletEidtor").GetComponent<BulletData>();
            BulletJson jsom;
            CopyData(data, out jsom);
            iL.CreatObjer(GameObject.Instantiate(bullet), GameObject.Instantiate(bulletEf), jsom, this.transform.position + (Vector3.forward * 2), this.transform.position, 1, null, null);
        }
    }

    static void CopyData(BulletData data, out BulletJson jsonObj)
    {
        jsonObj = new BulletJson();
        jsonObj.MaxLevel = data.MaxLevel;
        jsonObj.curLevel = data.curLevel;
        jsonObj.Model = data.Model;
        jsonObj.BombEf = data.BombEf;
        jsonObj.gravitySpeed = data.gravitySpeed;
        jsonObj.gravity = data.gravity;
        jsonObj.height = data.height;
        jsonObj.Speed = data.Speed;
        jsonObj.BulletNumber = data.BulletNumber;
        jsonObj.BulletTime = data.BulletTime;
        jsonObj.IsFireAverage = data.IsFireAverage;
        jsonObj.AnglesVertical = data.AnglesVertical;
        jsonObj.AnglesHorizontal = data.AnglesHorizontal;
        jsonObj.FireCD = data.FireCD;
        jsonObj.FirePointOffsetX = data.FirePointOffsetX;
        jsonObj.FirePointOffsetY = data.FirePointOffsetY;
        jsonObj.DestoryTime = data.DestoryTime;
        //  jsonObj.bulletSpeed = data.bulletSpeed;
        jsonObj.IsInfiniteSpeed = data.IsInfiniteSpeed;
        jsonObj.BulletAttackMax = data.BulletAttackMax;
        jsonObj.BulletAttackMin = data.BulletAttackMin;
        jsonObj.BulletRangeAttackMax = data.BulletRangeAttackMax;
        jsonObj.BulletRangeAttackMin = data.BulletRangeAttackMin;
        jsonObj.BullerBombCD = data.BullerBombCD;
        jsonObj.Trace = data.Trace;
        jsonObj.Rebound = data.Rebound;
        jsonObj.IsPenetrate = data.IsPenetrate;
        jsonObj.IsShake = data.IsShake;
        jsonObj.IsShake = data.IsShake;

        jsonObj.fireType = data.fireType;
        jsonObj.areaType = data.areaType;
        jsonObj.bulletType = data.bulletType;
        jsonObj.ChargeTime = data.ChargeTime;
        jsonObj.WaitTime = data.WaitTime;
        jsonObj.BombWaitTime = data.BombWaitTime;
        jsonObj.FireAnglesRange = data.FireAnglesRange;
        jsonObj.Model = data.Model;
        jsonObj.BombEf = data.BombEf;
        jsonObj.bulletID = data.BulletName;
        jsonObj.BulletBuff = data.BuffType;
    }
}
