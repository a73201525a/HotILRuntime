using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ILMonoBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action OnUpdate;
    public Action OnLateUpdate;
    public Action ILOnDestroy;
    public Action ILOnApplicationQuit;
    public Action OnFixedUpdate;
    public Action OnAnimatorEvent;
    public Action<string> OnAnimatorAction;
    void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    void Update()
    {
        OnUpdate?.Invoke();
    }

    private void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }

    private void OnDestroy()
    {
        ILOnDestroy?.Invoke();
    }

    void OnApplicationQuit()
    {
        ILOnApplicationQuit?.Invoke();
    }

    public Action<PointerEventData> BeginDrag;
    public Action<PointerEventData> Drag;
    public Action<PointerEventData> EndDrag;
    public Action<PointerEventData> PointerEnter;
    public Action<PointerEventData> PointerExit;
    public Action<PointerEventData> PointUp;
    public Action<PointerEventData> PointerDown;
    public Action<PointerEventData> PointerClick;
    public Action<GameObject> EventEnter;
    public Action<GameObject> EventExit;

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Drag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag?.Invoke(eventData);
    }

    public Action<GameObject, Collider> EnterTrigger;
    public Action<GameObject, Collider> StayTrigger;
    public Action<GameObject, Collider> ExitTrigger;
    public Action<GameObject, Collider> CollisionEnter;

    //public Action<GameObject, Collision> EnterCollision;
    //public Action<GameObject, Collision> StayCollision;
    //public Action<GameObject, Collision> ExitCollision;

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter?.Invoke(this.gameObject, collision.collider);
    }


    private void OnTriggerEnter(Collider other)
    {
        EnterTrigger?.Invoke(this.gameObject, other);
    }

    private void OnTriggerStay(Collider other)
    {
        StayTrigger?.Invoke(this.gameObject, other);
    }
    private void OnMouseEnter()
    {
        EventEnter?.Invoke(this.gameObject);
    }
    private void OnMouseExit()
    {
        EventExit?.Invoke(this.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        ExitTrigger?.Invoke(this.gameObject, other);
    }


    public void AnimatorEvent(string name)
    {

        //OnAnimatorEvent?.Invoke();
        OnAnimatorAction?.Invoke(name);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    EnterCollision(this.gameObject, collision);
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    StayCollision(this.gameObject, collision);
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    ExitCollision(this.gameObject, collision);
    //}
    /// <summary>
    ///切换场景 不删除该对象
    /// </summary>
    /// <param name="obj"></param>
    public void DontDestoryObj(GameObject obj)
    {

        DontDestroyOnLoad(obj);

    }

    public Action DrawGizmosSelected;

    void OnDrawGizmos()
    {
        // Debug.Log("22222222222");
        DrawGizmosSelected?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke(eventData);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke(eventData);

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PointerClick?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointUp?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke(eventData);
    }
}
