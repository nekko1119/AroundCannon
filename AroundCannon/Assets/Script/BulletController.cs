using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    // 弾の飛ぶ強さ(飛距離)
    private float power = 30f;
    // 弾の角度の揺らぎ(誤差の演出)
    private float tolerance = 0.02f;
    
    // 弾の生存期間
    private readonly float exsistedTime = 4f;

    void Start()
    {
        float tol = Random.Range(-tolerance, tolerance);
        // ローカル座標で初速度を設定し、ローカルvelocityをワールドvelocityに変換する
        var localVelocity = new Vector3(tol, 1f + tol, tol) * power;
        rigidbody.velocity = transform.TransformDirection(localVelocity);
        // 生存期間が過ぎたら破棄される
        Destroy(gameObject, exsistedTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}