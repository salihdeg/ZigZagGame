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
        [SerializeField] private float _speedDifficultiy = 0.001f;
        [SerializeField] private float _score = 0;
        [SerializeField] private float _incermentValue = 1f;

        [Header("Outer Components")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private TextMeshProUGUI _StartBestScoreText;
        [SerializeField] private GameObject _restartPanel;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private Animator _newHighScoreAnimator;

        public int _bestScore = 0;

        private GroundSpawner _groundSpawner;
        private int _lastScoreCheck = 0;
        private Vector3 _dir;
        private bool _dirChanged = false;
        private bool _isNewHighScore = false;
        public static bool isDead = true;

        private readonly string GROUND_TAG = "Ground";

        private void Awake()
        {
            _groundSpawner = GetComponentInChildren<GroundSpawner>();
            _dir = Vector3.forward;
        }

        private void Start()
        {
            if (RestartGame.isRestart)
            {
                _startPanel.SetActive(false);
                isDead = false;
            }

            _bestScore = PlayerPrefs.GetInt("BestScore", 0);
            _bestScoreText.text = "Best: " + _bestScore;
            _StartBestScoreText.text = "Best Score: " + _bestScore;
        }

        private void Update()
        {
            if (isDead) return;

            ChangeMoveDirection();

            CheckFalling();

            CheckNewHighScore();

            IfScoreMod30();
        }

        private void FixedUpdate()
        {
            if (isDead) return;

            Move();
            //AddScore();
            SetScoreText();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isDead) return;

            if (other.CompareTag("Gold"))
            {
                Destroy(other.gameObject);
                AddScore();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(GROUND_TAG))
            {
                _groundSpawner.CreateGround();
                StartCoroutine(DestroyGround(collision.gameObject));
            }
        }

        //Public Functions

        public void StartGame()
        {
            _startPanel.SetActive(false);
            isDead = false;
        }


        // Private Functions
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
                _restartPanel.SetActive(true);

                Destroy(gameObject, 1f);
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

        private IEnumerator DestroyGround(GameObject ground) //Fall and destroy ground in time
        {
            yield return new WaitForSeconds(.3f);
            ground.AddComponent<Rigidbody>();

            yield return new WaitForSeconds(1.5f);
            Destroy(ground);
        }

        private void SetScoreText()
        {
            _scoreText.text = "Score: " + ((int)_score).ToString();
        }

        private void AddScore()
        {
            //_score += _incermentValue * Time.deltaTime;
            _score += 1;
        }

        private void IfScoreMod30()
        {
            if ((int)_score / 30 > _lastScoreCheck && (int)_score != 0 && _bestScore != 0) // 30'a bölümünden kalan 0'sa
            {
                _speed += 0.15f;
                _incermentValue += 0.2f;
                _lastScoreCheck++;
            }
        }

        private void CheckNewHighScore()
        {
            if (!_isNewHighScore)
            {
                if (_score > _bestScore)
                {
                    _newHighScoreAnimator.SetTrigger("NewHighScore");
                    _isNewHighScore = true;
                }
            }
        }

    }
}

