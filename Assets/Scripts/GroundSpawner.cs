using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _sonZemin;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateGround();
        }
    }

    private void CreateGround()
    {
        _sonZemin = Instantiate(_sonZemin, _sonZemin.transform.position + Vector3.forward, _sonZemin.transform.rotation);
    }
}
