using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SyncTransPos : NetworkBehaviour
{
    [SyncVar]
    private Vector2 syncPos;

    float lerpRate = 15;
    Vector2 lastPos = Vector2.zero;
    float threshold = 0.1f;

    void Update()
    {
        if (isLocalPlayer)
            SendPosition();

        LerpPosition();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
            transform.position = Vector2.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
    }

    [Command]
    void CmdGetPos(Vector2 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void SendPosition()
    {
        if (Vector2.Distance(transform.position, lastPos) >= threshold)
        {
            CmdGetPos(transform.position);
            lastPos = transform.position;
        }
    }
}
