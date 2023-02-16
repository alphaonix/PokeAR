using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Lobby : MonoBehaviourPunCallbacks
{

    void Awake()
    {
        Debug.Log("Conectando no servidor...");
        PhotonNetwork.LocalPlayer.NickName = "Player" + Random.Range(0, 1000);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("\nConectado no servidor!");
        if (PhotonNetwork.InLobby == false)
        {
            Debug.Log("\nEntrando no Lobby...");
            PhotonNetwork.JoinLobby();
        }
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("\nEntrou no Lobby!");
        PhotonNetwork.JoinRoom("GameLoot");
        Debug.Log("\nEntrando na sala GameLoot...");
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("\nErro ao entrar na sala: " + message + " return code = " + returnCode);

        if (returnCode == ErrorCode.GameDoesNotExist)
        {
            RoomOptions room = new RoomOptions { MaxPlayers = 20 };
            PhotonNetwork.CreateRoom("GameLoot", room, null);
            Debug.Log("\nCriando sala GameLoot!");
        }
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    Debug.Log("\nPlayer entrou na sala: " + newPlayer.NickName);
    //}


    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    Debug.Log("\nPlayer saiu na sala: " + otherPlayer.NickName);
    //}

    public override void OnLeftRoom()
    {
        Debug.Log("\nVocê saiu da sala...");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("\nVocê entrou na sala: GameLoot, como: " + PhotonNetwork.LocalPlayer.NickName);
        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
        Quaternion rotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360.0f));
        //Instantiate do Photon carrega um prefab do Resources
        PhotonNetwork.Instantiate("PlayerCube", position, rotation);
    }



}
