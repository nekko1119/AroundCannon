using UnityEngine;
using System.Collections;

public class MyUtility
{
    //minからmaxの範囲で0から1までのrateの割合の値を返す
    //例 mix = 2 max = 12 rate = 0.4 => 6
    public static  float Interpolate(float min, float max, float rate)
    {
        return (max - min) * rate + min;
    }
}