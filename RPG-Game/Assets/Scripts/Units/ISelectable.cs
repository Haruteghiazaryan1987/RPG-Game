using UnityEngine;

namespace Assets.Scripts
{
    public interface ISelectable : IBase
    {
        void Init();
        bool IsSelected { get; }
        Vector3 GetPosition();
        void Select();
        void Deselect();
    }
}