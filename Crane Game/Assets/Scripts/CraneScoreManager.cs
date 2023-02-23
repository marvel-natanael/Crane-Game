using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScoreManager : MonoBehaviour
{
    [SerializeField]
    int _score { get; set; }
    [SerializeField]
    private List<GameObject> _gameObjects = new List<GameObject>();
    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Objects")
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
        _score = _gameObjects.Count;
        Debug.Log(_score);
    }
}
