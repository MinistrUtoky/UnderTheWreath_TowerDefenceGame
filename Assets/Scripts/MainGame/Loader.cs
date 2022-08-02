using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject shootingController;
    [SerializeField] private GameObject levelManager;

    private ArcherController _archer;
    

    private void Start()
    {
        _archer = GameObject.Find("Archer").GetComponent<ArcherController>();
        shootingController.GetComponent<ShootingController>().SetArcher(_archer);
        levelManager.GetComponent<LevelManager>().SetArcher(_archer);
    }
}
