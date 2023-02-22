using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Baralho : MonoBehaviourPunCallbacks
{
    [SerializeField]
    float positionMesa;

    public List<GameObject> cartas;

    public List<GameObject> baralho;

    private Photon.Realtime.Player photonPlayer;
    private int id;
    public Mesa m;

    public Jogador p;

    public void Inicializa(Photon.Realtime.Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;
    }

    void Start()
    {
        InicializaBaralho();
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
            baralho[i].transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        }

        for (int i = 0; i < baralho.Count; i++)
        {
            baralho[i].tag = "Player";
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
        int positionMesa = 2;

        //Adiciona apenas 5 cartas na mesa
        for (int i = 0; i < aux; i++)
        {
            baralho[2].tag = "Check1";
            baralho[3].tag = "Check2";
            baralho[4].tag = "Check3";
            //Adiciona as cartas na mesa
            m.mesa.Add(baralho[i]);

            //-20 -10 0 10 20  
            if (positionMesa > 0)
            {
                //As 2 cartas da mesa virada para cima
                Instantiate(baralho[i], transform.position + new Vector3(0 - positionMesa, 0, 0), Quaternion.Euler(360, 0, 0), transform.parent);
            }
            else
            {
                //As 3 cartas da mesa virada para baixo
                Instantiate(baralho[i], transform.position + new Vector3(0 - positionMesa, 0, 0), Quaternion.Euler(180, 0, 0), transform.parent);
            }
            //Auxiliar para ir reposicionando as cartas na tela
            positionMesa -= 1;
        }

        //Auxilio para reposicionar as cartas do jogador
        float xP = 0.0f;
        float yP = 0.0f;
        float zP = 0.0f;

        //Auxilio para rotacao das cartas do jogador
        float zrP = 0f;

        //Adiciona apenas 2 cartas ao jogador
        for (int i = aux; i < aux + 2; i++)
        {
            //Adiciona as cartas ao jogador
            p.jogador.Add(baralho[i]);

            //Posiciona as Cartas do jogador na tela
            Instantiate(baralho[i], transform.position + new Vector3(-2.31f + xP, 0 + yP, -1.2f + zP), Quaternion.Euler(0, 27.64f + zrP, 0), transform.parent);

            //Variaveis para auxiliar na segunda carta do jogador
            xP = 0.22f;
            yP = 0.2f;
            zP = -0.16f;
            zrP = 13.92f;
        }        
    }
}