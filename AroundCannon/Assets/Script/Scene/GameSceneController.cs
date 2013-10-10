using UnityEngine;
using System.Collections;

public class GameSceneController : MonoBehaviour
{
    // GUIに表示するフォントの設定
    private GUIFont guiFont = null;

    void Awake()
    {
        guiFont = new GUIFont()
        {
            FontColor = Color.white,
            FontSize = 30,
            Position = new Rect(100, 100, 100, 20)
        };
    }

    void OnGUI()
    {
        var enemyGenCtrl = FindObjectOfType(typeof(EnemyGeneratorController)) as EnemyGeneratorController;
        GUI.Label(guiFont.Position, "残りの敵の数 : " + enemyGenCtrl.RemainedEnemy, guiFont.GUIStyle);
    }

    public void LoadGameOverScene()
    {
        Application.LoadLevel("GameOverScene");
    }

    public void LoadGameClearScene()
    {
        Application.LoadLevel("GameClearScene");
    }
}
