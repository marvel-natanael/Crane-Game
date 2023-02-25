using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject _playerPrefab;
    private Transform[] _spawnPoints;

    public void SpawnPlayer(int i)
    {
        PhotonNetwork.Instantiate(_playerPrefab.name, _spawnPoints[i].position, _spawnPoints[i].rotation, 0);
    }    

    public void SpawnPlayers()
    {
        PhotonView photonView = _playerPrefab.GetComponent<PhotonView>();
        int temp = photonView.InstantiationId;
        Debug.Log(temp);
    }
}
