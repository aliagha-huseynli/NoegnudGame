using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    public GameObject CubeGameObject;
    public GameObject Player;
    private bool _shouldMove = false;
    private GameObject _target;
    private Vector3 _targetPosVector3;
    private int _randomBoxNumber;
    private bool _oneTimeClick = true;
    private bool _isActive = true;

    
    public IEnumerator SleepTime()
    {
        yield return new WaitForSeconds(3f);

        switch (_randomBoxNumber)
        {
            case 0:
                SceneManager.LoadScene("FightScene", LoadSceneMode.Single);
                print("It was at this moment he knew, he fucked up!");
                break;
            case 1:
                print("You got Random Potion bitch!");
                break;
            //random potion damage, armor, heal PS heal could 
            case 2:
                print("You got Item bitch!");
                break;
            case 3:
                print("Story time");
                break;
            case 4:
                print("Camp time for healing Player");
                break;
        }
        _oneTimeClick = true;

    }

    private void Start()
    {
        Instantiate(CubeGameObject, new Vector3(-1.5f, 2, 0), Quaternion.identity);
        Instantiate(CubeGameObject, new Vector3(0, 2, 0), Quaternion.identity);
        Instantiate(CubeGameObject, new Vector3(1.5f, 2, 0), Quaternion.identity);

        Instantiate(CubeGameObject, new Vector3(-1.5f, 0.5f, 0), Quaternion.identity);
        Instantiate(CubeGameObject, new Vector3(0, 0.5f, 0), Quaternion.identity);
        Instantiate(CubeGameObject, new Vector3(1.5f, 0.5f, 0), Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isActive == false)
            {
                _target.SetActive(true);

                if (_oneTimeClick)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 mousePos2D = new Vector3(mousePos.x, mousePos.y);
                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector3.zero);
                    _oneTimeClick = false;


                    if (hit && hit.transform.gameObject.tag == "Cubes")
                    {
                        _shouldMove = true;
                        _target = hit.transform.gameObject;
                        _targetPosVector3 = hit.transform.position;

                        _randomBoxNumber = Random.Range(0, 6); //Random Box Number Generator
                        print("Random Box Number is: " + _randomBoxNumber);
                        StartCoroutine(SleepTime()); //Wait for 3 second for Action after Warrior move

                    }
                }
            }
            else
            {
                if (_oneTimeClick)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 mousePos2D = new Vector3(mousePos.x, mousePos.y);
                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector3.zero);
                    _oneTimeClick = false;


                    if (hit && hit.transform.gameObject.tag == "Cubes")
                    {
                        _shouldMove = true;
                        _target = hit.transform.gameObject;
                        _targetPosVector3 = hit.transform.position;

                        _randomBoxNumber = Random.Range(0, 3); //Random Box Number Generator
                        print("Random Box Number is: " + _randomBoxNumber);
                        StartCoroutine(SleepTime()); //Wait for 3 second for Action after Warrior move

                    }
                }
            }
        }

        if (!_shouldMove) { return; }

        Vector3 startPosition = Player.transform.position;
        Vector3 endPosition = _targetPosVector3;
        Vector3 currentPos = Vector3.Lerp(Player.transform.position, endPosition, 2 * Time.deltaTime);
        Player.transform.position = currentPos;

        if (Vector3.Distance(Player.transform.position, _targetPosVector3) < 0.5)
        {
            _target.SetActive(false);
            _isActive = false;
        }

        if (Player.transform.position == _targetPosVector3)
        {
            _shouldMove = false;
        }
    }
}