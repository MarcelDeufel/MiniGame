using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnScript: MonoBehaviour
{
    public Vector3 spawnValues;
    private float spawnTime;
    private GameObject Cubetransform;

    private Animator greatAnimator;
    private Animator failAnimator;

    private Transform BoxToHit;

    private TextMeshProUGUI scoreText;

    private bool notFailedYet;
    private bool timeNotOver;

    public int score;
    private HitBoxController _HitBoxController;                  

    void Start()
    {
        _HitBoxController = GameObject.Find("BoxToHit").GetComponent<HitBoxController>();
        greatAnimator = GameObject.Find("GreatImage").GetComponent<Animator>();
        failAnimator = GameObject.Find("FailImage").GetComponent<Animator>();
        score = 0;
        timeNotOver = true;
        notFailedYet = true;
        spawnTime = Time.time;
        Cubetransform = GameObject.Find("Cube");
        BoxToHit = GameObject.Find("BoxToHit").transform;                            
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timeNotOver = _HitBoxController.spawnBool;
        scoreText.text = score.ToString();

            float now = Time.time;

            if (now - spawnTime > 0.75f && notFailedYet && timeNotOver)
            {
                var candy = createCandyController();
                if(BoxToHit != null)
                {
                    candy.StartMe(hitHandler, BoxToHit);
                }
                spawnTime = now;
            }
    }
    private CandyController createCandyController()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-4, -1), 0) + transform.TransformPoint(0, 0, 0);
        GameObject instance = Instantiate(Cubetransform, spawnPosition, gameObject.transform.rotation);
        CandyController ret;

        if (Random.value < 0.9)
        {
            instance.AddComponent<GoodCandyController>();
            ret = instance.GetComponent<GoodCandyController>();
        }
        else 
        {
            instance.AddComponent<BadCandyController>();
            ret = instance.GetComponent<BadCandyController>();
        }

        return ret;
    }
    private void hitHandler(int delta)
    {

        if (delta < 1)
        {
            FindObjectOfType<AudioManager>().Play("KakerlakenHit");
            failAnimator.SetTrigger("StartFailAnimation");
            notFailedYet = false;
            StartCoroutine(onFailingDelay());
        }
        else
        {
            score += delta;
            greatAnimator.SetTrigger("StartGreatAnimation");
            FindObjectOfType<AudioManager>().Play("CandyHit");
            StartCoroutine(waitForAnimationToEnd());
        }
        if (score < 0)
        {
            score = 0;
        }
    }

    private IEnumerator waitForAnimationToEnd()
    {
        yield return new WaitForSeconds(0.5f);
        greatAnimator.SetTrigger("StopGreatAnimation");
    }
    private IEnumerator onFailingDelay()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("PlayerScore", 0);
        SceneManager.LoadScene("ScoreScene");
    }
}