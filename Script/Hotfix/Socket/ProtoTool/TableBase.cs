using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ITable { }

public abstract class TableBase<T> : ITable where T : TableBase<T>, new()
{
    public static T Instance { get; private set; }
    public static void Initialize(string filePath)
    {
        Instance = Activator.CreateInstance<T>();
        byte[] buffer = File.ReadAllBytes(filePath);
        byte[] fileBuffer = new byte[buffer.Length - 1];
        for (int i = 1; i < buffer.Length; i++)
        {
            fileBuffer[i - 1] = (byte)(buffer[i] ^ buffer[0]);
        }
        MemoryStream memoryStream = new MemoryStream(fileBuffer);
        BinaryReader binaryReader = new BinaryReader(memoryStream);
        while (memoryStream.Position < memoryStream.Length)
        {
            try
            {
                T t2 = Activator.CreateInstance<T>();
                t2.Parse(binaryReader);
                t2.Init();
            }
            catch (Exception ex)
            {
                Debug.LogError("Element error file : " + filePath);
                throw ex;
            }
        }
        binaryReader.Close();
        memoryStream.Close();
        memoryStream.Dispose();
    }
    public abstract int Parse(BinaryReader bReader);
    public virtual void Init()
    {
    }
}
