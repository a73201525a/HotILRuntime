using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SocketBuffer
{
    private byte[] headByte;

    // head长度
    private byte headLength = 6;

    //接受到的数据
    private byte[] allRecvData;

    //当前接收到数据的长度
    private int curRecvLenght;

    //总共接收的数据长度
    private int allDataLenght;

    public SocketBuffer(byte tmpHeadLenght, CallBackRecvOver tmpCall)
    {
        headLength = tmpHeadLenght;

        headByte = new byte[headLength];

        callBackRecvOver = tmpCall;
    }

    public void RecvByte(byte[] recvByte, int realLength)
    {
        if (realLength == 0) return;

        if (curRecvLenght < headByte.Length)
        {
            RecvHead(recvByte, realLength);
        }
        else
        {
            //接收的总长度
            int tmpLength = curRecvLenght + realLength;

            if (tmpLength == allDataLenght)
            {
                RecvOneAll(recvByte, realLength);
            }
            else if (tmpLength > allDataLenght)
            {
                RecvLarger(recvByte, realLength);
            }
            else
            {
                RecvSmall(recvByte, realLength);
            }
        }
    }

    private void RecvLarger(byte[] recvByte, int realLenght)
    {
        int tmpLength = allDataLenght - curRecvLenght;

        Buffer.BlockCopy(recvByte, 0, allRecvData, curRecvLenght, tmpLength);

        curRecvLenght += tmpLength;

        RecvOneMsgOver();

        int remainLength = realLenght - tmpLength;

        byte[] reaminByte = new byte[remainLength];

        Buffer.BlockCopy(recvByte, tmpLength, reaminByte, 0, remainLength);

        RecvByte(reaminByte,remainLength);
    }

    private void RecvSmall(byte[] recvByte, int realLenght)
    {
        Buffer.BlockCopy(recvByte, 0, allRecvData, curRecvLenght, realLenght);

        curRecvLenght += realLenght;
    }

    private void RecvOneAll(byte[] recvByte, int realLenght)
    {
        Buffer.BlockCopy(recvByte, 0, allRecvData, curRecvLenght, realLenght);

        curRecvLenght += realLenght;

        RecvOneMsgOver();


    }

    private void RecvHead(byte[] recvByte, int realLength)
    {
        //差多少字节才能组成head
        int tmpReal = headByte.Length - curRecvLenght;
        //现在接收的 和已经接收的总长度
        int tmpLenght = curRecvLenght + realLength;
        //总长度小于头
        if (tmpLenght < headByte.Length)
        {
            Buffer.BlockCopy(recvByte, 0, headByte, curRecvLenght, realLength);

            curRecvLenght += realLength;
        }
        else//总长度大于等于head
        {
            Buffer.BlockCopy(recvByte, 0, headByte, curRecvLenght, tmpReal);
            curRecvLenght += tmpReal;

            //取出四个字节 转换总长度int
            allDataLenght = BitConverter.ToInt32(headByte, 0) + headLength;

            allRecvData = new byte[allDataLenght];//body+head

            Buffer.BlockCopy(recvByte, 0, allRecvData, 0, headLength);

            int tmpRemin = realLength - tmpReal;

            //表示recbyte 是否还有数据
            if (tmpRemin > 0)
            {
                byte[] tmpByte = new byte[tmpRemin];

                //将剩下的字节 拷入 tmpbyte
                Buffer.BlockCopy(recvByte, tmpReal, tmpByte, 0, tmpRemin);

                RecvByte(tmpByte, tmpRemin);
            }
            else
            {
                //表示msg接收完成
                RecvOneMsgOver();
            }
        }
    }
    #region recv over back to

    public delegate void CallBackRecvOver(byte[] allData);

    CallBackRecvOver callBackRecvOver;

    private void RecvOneMsgOver()
    {
        if (callBackRecvOver != null)
        {
            callBackRecvOver(allRecvData);
        }

        curRecvLenght = 0;
        allDataLenght = 0;
        allRecvData = null;
    }
    #endregion
}
