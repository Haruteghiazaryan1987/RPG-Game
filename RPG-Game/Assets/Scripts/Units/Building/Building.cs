using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Building : Unit, IBuilding
    {
        private ISelectionIdicationWithGlow SelectionIndicator;
        protected BuildingType buildingType;
        
        public override void Init()
        {
            SelectionIndicator = gameObject.AddComponent<GlowObjectCmd>();
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
        
        
        public BuildingType BuildingType{
            get => buildingType;
            set => buildingType = value;
        }

        protected Building(BuildingType type){
            buildingType = type;
        }


        public virtual BuildingType GetType(){
            return buildingType;
        }

        public virtual string GetName(){
            return buildingType.ToString();
        }
    }
}
