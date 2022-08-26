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
    private bool _builderIsGoingToHeal = false;
    private Builder_Script _builderController;

    void Awake()
    {
        _level = 1;
        _hp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (!_builderIsGoingToHeal)
        {
            ActivateBuilder();
            _builderIsGoingToHeal = true;
        }
        if (!builderIsSpawned)
        {
            builderIsSpawned = true;
        }
        if (_hp <= 0)
        {
            _hp = 0;
            if (gameObject.name == "Townhall")
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            builder.GetComponent<Builder_Script>().Retreat();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void ActivateBuilder()
    {
        if (builderIsSpawned)
        {
            if (!builder.activeSelf)
            {
                builder.transform.position = new Vector3(8.65f, -3.7f, 0);
                builder.SetActive(true);
                _builderController.Go();
            }
            else if (_builderController.IsMoving()) _builderController.Go();
        }
        else
        {
            builder = Instantiate(builder, new Vector3(8.7f, -3.7f, 0), Quaternion.identity);
            _builderController = builder.GetComponent<Builder_Script>();
        }
    }

    public void Heal(int heal)
    {
        _hp += heal;
        if (_hp >= _maxHp)
        {
            _builderIsGoingToHeal = false;
            _hp = _maxHp;
            builder.GetComponent<Builder_Script>().Retreat();
        }
    }

    public void LevelUp()
    {
        _level += 1;    
    }

    public bool IsDamaged()
    {
        return _hp != _maxHp;
    }

    public bool IsRuined()
    {
        return _hp == 0;
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
            _builderController.Stop();
            StartCoroutine(RecieveHealing(_builderController.GetHealing()));
        }
    }
}
