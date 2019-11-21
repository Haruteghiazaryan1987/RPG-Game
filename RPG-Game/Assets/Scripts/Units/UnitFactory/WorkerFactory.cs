using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.UnitFactory
{
    public class WorkerFactory : UnitFactory<Worker>
    {
        public override Worker CreateUnit(ObjectSelection objectSelection)
        {
            var prefab = Resources.Load<UnitCharacter>("Prefabs/Worker");
            var circlePrefab = Resources.Load("Prefabs/SelectionCircle", typeof(ISelectionIndicationWithShape)) as ISelectionIndicationWithShape;
            var worker = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            worker.Init(circlePrefab);
            objectSelection.AddNewInitsObject(worker.gameObject);
            return worker.GetComponent<Worker>();
        }

    }
}
