using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ILManagerID
{
    NetManager = 0,

    GameManager = ILFrameTools.msgSpan,

    AssetManager = ILFrameTools.msgSpan*2,

    UIManager = ILFrameTools.msgSpan * 3,

    NpcManager = ILFrameTools.msgSpan*4,

    AdventureManager = ILFrameTools.msgSpan*5,
}

public class ILFrameTools
{
    public const int msgSpan = 3000;

}
