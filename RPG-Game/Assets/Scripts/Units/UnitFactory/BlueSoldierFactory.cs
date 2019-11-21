using UnityEngine;

namespace Assets.Scripts.Units.UnitFactory
{
    public class BlueSoldierFactory : UnitFactory<BlueSoldier>
    {
        public override BlueSoldier CreateUnit(ObjectSelection objectSelection){
            var prefab = Resources.Load("Prefabs/BlueSoldier") as GameObject;
            var circlePrefab = Resources.Load("Prefabs/SelectionCircle", typeof(ISelectionIndicationWithShape)) as ISelectionIndicationWithShape;
            var soldier = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            soldier.GetComponent<UnitCharacter>().Init(circlePrefab);
            objectSelection.AddNewInitsObject(soldier);
            return soldier.GetComponent<BlueSoldier>();
        }
    }
}