﻿using System;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class TankManager
    {
        public Transform m_SpawnPoint;
        [SerializeField] Color m_PlayerColor;
        [HideInInspector] public int m_PlayerNumber;
        [HideInInspector] public string m_ColoredPlayerText;
        [HideInInspector] public GameObject m_Instance;
        [HideInInspector] public int m_Wins;

        private TankMovement m_Movement;
        private TankShooting m_Shooting;

        public void Setup()
        {
            m_Movement = m_Instance.GetComponent<TankMovement>();
            m_Shooting = m_Instance.GetComponent<TankShooting>();

            m_Movement.m_PayerNumber = m_PlayerNumber;
            m_Shooting.m_PayerNumber = m_PlayerNumber;

            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

            var renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = m_PlayerColor;
            }
        }

        public void DisableControl()
        {
            m_Movement.enabled = false;
            m_Shooting.enabled = false;
        }

        public void EnableControl()
        {
            m_Movement.enabled = true;
            m_Shooting.enabled = true;
        }

        public void Reset()
        {
            m_Instance.transform.SetPositionAndRotation(m_SpawnPoint.position, m_SpawnPoint.rotation);

            m_Instance.SetActive(false);
            m_Instance.SetActive(true);
        }
    }
}