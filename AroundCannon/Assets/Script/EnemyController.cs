using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    // 砲台(プレイヤー)
    public GameObject cannonPrefab = null;
    // 消滅するときの爆発音
    public AudioClip bombClip = null;
    
    // 回転するアニメーションの速度
    private float rotateSpeed = 0f;
    // 回転軸
    private Vector3 rotateAxis = Vector3.zero;
    // プレイヤーと衝突するまでにかかる時間
    private float arrivalTime = 900f;
    // 移動速度(計算で求める)
    private float speed = 0f;
    // 移動する方向(計算で求める)
    private Vector3 direction = Vector3.zero;
    // 砲台の座標
    private Vector3 cannonPosition = Vector3.zero;

    // 回転するアニメーションの最高速度
    private readonly float maxRotateSpeed = 9f;
    // 回転するアニメーションの最高速度
    private readonly float minRotateSpeed = 3f;

    void Start()
    {
        // 自転しつつ接近する。その自転の回転軸、速度、プレイヤーの一を設定する
        cannonPosition = cannonPrefab.transform.position;
        rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
        rotateAxis = new Vector3(Random.value, Random.value, Random.value).normalized;
        speed = getDistance() / arrivalTime;
        direction = getDirection();
    }

    void FixedUpdate()
    {
        // GameScene以外の時は動かせない
        if (Application.loadedLevelName != "GameScene")
        {
            return;
        }

        // 回転しつつ砲台へ接近する
        transform.Rotate(rotateAxis * Time.deltaTime, rotateSpeed);
        transform.position += direction * speed;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.collider.tag == "Bullet")
        {
            if (bombClip)
            {
                AudioSource.PlayClipAtPoint(bombClip, transform.position);
            }
            // 弾に当たったら敵の残り数を減らし、消滅する
            EnemyGeneratorController parent = FindObjectOfType(typeof(EnemyGeneratorController)) as EnemyGeneratorController;
            parent.ReduceEnemy();
            Destroy(gameObject);
        }
    }

    //砲台と自身の距離を得る
    private float getDistance()
    {
        return Vector3.Distance(cannonPosition, transform.position);
    }

    //放題と自身の方向を得る
    private Vector3 getDirection()
    {
        return (cannonPosition - transform.position).normalized;
    }
}