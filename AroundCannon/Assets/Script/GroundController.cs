using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}