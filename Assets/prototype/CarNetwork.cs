using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CarNetwork : NetworkBehaviour
{
    private NetworkIdentity networkId;

    void Start()
    {
        networkId = GetComponent<NetworkIdentity>();

        // enable-disable controller
        if (!networkId.isLocalPlayer)
        {
            GetComponent<CarController>().enabled = false;
        }
        else
        {
            GetComponent<CarController>().enabled = true;
        }
    }

    void Update()
    {

    }
}
