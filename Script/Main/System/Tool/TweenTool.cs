using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTool 
{
   
    public TweenTool()
    {
         
    }
    /// <summary>
    /// 物体横轴位移
    /// </summary>
    /// <param name="transform">位置</param>
    /// <param name="endValue">终点</param>
    /// <param name="duration">持续时间</param>
    public void TransFromMoveX(Transform transform,float endValue,float duration)
    {
        transform.DOLocalMoveX(endValue, duration);
    }
    /// <summary>
    /// 物体枢轴位移
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    public void TransFromMoveY(Transform transform,float endValue,float duration)
    {
        transform.DOLocalMoveY(endValue, duration);
    }  
    public void TransFromMove(Transform transform,Vector3 to, float duration)
    {
        transform.DOMove(to, duration);
    }
    /// <summary>
    /// 乒乓运动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="duration"></param>
    public void TransPingPong(Transform transform,Vector3 from,Vector3 to,float duration)
    {
        transform.DOMove(to, duration).OnComplete(() => TransPingPong(transform, to, from, duration));
    }
}
