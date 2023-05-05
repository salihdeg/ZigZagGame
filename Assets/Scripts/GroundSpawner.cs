using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _sonZemin;

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            CreateGround();
        }
    }

    public void CreateGround()
    {
        Vector3 dir;

        if (Random.Range(0,2) == 0) // 0 gelirse -x ekseninde zemin oluştur
        {
            dir = Vector3.left;
        }
        else // 1 gelirse z eksinden zemin oluştur
        {
            dir = Vector3.forward;
        }

        _sonZemin = Instantiate(_sonZemin, _sonZemin.transform.position + dir, _sonZemin.transform.rotation);
    }
}
