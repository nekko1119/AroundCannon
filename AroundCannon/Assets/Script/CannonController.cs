using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour
{
    // ※ここで言う仰角は縦・上下の動きのこと

    // 仰角を動かす速度
    private float elevationSpeed = 0.5f;

    // 仰角の最小値(一番上を向く角度)
    private readonly float minElevationAngle = 70f;
    // 仰角の最大値(一番下を向く角度)
    private readonly float maxElevationAngle = 100f;

    void OnLevelWasLoaded()
    {
        // GameOverの時は、すでにシーンにカメラが存在するのでカメラを削除する
        if (Application.loadedLevelName == "GameOverScene")
        {
            Destroy(transform.FindChild("Main Camera").gameObject);
        }
    }

    void FixedUpdate()
    {
        // GameScene以外では何もしない
        if (Application.loadedLevelName != "GameScene")
        {
            return;
        }

        //垂直方向に動く処理
        transform.Rotate(getVerticalDirection() * Time.deltaTime, elevationSpeed);
        elevationClamp();
        
        // 寝ているとOnTriggerEnterが呼ばれないので起こす
        if (rigidbody.IsSleeping())
        {
            rigidbody.WakeUp();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        // GameScene以外では何もしない
        if (Application.loadedLevelName != "GameScene")
        {
            return;
        }

        if (collider.tag == "Enemy")
        {
            // 親オブジェクトを次のシーンでもそのまま使えるようにする
            var parent = FindObjectOfType(typeof(CannonParentController)) as CannonParentController;
            DontDestroyOnLoad(parent.transform);
            // GameOverSceneをロードする
            var scene = FindObjectOfType(typeof(GameSceneController)) as GameSceneController;
            scene.LoadGameOverScene();
        }
    }

    // 入力によって仰角の単位ベクトルを返す
    private Vector3 getVerticalDirection()
    {
        var direction = Input.GetAxis("Vertical");
        if (direction > 0.0f)
        {
            return Vector3.back;
        }
        else if (direction < 0.0f)
        {
            return Vector3.forward;
        }
        else
        {
            return Vector3.zero;
        }
    }

    // 仰角を決まった範囲に収める
    private void elevationClamp()
    {
        var euler = transform.rotation.eulerAngles;
        euler.z = Mathf.Clamp(euler.z, minElevationAngle, maxElevationAngle);
        transform.rotation = Quaternion.Euler(euler);
    }
}
