﻿using System;
using Assets.Scripts.GameFlowStates;
using Prime31.StateKit;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameFlowController : MonoBehaviour
    {
        public event Action<float> SpeedChanged;

        private SKStateMachine<GameFlowController> stateMachine; 

        public event Action<SKState<GameFlowController>> StateChanged;

        public static GameFlowController Current { get; private set; }

        public bool StartAutomatically;

        public Cellphone CellphonePrefab;

        public Canvas MainCanvas;

        public int NumToCompleteThenFinale = 10;

        public int NumCompleted { get; private set; }

        public Text SpeedingUpTextPrefab;

        public AudioClip SpeedingUpMusic;

        public float CurrentSpeed = 1;

        public void Awake()
        {
            Current = this;
        }

        public void Start()
        {
            stateMachine = new SKStateMachine<GameFlowController>(this, new StartingUp(StartAutomatically));

            stateMachine.onStateChanged += StateMachine_onStateChanged;

            stateMachine.addState(new ShowingCellphone(CellphonePrefab, MainCanvas));
            stateMachine.addState(new InMinigame());
            stateMachine.addState(new MinigameFinished());
            stateMachine.addState(new DecidingNextMinigame());
            stateMachine.addState(new SpeedingUp());
        }

        void StateMachine_onStateChanged ()
        {
            if (StateChanged != null)
            {
                StateChanged(stateMachine.currentState);
            }
        }

        public void Update()
        {
            stateMachine.update(Time.deltaTime);
        }

        public void MarkMinigameAsFinished()
        {
            if (stateMachine.currentState is InMinigame)
            {
                NumCompleted++;
                stateMachine.changeState<MinigameFinished>();
            }
        }

        public void MarkMinigameAsCompletelyFinished()
        {
            var state = stateMachine.currentState as MinigameFinished;
            if (state != null)
            {
                state.MinigameIsCompletelyFinished();
            }
        }

        public void ResetNumCompleted()
        {
            NumCompleted = 0;
        }

        public void ResetSpeed()
        {
            CurrentSpeed = 1.0f;
            SetupSpeed();
        }

        /// <summary>baseIncreaseAmount of 0.1 will increase the speed by 10% of the base speed.</summary>
        public void SpeedUpBy(float baseIncreaseAmount)
        {
            CurrentSpeed += baseIncreaseAmount;
            SetupSpeed();
        }

        public void SetupSpeed()
        {
            MinigameController.Current.SpeedFactor = CurrentSpeed;
            GetComponent<AudioSource>().pitch = CurrentSpeed;

            if (SpeedChanged != null)
                SpeedChanged(CurrentSpeed);
        }
    }
}