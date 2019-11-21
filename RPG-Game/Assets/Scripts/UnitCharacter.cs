using System;
using Assets.Scripts.Units;
using Units;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class UnitCharacter : Unit, IMoveable
    {
        private ISelectionIndicationWithShape SelectionIndicator;
        UnitControl unitControl;

        public override void Init(IBaseSelectionIndication _indicator)
        {
            IBaseSelectionIndication childIndicator = GetComponentInChildren<IBaseSelectionIndication>();
            if (childIndicator != null)
            {
                childIndicator.Destroy();
            }

            ISelectionIndicationWithShape indicator = _indicator as ISelectionIndicationWithShape;
            SelectionIndicator = Instantiate(indicator.SelectionShape, transform).GetComponent<ISelectionIndicationWithShape>();
            Init();
        }

        public override void Init()
        {
            SelectionIndicator.Init();
        }

        public override void Select()
        {
            base.Select();
            SelectionIndicator.Select();
        }


        public override void Deselect()
        {
            base.Deselect();
            SelectionIndicator.Deselect();
        }

        public virtual void Move(Vector3 pos){
            if (IsSelected){
                unitControl = gameObject.GetComponent<UnitControl>();
                unitControl.GoToPositions(pos);
                Deselect();
            }
        }

        public virtual void MoveDefault(Vector3 pos){
            unitControl = gameObject.GetComponent<UnitControl>();
            unitControl.GoToPositionIfInits(pos);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }
    }
}
