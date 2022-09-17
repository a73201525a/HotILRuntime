using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class BulletEidtor : MonoBehaviour
{


    public static void Save()
    {
        string path = Application.dataPath + "/Art/BulletJson/";

        BulletData data = GameObject.Find("BulletEidtor").GetComponent<BulletData>();
        BulletJson jsonObj;

        CopyData(data, out jsonObj);


        string json = LitJson.JsonMapper.ToJson(jsonObj);

        string str = Path.Combine(path, data.BulletName.ToString() + ".txt");

        FileStream aFile = new FileStream(str, FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(aFile);
        sw.Write(json);
        sw.Close();
        sw.Dispose();

        // Debug.Log(json);

        Debug.Log("保存成功");
        AssetDatabase.Refresh();
        //string aa =  File.ReadAllText(Path);
        //BulletJson a = LitJson.JsonMapper.ToObject<BulletJson>(aa);
        //Debug.Log(a.MaxLevel);
    }

    public static void Read(string id)
    {
        string path = Application.dataPath + "/Art/BulletJson/";

        BulletData data = GameObject.Find("BulletEidtor").GetComponent<BulletData>();
        data.BulletName = int.Parse(id);
        string str = Path.Combine(path, id + ".txt");

        string js = File.ReadAllText(str);
        BulletJson jObj = LitJson.JsonMapper.ToObject<BulletJson>(js);
        ReadData(data, jObj);
    }
    //public  void CreatBullet()
    //{
    //    Debug.Log(bullerObj);
    //   GameObject go =   GameObject.Instantiate(bullerObj);
    //}
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

    static void ReadData(BulletData jsonObj, BulletJson data)
    {
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
        // jsonObj.bulletSpeed = data.bulletSpeed;
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
        jsonObj.BulletName = data.bulletID;
        jsonObj.BuffType = data.BulletBuff;
    }
}
