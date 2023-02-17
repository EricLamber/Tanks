using UnityEngine;

namespace Complete
{
    public class BulletLogic : MonoBehaviour
    {
        [SerializeField] LayerMask m_TankMask;
        [SerializeField] float m_Damage = 1f;
        [SerializeField] float m_HitboxRadius = 0.2f;
        [SerializeField] float m_BulletSpeed = 2f;

        private string m_WallTag = "Wall";

        bool isActive = false;

        private void OnEnable() => isActive = true;
        private void OnDisable() => isActive = false;
        
        private void FixedUpdate()
        {
            if(isActive)
                transform.Translate(m_BulletSpeed * Time.deltaTime * Vector3.forward);
        }


        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.tag != m_WallTag)
                BulletEnd();
            else
                BulletReflect(coll);
        }

        private void BulletReflect(Collision coll)
        {
            var reflectdir = Vector3.Reflect(transform.forward, coll.contacts[0].normal);
            var rot = 90-Mathf.Atan2(reflectdir.z, reflectdir.x) * Mathf.Rad2Deg ;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }

        private void BulletEnd()
        {
            var colliders = Physics.OverlapSphere(transform.position, m_HitboxRadius, m_TankMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                var targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                if (!targetRigidbody)
                    continue;

                var targetHealth = targetRigidbody.GetComponent<TankHealth>();

                if (!targetHealth)
                    continue;

                var damage = m_Damage;

                targetHealth.TakeDamage(damage);
            }

            gameObject.SetActive(false);
        }
    }
}