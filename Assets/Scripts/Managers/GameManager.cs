using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int m_NumRoundsToWin = 5;
        [SerializeField] float m_StartDelay = 3f;
        [SerializeField] float m_EndDelay = 3f;
        [SerializeField] Text m_MessageText;
        [SerializeField] GameObject m_TankPrefab;
        [SerializeField] TankManager[] m_Tanks;

        private int m_RoundNumber;
        private WaitForSeconds m_StartWait;
        private WaitForSeconds m_EndWait;
        private TankManager m_RoundWinner;
        private TankManager m_GameWinner;


        private void Start()
        {
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            SpawnAllTanks();

            StartCoroutine(GameLoop());
        }

        private void SpawnAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].m_Instance =
                    Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
                m_Tanks[i].m_PlayerNumber = i + 1;
                m_Tanks[i].Setup();
            }
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());

            yield return StartCoroutine(RoundPlaying());

            yield return StartCoroutine(RoundEnding());

            if (m_GameWinner != null)
                EndGame();
            else
                StartCoroutine(GameLoop());
        }

        private IEnumerator RoundStarting()
        {
            ResetAllTanks();
            DisableTankControl();

            m_RoundNumber++;
            m_MessageText.text = "ROUND " + m_RoundNumber;

            yield return m_StartWait;
        }

        private IEnumerator RoundPlaying()
        {
            EnableTankControl();

            m_MessageText.text = string.Empty;

            while (!OneTankLeft())
            {
                yield return null;
            }
        }

        private IEnumerator RoundEnding()
        {
            DisableTankControl();

            m_RoundWinner = null;

            m_RoundWinner = GetRoundWinner();

            if (m_RoundWinner != null)
                m_RoundWinner.m_Wins++;

            m_GameWinner = GetGameWinner();

            var message = EndMessage();
            m_MessageText.text = message;

            yield return m_EndWait;
        }

        private bool OneTankLeft()
        {
            int numTanksLeft = 0;

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance.activeSelf)
                    numTanksLeft++;
            }

            return numTanksLeft <= 1;
        }

        private TankManager GetRoundWinner()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance.activeSelf)
                    return m_Tanks[i];
            }

            return null;
        }

        private TankManager GetGameWinner()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                    return m_Tanks[i];
            }

            return null;
        }

        private string EndMessage()
        {
            var message = "DRAW!";

            if (m_RoundWinner != null)
                message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

            message += "\n\n\n\n";

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
            }

            if (m_GameWinner != null)
                message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

            return message;
        }

        private void ResetAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].Reset();
            }
        }

        private void EnableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].EnableControl();
            }
        }

        private void DisableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].DisableControl();
            }
        }

        private void EndGame()
        {
            DisableTankControl();

            var message = EndMessage();
            m_MessageText.text = message;
        }
    }
}