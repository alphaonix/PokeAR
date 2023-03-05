using System.Collections;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;
using Random = System.Random;

public class GameController : MonoBehaviourPunCallbacks
{

    #region Private Fields
   
    private int round = 0;
    private int move = 0;
    private bool turn;
    int raisePlayer;
    private int _playerMoney;
    private int _playerBet;
    private int _opponentBet;
    private string _stringPlayer;
    private string _stringOpponent;
    private string _statusPlayer;
    private string _statusOpponent;
    private int amountPlayer;
    private int moneyPlayer;
    private int moneyOpponent;
    private int amountPot;
    
    #endregion
    
    #region Serialize Fields
    
    [Header("PlayerUI")]
    [SerializeField]
    TextMeshProUGUI P1Money;
    [SerializeField]
    TextMeshProUGUI P2Money;
    [SerializeField]
    TextMeshProUGUI P1Bet;
    [SerializeField]
    TextMeshProUGUI P2Bet;
    [SerializeField]
    TextMeshProUGUI P1Name;
    [SerializeField]
    TextMeshProUGUI P2Name;
    [SerializeField]
    TextMeshProUGUI Pot;
    [SerializeField]
    TextMeshProUGUI Turn;
    [SerializeField]
    Slider slider;
    [SerializeField]
    TextMeshProUGUI RaiseValue;
    [SerializeField]
    TextMeshProUGUI Vencedor;
    [Header("Buttons")]
    
    public GameObject call;
    public GameObject check;
    public GameObject fold;
    public GameObject raise;
    public GameObject vencedorInfo;
    
    [Header("Cards Manager")]
    public List<GameObject> cards;
    public List<GameObject> randomCards;

    #endregion
    
