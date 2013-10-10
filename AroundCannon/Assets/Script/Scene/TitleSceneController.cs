using UnityEngine;
using System.Collections;

public class TitleSceneController : MonoBehaviour
{
    //ロゴの画像
    public Texture2D logoTexture = null;

    // GUIに表示するフォントの設定
    private GUIFont guiFont = null;
    // タイトルロゴの設定
    private GUIFont logo = null;

    void Awake()
    {
        guiFont = new GUIFont()
        {
            FontColor = Color.black,
            FontSize = 30,
            Position = new Rect(180, 60, 200, 30)
        };

        logo = new GUIFont()
        {
            Backgorund = logoTexture,
            Position = new Rect(220, 280, 400, 240)
        };
    }

    void OnGUI()
    {
        GUI.Label(logo.Position, logo.Backgorund);
        GUI.Label(guiFont.Position, "始めるには決定ボタンを押して下さい", guiFont.GUIStyle);

        if (Input.GetButton("Shot"))
        {
            LoadGameScene();
        }
    }

    // Titleの次のシーンであるGameSceneをロードする
    public void LoadGameScene()
    {
        Application.LoadLevel("GameScene");
    }
}