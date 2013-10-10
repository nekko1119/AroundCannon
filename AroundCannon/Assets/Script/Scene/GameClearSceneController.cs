using UnityEngine;
using System.Collections;

public class GameClearSceneController : MonoBehaviour
{
    // ゲームクリアジングル
    public AudioClip gameClearClip = null;
    // GUIに表示するフォントの設定
    private GUIFont guiFont = null;
    // AudioSourceをスクリプトで動的にメインカメラに追加する
    private GameObject mainCamera = null;

    void Awake()
    {
        guiFont = new GUIFont()
        {
            FontColor = Color.cyan,
            FontSize = 160,
            Position = new Rect(0, 180, 400, 170)
        };

        mainCamera = GameObject.Find("Main Camera");
        mainCamera.AddComponent<AudioSource>();
        mainCamera.audio.clip = gameClearClip;
        mainCamera.audio.Play();
    }

    void Update()
    {
        //GameOverジングルが鳴り終わるまで操作不能
        if (mainCamera.audio.isPlaying)
        {
            return;
        }

        // 決定ボタンでタイトルに戻る。このシーンに使いまわすためにDontDestroyOnLoadを呼んだオブジェクトを明示的に破棄する
        if (Input.GetButton("Shot"))
        {
            var cannon = GameObject.Find("CannonParentPrefab");
            Destroy(cannon);
            var generator = GameObject.Find("EnemyGeneratorPrefab");
            Destroy(generator);
            Application.LoadLevel("TitleScene");
        }
    }

    void OnGUI()
    {
        GUI.Label(guiFont.Position, "Game Clear!", guiFont.GUIStyle);
    }
}