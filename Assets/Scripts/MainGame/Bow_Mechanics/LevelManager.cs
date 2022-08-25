using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private Vector2 defaultRotationDirection;
    [SerializeField] private Transform archerSpawningPoint;
    
    private ArcherController _archer;

    public ArcherController Archer
    {
        get
        {
            if (_archer == null)
            {
                _archer = Instantiate(archerPrefab, archerSpawningPoint.position, Quaternion.identity).GetComponent<ArcherController>();   
                _archer.transform.parent = GameObject.Find("Townhall").transform;
            }

            return _archer;
        }
    }

    public Vector2 DefaultRotationDirection => defaultRotationDirection;
}
