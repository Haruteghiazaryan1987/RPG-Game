using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ObjectSelection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
    {
        public event Action<List<ISelectable>> ClickedDifferentObjects;
        public event Action<Vector3> MousePos;
        public event Action OnNoClickedObjects;
        public event Action<ISelectable> OnAddNewUnit;
        [SerializeField] private GameObject panel;
        private NavigationSystem navigationSystem;
        private List<ISelectable> initsGameObjects;
        private RectTransform panelTransform;

        private Vector2 startPos;
        private Vector2 endPos;
        private Rect rect;
        private bool cursorSelection;
        private Camera camera;
        private Shader shader;
        List<ISelectable> listSelected;
        private Vector3 mousePose = Vector3.zero;

        void Start(){
            navigationSystem = new NavigationSystem();
            navigationSystem.Setup(this);
            listSelected = new List<ISelectable>();
            camera = Camera.main;
            panelTransform = panel.GetComponent<RectTransform>();
            initsGameObjects = new List<ISelectable>();
            List<GameObject> allObjs = FindObjectsOfType<GameObject>().ToList();
            for (var i = 0; i < allObjs.Count; i++){
                ISelectable selectable = allObjs[i].GetComponent<ISelectable>();
                if (selectable != null){
                    initsGameObjects.Add(selectable);
                }
            }
        }

        public void GetListSelected(){
            listSelected.Clear();
            for (int i = 0; i < initsGameObjects.Count; i++){
                if (initsGameObjects[i].IsSelected){
                    listSelected.Add(initsGameObjects[i]);
                }
            }

            if (listSelected.Count == 0){

                OnNoClickedObjects?.Invoke();
            }

            ClickedDifferentObjects?.Invoke(listSelected);
        }

        public void AddNewInitsObject(GameObject selectedObject){
            var iSelectable = selectedObject.GetComponent<ISelectable>();
            if (iSelectable != null){
                initsGameObjects.Add(iSelectable);
                OnAddNewUnit?.Invoke(iSelectable);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            startPos = eventData.position;

            RaycastHit hit = new RaycastHit();
            Ray ray = camera.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out hit, 500))
            {
                var selectedGameObject = hit.transform.GetComponent<ISelectable>();
                if (selectedGameObject != null)
                {
                    if (!selectedGameObject.IsSelected)
                    {
                        selectedGameObject.Select();
                    }
                    else
                    {
                        foreach (var initsObject in initsGameObjects)
                        {
                            if (selectedGameObject != initsObject)
                            {
                                initsObject.Deselect();
                            }
                        }
                    }

                    mousePose = Vector3.zero;
                }

                if (selectedGameObject == null)
                {
                    if (listSelected.Count != 0)
                    {
                        mousePose = hit.point;
                        MousePos?.Invoke(mousePose);
                    }
                }
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            panel.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            endPos = eventData.position;
            

            rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                Screen.height - Mathf.Max(endPos.y, startPos.y),
                Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y));

            rect.xMin = Mathf.Min(endPos.x, startPos.x);
            rect.xMax = Mathf.Max(endPos.x, startPos.x);
            rect.yMin = Mathf.Min(endPos.y, startPos.y);
            rect.yMax = Mathf.Max(endPos.y, startPos.y);
            panelTransform.offsetMin = rect.min;
            panelTransform.offsetMax = rect.max;

            for (int i = 0; i < initsGameObjects.Count; i++)
            {
                Vector3 objectPos = new Vector2(
                    camera.WorldToScreenPoint(initsGameObjects[i].GetPosition()).x,
                    camera.WorldToScreenPoint(initsGameObjects[i].GetPosition()).y);
                if (rect.Contains(objectPos))
                {
                    if (!initsGameObjects[i].IsSelected)
                    {
                        initsGameObjects[i].Select();
                    }
                }
                else{
                    initsGameObjects[i].Deselect();
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            panel.SetActive(false);
            GetListSelected();
        }

    }
}