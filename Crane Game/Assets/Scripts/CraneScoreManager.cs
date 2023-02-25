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
    [SerializeField]
    private PhotonView _photonView;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Objects")
        {
            _gameObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Objects")
        {
            _gameObjects.Remove(other.gameObject);
        }
    }

    public void CalculateScore()
    {
        _gameObjects.TrimExcess();
        int temp = _gameObjects.Count;
        _score += temp;
    }  
    
    [PunRPC]
    public void Temp()
    {
        _score++;
    }

    public void UpdateScoreText()
    {
        _playerUIManager.UpdateText(_scoreText, _score.ToString());
        Debug.Log("h");
    }

    public int GetScore()
    {
        return _score;
    }
}
