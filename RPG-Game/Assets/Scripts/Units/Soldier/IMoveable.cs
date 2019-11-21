using UnityEngine;

namespace Assets.Scripts
{
    public interface IMoveable
    {
        
        void Move(Vector3 pos);
        void MoveDefault(Vector3 pos);
    }
}