using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private playerController _playerController;

    [Header("Config. Character")]

    public float velocityMovement;
    public float limitMaxY;
    public float limitMinY;
    public float limitMaxX;
    public float limitMinX;

    [Header("Config. Objects")]

    public float velocityObject;
    public float distanceDestroy;
    public float sizeBridge;
    public GameObject bridgePrefab;

    [Header("Config. Barrel")]

    public float posYTop;
    public float posYBottom;
    public int orderTop;
    public int orderBottom;
    public GameObject barrelPrefab;
    public float timeSpawn;

    [Header("Globals")]

    public int score;
    public Text txtScore;
    public float posXPlayer;

    [Header("FX Sound")]

    public AudioSource fxSource;
    public AudioClip fxPoints;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType(typeof(playerController)) as playerController;
        StartCoroutine("spawnBarrel");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        posXPlayer = _playerController.transform.position.x;
    }

    IEnumerator spawnBarrel()
    {
        yield return new WaitForSeconds(timeSpawn);
        float posY = 0;
        int order = 0;
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            posY = posYTop;
            order = orderTop;
        }
        else
        {
            posY = posYBottom;
            order = orderBottom;
        }

        GameObject temp = Instantiate(barrelPrefab);
        temp.transform.position = new Vector2(temp.transform.position.x, posY);
        temp.GetComponent<SpriteRenderer>().sortingOrder = order;
        StartCoroutine("spawnBarrel");
    }

    public void toScore(int qttPoints)
    {
        score += qttPoints;
        txtScore.text = "Score: " + score.ToString();
        fxSource.PlayOneShot(fxPoints);
    }

    public void changeScene(string sceneDestiny)
    {
        SceneManager.LoadScene(sceneDestiny);
    }
}
