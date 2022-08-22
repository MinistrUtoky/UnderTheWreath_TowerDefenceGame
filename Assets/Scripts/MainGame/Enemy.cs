using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    public float Speed => defaultSpeed;

    private void Update()
    {
        Walk();
    }

    public void Walk()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }
    
    
}
