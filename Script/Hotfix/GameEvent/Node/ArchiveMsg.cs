using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveMsg : ILMsgBase
{
    public ArchiveType archiveType;//�浵����
    /// <summary>
    /// ��ȡ��ǰ�浵ҳ��浵����
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="archiveType">�浵����</param>
    public ArchiveMsg(ushort msgID, ArchiveType archiveType)
    {
        this.archiveType = archiveType;
        this.msgID = msgID;
    }
}
