using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Baralho : MonoBehaviourPunCallbacks
{
    // [SerializeField]
    // public float positionMesa;

    [SerializeField]
    int scale;

    public List<GameObject> cartas;

    public List<GameObject> baralho;

    //public PhotonView photonView;
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

    // void Update(){
    //     photonView.RPC("InicializaBaralho", RpcTarget.All);
    // }

    public override void OnJoinedRoom()
    {
        Debug.Log("oi");
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
            baralho[i].transform.localScale = new Vector3(scale, scale, scale);
        }

        //Faz o embaralhamento
        for (int j = 0; j < baralho.Count; j++)
        {
            GameObject cartaAtual = baralho[j];
            int indexRandom = Random.Range(0, baralho.Count);
            baralho[j] = baralho[indexRandom];
            baralho[indexRandom] = cartaAtual;
        }

        //Usado para auxiliar o acesso das cartas da mesa e do jogador
        int aux = 5;
        //Usado para auxiliar no posicionamento das cartas da mesa
        int positionMesa = 800;

        //  Adiciona apenas 5 cartas na mesa
        for (int i = 0; i < aux; i++)
        {
            baralho[0].tag = "Check1";
            baralho[1].tag = "Check2";
            baralho[2].tag = "Check3";
            baralho[3].tag = "River";
            baralho[4].tag = "Turn";
            //Adiciona as cartas na mesa
            m.mesa.Add(baralho[i]);
            //Debug.Log(positionMesa);
            //-20 -10 0 10 20  
            if (positionMesa < 0)
            {
                PhotonNetwork.InstantiateRoomObject(baralho[i].name, transform.position + new Vector3(0 - positionMesa, 0, 0), Quaternion.Euler(90, 0, 0), 0);
                //As 2 cartas da mesa virada para cima
                // PhotonView.Instantiate(baralho[i], transform.position + new Vector3(0 - positionMesa, 0, 0), Quaternion.Euler(90, 0, 0), transform.parent);

            }
            else
            {
                //As 3 cartas da mesa virada para baixo
                PhotonView.Instantiate(baralho[i], transform.position + new Vector3(0 - positionMesa, 0, 0), Quaternion.Euler(-90, 0, 0), transform.parent);
            }
            //Auxiliar para ir reposicionando as cartas na tela
            positionMesa -= 400;
        }

        // //Auxilio para reposicionar as cartas do jogador
        // float xP = 0.0f;
        // float yP = 0.0f;
        // float zP = 0.0f;

        // //Auxilio para rotacao das cartas do jogador
        // float zrP = 0f;

        // //Adiciona apenas 2 cartas ao jogador
        // for (int i = aux; i < aux + 2; i++)
        // {
        //     //Adiciona as cartas ao jogador
        //    // p.jogador.Add(baralho[i]);

        //     //Posiciona as Cartas do jogador na tela
        //     //Instantiate(baralho[i], transform.position + new Vector3(-2.31f + xP, 0 + yP, -1.2f + zP), Quaternion.Euler(0, 27.64f + zrP, 0), transform.parent);

        //     //Variaveis para auxiliar na segunda carta do jogador
        //     xP = 0.22f;
        //     yP = 0.2f;
        //     zP = -0.16f;
        //     zrP = 13.92f;
        // }        
    }
}