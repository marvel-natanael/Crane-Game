using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class FruitSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject[] _fruit;
    [SerializeField]
    OnJoinedInstantiate _joinedInstantiate;
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

    private void RPCSpawnFood()
    {
        foreach(var fruit in _fruit)
        {
            var newFood = PhotonNetwork.InstantiateRoomObject(fruit.name, transform.position, Quaternion.identity, 0);
        }
    }
}
