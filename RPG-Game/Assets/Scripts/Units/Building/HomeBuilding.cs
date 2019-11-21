using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class HomeBuilding:Building
    {
        public HomeBuilding(BuildingType type) : base(type){
            buildingType = BuildingType.Home;
        }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.A)){
                var go = gameObject.GetComponent<ISelectable>();
                 print(go.IsSelected);
            }
        }
    }
}