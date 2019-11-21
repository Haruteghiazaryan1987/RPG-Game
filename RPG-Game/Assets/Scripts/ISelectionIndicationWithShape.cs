using UnityEngine;

namespace Assets.Scripts
{
    public interface ISelectionIndicationWithShape : IBaseSelectionIndication
    {
        GameObject SelectionShape { get; }
    }
}
