using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ISelectionIdicationWithGlow : IBaseSelectionIndication
    {
        GlowObjectCmd glowComponent { get; }
    }
}
