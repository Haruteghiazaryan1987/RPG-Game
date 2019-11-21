using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace Assets.Scripts.PopupSystem
{
    public class PopupSelectedUnits : Popup
    {
//        [SerializeField] private GameObject popInfo;
        [SerializeField] private RectTransform popUpSelectedUnits;
        
        private List<ISelectable> listSel;
        private int countType;
        private string redSoldier = "RedSoldier";
        private int redSoldierCount;
        private string greenSoldier = "GreenSoldier";
        private int greenSoldierCount;
        private string blueSoldier = "BlueSoldier";
        private int blueSoldierCount;
        private string house = "HouseScript";
        private int houseCount;

        public override void GetSelectionObjectList(List<ISelectable> selectables){
            ResettingParameters();
            listSel = selectables;
            GetTypeCount();
            GetAllTypeCount();
            InitsUnitsInfo();
//            Print();
        }

        private void Print(){
            print("blueSolsiercount = "+blueSoldierCount);
            print("greenSolsiercount = "+greenSoldierCount);
            print("redSolsiercount = "+redSoldierCount);
            print("housecount = "+houseCount);
            print("counType = "+ countType);
        }

        private void InitsUnitsInfo(){
            for (int i = 0; i < popUpSelectedUnits.childCount; i++)
            {
                Destroy(popUpSelectedUnits.GetChild(i).gameObject);
            }
            for (int i = 0; i < countType; i++){
//                var info = Instantiate(popInfo, popUpSelectedUnits);
            }
        }

        private void ResettingParameters(){
            countType = 0;
            redSoldierCount = 0;
            greenSoldierCount = 0;
            blueSoldierCount = 0;
            houseCount = 0;
        }
        
        private void GetAllTypeCount(){
            for (int i = 0; i < listSel.Count; i++){
/*                if (type != listSel[i].GetType()){
                    type = listSel[i].GetType();
                    print(listSel[i].GetType());
                    countType++;
                    print(countType);
                }*/

                switch (listSel[i].GetType().ToString()){
                    case "RedSoldier":
                        redSoldierCount++;
                        break;
                    case "GreenSoldier":
                        greenSoldierCount++;
                        break;
                    case "BlueSoldier":
                        blueSoldierCount++;
                        break;
                    case "HouseScript":
                        houseCount++;
                        break;
                }
            }
        }

        private void GetTypeCount(){
            IEnumerable<ISelectable> type = listSel.Distinct();
            countType = type.Count();
        }

        private void OnEnable(){
        }

        private void OnDisable(){
        }
    }
}