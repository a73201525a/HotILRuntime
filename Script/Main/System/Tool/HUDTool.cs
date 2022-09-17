using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTool : MonoBehaviour
{
    public Transform target;   //3D物体
    public RectTransform image;    //跟随3D物体的UI
    public Canvas canvas;   //UI所在的canvas

    public Vector3 offset;

    private Vector2 screenPos;
    private Vector3 mousePos;

   
    private Camera uiCamera;
    Vector3 cachePoint;
   
    private void Start()
    {
        
    }


    public void ShowHUD(bool isShow)
    {
        image.gameObject.SetActive(isShow);
    }

    public void SetValue(float value)
    {
        //slider.value = value;
        //if (slider.value <= 0)
        //{
        //    ShowHUD(false);
        //}
    }

    public void Init(Transform target)
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        image = transform.GetComponent<RectTransform>();
        this.target = target;
    }
    void Update()
    {
        if (image == null || canvas == null || target==null||Camera.main==null)
        {
            return;
        }
        if (cachePoint != target.position + offset)
        {
            screenPos = Camera.main.WorldToScreenPoint(target.position + offset);

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(image, screenPos, canvas.worldCamera, out mousePos))
            {
                image.position = mousePos;
            }
        }
       
    }
   
    public void  Clear()
    {
        uiCamera = null;
        target = null;
        canvas = null;
        image = null;
     
    }
}
