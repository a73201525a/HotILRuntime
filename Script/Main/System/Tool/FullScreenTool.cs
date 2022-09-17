using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FullScreenTool : MonoBehaviour
{
    public float  devHeight=9.6f;//��Ƹ߶�
    public float devWidth = 6.4f;//��ƿ��
    public Camera uiCamera;//UI���
    public Canvas canvas;
    private void Awake()
    {
        canvas = this.GetComponent<Canvas>();
    }
    void Start()
    {
        FullScreen();
       
    }
    private void Update()
    {
        FullScreen();
    }
    void FullScreen()
    {
        
        if (uiCamera == null)
        {
            uiCamera = Camera.main;
            uiCamera.orthographic = true;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
        }
        float screenHight = Screen.height;
        float orgSize = Camera.main.orthographicSize;
        float aspectRatio = uiCamera.orthographicSize;
        float cameraWidth = orgSize * 2 * aspectRatio;
        if (cameraWidth<devWidth)
        {
            orgSize = devWidth / (2 * aspectRatio);
            uiCamera.orthographicSize = orgSize;
        }
       
    }
   
}
