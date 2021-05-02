using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int Damage { get; private set; }
    public int Armor { get; private set; }
    public int SkillPoints { get; private set; }
    public string Name { get; private set; }

    public int Hp;
    public int MaxHp;
  
    [SerializeField] PlayerUIDisplayer playerUIDisplayer = null;

    public Vector3 defaultPosition = new Vector3();

    private void Start()
    {
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
            if (SaveGame.CurrentHp <= 0) { SceneManager.LoadScene(2); }
        }
        //if (Input.GetKeyDown(KeyCode.F)) { SceneManager.LoadScene(2); }
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

}
