using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int Damage { get; set; }
    public int Armor { get; set; }
    public int SkillPoints { get; set; }
    public string Name { get; set; }

    public bool hasFire = false;
    public bool hasWater = false;
    public bool hasElectric = false;

    public int Hp;
    public int MaxHp;

    [SerializeField] PlayerUIDisplayer playerUIDisplayer = null;
    private Inventory inventory;
    DiceController diceController;

    public static event Action OnPlayerTurnPass;


    public Vector3 defaultPosition = new Vector3();

    private void Start()
    {
        CubeController.OnBattleStarts += ApplyInventory;
        diceController = FindObjectOfType<DiceController>();
        ButtonController.OnPlayerAttack += Attack;
        ButtonController.OnPlayerHeal += Heal;
        ButtonController.OnPlayerGainArmor += GainArmor;
        Monster.OnMonsterAttack += TakeDamage;
        Monster.OnMonsterDefeated += DefeatMonster;
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

    private void TakeDamage(int damagePercentage)
    {
        float floatMaxHp = MaxHp;
        float damage = floatMaxHp / 100 * damagePercentage;
        //if(Armor > 0)
        //{
        //    if(Armor >= damage)
        //    {
        //        Armor -= Convert.ToInt32(damage);
        //        return;
        //    }
        //    else
        //    {
        //        damage -= Armor;
        //        Armor = 0;
        //    }
        //}
        if(Armor > damage)
        {
            return;
        }
        else
        {
            damage -= Armor;
        }
        Hp -= Convert.ToInt32(damage);
        Hp = Mathf.Max(Hp, 0);
        if (IsDead())
        {
            MainSceneUIController.ClearGender();
            SceneManager.LoadScene(1);
        }

        playerUIDisplayer.UpdateHealth();
    }

    private bool IsDead()
    {
        return Hp > 0 ? false : true;
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
        int damage = Damage;
        
        diceController = FindObjectOfType<DiceController>();
        damage += diceController.GetRolledAttack();
        var monster = FindObjectOfType<Monster>();
        monster.TakeDamage(damage);
        OnPlayerTurnPass?.Invoke();
    }

    private void Heal()
    {
        diceController = FindObjectOfType<DiceController>();
        Hp += diceController.GetRolledHp();
        Hp = Mathf.Min(Hp, MaxHp);
        playerUIDisplayer.UpdateHealth();

        OnPlayerTurnPass?.Invoke();
    }

    private void GainArmor()
    {
        diceController = FindObjectOfType<DiceController>();
        Armor += diceController.GetRolledArmor();

        OnPlayerTurnPass?.Invoke();
    }
    private void ApplyInventory()
    {
        foreach (var item in inventory.Items)
        {
            Damage += item.Damage;
            Armor += item.Armor;
            MaxHp += item.Hp;
            Hp += item.Hp;
            
            DefineItemElement(item);
        }
        foreach (var potion in inventory.Potions)
        {
            Damage += potion.Damage;
            Armor += potion.Armor;
            MaxHp += potion.Hp;
            Hp += potion.Hp;
        }
        playerUIDisplayer.UpdateHealth();
    }

    private void DefeatMonster()
    {
        SkillPoints++;
        foreach (var item in inventory.Items)
        {
            Damage -= item.Damage;
            Armor -= item.Armor;
            MaxHp -= item.Hp;
            Hp -= item.Hp;

            DefineItemElement(item);
        }
        foreach (var potion in inventory.Potions)
        {
            Damage -= potion.Damage;
            Armor -= potion.Armor;
            MaxHp -= potion.Hp;
            Hp -= potion.Hp;
        }
        inventory.Potions.Clear();
        SceneManager.LoadScene(2);
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