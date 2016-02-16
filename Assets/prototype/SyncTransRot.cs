using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SyncTransRot : NetworkBehaviour
{
    [SyncVar]
    private Quaternion syncRot;

    float lerpRate = 15;
    Quaternion lastRot;
    float threshold = 1f;

    void Start() 
    {
        lastRot = transform.rotation;
    }

    void Update()
    {
        if (isLocalPlayer)
            SendRotation();

        LerpRotation();
    }

    void LerpRotation()
    {
        if (!isLocalPlayer)
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, Time.deltaTime * lerpRate);
    }

    [Command]
    void CmdGetPos(Quaternion rot)
    {
        syncRot = rot;
    }

    [ClientCallback]
    void SendRotation()
    {
        if (Quaternion.Angle(transform.rotation, syncRot) >= threshold)
        {
            CmdGetPos(transform.rotation);
            lastRot = transform.rotation;
        }
    }
}
