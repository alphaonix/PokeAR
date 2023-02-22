using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameManager Instancia { get; private set; }

    [SerializeField] private string localizacaoPrefab;
    [SerializeField] private Transform HUD;

    Transform parent;

    private int jogadoresEmJogo = 0;

    

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        photonView.RPC("AdicionaJogador", RpcTarget.AllBuffered);
        //photonView.RPC("CriaJogador", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void AdicionaJogador()
    {
        jogadoresEmJogo++;
        if(jogadoresEmJogo == PhotonNetwork.PlayerList.Length)
        {
            CriaJogador();
        }
    }

    [PunRPC]
    private void CriaJogador()
    {
        //photonView.RPC("InicializaBaralho", RpcTarget.All);
        var jogadorObj = PhotonNetwork.Instantiate(localizacaoPrefab, Vector3.zero, Quaternion.identity, 0);

        jogadorObj.transform.SetParent(HUD);
    }
}
