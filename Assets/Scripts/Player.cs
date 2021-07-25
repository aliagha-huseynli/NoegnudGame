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

    public bool HasFire = false;
    public bool HasWater = false;
    public bool HasElectric = false;

    public int Hp;
    public int MaxHp;

    [SerializeField] PlayerUIDisplayer _playerUiDisplayer = null;
    public Inventory Inventory;
    DiceController _diceController;

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
        _diceController = FindObjectOfType<DiceController>();
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
        if(Inventory.Armor != null)
        {
            if(Inventory.Armor.Armor > 0)
            {
                if(Inventory.Armor.Armor > afterArmorDamage)
                {
                    Inventory.Armor.Armor -= afterArmorDamage;
                    afterArmorDamage = 0;
                }
                else
                {
                    afterArmorDamage -= Inventory.Armor.Armor;
                    Inventory.Armor.Armor = 0;
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

        _playerUiDisplayer.UpdateHealth();
        _playerUiDisplayer.UpdateArmor();
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
        
        _diceController = FindObjectOfType<DiceController>();
        damage += _diceController.GetRolledAttack();
        var monster = FindObjectOfType<Monster>();
        monster.TakeDamage(damage);
        OnPlayerTurnPass?.Invoke();
    }

    private void Heal()
    {
        _diceController = FindObjectOfType<DiceController>();
        Hp += _diceController.GetRolledHp();
        Hp = Mathf.Min(Hp, MaxHp);
        _playerUiDisplayer.UpdateHealth();

        OnPlayerTurnPass?.Invoke();
    }

    private void GainArmor()
    {
        _diceController = FindObjectOfType<DiceController>();
        Armor += _diceController.GetRolledArmor();

        OnPlayerTurnPass?.Invoke();
    }
    private void ApplyInventory()
    {
        if(Inventory.Sword != null)
        {
            Damage += Inventory.Sword.Damage;
            DefineItemElement(Inventory.Sword);
        }
        if(Inventory.Shield != null)
        {
            MaxHp += Inventory.Shield.Hp;
            Hp += Inventory.Shield.Hp;
            DefineItemElement(Inventory.Shield);
        }
        if(Inventory.Potion != null)
        {
            Damage += Inventory.Potion.Damage;
            MaxHp += Inventory.Potion.Hp;
            Hp += Inventory.Potion.Hp;
        }
        if(Inventory.Armor != null)
        {
            DefineItemElement(Inventory.Armor);
        }
        
        _playerUiDisplayer.UpdateHealth();
        _playerUiDisplayer.UpdateArmor();
    }

    private void DefeatMonster()
    {
        SkillPoints++;

        if (Inventory.Sword != null)
        {
            Damage -= Inventory.Sword.Damage;
        }
        if (Inventory.Shield != null)
        {
            MaxHp -= Inventory.Shield.Hp;
            Hp -= Inventory.Shield.Hp;
            Hp = Hp <= 0 ? 1 : Hp;
        }
        if (Inventory.Potion != null)
        {
            Damage -= Inventory.Potion.Damage;
            MaxHp -= Inventory.Potion.Hp;
            Hp -= Inventory.Potion.Hp;
        }
        _playerUiDisplayer.UpdateHealth();
        _playerUiDisplayer.UpdateArmor();
        Inventory.Potion = null;
        SceneManager.LoadScene(2);
    }

    private void DefineItemElement(Item item)
    {
        switch (item.itemElement)
        {
            case Element.Fire:
                HasFire = true;
                break;
            case Element.Water:
                HasWater = true;
                break;
            case Element.Electric:
                HasElectric = true;
                break;
            case Element.None:
                break;
        }
    }
}