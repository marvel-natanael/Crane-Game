using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CraneScoreManager : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private List<GameObject> _gameObjects = new List<GameObject>();
    [SerializeField]
    private PlayerUIManager _playerUIManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Objects")
        {
            _gameObjects.Add(other.gameObject);
        }
    }

    /*    private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Objects")
            {
                _gameObjects.Remove(other.gameObject);
            }
        }*/

    [PunRPC]
    public void CalculateScore()
    {
        _gameObjects.TrimExcess();
        int temp = _gameObjects.Count;
        _score = temp;

        if(_score >= 1)
        {
            PhotonView photonView = GetComponentInParent<PhotonView>();
            GameManager.Instance.ShowWinner(photonView.InstantiationId.ToString());
        }
    }  
    
    [PunRPC]
    public void Temp()
    {
        _score++;
    }

    public void UpdateScoreText()
    {
        _playerUIManager.UpdateText(_scoreText, _score.ToString());
    }

    public int GetScore()
    {
        return _score;
    }
}
