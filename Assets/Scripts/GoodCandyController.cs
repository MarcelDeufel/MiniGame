using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodCandyController : CandyController
{
    private Sprite candySprite;

    private void Awake()
    {
        candySprite = GameObject.Find("CandyCane").GetComponent<Image>().sprite;
    }
    protected override int getHitResult()
    {
        return 1;
    }
    protected override Sprite getSprite()
    {
        return candySprite;
    }
}
