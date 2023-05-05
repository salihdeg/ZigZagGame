using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _speedDifficultiy = 0.01f;
        [SerializeField] private float _score = 0;
        [SerializeField] private float _incermentValue = 1f;

        [Header("Outer Components")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private GameObject _restartPanel;
        [SerializeField] private GameObject _startPanel;

        public int _bestScore = 0;

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

        private void Start()
        {
            _bestScore = PlayerPrefs.GetInt("BestScore", 0);
            _bestScoreText.text = "Best: " + _bestScore;
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
                if (_bestScore < _score)
                {
                    _bestScore = (int)_score;
                    PlayerPrefs.SetInt("BestScore", _bestScore);
                }
                Destroy(gameObject, 1f);
            }
        }

        private void FixedUpdate()
        {
            if (isDead) return;

            Move();
            AddScore();
            SetScore();
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
            _speed += Time.deltaTime * _speed * _speedDifficultiy;
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

        private void SetScore()
        {
            _scoreText.text = "Score: " + ((int)_score).ToString();
        }

        private void AddScore()
        {
            _score += _incermentValue * Time.deltaTime;
        }

    }
}

