using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anima partículas para criar um "Ajax Loader"
/// </summary>
/// <copyright file="Launcher.cs" company="Exit Games GmbH">
/// Part of: Photon Unity Networking Demos
/// </copyright>

namespace JDWCompany
{
    public class LoaderLobby : MonoBehaviour
    {

        #region Public Variables

        [Tooltip("Velocidade por segundo")]
        public float speed = 180f;
        [Tooltip("Tamanho do signo")]
        public float radius = 1f;
        public GameObject particles;

        #endregion

        #region Private Variables

        Vector3 _offset;
        Transform _transform;
        Transform _particleTransform;
        bool _isAnimating;

        #endregion

        #region MonoBehaviour CallBacks

        void Awake()
        {
            // cache para eficiência
            _particleTransform = particles.GetComponent<Transform>();
            _transform = GetComponent<Transform>();
        }
        void Update()
        {
            if (_isAnimating)
            {
                _transform.Rotate(0f, 0f, speed * Time.deltaTime);
                _particleTransform.localPosition = Vector3.MoveTowards(_particleTransform.localPosition, _offset, 0.5f * Time.deltaTime);
            }
        }

        #endregion

        #region Public Methods

        public void StartLoaderAnimation()
        {
            _isAnimating = true;
            _offset = new Vector3(radius, 0f, 0f);
            particles.SetActive(true);
        }
        public void StopLoaderAnimation()
        {
            particles.SetActive(false);
        }

        #endregion
    }
}
