using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public delegate void HitEventReiceiverDelegate(int delta);

public abstract class CandyController : MonoBehaviour
{
    protected abstract int getHitResult();
    protected abstract Sprite getSprite();
    
    private float moveSpeed;
    private Vector3 startPosition;
    private Vector3 direction = Vector3.zero;
    private SpriteRenderer cubeSprite;
    private float now;
    private float timeScaler;


    private bool active = false;
    private Transform aimBox;
    private float timeLeftToHit;
    private float timeToLive = 1.5f;
    private float timeToStopFading = 0.75f;
    private bool isFadingOut;
    private Color alphaColour;
    HitEventReiceiverDelegate hitEventReceiver;

    public void Start()
    {
        if (PlayerPrefs.GetInt("BonusMultiplicator") == 3)
        {
            timeToLive = 1.25f;
            timeToStopFading = 0.5f;
        }
            
        else if (PlayerPrefs.GetInt("BonusMultiplicator") == 4)
        {
            timeToLive = 1f;
            timeToStopFading = 0.25f;
        }
            
        timeScaler = timeToLive;
    }
    public void StartMe(HitEventReiceiverDelegate d, Transform box)
    {
        timeLeftToHit = timeToLive;
        aimBox = box;
        hitEventReceiver = d;
        active = true;
        isFadingOut = true;
        cubeSprite = transform.GetComponent<SpriteRenderer>();
        cubeSprite.sprite = getSprite();
    }

    // Update is called once per frame
    void Update()
    {
        now = Time.timeSinceLevelLoad;
        var substractionFactorForFading = (1- (timeScaler - now))/2;
        if (now >= timeScaler)
        {
            timeScaler += timeToLive;
        }
        //Debug.Log("now has value = " + now + " timeScaler has value = " + timeScaler + " substraction = " + (1- (timeScaler-now)));
        if (isFadingOut)
        {
            if(substractionFactorForFading >= 0 && substractionFactorForFading < 1)
            {
                cubeSprite.color = new Color(1f,1f,1f, 1f - substractionFactorForFading);
                Debug.Log("now has value = " + now + " timeScaler has value = " + timeScaler + " substraction = " + (1- (timeScaler-now)));
            }
            //Debug.Log(transform.name + " has the float value on color a with " + alphaColour.a);
        }
        if (timeToLive < timeToStopFading)
        {
            isFadingOut = false;
        }
        if (!active)
            return;
        timeLeftToHit -= Time.deltaTime;
        if (timeLeftToHit < 0.25)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            transform.Translate(direction * Time.deltaTime);
        }
    }
    private void OnMouseDown()
    {
        timeLeftToHit = 10;
        startPosition = Input.mousePosition;
        isFadingOut = false;
        cubeSprite.color = new Color(1,1,1,1);
    }
    private void OnMouseUp()
    {
        Vector3 mousePosition = Input.mousePosition;
        direction = mousePosition - startPosition;
        direction = direction * 10 / direction.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BoxToHit")
        {
            hitEventReceiver(getHitResult());
        }
        Destroy(transform.gameObject);
    }
}
