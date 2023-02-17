using UnityEngine;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PayerNumber;
        [SerializeField] BulletLogic m_Bullet;
        [SerializeField] Transform m_FireTransform;
        [SerializeField] float m_ShootCD;

        private string m_FireButton;
        private float m_LastShootTime = 0;

        private BulletPool<BulletLogic> m_BulletPool;

        private void Start()
        {
            m_FireButton = "Fire" + m_PayerNumber;
            m_BulletPool = new BulletPool<BulletLogic>(m_Bullet, 3, true);
        }

        private void Update ()
        {
            float passedTime = Time.time - m_LastShootTime;
            if (Input.GetButtonDown(m_FireButton) && (passedTime > m_ShootCD))
            {
                Fire();
                m_LastShootTime = Time.time;
            }
        }

        private void Fire()
        {
            BulletLogic bulllet = m_BulletPool.GetFreeElement();
            bulllet.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation);
            bulllet.gameObject.SetActive(true);
        }
        
    }
}