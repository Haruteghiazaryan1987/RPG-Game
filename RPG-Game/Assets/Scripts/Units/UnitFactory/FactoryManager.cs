using InitsObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Assets.Scripts.Units.UnitFactory
{
    class FactoryManager : MonoBehaviour
    {
        private static FactoryManager _instance;


        public event Action<float> OnTick;
        public event Action OnQueueCountChanged;
        //[SerializeField] private WorkingButton workerButton;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if(_instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(_instance);
        }

        public static FactoryManager Instance => _instance;

        GameObject factory;
        ObjectSelection objectSelection;
        bool isFactoryRunning;
        Timer timer;

        public void AddToProduction(UnitType unitType,int count, int delay, ObjectSelection objectSelection)
        {
            switch (unitType)
            {
                case UnitType.Worker:
                    if (factory == null)
                    {
                        factory = new GameObject("Worker Factory");
                        
                        WorkerFactory workerFactory = factory.AddComponent(typeof(WorkerFactory)) as WorkerFactory;

                        timer = new Timer().CreateRepeatablTimer(count, delay); // To ask Arman
                        this.objectSelection = objectSelection;
                        timer.OnEndLap += OnUnitCreation;
                        timer.OnEndLap += CreateUnit;
                        timer.OnComplete += workerFactory.DestroyFactory;
                        timer.StartTimer();
                    }
                    else
                    {
                        timer.AddToQueue(count);
                    }
                    break;
            }
        }

        private void OnUnitCreation()
        {
            OnQueueCountChanged?.Invoke();
        }

        private void CreateUnit()
        {
            factory.GetComponent<WorkerFactory>().CreateUnit(objectSelection);
        }
        
    }
}
