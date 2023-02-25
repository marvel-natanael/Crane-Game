using Photon.Pun;
using UnityEngine;

public class SyncChildren : MonoBehaviour, IPunObservable
{
    [SerializeField]
    Transform[] childrenTransforms;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int i = 0; i < childrenTransforms.Length; i++)
            {
                if (childrenTransforms[i] != null)
                {
                    stream.SendNext(childrenTransforms[i].localPosition);
                    stream.SendNext(childrenTransforms[i].localRotation);
                }
            }
        }
        else
        {
            for (int i = 0; i < childrenTransforms.Length; i++)
            {
                if (childrenTransforms[i] != null)
                {
                    childrenTransforms[i].localPosition = (Vector3)stream.ReceiveNext();
                    childrenTransforms[i].localRotation = (Quaternion)stream.ReceiveNext();
                }
            }
        }
    }
}
