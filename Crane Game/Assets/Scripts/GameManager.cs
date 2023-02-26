using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager _gameManager;
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
            }

            return _gameManager;
        }
    }

    #region Initialization
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
    #endregion

    #region Game Controls
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
    #endregion

    #region Callbacks
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Test_Map");
        base.OnLeftRoom();
    }
    #endregion
}
