using System;
using Random = System.Random;
public static class RandomHelper
{

    private static readonly Random random = new Random();

    /// <summary>
    /// ��ȡlower��Upper֮��������
    /// </summary>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <returns></returns>
    public static int RandomNumber(int lower, int upper)
    {
        int value = random.Next(lower, upper);
        return value;
    }
    /// <summary>
    /// ������ɲ��ظ�
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static int[] GenerateRandomCode(int length)
    {
        int seed = Guid.NewGuid().GetHashCode();
        Random radom = new Random(seed);
        int[] index = new int[length];
        for (int i = 0; i < length; i++)
        {
            index[i] = i + 1;
        }

        int[] array = new int[length]; // ��������������ɵĲ��ظ����� 
        int site = length;             // �������� 
        int idx;                       // ��ȡindex����������Ϊidxλ�õ����ݣ������������array��j����λ��
        for (int j = 0; j < length; j++)
        {
            idx = radom.Next(0, site - 1);  // �������������
            array[j] = index[idx];          // ���������λ��ȡ��һ���������浽������� 
            index[idx] = index[site - 1];   // ���ϵ�ǰ����λ�����ݣ�������������һ�����ݴ���֮
            site--;                         // ����λ�õ����޼�һ���������һ�����ݣ�
        }
        return array;

    }
    public static int RandomRate()
    {
        int value = random.Next(1, 101);
        return value;
    }

    public static float WeightRandomRate(float weight)
    {
        float value = random.Next(1, 101);
        value = value * (weight / 100);
        return value;
    }
}
