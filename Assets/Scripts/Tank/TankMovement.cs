using UnityEngine;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public int m_PayerNumber;
        [SerializeField] float m_Speed = 12f;
        [SerializeField] float m_TurnSpeed = 180f;

        private string m_MovementAxisName;
        private string m_TurnAxisName;
        private Rigidbody m_Rigidbody;
        private float m_MovementInputValue;
        private float m_TurnInputValue;

        private void Awake() =>
            m_Rigidbody = GetComponent<Rigidbody>();

        private void OnEnable()
        {
            m_Rigidbody.isKinematic = false;

            m_MovementInputValue = 0f;
            m_TurnInputValue = 0f;
        }

        private void OnDisable() =>
            m_Rigidbody.isKinematic = true;

        private void Start()
        {
            m_MovementAxisName = "Vertical" + m_PayerNumber;
            m_TurnAxisName = "Horizontal" + m_PayerNumber;
        }

        private void Update()
        {
            m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
            m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        }

        private void FixedUpdate()
        {
            Move();
            Turn();
        }

        private void Move()
        {
            var movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

        private void Turn()
        {
            var turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

            var turnRotation = Quaternion.Euler(0f, turn, 0f);

            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        }
    }
}