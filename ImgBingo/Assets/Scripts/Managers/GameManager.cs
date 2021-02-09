using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Image bingoImg;
    [SerializeField]
    private GameObject video;
    [SerializeField]
    private GameObject end;

    [SerializeField]
    private Sprite[] bingoIntro;
    [SerializeField]
    private Sprite[] bingoNumbers;

    private List<int> retrievedInds = new List<int>();

    private int indice = 0;
    private bool intro = true;
    private bool firstTime = true;

    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = video.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += CheckVideoEnd;
    }

    private void Start()
    {
        GetNextIntroImg();
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
                    bingoImg.enabled = false;
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
            bingoImg.sprite = bingoNumbers[r];
        }
        else
        {
            if(retrievedInds.Count == bingoNumbers.Length)
            {
                // game finished
                bingoImg.enabled = false;
                end.SetActive(true);
            }
            else
            {
                GetRandomBingoInt();
            }
        }
    }

    private void GetNextIntroImg()
    {
        bingoImg.sprite = bingoIntro[indice];
        indice++;

        if (indice > (bingoIntro.Length - 1))
        {
            intro = false;
        }
    }

    void CheckVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        firstTime = false;

        bingoImg.enabled = true;
        video.SetActive(false);

        GetRandomBingoInt();
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= CheckVideoEnd; 
    }
}
