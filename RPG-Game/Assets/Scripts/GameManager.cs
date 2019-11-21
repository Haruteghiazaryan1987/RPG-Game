using Assets.Scripts.Units.UnitFactory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private FactoryManager factoryManager;
        [SerializeField] private ObjectSelection selectionSystem;
        
        [SerializeField] private GameObject indicator;
        UnitCharacter[] unitCharacters;
        Building[] buildings;

        private void Awake(){
            selectionSystem.ClickedDifferentObjects += GetSelectionObjectList;
            selectionSystem.OnNoClickedObjects += NoClickedObjects;

            //factoryManager.OnFactoryComplete += ChangeFactoryStatus;
            factoryManager.OnQueueCountChanged += ChangeQueueCount;

            unitCharacters = FindObjectsOfType<UnitCharacter>();
            buildings = FindObjectsOfType<Building>();
            foreach (UnitCharacter u in unitCharacters)
            {
                // u.Init(Resources.Load("Prefabs/SelectionCircle", typeof(IBaseSelectionIndication)) as IBaseSelectionIndication);
                u.Init(indicator.GetComponent<ISelectionIndicationWithShape>());
            }
            foreach (Building b in buildings)
            {
                b.Init();
            }
        }

        private void ChangeQueueCount()
        {
            uiManager.ChangeQueueCount();
        }

        private void NoClickedObjects(){
            uiManager.HidePopup();
        }

        private void HouseClicked(){
            uiManager.BuildingSelected();
        }
        public void GetSelectionObjectList(List<ISelectable> selectables){
            uiManager.GetSelectionObjectList(selectables);
        }
    }
}
