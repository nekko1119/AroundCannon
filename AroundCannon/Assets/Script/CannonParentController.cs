using UnityEngine;
using System.Collections;

public class CannonParentController : MonoBehaviour 
{
    // 旋回速度
    private float rotateSpeed = 1f;
    
    void FixedUpdate()
    {
        if (Application.loadedLevelName != "GameScene")
        {
            return;
        }

        transform.Rotate(getHorizontalDirection() * Time.deltaTime, rotateSpeed);
    }

    // 入力によって旋回すべき方向への単位ベクトルを返す
    private Vector3 getHorizontalDirection()
    {
        var direction = Input.GetAxis("Horizontal");
        if (direction > 0.0f)
        {
            return Vector3.up;
        }
        else if (direction < 0.0f)
        {
            return Vector3.down;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
