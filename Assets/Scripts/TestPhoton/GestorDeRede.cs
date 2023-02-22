using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestorDeRede : MonoBehaviourPunCallbacks
{
    public static GestorDeRede Instancia { get; private set; }

    private void Awake()
    {
        if(Instancia != null && Instancia != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexão bem Sucedida");
    }

    public void CriaSala(string nomeDaSala)
    {
        PhotonNetwork.CreateRoom(nomeDaSala);
    }

    public void EntraSala(string nomeDaSala)
    {
        PhotonNetwork.JoinRoom(nomeDaSala);
    }

    public void MudaNick(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }

    public string ObterListaDeJogadores()
    {
        var lista = "";

        foreach(var player in PhotonNetwork.PlayerList)
        {
            lista += player.NickName + "\n";
        }
        return lista;
    }

    public bool DonoDaSala()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public void SairDoLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public void ComecaJogo(string nomeCena)
    {
        PhotonNetwork.LoadLevel(nomeCena);
    }
}
