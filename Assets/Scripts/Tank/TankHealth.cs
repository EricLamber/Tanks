using UnityEngine;

namespace Complete
{
    public class TankHealth : MonoBehaviour
    {
        [SerializeField] float m_StartingHealth = 1f;

        private float m_CurrentHealth;
        private bool m_Dead;

        private void OnEnable()
        {
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;
        }

        public void TakeDamage(float amount)
        {
            m_CurrentHealth -= amount;

            if (m_CurrentHealth <= 0f && !m_Dead)
                OnDeath();
        }

        private void OnDeath()
        {
            m_Dead = true;

            gameObject.SetActive(false);
        }
    }
}