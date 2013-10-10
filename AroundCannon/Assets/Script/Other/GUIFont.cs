using UnityEngine;
using System.Collections;

// GUIに表示するフォントのスタイルなどを纏めた構造体
public class GUIFont
{
    // フォントカラーを設定
    private GUIStyleState guiStyleState = new GUIStyleState();
    // フォントサイズなどの設定
    private GUIStyle guiStyle = new GUIStyle();
    // 表示位置
    private Rect position = new Rect();

    public GUIStyle GUIStyle
    {
        get
        {
            guiStyle.normal = guiStyleState;
            return guiStyle;
        }
    }

    public int FontSize
    {
        set
        {
            guiStyle.fontSize = value;
        }
    }

    public Color FontColor
    {
        set
        {
            guiStyleState.textColor = value;
        }
    }

    public Rect Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
        }
    }

    public Texture2D Backgorund
    {
        get
        {
            return guiStyleState.background;
        }
        set
        {
            guiStyleState.background = value;
        }
    }
}