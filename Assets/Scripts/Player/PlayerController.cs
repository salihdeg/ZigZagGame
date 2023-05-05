using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        private GroundSpawner _groundSpawner;

        private Vector3 _dir;
        private bool _dirChanged = false;
        public static bool isDead = false;

        private readonly string GROUND_TAG = "Ground";

        private void Awake()
        {
            _groundSpawner = GetComponentInChildren<GroundSpawner>();
            _dir = Vector3.forward;
        }

        private void Update()
        {
            if (isDead) return;

            ChangeMoveDirection();

            CheckFalling();
        }

        private void CheckFalling()
        {
            if (transform.position.y < 0.2f)
            {
                isDead = true;
                Destroy(gameObject, 2f);
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(GROUND_TAG))
            {
                _groundSpawner.CreateGround();
                StartCoroutine(DestroyGround(collision.gameObject));
            }
        }

        private void ChangeMoveDirection() // if mouse touched, change direction
        {
            if (Input.GetMouseButtonDown(0)) //if mouse button 0 is down or screen touched
            {
                _dirChanged = !_dirChanged; // change to opposite of current state

                if (!_dirChanged)
                {
                    _dir = Vector3.forward; // move on Z
                }
                else
                {
                    _dir = Vector3.left; // move on -X
                }
            }
        }

        private void Move() // Change player position
        {
            //transform.Translate(_speed * Time.deltaTime * _dir); // move
            Vector3 move = _speed * Time.deltaTime * _dir;
            transform.position += move;
        }

        private IEnumerator DestroyGround(GameObject ground)
        {
            yield return new WaitForSeconds(.3f);
            ground.AddComponent<Rigidbody>();

            yield return new WaitForSeconds(1.5f);
            Destroy(ground);
        }

    }
}

