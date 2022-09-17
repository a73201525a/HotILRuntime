using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using ProtoMsg;
using Google.Protobuf;
using System;



public class ProtoBufTool
{


    //消息构成 总长度int（4个字节）+msgID(ushort)（2个字节）+body
    public static byte[] Serialize(BufferEntity bufferEntity)
    {

        //做安全检验：确保发送的协议 是预先定制好的
        if (ProtoConfig.CheckC2SPB(bufferEntity.id))
        {
            //序列化的操作 得到包体
            byte[] body = bufferEntity.body;

            //打印需要发送的消息
            //Debug.Log("发送的pb实体:" + JsonMapper.ToJson(bufferEntity.message));
            try
            {
                //内存操作流程
                MemoryStream ms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(ms, Encoding.Default);

                //包体的长度
                bw.Write(BitConverter.GetBytes(body.Length));//包体长度
                bw.Write(BitConverter.GetBytes(bufferEntity.id));//消息编号
                
                    //写入消息体
                bw.Write(bufferEntity.body);
                byte[] data = ms.ToArray();

                bw.Close();//关闭内存操作流
                ms.Dispose();//释放清理
                return data;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }
        else
        {
            Debug.Log("协议未在配置中");
            return null;
        }
    }

    public static byte[] Deserialize(byte[] data)
    {
        byte[] buff = new byte[data.Length - 6];
        Buffer.BlockCopy(data, 6, buff, 0, data.Length - 6);
        return buff;
    }

}

public class BufferEntity
{
    public ushort id = 0;//协议ID
    public int type = 0;//协议类型 proto xml json
    public byte[] body;//包体

    public IMessage message;//pb实体

    public BufferEntity()
    {

    }

    public BufferEntity(ushort id, int type, IMessage message)
    {
        this.id = id;
        this.type = type;
        this.message = message;
        this.body = message.ToByteArray();//序列化成byte数组
    }
}
