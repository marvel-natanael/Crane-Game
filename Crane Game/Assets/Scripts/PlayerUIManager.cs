using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    private PhotonView _photonView;
    private void Awake()
    {
        _photonView = GetComponentInParent<PhotonView>();
        _nameText.text = "Player" + _photonView.InstantiationId;
    }
    public void UpdateText(TextMeshProUGUI textToUpdate, string text)
    {
        textToUpdate.text = text;
    }
}
