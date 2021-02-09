using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] bingoIntro;
    [SerializeField]
    private Sprite[] bingoNumbers;
    [SerializeField]
    private Sprite endGame;

    private List<int> retrievedInds = new List<int>();

    private int indice = 0;
    private bool intro = true;
    private bool firstTime = true; 

    public Image image;
    public GameObject video;

    private void Start()
    {
        GetNextIntroImg();

        video.GetComponent<VideoPlayer>().loopPointReached += CheckEnd; 
    }


    private void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (intro)
            {
                GetNextIntroImg();
            }
            else
            {
                if (firstTime)
                {
                    image.enabled = false;
                    video.SetActive(true);
                }
                else
                {

                    GetRandomBingoInt();
                }
            }
        }
    }

    private void GetRandomBingoInt()
    {
        int r = Random.Range(0, bingoNumbers.Length);

        if (!retrievedInds.Contains(r))
        {
            retrievedInds.Add(r);
            image.sprite = bingoNumbers[r];
        }
        else
        {
            if(retrievedInds.Count == bingoNumbers.Length)
            {
                // game finished
                image.sprite = endGame;
            }
            else
            {
                GetRandomBingoInt();
            }
        }
    }

    private void GetNextIntroImg()
    {
        image.sprite = bingoIntro[indice];
        indice++;

        if (indice > (bingoIntro.Length - 1))
        {
            intro = false;
        }
    }

    void CheckEnd(UnityEngine.Video.VideoPlayer vp)
    {
        firstTime = false;

        image.enabled = true;
        video.SetActive(false);

        GetRandomBingoInt();
    }
}
