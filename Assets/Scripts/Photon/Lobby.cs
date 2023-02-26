using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace JDWCompany
{

    public class Lobby : MonoBehaviourPunCallbacks
    {
        /// <summary>
        ///Cria a conexão com o servidor de sinalização para conectar e criar/entrar na sala automáticamente.
        /// <summary> 

        #region Private Serializable Fields

        [Tooltip("A UI onde o usuário pode escrever seu nome, conectar e jogar")]
        [SerializeField]
        private GameObject controlPanel;

        [Tooltip("A UI que mostra o progresso de conexão para o usuário")]
        [SerializeField]
        private Text feedbackText;

        [Tooltip("A UI do lobby inicial")]
        [SerializeField]
        private LoaderLobby loaderLobby;

        [Tooltip("O máximo de jogadores por sala")]
        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        #endregion

        #region Private Fields and Contants
        bool isConnecting;
        #endregion

        #region MonoBehaviour CallBacks
        //Executa antes do primeiro frame/star() do jogo chamando um GameObject.
        void Awake()
        {
            //Garante que PhotonNetwork.LoadLevel() do cliente Master e dos outros clientes sincronizem automaticamente na mesma sala.
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        #endregion

        #region Public Methods
        public void Connect()
        {
            feedbackText.text = "";
            isConnecting = true;
            controlPanel.SetActive(false);
            if (loaderLobby != null)
            {
                loaderLobby.StartLoaderAnimation();
            }
            // Verifica se o Player está conectado - se estiver entra na sala, senão inicia uma conexão com o servidor.
            if (PhotonNetwork.IsConnected)
            {
                LogFeedback("Entrando da Sala...");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                LogFeedback("Conectando...");
                // Pega as configurações da Unity para se conectar com o servidor online do Photon
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        void LogFeedback(string message)
        {
            if (feedbackText == null)
            {
                return;
            }
            feedbackText.text += System.Environment.NewLine + message;
        }
        #endregion

        #region MonoBehaviourPunCallbacks CallBacks
        //Chamada após a conexão com o Master foi estabelecida.
        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                LogFeedback("Conexão com o Master foi estabelecida, entrando na sala ...");
                //O Player tenta entrar em uma sala, caso falhe OnjoinRandomFailed() � chamado               
                PhotonNetwork.JoinRandomRoom();
            }
        }
        //Chamada quando JonRandom() falha.
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Criando uma nova sala");
            //if (returnCode == ErrorCode.GameDoesNotExist)
            //{
            //    RoomOptions room = new RoomOptions { MaxPlayers = this.maxPlayersPerRoom };
            //    PhotonNetwork.CreateRoom(null, room);
            //}
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
        }
        //Chamada quando o servidor do Photon é desconectado.
        public override void OnDisconnected(DisconnectCause cause)
        {
            LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);
            loaderLobby.StopLoaderAnimation();
            isConnecting = false;
            controlPanel.SetActive(true);
        }
        //Chamada ao entrar em uma sala (cria/conecta).
        public override void OnJoinedRoom()
        {
            LogFeedback("<Color=Green>Entrando na sala ...</Color> (" + PhotonNetwork.CurrentRoom.PlayerCount + ")");
            //Carrega apena se for o primeiro Player a entrar, caso contr�rio a cena � sincronizada autom�ticamente.
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                //Nome da cena que será carregada.
                PhotonNetwork.LoadLevel("CardsPhoton");
            }
        }
        #endregion

    }
}


