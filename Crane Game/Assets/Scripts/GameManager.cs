using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    private Dictionary<Player, string> _playerList;
    private static GameManager _gameManager;
    private PhotonView _photonView;
    public static GameManager Instance
    {
        get
        {
            if (!_gameManager)
            {
                _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!_gameManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    _gameManager.Init();
                }
            }

            return _gameManager;
        }
    }

    void Init()
    {
        _photonView = GetComponent<PhotonView>();
        if (_playerList == null)
        {
            _playerList = new Dictionary<Player, string>();
        }
    }

    [PunRPC]
    public void RegisterPlayerRPC(Player player, string crane)
    {
        string thisEvent;
        if (Instance._playerList.TryGetValue(player, out thisEvent))
        {
            thisEvent = crane;
        }
        else
        {
            thisEvent = crane;
            Instance._playerList.Add(player, thisEvent);
        }
    }
    public void RegisterPlayer(Player crane, CraneControls score)
    {
        _photonView.RPC("RegisterPlayerRPC", RpcTarget.All, crane, score);
    }

    public void UpdateScore(CraneScoreManager scoreManager)
    {
        //scoreManager.UpdateScoreText();
    }

    [PunRPC]
    public void UpdateScoreText(CraneScoreManager scoreManager)
    {
        //scoreManager.UpdateScoreText();
    }
}
