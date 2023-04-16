using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{
    public static void FisherYatesShuffle<T>(T[] array)
{
    System.Random r = new System.Random();
    for (int i = array.Length - 1; i > 0; i--)
    {
        int j = r.Next(0, i + 1);
        T temp = array[j];
        array[j] = array[i];
        array[i] = temp;
    }
}
}
