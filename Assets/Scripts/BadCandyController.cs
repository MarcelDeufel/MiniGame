using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadCandyController : CandyController
{
    private Sprite KakerlakenJulian;

    private void Awake()
    {
        KakerlakenJulian = GameObject.Find("KakerlakenJulian").GetComponent<Image>().sprite;
    }
    protected override int getHitResult()
    {
        return -1;
    }
    protected override Sprite getSprite()
    {
        return KakerlakenJulian;
    }
}
