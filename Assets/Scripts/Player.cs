using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int Damage { get; private set; }
    public int Armor { get; private set; }
    public int SkillPoints { get; private set; }
    public string Name { get; private set; }

    public bool hasFire = false;
    public bool hasWater = false;
    public bool hasElectric = false;

    public int Hp;
    public int MaxHp;
  
    [SerializeField] PlayerUIDisplayer playerUIDisplayer = null;
    private Inventory inventory;


    public Vector3 defaultPosition = new Vector3();

    private void Start()
    {
        
        ButtonController.OnPlayerAttack += Attack;
        inventory = GetComponent<Inventory>();
        defaultPosition = transform.position;
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1;
        SetStartStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            if (Hp <= 0) { SceneManager.LoadScene(2); }
        }
        if (Input.GetKeyDown(KeyCode.P)) { SceneManager.LoadScene(2); }
    }

    private void TakeDamage(int damage)
    {
        Hp -= damage;
        playerUIDisplayer.UpdateHealth();
    }

    public void SetStartStats()
    {
        Hp = 50;
        MaxHp = 50;
        Damage = 3;
        Armor = 3;
        SkillPoints = 5;
    }

    private void Attack()
    {
        foreach(var item in inventory.Items)
        {
            Damage += item.Damage;
            DefineItemElement(item);
        }
        foreach(var potion in inventory.Potions)
        {
            Damage += potion.Damage;
        }
        var diceController = FindObjectOfType<DiceController>();
        Damage += diceController.GetRolledAttack();
        var monster = FindObjectOfType<Monster>();
        monster.TakeDamage(Damage);

    }

    private void DefineItemElement(Item item)
    {
        switch (item.itemElement)
        {
            case Element.Fire:
                hasFire = true;
                break;
            case Element.Water:
                hasWater = true;
                break;
            case Element.Electric:
                hasElectric = true;
                break;
            case Element.None:
                break;
        }
    }
}