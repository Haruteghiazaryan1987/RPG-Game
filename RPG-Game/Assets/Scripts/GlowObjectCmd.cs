using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class GlowObjectCmd : MonoBehaviour, ISelectionIdicationWithGlow
    {
        private Color GlowColor = Color.yellow;

        public Renderer[] Renderers
        {
            get;
            private set;
        }

        public Color CurrentColor
        {
            get { return _currentColor; }
        }

        public GlowObjectCmd glowComponent => this;

        private Color _currentColor;
        private Color _targetColor;

        void Start()
        {
            Renderers = GetComponentsInChildren<Renderer>();
            GlowController.RegisterObject(this);
        }

        public void Init()
        {
            //
        }

        public void Select()
        {
            _currentColor = GlowColor;
        }

        public void Deselect()
        {
            _currentColor = Color.black;
        }

        public void Destroy()
        {
            Destroy(this);
        }
    }
}
