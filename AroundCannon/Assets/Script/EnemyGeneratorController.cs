using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour
{
    // 生成する敵のプレハブ
    public GameObject enemyPrefab = null;
    
    // 敵の初期座標の砲台との距離
    private float distance = 200f;
    // 現在の敵の生成間隔
    private float currentIntervalTime = 0f;
    // 最後に敵が生成された時間
    private float lastTime = 0.0f;
    // 現在の敵の残り数(死んだ時減る値)
    private int currentRemainedEnemy = 0;
    // 生成できる敵の数(生成したとき減る値)
    private int currentAliveEnemy = 0;
    
    // 実際の砲台は親オブジェクトより少し上にあり、その補正y座標
    private readonly float height = 3f;
    // 初期の敵の生成間隔
    private readonly float maxIntervalTime = 4f;
    // 最終的な敵の生成間隔
    private readonly float minIntervalTime = 1f;
    // 初期の敵の残り数
    private readonly int maxRemainedEnemy = 50; 

    void Start()
    {
        DontDestroyOnLoad(this);
        currentIntervalTime = maxRemainedEnemy;
        currentRemainedEnemy = maxRemainedEnemy;
        currentAliveEnemy = currentRemainedEnemy;
        // 最初の一体
        generateEnemy();
    }

    void Update()
    {
        //GameScene以外では敵を生成しない
        if (Application.loadedLevelName != "GameScene")
        {
            return;
        }

        // 生成できる敵が残っている時、一定間隔で敵を生成する
        if (currentAliveEnemy > 0 && (Time.time - lastTime > currentIntervalTime))
        {
            generateEnemy();
        }

        if (currentRemainedEnemy <= 0)
        {
            // 砲台オブジェクトを次のシーンでもそのまま使えるようにする
            var parent = FindObjectOfType(typeof(CannonParentController)) as CannonParentController;
            DontDestroyOnLoad(parent.transform);
            // シーン遷移
            var scene = FindObjectOfType(typeof(GameSceneController)) as GameSceneController;
            scene.LoadGameClearScene();
        }
    }

    // 残っている敵の数を減らす
    public void ReduceEnemy()
    {
        --currentRemainedEnemy;
        updateCurrentIntervalTime();
    }

    // 残っている敵の数を得る
    public int RemainedEnemy
    {
        get
        {
            return currentRemainedEnemy;
        }
    }

    private void generateEnemy()
    {
        // 砲台の360度周りからランダムな場所に生成する
        var radian = Random.Range(0f, Mathf.PI * 2f);
        var position = new Vector3(Mathf.Cos(radian) * distance, height, Mathf.Sin(radian) * distance);
        var enemy = Instantiate(enemyPrefab, position, Quaternion.identity) as GameObject;
        // このクラスを親にし、一元管理できるようにする
        enemy.transform.parent = transform;
        // 最後に生成した時間を更新する
        lastTime = Time.time;
        // 生成できる残りの敵の数を減らす
        --currentAliveEnemy;
    }

    // 敵の残り数から生成間隔を更新する
    private void updateCurrentIntervalTime()
    {
        var rate = (maxRemainedEnemy - currentRemainedEnemy) / (float)maxRemainedEnemy;
        var reverseRate = 1f - rate;
        currentIntervalTime = MyUtility.Interpolate(minIntervalTime, maxIntervalTime, reverseRate);
    }
}