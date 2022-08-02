using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    private ArcherController _archer;
    [SerializeField] private Vector2 defaultRotationDirection;

    public void SetArcher(ArcherController archer)
    {
        _archer = archer;
        archer.SetDefaultPosition(defaultRotationDirection.normalized);
    }
    
    
}
