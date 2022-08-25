using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Building_Script : MonoBehaviour
{
    [SerializeField] protected int _maxHp = 50;
    [SerializeField] private GameObject builder;
    [SerializeField] private int _hp;
    [SerializeField] private int _level;
    protected bool builderIsSpawned = false;
    private Builder_Script builderController;

    void Awake()
    {
        _level = 1;
        _hp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (!builderIsSpawned)
        {
            SpawnBuilder();
            builderIsSpawned = true;
        }
        if (_hp <= 0)
        {
            if (gameObject.name == "Townhall")
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            builder.GetComponent<Builder_Script>().Retreat();
            gameObject.SetActive(false);
        }
    }

    protected void SpawnBuilder()
    {
        builder = Instantiate(builder, new Vector3(8.7f, -3.7f, 0), Quaternion.identity);
        builderController = builder.GetComponent<Builder_Script>();
    }

    private void Heal(int heal)
    {
        if (_hp == _maxHp) return;
        _hp += heal;
        if (_hp >= _maxHp)
        {
            _hp = _maxHp;
            builder.GetComponent<Builder_Script>().Retreat();
        }
    }

    public void LevelUp()
    {
        _level += 1;    
    }

    private IEnumerator RecieveHealing(int heal)
    {
        for (; _hp > 0 && _hp < _maxHp;)
        {
            Heal(heal);
            yield return new WaitForSeconds(2);
        }
    } 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == builder)
        {
            builderController.Stop();
            StartCoroutine(RecieveHealing(builderController.GetHealing()));
        }
    }
}
