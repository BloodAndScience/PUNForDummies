﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;


namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class LauncherUI : MonoBehaviour
    {
        [SerializeField]
        private Launcher _launcher;

        [SerializeField]
        TMP_InputField _inputField;

        [SerializeField]
        private Button _connectButton;

        #region Private Constants
        const string playerNamePrefKey = "PlayerName"; 
        #endregion


        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _connectButton.onClick.AddListener( () => {_launcher.Connect();});
            _inputField.onValueChanged.AddListener(SetPlayerName);
        }

        void Start()
        {
            string defaultName = string.Empty;
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }


            PhotonNetwork.NickName = defaultName;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value">The name of the Player</param>
        public void SetPlayerName(string value)
        {
            // #Important
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }

            PhotonNetwork.NickName = value;


            PlayerPrefs.SetString(playerNamePrefKey, value);
        }

        #endregion
    }
}