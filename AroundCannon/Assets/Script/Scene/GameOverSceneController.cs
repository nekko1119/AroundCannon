using UnityEngine;
using System.Collections;

public class GameOverSceneController : MonoBehaviour
{
    // GUIに表示するフォントの設定
    private GUIFont guiFont = null;

    void Awake()
    {
        guiFont = new GUIFont()
        {
            FontColor = Color.magenta,
            FontSize = 160,
            Position = new Rect(0, 180, 400, 170)
        };
    }

    void Update()
    {
        //GameOverジングルが鳴り終わるまで操作不能
        if (GameObject.Find("Main Camera").audio.isPlaying)
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
        GUI.Label(guiFont.Position, "Game Over", guiFont.GUIStyle);
    }
}