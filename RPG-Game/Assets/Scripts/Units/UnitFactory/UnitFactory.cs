using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Assets.Scripts.Units.UnitFactory
{
    public abstract class UnitFactory<T> : MonoBehaviour where T : Unit
    {
        public abstract T CreateUnit(ObjectSelection objectSelection);

        public virtual void DestroyFactory(){
            Destroy(gameObject);
        }
    }
}
