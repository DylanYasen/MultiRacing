using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCarMovement : NetworkBehaviour
{
    [SyncVar]
    private Vector2 pos;

    float lerpRate = 15;

    void Start()
    {

    }

    void Update()
    {

    }

    void LerpPosition()
    {

    }
}
