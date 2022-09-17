using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOD_SkyMgr : MonoBehaviour
{
    private TOD_Sky _sky;
    // Use this for initialization
    void Awake()
    {
        _sky = GetComponent<TOD_Sky>();
    }

    public void SetCurHour(double hour)
    {
        _sky.Cycle.Hour = (float)hour;
    }

    public double GetCurHour()
    {
        return _sky.Cycle.Hour;
    }

    double curTime;
    void Update()
    {
        curTime = (curTime + (Time.deltaTime * 7200/7200)) % 24;
        SetCurHour(curTime);
    }
}
