using UnityEngine;
using System.Collections;

public class LauncherController : MonoBehaviour
{
    // 弾
    public GameObject bulletPrefab = null;
    // 弾の発射音
    public AudioClip shotClip = null;

    // 前に弾を発射してから何フレーム経過したかカウントする
    private int counter = 0;

    // 弾の連射間隔(2フレーム)
    private readonly int interval = 2;

    void FixedUpdate()
    {
        // GameScene以外では弾を撃てない
        if (Application.loadedLevelName != "GameScene")
        {
            return;
        }

        // ボタンが押されたら一定間隔で弾を生成する
        if (Input.GetButton("Shot") && counter > interval)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            counter = 0;

            if (shotClip)
            {
                audio.clip = shotClip;
                audio.Play();
            }
        }
        else
        {
            ++counter;
        }
    }
}
