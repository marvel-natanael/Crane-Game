using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RBTransform : MonoBehaviour, IPunObservable
{
    [SerializeField]
    private Rigidbody _rigidbody;

    #region Callbacks
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_rigidbody.position);
            stream.SendNext(_rigidbody.rotation);
            stream.SendNext(_rigidbody.velocity);
        }
        else
        {
            _rigidbody.position = (Vector3)stream.ReceiveNext();
            _rigidbody.rotation = (Quaternion)stream.ReceiveNext();
            _rigidbody.velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            _rigidbody.position += _rigidbody.velocity * lag;
        }
    }
    #endregion
}
