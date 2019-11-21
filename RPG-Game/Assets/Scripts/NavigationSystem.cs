using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class NavigationSystem : MonoBehaviour
    {
        private UnitControl unitControl;

        private ObjectSelection objectSelection;
        private List<ISelectable> listISelectables;
        private Vector3 mousePos;

        private float radius = 1f;
        private List<Vector3> vectorList;
        private int count;
        private int width;

        private List<ISelectable> newInitsUnitList;
        private List<Vector3> newUnitVectors;
        private Vector3 newUnitStartPos = new Vector3(0, 1, -9);
        private int m;


        public void Setup(ObjectSelection obj){
            newInitsUnitList = new List<ISelectable>();
            newUnitVectors = new List<Vector3>();
            objectSelection = obj;
            objectSelection.ClickedDifferentObjects += GetISelectableList;
            objectSelection.OnAddNewUnit += AddNewUnit;
            vectorList = new List<Vector3>();
        }

        private void AddNewUnit(ISelectable unit){
            newInitsUnitList.Add(unit);

            if (newInitsUnitList.Count == 1){
                newUnitVectors.Add(newUnitStartPos);
            }

            if (newInitsUnitList.Count > newUnitVectors.Count){
                GetnewUnitVectors(newInitsUnitList);
                ReconstructionPosition();
                m++;
                return;
            }

            SetPositionInUnit(unit);
        }

        private void SetPositionInUnit(ISelectable unit){
            var newUnit = unit.GetInterface<IMoveable>();
            if (newUnit != null){
                newUnit.MoveDefault(newUnitVectors[m]);
            }

            m++;
        }

        private void ReconstructionPosition(){
            for (int i = 0; i < newInitsUnitList.Count; i++){
                var newUnit = newInitsUnitList[i].GetInterface<IMoveable>();
                if (newUnit != null){
                    newUnit.MoveDefault(newUnitVectors[i]);
                }

                m = i;
            }
        }


        private void SetMousePosition(Vector3 pos){
            mousePos = pos;
            objectSelection.MousePos -= SetMousePosition;
            SortedListForTargetPos();
            GetGizmosPosition();
            MoveSelectionObject();
            if (listISelectables.Count == 1){
                var building = listISelectables[0].GetInterface<Building>();
                if (building != null){
                    building.Deselect();
                }
            }
        }

        private void GetISelectableList(List<ISelectable> list){
            listISelectables = list;

            newInitsUnitList.RemoveAll(selectable => selectable.IsSelected);
            
                m = newInitsUnitList.Count;
            GetnewUnitVectors(newInitsUnitList);
            objectSelection.MousePos += SetMousePosition;
            count = listISelectables.Count;
            double n = Math.Sqrt(count);
            width = (int) Math.Ceiling(n);
        }

        private void MoveSelectionObject(){
            for (int i = 0; i < listISelectables.Count; i++){
                IMoveable moveObject = listISelectables[i].GetInterface<IMoveable>();
                if (moveObject != null){
                    moveObject.Move(vectorList[i]);
                }
            }

            vectorList.Clear();
        }

        private void SortedListForTargetPos(){
            var sortedList = listISelectables.OrderBy(x => (x.GetPosition() - mousePos).magnitude).ToList();
            listISelectables = sortedList;
        }

        private void GetGizmosPosition(){
            int height = width;
            List<Vector3> list = new List<Vector3>();
            Vector3 posStart = new Vector3((mousePos.x - (width - 1) * radius), 0.25f,
                (mousePos.z + (width - 1) * radius));
            Vector3 pos = posStart;
            for (int i = 0; i < height; i++){
                for (int j = 0; j < width; j++){
                    list.Add(pos);
                    pos.z -= 2 * radius;
                }

                pos.z = posStart.z;
                pos.x += 2 * radius;
            }

            for (int i = 0; i < listISelectables.Count; i++){
                vectorList.Add(list[i]);
            }
        }

        private void GetnewUnitVectors(List<ISelectable> newUnitList){

            newUnitVectors.Clear();
            int xwidth = (int) Math.Ceiling(Math.Sqrt(newUnitList.Count));
            int zheigth = xwidth;


            Vector3 posStart = new Vector3((newUnitStartPos.x - (xwidth - 1) * radius), 0.25f,
                (newUnitStartPos.z - (xwidth - 1) * radius));
            Vector3 pos = posStart;

            for (int i = 0; i < zheigth; i++){
                for (int j = 0; j < xwidth; j++){
                    newUnitVectors.Add(pos);
                    pos.x += 2 * radius;
                }

                pos.x = posStart.x;
                pos.z += 2 * radius;
            }
        }

        private void InitsObject(Vector3 pos){
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.GetComponent<Renderer>().material.color = Color.blue;
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            go.transform.position = pos;
        }
    }
}