    #region PunCallbacks
    
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PlayerInfoUI();
            InicializaBaralho();
            StartCoroutine(SpawnPlayerCards());
        }
        else
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P2Name", PhotonNetwork.LocalPlayer.NickName } });
            StartCoroutine(SpawnPlayerCards());
        }
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        PlayerInfoUI();
    }
    
    #endregion
    
    #region IEnumerators Functions
    
    public IEnumerator SpawnCheck()
    {
        yield return new WaitForSeconds(1.0f);
        PhotonNetwork.InstantiateRoomObject(this.randomCards[4].name, new Vector3(0 - 600, 0f, 0f), Quaternion.Euler(-90, 0, 0), 0);
        PhotonNetwork.InstantiateRoomObject(this.randomCards[5].name, new Vector3(0 - 300, 0f, 0f), Quaternion.Euler(-90, 0, 0), 0);
        PhotonNetwork.InstantiateRoomObject(this.randomCards[6].name, new Vector3(0f, 0f, 0f), Quaternion.Euler(-90, 0, 0), 0);
    }
    
    public IEnumerator SpawnTurn()
    {
        yield return new WaitForSeconds(1.0f);
        PhotonNetwork.InstantiateRoomObject(this.randomCards[8].name, new Vector3(300, 0f, 0f), Quaternion.Euler(-90, 0, 0), 0);
    }
    
    public IEnumerator SpawnRiver()
    {
        yield return new WaitForSeconds(1.0f);
        PhotonNetwork.InstantiateRoomObject(this.randomCards[7].name, new Vector3(600, 0f, 0f), Quaternion.Euler(-90, 0, 0), 0);
    }
    
    public IEnumerator SpawnPlayerCards()
    {
        yield return new WaitForSeconds(1.0f);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject(this.randomCards[0].name, new Vector3(-980, -400, -25), Quaternion.Euler(-90, 0, 0), 0);
            PhotonNetwork.InstantiateRoomObject(this.randomCards[1].name, new Vector3(-810, -400, -25), Quaternion.Euler(-90, 0, 0), 0);
        }
        else
        {
            PhotonNetwork.InstantiateRoomObject(this.randomCards[2].name, new Vector3(-980, -400, -25), Quaternion.Euler(-90, 0, 0), 0);
            PhotonNetwork.InstantiateRoomObject(this.randomCards[3].name, new Vector3(-810, -400, -25), Quaternion.Euler(-90, 0, 0), 0);
        }
    }
    
    #endregion
    
    #region Fuctions Buttons
    
    public void CallButton()
    {
        GameState();
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Status", "CALL" } });
        
        if (_playerBet == _opponentBet)
        {
            GameState();
        }
        
        if (_playerBet < _opponentBet)
        {
            MoneyUpdatePlayer(_opponentBet - _playerBet);
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Bet", _opponentBet } });
            PotUpdate(_opponentBet - _playerBet);
        }
    }
   
    public void CheckButton()
    {
        GameState();
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Status", "CHECK" } });
    }
    
    public void FoldButton()
    {
        GameState();
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Status", "FOLD" } });
        amountPot = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["Pot"]) + Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P1Bet"]) + Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P2Bet"]);
        MoneyPotFold(amountPot);
        RestartGame();
    }
    
    public void RaiseButton()
    {
        GameState();
        int aux = Convert.ToInt32(RaiseValue.text);
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Status", "RAISE" } });
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Raise", aux } });
        raisePlayer = aux - _playerBet;
        PotUpdate(raisePlayer);
        int aux2 = _playerMoney - raisePlayer;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Money", aux2 } });
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Bet", aux } });
        // Debug.Log(RaiseValue.text);
        //Debug.Log(aux);
        //Debug.Log(raisePlayer);
        // UpdateStatus();
        //photonView.RPC("UpdateStatus", RpcTarget.All);
    }
    
    public void Slide()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            RaiseValue.text = v.ToString("0");
        });
        //Debug.Log(RaiseValue.text);
        //return Convert.ToInt32(RaiseValue);
    }
    
    public void ShowCheck(bool value)
    {
        bool show = value;
        bool dontShow = !value;
        call.SetActive(dontShow);
        check.SetActive(show);
    }
    
    #endregion
    
    [PunRPC]
    void PlayerInfoUI()
    {
        //Dinheiro Total do Jogador
        P1Money.text = PhotonNetwork.CurrentRoom.CustomProperties["P1Money"].ToString();
        P2Money.text = PhotonNetwork.CurrentRoom.CustomProperties["P2Money"].ToString();
        
        //Aposta do jogador na Partida
        P1Bet.text = PhotonNetwork.CurrentRoom.CustomProperties["P1Bet"].ToString();
        P2Bet.text = PhotonNetwork.CurrentRoom.CustomProperties["P2Bet"].ToString();
        
        //Status do jogador
        _statusPlayer = PhotonNetwork.CurrentRoom.CustomProperties["P1Status"].ToString();
        _statusOpponent = PhotonNetwork.CurrentRoom.CustomProperties["P2Status"].ToString();
        
        //Contabiliza total de Jogadas do Player
        //_playerMove = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P1Move"]);
        //_opponentMove = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P2Move"]);
        
        //Nomes dos Jogadores
        P1Name.text = PhotonNetwork.CurrentRoom.CustomProperties["P1Name"].ToString();
        P2Name.text = PhotonNetwork.CurrentRoom.CustomProperties["P2Name"].ToString();
        
        //Valor do pote total do Pot
        Pot.text = PhotonNetwork.CurrentRoom.CustomProperties["Pot"].ToString();
        
        //Valor do pote total do Raise
        raisePlayer = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["Raise"]);
        
        //Trocar a vez do Jogador
        turn = Convert.ToBoolean(PhotonNetwork.CurrentRoom.CustomProperties["Turn"]);
    }
    
    [PunRPC]
    public void InicializaBaralho()
    {
        //Criando o randomCards
        randomCards = new List<GameObject>();
      
        //Percorre toda a lista de cards
        for (int i = 0; i < cards.Count; i++)
        {
            //Adiciona todas as 52 cards ao randomCards
            randomCards.Add(cards[i]);
        }
        
        //Faz o embaralhamento
        for (int j = 0; j < randomCards.Count; j++)
        {
            GameObject cartaAtual = randomCards[j];
            Random rand = new Random();
            int indexRandom = rand.Next(0, randomCards.Count);
            randomCards[j] = randomCards[indexRandom];
            randomCards[indexRandom] = cartaAtual;
        }
    }
    
    public void GameState()
    {
        _playerMoney = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringPlayer + "Money"]);
        _playerBet = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringPlayer + "Bet"]);
        _opponentBet = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringOpponent + "Bet"]);

        if (Convert.ToBoolean(PhotonNetwork.CurrentRoom.CustomProperties["Turn"]))
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Turn", false } });
        }
        else
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Turn", true } });
        }
    }
    
    public void UpdateStatus()
    {
        _statusPlayer = PhotonNetwork.CurrentRoom.CustomProperties["P1Status"].ToString();
        _statusOpponent = PhotonNetwork.CurrentRoom.CustomProperties["P2Status"].ToString();
        turn = Convert.ToBoolean(PhotonNetwork.CurrentRoom.CustomProperties["Turn"]);
        Debug.Log("P1 = " + _statusPlayer + "   -   P2 = " + _statusOpponent + "   -   T = " + turn);
    }
    
    #region Restart, New Game and Updates
    
    private void Update()
    {
        Log();
        UpdateStatus();
        // Debug.Log(RaiseValue.text);
        //if (_statusPlayer == "CHECK" && _statusOpponent == "CHECK" || _statusPlayer == "CALL" && _statusOpponent == "CALL" || _statusPlayer == "CALL" && _statusOpponent == "RAISE")
        //{
        //    _statusPlayer = "null";
        //    _statusOpponent = "null";
        //    round++;
        //}
        //if (_statusPlayer == "FOLD" || _statusOpponent == "FOLD")
        //{
        //    _statusPlayer = "null";
        //    _statusOpponent = "null";
        //    round++;
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vencedor.text = P1Name.text + " Ganhou o round.";
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vencedor.text = P2Name.text + " Ganhou o round.";
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Pot", 0 } });
            int aux2 = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P1Bet"]);
            int aux = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P2Bet"]);
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P1Money", aux2 + aux } });
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vencedor.text = "";
            NovoJogo();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            round++;
            GameState2();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //InicializaBaralho();
            StartCoroutine(SpawnPlayerCards());
        }
    }
    
    public void Log()
    {
        if (Convert.ToBoolean(PhotonNetwork.CurrentRoom.CustomProperties["Turn"])) // TRUE == Player1
        {
            Turn.text = "LOG: Vez de " + P1Name.text;
            _stringPlayer = "P1";
            _stringOpponent = "P2";
        }
        else // FALSE == Player2
        {
            Turn.text = "LOG: Vez de " + P2Name.text;
            _stringPlayer = "P2";
            _stringOpponent = "P1";
        }
    }
    
    public void GameState2()
    {
        int pot = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P2Bet"]) + Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["P1Bet"]);
        if (round == 1)
        {
            StartCoroutine(SpawnCheck());
        }
        if (round == 2)
        {
            StartCoroutine(SpawnTurn());
        }
        if (round == 3)
        {
            StartCoroutine(SpawnRiver());
        }
        if (round == 4)
        {
            RestartGame();
            NovoJogo();
        }
    }
    
    void NovoJogo()
    {
        round = 0;
    }
    
    void RestartGame()
    {
        amountPot = 0;
        foreach (var gameObj in GameObject.FindGameObjectsWithTag("Cards"))
        {
            Destroy(gameObj);
        }
        randomCards = new List<GameObject>();
    }
    
    public void PotStart()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Pot", 150 } });
    }
    
    public void PotUpdate(int moneyPot)
    {
        int aux = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties["Pot"]);
        moneyPot += aux;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Pot", moneyPot } });
    }
    
    public void MoneyUpdatePlayer(int moneyUpdate)
    {
        moneyPlayer = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringPlayer + "Money"]) - moneyUpdate;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Money", moneyPlayer } });
        //PotUpdate(moneyPlayer);
    }
    
    public void MoneyBet(int moneyUpdate)
    {
        moneyPlayer = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringPlayer + "Money"]) - moneyUpdate;
        //moneyUpdate = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringPlayer + "Bet"]);
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Bet", moneyUpdate } });
    }
    
    public void MoneyPotFold(int moneyUpdate)
    {
        moneyPlayer = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringPlayer + "Money"]);
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Money", moneyPlayer } });
        moneyOpponent = Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[_stringOpponent + "Money"]) + moneyUpdate;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringOpponent + "Money", moneyOpponent } });
        PotStart();
        if (!turn)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringOpponent + "Bet", 50 } });
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Bet", 100 } });
        }
        else
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringOpponent + "Bet", 100 } });
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { _stringPlayer + "Bet", 50 } });
        }
        PotStart();
        // PotUpdate(150);
    }

    #endregion

}
