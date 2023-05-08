using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _sonZemin;
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private GameObject _gold;

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

        if (Random.Range(0, 2) == 0) // 0 gelirse -x ekseninde zemin oluştur
        {
            dir = Vector3.left;
        }
        else // 1 gelirse z eksinden zemin oluştur
        {
            dir = Vector3.forward;
        }
        
        GameObject newObject = Instantiate(_sonZemin, _sonZemin.transform.position + dir, _sonZemin.transform.rotation, _spawnParent);

        DestroyAllChilds(newObject.transform);
        CreateGoldRandom(newObject.transform);
        newObject.name = "Ground";

        _sonZemin = newObject;
    }

    public void DestroyAllChilds(Transform comingTransform)
    {
        if (comingTransform.childCount > 0)
            foreach (Transform item in comingTransform)
                Destroy(item.gameObject);
    }

    public void CreateGoldRandom(Transform newGround)
    {
        int randomNumber = Random.Range(0, 2);

        if (randomNumber == 1)
        {
            Instantiate(_gold, newGround);
        }
    }
}
