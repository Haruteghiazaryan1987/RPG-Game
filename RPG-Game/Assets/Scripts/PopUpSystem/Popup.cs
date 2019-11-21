using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PopupSystem
{
    public abstract class Popup : MonoBehaviour
    {
        protected UIManager uiManager;

        public void Setup(UIManager uiManager){
            this.uiManager = uiManager;
        }

        public void ShowPopup(){
            gameObject.SetActive(true);
        }
        public void HidePopup() {
            gameObject.SetActive(false);
        }

        public virtual void GetSelectionObjectList(List<ISelectable> selectables){
        }
    }
}