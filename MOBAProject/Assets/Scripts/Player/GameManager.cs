using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour
{

    public GameObject player;
    private new PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Vector3 playerPosition = new Vector3(Random.Range(1f, 5f), Random.Range(1f, 1f), Random.Range(1f, 5f));
        PhotonNetwork.Instantiate(player.name, playerPosition, Quaternion.identity, 0);
    }
    private void Update()
    {
        
    }

    public void OnRespawn()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(6);
        Vector3 playerPosition = new Vector3(Random.Range(1f, 5f), Random.Range(1f, 1f), Random.Range(1f, 5f));
        PhotonNetwork.Instantiate(player.name, playerPosition, Quaternion.identity, 0);
    }


    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(1);
    }

    public void Leave()
    {
        /*PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.room.PlayerCount == 1)
        {
            
        }*/
        photonView.RPC("RPC_Kick", PhotonTargets.All);
    }

    [PunRPC]
    private void RPC_Kick()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} joined the game");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log($"Player {otherPlayer.NickName} disconnected the game");

    }
}
