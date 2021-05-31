using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int Damage { get; set; }
    public int Armor { get; set; }
    public int SkillPoints { get; set; }
    public string Name { get; set; }
    public int MaxArmor { get; set; }

    public bool hasFire = false;
    public bool hasWater = false;
    public bool hasElectric = false;

    public int Hp;
    public int MaxHp;

    [SerializeField] PlayerUIDisplayer playerUIDisplayer = null;
    public Inventory inventory;
    DiceController diceController;

    public static event Action OnPlayerTurnPass;


    public Vector3 defaultPosition = new Vector3();

    private void Awake()
    {
        var players = FindObjectsOfType<Player>();
        if (players.Length > 1)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        diceController = FindObjectOfType<DiceController>();
        CubeController.OnBattleStarts += ApplyInventory;
        ButtonController.OnPlayerAttack += Attack;
        ButtonController.OnPlayerHeal += Heal;
        ButtonController.OnPlayerGainArmor += GainArmor;
        Monster.OnMonsterAttack += TakeDamage;
        Monster.OnMonsterDefeated += DefeatMonster;
        defaultPosition = transform.position;
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1;
        SetStartStats();
    }

    private void TakeDamage(int damagePercentage)
    {
        float floatMaxHp = MaxHp;
        int beforeArmorDamage = Convert.ToInt32(floatMaxHp / 100 * damagePercentage);
        int afterArmorDamage = beforeArmorDamage - Armor;
        print(afterArmorDamage);
        if(inventory.Armor != null)
        {
            if(inventory.Armor.Armor > 0)
            {
                if(inventory.Armor.Armor > afterArmorDamage)
                {
                    inventory.Armor.Armor -= afterArmorDamage;
                    afterArmorDamage = 0;
                }
                else
                {
                    afterArmorDamage -= inventory.Armor.Armor;
                    inventory.Armor.Armor = 0;
                }
            }   
        }
        Hp -= afterArmorDamage;
        Hp = Mathf.Max(Hp, 0);
        if (IsDead())
        {
            MainSceneUIController.ClearGender();
            SceneManager.LoadScene(1);
            //ShowCube();
        }

        playerUIDisplayer.UpdateHealth();
        playerUIDisplayer.UpdateArmor();
    }

    private void ShowCube()
    {
        GameObject Cubes =GameObject.FindGameObjectWithTag("Cubes");
        Cubes.SetActive(true);
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
        if(inventory.Sword != null)
        {
            Damage += inventory.Sword.Damage;
            DefineItemElement(inventory.Sword);
        }
        if(inventory.Shield != null)
        {
            MaxHp += inventory.Shield.Hp;
            Hp += inventory.Shield.Hp;
            DefineItemElement(inventory.Shield);
        }
        if(inventory.Potion != null)
        {
            Damage += inventory.Potion.Damage;
            MaxHp += inventory.Potion.Hp;
            Hp += inventory.Potion.Hp;
        }
        if(inventory.Armor != null)
        {
            DefineItemElement(inventory.Armor);
        }
        
        playerUIDisplayer.UpdateHealth();
        playerUIDisplayer.UpdateArmor();
    }

    private void DefeatMonster()
    {
        SkillPoints++;

        if (inventory.Sword != null)
        {
            Damage -= inventory.Sword.Damage;
        }
        if (inventory.Shield != null)
        {
            MaxHp -= inventory.Shield.Hp;
            Hp -= inventory.Shield.Hp;
            Hp = Hp <= 0 ? 1 : Hp;
        }
        if (inventory.Potion != null)
        {
            Damage -= inventory.Potion.Damage;
            MaxHp -= inventory.Potion.Hp;
            Hp -= inventory.Potion.Hp;
        }
        playerUIDisplayer.UpdateHealth();
        playerUIDisplayer.UpdateArmor();
        inventory.Potion = null;
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