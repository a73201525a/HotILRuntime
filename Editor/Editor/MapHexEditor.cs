using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MapHexEditor : MonoBehaviour
{
    [MenuItem("Bullet/CreatMap", false, 0)]
    public static void CreatMap()
    {
        MapData data = GameObject.Find("MapEditor").GetComponent<MapData>();
        GameObject parent = new GameObject("MapData");
        for (int i = 0; i < data.wide; i++)
        {
            for (int j = 0; j < data.height; j++)
            {
                GameObject go = GameObject.Instantiate(data.obj);
                go.transform.position = new Vector3(i * data.offset.x, 0, j * data.offset.y);
                go.transform.parent = parent.transform;
                MapHex hex = go.GetComponent<MapHex>();
                hex.X = i;
                hex.Y = j;
                hex.Type = MapHexType.plain;
              //  go.GetComponentInChildren<Text>().text = i + "," + j;
            }
        }

    }

    [MenuItem("Bullet/CreatMapEditor", false, 0)]
    public static void CreatMapData()
    {
        GameObject.Instantiate(Resources.Load("MapEditor")).name = "MapEditor";
    }


}
