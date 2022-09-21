using UnityEngine;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PayerNumber;
        [SerializeField] GameObject m_Bullet;
        [SerializeField] Transform m_FireTransform;

        private string m_FireButton;

        private void Start () =>        
            m_FireButton = "Fire" + m_PayerNumber;

        private void Update ()
        {
            if (Input.GetButtonDown (m_FireButton))
             Fire ();
        }

        private void Fire()
        {
            Instantiate(m_Bullet, m_FireTransform.position, m_FireTransform.rotation);
        }
        
    }
}