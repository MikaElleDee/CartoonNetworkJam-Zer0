﻿using UnityEngine;
using TouchScript.Layers;

namespace TouchScript.Examples.Cube
{
    public class LayerDelegate : MonoBehaviour, ILayerDelegate
    {

        public RedirectInput Source;
        public TouchLayer RenderTextureLayer;

        public bool ShouldReceiveTouch(TouchLayer layer, TouchPoint touch)
        {
            if (layer == RenderTextureLayer)
#pragma warning disable 252,253
                return touch.InputSource == Source;
#pragma warning restore 252,253
#pragma warning disable 252,253
            return touch.InputSource != Source;
#pragma warning restore 252,253
        }
    }
}
