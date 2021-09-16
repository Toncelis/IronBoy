using UnityEngine;

namespace Game.Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class SimpleMovement : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float jumpForce;

        private Rigidbody2D myRigidBody;
        [SerializeField] private float playerHeight = 2.3f;

        private void Start()
        {
            myRigidBody = GetComponent<Rigidbody2D>();
            myRigidBody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, playerHeight/2+0.1f);
        }

        void Update()
        {
            myRigidBody.velocity = new Vector2(
                Input.GetAxis("Horizontal") * speed,
                myRigidBody.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded())
                    myRigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            }
        }
    }

}