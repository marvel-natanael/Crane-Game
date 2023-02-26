using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class GameManager : MonoBehaviourPunCallbacks
{
    private Dictionary<Player, string> _playerList;
    private static GameManager _gameManager;
    private PhotonView _photonView;

    [SerializeField]
    private GameObject _fruit;
    [SerializeField]
    private GameObject _startPanel;
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private TextMeshProUGUI _winText;
    [SerializeField]
    private ConnectAndJoinRandom connectAndJoin;
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

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1024, 768, false);
        connectAndJoin.enabled = false;
    }

    public void StartGame()
    {
        _startPanel.gameObject.SetActive(false);
        connectAndJoin.enabled = true;
    }

    public void ShowWinner(string winnerName)
    {
        _winPanel.gameObject.SetActive(true);
        _winText.text = winnerName + " wins!";

        StartCoroutine(QuitGame());
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(2f);
        PhotonNetwork.Disconnect();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Test_Map");
        base.OnLeftRoom();
    }
}
