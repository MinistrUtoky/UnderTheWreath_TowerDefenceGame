using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Enemy_Script : MonoBehaviour
{
    [SerializeField] protected float speed = 3.0f;
    [SerializeField] protected int hp = 10;
    [SerializeField] protected int damage = 5;
    protected Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement() {
        if (rb.velocity == Vector2.zero) return;
        if (rb.velocity.x < 0) return;
        rb.velocity = new Vector3(speed, 0, 0);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0) Destroy(gameObject);
    }

    protected IEnumerator HittingBuiling(GameObject obj)
    {
        for (; obj.activeSelf; )
        {
            DamageDealing(obj);
            yield return new WaitForSeconds(2);
        }
    }

    protected void DamageDealing(GameObject obj)
    {
        if (obj.name == "Barracks") obj.GetComponent<Barracks_Script>().TakeDamage(damage);
        else if (obj.name == "Townhall") obj.GetComponent<Townhall_Script>().TakeDamage(damage);
        else if (obj.name == "Wall") obj.GetComponent<Wall_Script>().TakeDamage(damage);
    }
}
