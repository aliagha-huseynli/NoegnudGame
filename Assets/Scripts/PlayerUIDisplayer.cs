using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIDisplayer : MonoBehaviour
{
    public static string IS_IMAGE_ASSIGNED_STRING = "IsImageAssigned";
    public static string GENDER = "Gender";
    public static string NAME = "Name";

    [SerializeField] Player player = null;
    public SpriteRenderer _spriteRenderer;
    public Sprite MaleSprite, FemaleSprite;

    public Text HealthText = null;
    
    public Text PlayerName = null;

    public Slider HpSlider;
    public Slider ArmorSlider;
    public Gradient Gradient;
    public Image FillImage;

    private void Start()
    {
        SetSliderValues(player.MaxHp, player.Hp);
    }

    private void Update()
    {
        UpdateHealth();
    }

    public void SetSliderValues(int max, int value)
    {
        HpSlider.maxValue = max;
        HpSlider.value = value;
        if(player.inventory.Armor == null)
        {
            ArmorSlider.maxValue = 0;
            ArmorSlider.value = 0;
        }
        else
        {
            ArmorSlider.maxValue = player.inventory.Armor.MaxArmor;
            ArmorSlider.value = player.inventory.Armor.Armor;
        }
    }

    public void UpdateHealth()
    {
        if (player.MaxHp == 0) { return; }
        HpSlider.maxValue = player.MaxHp;
        HpSlider.value = player.Hp;
        FillImage.color = Gradient.Evaluate((float)player.Hp / (float)player.MaxHp);
        HealthText.text = $"{player.Hp}/{player.MaxHp}";
    }

    public void UpdateArmor()
    {
        if (player.inventory.Armor == null)
        {
            ArmorSlider.maxValue = 0;
            ArmorSlider.value = 0;
        }
        else
        {
            ArmorSlider.maxValue = player.inventory.Armor.MaxArmor;
            ArmorSlider.value = player.inventory.Armor.Armor;
        }
    }
}