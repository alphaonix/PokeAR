using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Public Fields
    static public GameManager Instance;
    #endregion

    #region Private Fields

    private GameObject instance;
    [Tooltip("O prefab que representa o Player")]
    [SerializeField]
    private GameObject playerPrefab;

    [Tooltip("A UI que mostra o progresso de conexão para o usuário")]
    [SerializeField]
    private Text feedbackText;
    #endregion

    #region MonoBehaviour CallBacks
    void Start()
    {

        Instance = this;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LoadLevel("Lobby");
            return;
        }
        if (playerPrefab == null)
        {

            Debug.LogError("Player Prefab não está instanciado");
        }
        else
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            Debug.Log("Instantiate playerPrefab for " + PhotonNetwork.PlayerList);
            
    }
        }

    

   



    void Update()
    {
        // "back" button of phone equals "Escape". quit app if that's pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #endregion

    #region Photon Callbacks
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom() " + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerLeftRoom() " + newPlayer.NickName);

    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
    #endregion

    void playerNames()
    {

        foreach (var player in PhotonNetwork.PlayerList)
        {
            feedbackText.text += System.Environment.NewLine + player.NickName;
            // list += player.NickName + "\n";
        }
        // return list;
    }
}
