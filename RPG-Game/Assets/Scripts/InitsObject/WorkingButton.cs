using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Units.UnitFactory;
using Units;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InitsObject
{
    public class WorkingButton : MonoBehaviour
    {
        [SerializeField] private Text countText;
        [SerializeField] private ObjectSelection selection;
        [SerializeField] private UnitType unitType;
        private int count;
        private const int CreationTime = 3;

        public void SetTextCount(int _count){
            countText.text = $"{_count}";
        }

        public void OnButtonClicked(){
            count++;
            SetTextCount(count);

            FactoryManager.Instance.AddToProduction(unitType, count, CreationTime, selection);
        }

        public void SetTextCount(){
            count--;
            SetTextCount(count);
        }
    }
}
