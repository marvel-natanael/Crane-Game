using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class FruitSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField]
    OnJoinedInstantiate _joinedInstantiate;

    #region Callbacks
    public override void OnCreatedRoom()
    {
        if(PhotonNetwork.CountOfPlayersInRooms > 1)
        {
            _joinedInstantiate.enabled = true;
        }

        _joinedInstantiate.enabled = false;
        //_photonView.RPC("RPCSpawnFood", RpcTarget.AllBufferedViaServer);
        //RPCSpawnFood();
        base.OnCreatedRoom();
    }
    #endregion
}
