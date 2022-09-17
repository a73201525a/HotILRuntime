using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attrStar : Graphic {

    LineRenderer line = null;
	void Start () {
        line = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void StarSetAllDirty()
    {
        SetAllDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (transform.childCount == 0)
        {
            return;
        }
        Color32 color32 = color;
        vh.Clear();

        // 几何图形的顶点，本例中根据子节点坐标确定顶点
        int index = 0;
        if (!line)
        {
            line = GetComponent<LineRenderer>();
        }
        foreach (Transform child in transform)
        {
            vh.AddVert(child.localPosition, color32, new Vector2(0f, 0f));
            if (line)
            {
                line.SetPosition(index++, child.localPosition);
            }
        }
        //要闭合
        if (line)
        {
            line.SetPosition(index, transform.GetChild(0).localPosition);
        }
        // 几何图形中的三角形
        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(0, 2, 3);
        vh.AddTriangle(0, 3, 4);

    }
}
