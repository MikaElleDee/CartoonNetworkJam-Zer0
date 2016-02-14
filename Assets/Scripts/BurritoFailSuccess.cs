﻿using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BurritoFailSuccess : CompletedMinigameScriptBase
    {
        public GameObject LaserPrefab;

        public GameObject CookedBurritoPrefab;

        public GameObject IceBurritoPrefab;

        public AudioClip LoseSound;

        private SoundKit.SKSound Loser;

        private Action finished;
        private int scoreEarned;

        protected override void OnMinigameCompletedSuccessfully(Action finished)
        {
            this.finished = finished;

            StartCoroutine(SuccessSequence());
            Loser = SoundKit.instance.playSound(LoseSound);
        }

        public IEnumerator SuccessSequence()
        {
            var instance = Instantiate(LaserPrefab);
            Camera.main.GetComponent<ScreenShake>().ShakeCamera(1.0f, TimeSpan.FromSeconds(0.75));

            yield return new WaitForSeconds(0.75f);

            Destroy(instance);

            var cookedInstance = Instantiate(CookedBurritoPrefab);
            LikeCounterController.Current.AddToPointCount(scoreEarned);

            yield return new WaitForSeconds(1.5f);

            Destroy(cookedInstance);

            finished();
        }

        protected override void OnMinigameFailed(Action finished)
        {
            this.finished = finished;

            StartCoroutine(FailSequence());

        }

        public IEnumerator FailSequence()
        {
            var instance = Instantiate(IceBurritoPrefab);

            yield return new WaitForSeconds(3.0f / GameFlowController.Current.CurrentSpeed);

            Destroy(instance);

            finished();
        }

        public override void ScoreEarned(int scoreEarned)
        {
            this.scoreEarned = scoreEarned;
        }
    }
}