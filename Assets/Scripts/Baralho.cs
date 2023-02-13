using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baralho : MonoBehaviour
{
    [SerializeField]
    float escala;

    public List<GameObject> cartas;

    public List<GameObject> baralho;

    public Mesa m;

    public Player p;

    void Start()
    {
        //Criando o baralho
        baralho = new List<GameObject>();

        //Percorre toda a lista de cartas
        for (int i = 0; i < cartas.Count; i++)
        {
            //Adiciona todas as 52 cartas ao baralho
            baralho.Add(cartas[i]);

            //Aumenta a escala das cartas
            baralho[i].transform.localScale = new Vector3(escala, escala, escala);
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
        int positionMesa = 6;

        //Adiciona apenas 5 cartas na mesa
        for (int i = 0; i < aux; i++)
        {

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
            positionMesa -= 3;
        }

        //Auxilio para reposicionar as cartas do jogador
        int xP = 0;
        int yP = 0;
        int zP = 0;

        //Auxilio para rotacao das cartas do jogador
        float zrP = 0f;

        //Adiciona apenas 2 cartas ao jogador
        for (int i = aux; i < aux + 2; i++)
        {
            //Adiciona as cartas ao jogador
            p.player.Add(baralho[i]);

            //Posiciona as Cartas do jogador na tela
            Instantiate(baralho[i], transform.position + new Vector3(-19 + xP, 0 + yP, -19 + zP), Quaternion.Euler(-90, 0, 15.5f + zrP), transform.parent);

            //Variaveis para auxiliar na segunda carta do jogador
            xP = 2;
            yP = 1;
            zP = -1;

            zrP = 20.251f;
        }
    }


}