using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManagerID
{
    NetManager = 0,

    GameManager = FrameTools.msgSpan,

    AssetManager = FrameTools.msgSpan * 2,

    UIManager = FrameTools.msgSpan * 3,

}

public class FrameTools
{
    public const int msgSpan = 3000;

}
