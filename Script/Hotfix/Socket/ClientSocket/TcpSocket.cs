using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;
using Google.Protobuf;

public class TcpSocket : NetBase
{
    NetWorkToServer socket = null;

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        switch ((ushort)tmpMsg.msgID)
        {
            case (ushort)TCPEvent.TcpConnect:
                {
                    TCPConnectMsg connectMsg = (TCPConnectMsg)tmpMsg;
                    socket = new NetWorkToServer(connectMsg.ip, connectMsg.port);
                    socket.mono.OnUpdate += Update;
                }
                break;
            case (ushort)TCPEvent.TcpSendMsg:
                {
                    TCPMsg sendMsg = (TCPMsg)tmpMsg;
                    socket.PutSendMsgToPool(SendBuff(sendMsg.portoMsgId, sendMsg.netMsg));
                }
                break;
            case (ushort)TCPEvent.MsgTestCs:
                {
                    NetMsgBase recvMsg = (NetMsgBase)tmpMsg;
                    TestProto t = TestProto.Parser.ParseFrom(recvMsg.GetBody());

                    Debug.Log("收到服务器消息");
                    Debug.Log(t.Id);
                    Debug.Log(t.Name);
                    Debug.Log(t.Str[5]);
                }
                break;
        }
    }

    public void Awake()
    {
        msgIds = new ushort[] {

            (ushort)TCPEvent.TcpConnect,
            (ushort)TCPEvent.TcpSendMsg,
            (ushort)TCPEvent.MsgTestSc,
             (ushort)TCPEvent.MsgTestCs,

        };

        RegistSelf(this, msgIds);
    }

    private void Update()
    {
        if (socket != null)
        {
            socket.Update();
        }
    }

    /// <summary>
    /// proto 转换  netmsgbase
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public NetMsgBase SendBuff(ProtoMsgType messageId, IMessage message)
    {
        BufferEntity msgData = new BufferEntity((ushort)messageId, 0, message);

        byte[] tmpBuffer = ProtoBufTool.Serialize(msgData);

        NetMsgBase tmp = new NetMsgBase(tmpBuffer);

        return tmp;
    }


    public void RecvBuff(byte[] data)
    {
        ProtoBufTool.Deserialize(data);
    }
}
