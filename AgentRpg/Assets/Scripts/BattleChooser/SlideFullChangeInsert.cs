﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideFullChangeInsert : MonoBehaviour
{
    GameObject CurrentSlideUI;
    int PreviousSlide = 0;
    SliderChange MainSlideChange;
    [SerializeField]
    GameObject[] SlideUIPrefabs;
    CharacterRememberance CarryOverIntoScene;
    //[E1,E1,E1,E2,E2,E2,E3...]
    [SerializeField]
    GameObject[] Enemies;
    // Start is called before the first frame update
    void Start()
    {
        MainSlideChange = Camera.main.GetComponent<SliderChange>();
        CarryOverIntoScene = GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();
        CurrentSlideUI = Instantiate(SlideUIPrefabs[MainSlideChange.CurrentSlide]);
        CurrentSlideUI.GetComponent<MoveSum>().Enemies[0] = Enemies[MainSlideChange.CurrentSlide * 3];
        CurrentSlideUI.GetComponent<MoveSum>().Enemies[1] = Enemies[MainSlideChange.CurrentSlide * 3 + 1];
        CurrentSlideUI.GetComponent<MoveSum>().Enemies[2] = Enemies[MainSlideChange.CurrentSlide * 3 + 2];
        CarryOverIntoScene.Enemies = CurrentSlideUI.GetComponent<MoveSum>().Enemies;
        PreviousSlide = MainSlideChange.CurrentSlide;
    }

    // Update is called once per frame
    void Update()
    {
        if(PreviousSlide != MainSlideChange.CurrentSlide)
        {
            Destroy(CurrentSlideUI);
            CurrentSlideUI = Instantiate(SlideUIPrefabs[MainSlideChange.CurrentSlide]);
            CurrentSlideUI.GetComponent<MoveSum>().Enemies[0] = Enemies[MainSlideChange.CurrentSlide * 3];
            CurrentSlideUI.GetComponent<MoveSum>().Enemies[1] = Enemies[MainSlideChange.CurrentSlide * 3+1];
            CurrentSlideUI.GetComponent<MoveSum>().Enemies[2] = Enemies[MainSlideChange.CurrentSlide * 3+2];
            CarryOverIntoScene.Enemies = CurrentSlideUI.GetComponent<MoveSum>().Enemies;
        }
        PreviousSlide = MainSlideChange.CurrentSlide;
    }
}
