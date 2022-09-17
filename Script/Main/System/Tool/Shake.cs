using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float ShakeTime = 0.2f;
    public float Fps = 20.0f;
    public float FrameTime = 0.03f;
    public float ShakeDelta = 0.005f;
    public float ShakeLevel = 2f;


    private float shakeTime ;//震动时间
    private float fps ;
    private float frameTime ;
    private float shakeDelta ;//震动系数
    private float shakeLevel;//震动系数
    public Camera cam;
    public  bool isshakeCamera = false;
    // Use this for initialization
    void Start()
    {
        shakeTime = ShakeTime;
        fps = Fps;
        frameTime = FrameTime;
        shakeDelta = ShakeDelta;
        shakeLevel = ShakeLevel;
        //isshakeCamera=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isshakeCamera = false;
                    shakeTime = ShakeTime;
                    fps = Fps;
                    frameTime = FrameTime;
                    shakeDelta = ShakeDelta;
                    shakeLevel = ShakeLevel;
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        cam.rect = new Rect(shakeDelta * (-1.0f + shakeLevel * Random.value), shakeDelta * (-1.0f + shakeLevel * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }
}
