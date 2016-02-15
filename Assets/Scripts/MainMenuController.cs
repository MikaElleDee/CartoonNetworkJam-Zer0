﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Assets.Scripts
{
    public class MainMenuController : MonoBehaviour
    {

        public GameObject FlashPrefab;
        public float FlashAt;
        public AudioClip audio;
        public SoundKit.SKSound sound;

        public void Start()
        {
            StartCoroutine(FlashScreen());
            Screen.orientation = ScreenOrientation.Landscape;
            sound = SoundKit.instance.playSoundLooped(audio);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(k.Scenes.DEFAULT);
            sound.stop();
        }

        public void Credits()
        {
            SceneManager.LoadScene(k.Scenes.CREDITS);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }            
        }

        public IEnumerator FlashScreen()
        {
            yield return new WaitForSeconds(FlashAt);

            Instantiate(FlashPrefab);
        }
    }
}