using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PopupSystem;
using Assets.Scripts.Units;
using InitsObject;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Popup popupBuilding;
        [SerializeField] private Popup popupSelectedUnits;
        [SerializeField] private WorkingButton workerButton;

        public event Action<int> OnQueueCountChanged;

        private Popup activePopUp;
    
        private void Awake(){
            InitPopUp();
        }

        private void InitPopUp(){
            //popUpHouse.Setup(this);
            popupSelectedUnits.Setup(this); //
        }
    
        private void ShowPopup(Popup popup) {
            if (activePopUp != null) {
                activePopUp.HidePopup();
            }

            activePopUp = popup;
            activePopUp.ShowPopup();
        }

        public void HidePopup(){
            if (activePopUp != null) {
                activePopUp.HidePopup();
            }

            activePopUp = null;
        }

        public void BuildingSelected(){
            ShowPopup(popupBuilding);
        }
        public void GetSelectionObjectList(List<ISelectable> selectables){
            if (selectables.Count > 1){
//                popupSelectedUnits.GetSelectionObjectList(selectables);
//                ShowPopup(popupSelectedUnits);
            }
            else{
                foreach (var obj in selectables){
                    if (obj.GetInterface<Building>()){
                        var building = obj.GetInterface<Building>();
                        int n=(int) Enum.Parse(typeof(BuildingType), building.GetName());
                        switch (n){
                            case 0:
                                popupBuilding.GetSelectionObjectList(selectables);
                                ShowPopup(popupBuilding);
                                break;
                            default:
                                Debug.Log("Does not exist Building");
                                break;
                        }
                    }
                }


            }
        }

        public void ChangeQueueCount()
        {
            workerButton.SetTextCount();
        }




    }

}