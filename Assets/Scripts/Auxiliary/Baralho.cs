using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Baralho : MonoBehaviourPunCallbacks
{

    [SerializeField]
    int scale;

    public List<GameObject> cartas;

    public List<GameObject> baralho;

    public PhotonView photonView;

    private int id;
    public Mesa m;
    public Jogador p;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            InicializaBaralho();
        }
    }



    [PunRPC]
    public void InicializaBaralho()
    {
        //Criando o baralho
        baralho = new List<GameObject>();

        //Percorre toda a lista de cartas
        for (int i = 0; i < cartas.Count; i++)
        {
            //Adiciona todas as 52 cartas ao baralho
            baralho.Add(cartas[i]);

            //Aumenta a escala das cartas
            // baralho[i].transform.localScale = new Vector3(scale, scale, scale);
        }

        //Faz o embaralhamento
        for (int j = 0; j < baralho.Count; j++)
        {
            GameObject cartaAtual = baralho[j];
            int indexRandom = Random.Range(0, baralho.Count);
            baralho[j] = baralho[indexRandom];
            baralho[indexRandom] = cartaAtual;
        }

       
    }
}