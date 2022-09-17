using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveMsg : ILMsgBase
{
    public ArchiveType archiveType;//存档类型
    /// <summary>
    /// 获取当前存档页面存档类型
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="archiveType">存档类型</param>
    public ArchiveMsg(ushort msgID, ArchiveType archiveType)
    {
        this.archiveType = archiveType;
        this.msgID = msgID;
    }
}
