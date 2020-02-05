using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitBoxController : MonoBehaviour
{
    private GameObject BoxToHit;
    private Transform BoxToHitTransform;
    private SpawnScript _SpawnScript;
    private float moveSpeed = 0.5f;

    private Animator youDidIt;
    private Animator youFailedIt;

    public bool spawnBool;
    // Start is called before the first frame update
    void Start()
    {
        BoxToHit = GameObject.Find("BoxToHit");
        BoxToHitTransform = GameObject.Find("BoxToHit").GetComponent<Transform>();
        
        youFailedIt = GameObject.Find("YouFailedIt").GetComponent<Animator>();
        youDidIt = GameObject.Find("YouDidIt").GetComponent<Animator>();
        _SpawnScript = GameObject.Find("Spawner").GetComponent<SpawnScript>();
        spawnBool = true;

        if(PlayerPrefs.GetInt("BonusMultiplicator") == 1)
        {
            moveSpeed = 0.75f;
        }
        else if (PlayerPrefs.GetInt("BonusMultiplicator") == 2)
        {
            BoxToHitTransform.localScale -= new Vector3(0.5f, 0, 0);
            moveSpeed = 0.75f;
        }
        else if (PlayerPrefs.GetInt("BonusMultiplicator") == 3)
        {
            BoxToHitTransform.localScale -= new Vector3(0.5f, 0, 0);
            moveSpeed = 0.75f;
        }
        else if (PlayerPrefs.GetInt("BonusMultiplicator") == 4)
        {
            BoxToHitTransform.localScale -= new Vector3(0.5f, 0, 0);
            moveSpeed = 1f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        BoxToHitTransform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "LeftWall")
        {
            var score = _SpawnScript.score;
            spawnBool = false;
            if(score >= 5)
            {
                youDidIt.SetTrigger("YouDidItTrigger");
                FindObjectOfType<AudioManager>().Play("YouWonSound");
            }
            else
            {
                youFailedIt.SetTrigger("YouFailedItTrigger");
                FindObjectOfType<AudioManager>().Play("KakerlakenHit");
            }
            PlayerPrefs.SetInt("PlayerScore", score);
            StartCoroutine(gameEndedDelay());
        }
    }
    IEnumerator gameEndedDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ScoreScene");
    }

}
