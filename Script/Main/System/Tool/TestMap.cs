using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct MapGridTest
{
    public Vector2 Key;
    public Vector3 pos;
    public Vector3 v0;
    public Vector3 v1;
    public Vector3 v2;
    public Vector3 v3;
    public Vector3 v4;
    public Vector3 v5;
}


public class TestMap : MonoBehaviour
{
    public static List<MapGridTest> list = new List<MapGridTest>();


    public ComputeShader shader;


    private ComputeBuffer preBuffer;

    private ComputeBuffer nextBuffer;

    private ComputeBuffer resultBuffer;


    public Vector3[] array1;

    public Vector3[] array2;

    public MapGridTest[] resultArr;


    private int length = 16;


    private int kernel;



    void Start()
    {
        //array1 = new Vector3[length];

        //array2 = new Vector3[length];

        //    resultArr = new MapGridTest[length];


        //for (int i = 0; i < length; i++)
        //{

        //    array1[i] = Vector3.one;

        //    array2[i] = Vector3.one * 5;

        //}



        kernel = shader.FindKernel("CSMain");

        //preBuffer = new ComputeBuffer(array1.Length, 12);

        //preBuffer.SetData(array1);

        //nextBuffer = new ComputeBuffer(array2.Length, 12);

        //nextBuffer.SetData(array2);


        //    resultBuffer = new ComputeBuffer(resultArr.Length, 12);
        // test.SetData(array3);


        //shader.SetBuffer(kernel, "preVertices", preBuffer);

        //shader.SetBuffer(kernel, "nextVertices", nextBuffer);

        //  shader.SetBuffer(kernel, "Result", resultBuffer);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            resultArr = new MapGridTest[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                resultArr[i] = list[i];
            }

            int vector3Stride = sizeof(float) * 3;
            int vector2Stride = sizeof(float) * 2;

            int Stride = vector3Stride * 7+ vector2Stride;
            Debug.Log("Stride:::" + Stride);

            resultBuffer = new ComputeBuffer(resultArr.Length, Stride);
            resultBuffer.SetData(resultArr);
            shader.SetBuffer(kernel, "Result", resultBuffer);

            for (int i = 0; i < list.Count; i++)
            {
                //Debug.Log("list::::" + list[i].Key);
                Debug.Log("list::::" + resultArr[i].v0);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

            shader.Dispatch(kernel, 8, 8, 1);

            resultBuffer.GetData(resultArr);

            //do something with resultArr.

            for (int i = 0; i < resultArr.Length; i++)
            {
                Debug.Log("resultArr:::::" + resultArr[i].v0);
            }

            resultBuffer.Release();
        }


    }
}
