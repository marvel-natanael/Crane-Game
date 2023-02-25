using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectSettings : MonoBehaviour
{
    public ConnectAndJoinRandom AutoConnect;
    IEnumerator Start()
    {

        //Debug.Log (SceneManager.GetSceneByName (PunCockpit_scene).IsValid());

        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);

        yield return new WaitForSeconds(2f);

        if (SceneManager.sceneCount == 1)
        {

            AutoConnect.ConnectNow();
        }
        else
        {
            Destroy(AutoConnect);
        }

        yield return 0;
    }
}
