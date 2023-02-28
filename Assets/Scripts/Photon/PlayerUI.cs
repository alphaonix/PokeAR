using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    // #region Private Fields


	//     [Tooltip("UI Players Name")]
	//     [SerializeField]
	//     private Text playerNameText;

	//     [Tooltip("UI Slider to display Player's Health")]
	//     [SerializeField]
	//     private Text playerMoney;

    //     PlayerManager target;

	// 	float characterControllerMoney;

	// 	Transform targetTransform;

	// 	Renderer targetRenderer;

	//     CanvasGroup _canvasGroup;
	    
	// 	Vector3 targetPosition;

	// 	#endregion

    // #region MonoBehaviour Messages
		
	// 	void Awake()
	// 	{

	// 		_canvasGroup = this.GetComponent<CanvasGroup>();
	// 		this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
	// 	}

	// 	void Update()
	// 	{
    //         //Destroi a si mesmo
	// 		if (target == null) {
	// 			Destroy(this.gameObject);
	// 			return;
	// 		}

	// 		if (playerMoney != null) {
	// 			playerMoney.text = Convert.ToString(target.Money);
	// 		}
	// 	}

	// 	void LateUpdate () {

    //         // Não mostra a interface do usuário se não estivermos visíveis para a câmera, evitando assim possíveis erros ao ver a interface do usuário, mas não o próprio player.			if (targetRenderer!=null)
	// 		if (targetRenderer!=null)
	// 		{
	// 			this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
	// 		}
	// 	}




	// 	#endregion

	// 	#region Public Methods

	// 	public void SetTarget(PlayerManager _target){

	// 		if (_target == null) {
	// 			Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
	// 			return;
	// 		}

	// 		if (playerNameText != null) {
    //             playerNameText.text = this.target.photonView.Owner.NickName;
	// 		}
	// 	}

	// 	#endregion





}
