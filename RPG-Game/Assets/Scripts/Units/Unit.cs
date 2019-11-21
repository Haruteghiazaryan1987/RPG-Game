using UnityEngine;

namespace Assets.Scripts.Units
{
    public abstract class Unit : MonoBehaviour, ISelectable, IDamageable
    {
        [SerializeField] private float hp;
        private bool isSelected;

        public float HP
        {
            get => hp;
            protected set => hp = value;
        }


        public virtual void Init(IBaseSelectionIndication _indicator)
        {
            //
        }

        public virtual void Init()
        {
            //
        }



        public bool IsSelected
        {
            get => isSelected;
            protected set => isSelected = value;
        }


        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }

        public virtual void Select()
        {
            IsSelected = true;
        }


        public virtual void Deselect()
        {
            IsSelected = false;
        }


        public T GetInterface<T>()
        {
            return GetComponent<T>();
        }


        public virtual void TakeDamage(float damage)
        {
            HP -= damage;
        }
    }
}
