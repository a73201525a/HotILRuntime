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
    /// �������λ��
    /// </summary>
    /// <param name="transform">λ��</param>
    /// <param name="endValue">�յ�</param>
    /// <param name="duration">����ʱ��</param>
    public void TransFromMoveX(Transform transform,float endValue,float duration)
    {
        transform.DOLocalMoveX(endValue, duration);
    }
    /// <summary>
    /// ��������λ��
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
    /// ƹ���˶�
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
