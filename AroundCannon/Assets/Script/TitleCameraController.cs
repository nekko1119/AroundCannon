using UnityEngine;
using System.Collections;

public class TitleCameraController : MonoBehaviour
{
    // 注視するオブジェクト
    public GameObject target;
    
    // １周するのにかかる時間
    private int aroundTime = 1200;
    // 現在のカウント(時間)
    private int count = 0;
    // 注視点とのxz平面距離(=半径)
    private int distance = 10;

    void FixedUpdate()
    {
        rotatePosition();
        // 注視点はターゲット
        transform.LookAt(target.transform);
        ++count;
    }

    //注視点を中心にカメラを円形に移動させる
    private void rotatePosition()
    {
        // 現在の位置が[0, aroundTime]の何分目の位置にいるか求める
        var rate = (aroundTime - (count % aroundTime)) / (float)aroundTime;
        // 求めたrate = [0, 1]をいるべき位置のラジアン[0, 2π]に変換する
        var radian = MyUtility.Interpolate(0f, 2f * Mathf.PI, rate);

        // 座標を更新する
        var currentPosition = transform.position;
        currentPosition = new Vector3(Mathf.Cos(radian) * distance, currentPosition.y, Mathf.Sin(radian) * distance);
        transform.position = currentPosition;
    }
}