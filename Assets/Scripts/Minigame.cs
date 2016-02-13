﻿using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Minigame : MonoBehaviour
    {
        [Range(0, 10)]
        public float StartingSecondsToComplete = 5;

        public MinigameScriptBase MinigameScript { get; private set; }

        private CompletedMinigameScriptBase CompletedScript { get; set; }

        public MinigameController MinigameController { get; set; }

        public bool IsFinale { get; set; }

        public void Awake()
        {
            MinigameScript = GetComponent<MinigameScriptBase>();

            CompletedScript = GetComponent<CompletedMinigameScriptBase>();

            if (MinigameController == null)
                MinigameController = GameObject.Find("Minigame Controller").GetComponent<MinigameController>();

            if (MinigameScript == null || CompletedScript == null)
                throw new InvalidOperationException("Missing minigame script or completed script.");

            CompletedScript.Minigame = this;
            MinigameScript.Minigame = this;

            CompletedScript.FinishedScript += OnMinigameCompletelyFinished;
        }

        private void OnMinigameCompletelyFinished()
        {
            MinigameController.MinigameCompletelyFinished();
        }

        public void StartMinigame(MinigameStartInfo info)
        {
            MinigameScript.StartMinigame(info);
        }

        public void Finished(bool success)
        {
            GameFlowController.Current.MarkMinigameAsFinished();

            MinigameScript.StopMinigame();

            if (success)
            {
                CompletedScript.MinigameCompletedSuccessfully();
            }
            else
            {
                CompletedScript.MinigameFailed();
            }
        }
    }
}