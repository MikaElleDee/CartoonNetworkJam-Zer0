﻿using System.Collections;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts.GameFlowStates
{
    public class ShowingCellphone : SKState<GameFlowController>
    {
        private readonly Cellphone cellphonePrefab;

        private Cellphone cellphoneInstance;
        private bool finished;

        public ShowingCellphone(Cellphone cellphonePrefab)
        {
            this.cellphonePrefab = cellphonePrefab;
        }

        public override void begin()
        {
            finished = false;
            cellphoneInstance = Object.Instantiate(cellphonePrefab).GetComponent<Cellphone>();

            _context.StartCoroutine(Sequence());
        }

        public IEnumerator Sequence()
        {
            yield return cellphoneInstance.Raise();
            cellphoneInstance.ShowScreen();
            yield return new WaitForSeconds(cellphoneInstance.WaitSeconds);
            yield return cellphoneInstance.Lower();

            finished = true;
        }

        public override void reason()
        {
            if (finished)
            {
                _machine.changeState<InMinigame>();
            }
        }

        public override void end()
        {
            Object.Destroy(cellphoneInstance.gameObject);
        }

        public override void update(float deltaTime)
        {
        }
    }
}