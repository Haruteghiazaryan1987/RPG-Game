using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

namespace Assets.Scripts.Archive
{
    class SelectionSystem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private Camera camera;
        [SerializeField] private RectTransform selectionRect;
        [SerializeField] private LayerMask unitLayerMask;
        [SerializeField] private LayerMask buildingLayerMask;
        private Vector3 startPos, endPos;
        private Vector3? wStartPos, wEndPos;
        private List<ISelectable> selectedObjects;
        private Rect rect;

        [SerializeField] private Transform corner1;
        [SerializeField] private Transform corner2;
        [SerializeField] private Transform corner3;
        [SerializeField] private Transform corner4;
        private Bounds SelectionBounds
        {
            get
            {
                //if (wStartPos != null && wEndPos != null)
                //{
                    Bounds bounds = new Bounds();
                    bounds.SetMinMax((Vector3)wStartPos, (Vector3)wEndPos);
                    return bounds;
                //}
                //return Exception;
            }
        }
        private void Awake()
        {
            selectedObjects = new List<ISelectable>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            startPos = eventData.position;
            wStartPos = getGroundPoint(startPos);
            rect = new Rect();

            if (!Input.GetKeyDown(KeyCode.Space))
            {
                DeselectAll();
            }
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(eventData.position),out hit))
            {
                ISelectable unit = hit.collider.GetComponent<ISelectable>();
                if (unit != null)
                {
                    selectedObjects.Add(hit.collider.GetComponent<ISelectable>());
                    unit.Select();
                }
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            selectionRect.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            endPos = eventData.position;
            wEndPos = getGroundPoint(endPos);
            

            #region ***Rect drawing***
            // Getting rect values
            rect.xMin = Mathf.Min(endPos.x, startPos.x);
            rect.xMax = Mathf.Max(endPos.x, startPos.x);
            rect.yMin = Mathf.Min(endPos.y, startPos.y);
            rect.yMax = Mathf.Max(endPos.y, startPos.y);

            // Setting rect values
            selectionRect.offsetMin = rect.min;
            selectionRect.offsetMax = rect.max;
            #endregion

            if(wStartPos != null && wEndPos != null)
            {
                
                 // Checking shift key press
                 if (!Input.GetKeyDown(KeyCode.LeftShift))
                 {
                     DeselectAll();
                 }

                // If there are any units select them, otherwise select buildings
                Vector3 absExtents = new Vector3(Mathf.Abs(SelectionBounds.extents.x), Mathf.Abs(SelectionBounds.extents.y), Mathf.Abs(SelectionBounds.extents.z));
                Collider[] selectedUnits = Physics.OverlapBox(SelectionBounds.center, absExtents, Quaternion.identity, unitLayerMask);
                selectedObjects.AddRange
                (
                   ( selectedUnits != null && selectedUnits.Length > 0 
                    ? selectedUnits
                    : Physics.OverlapBox(SelectionBounds.center, absExtents, Quaternion.identity, buildingLayerMask)).Select(unit => unit.GetComponent<ISelectable>())
                );
                Debug.Log(selectedObjects.Count);

                selectedObjects.ForEach(obj => obj.Select());
            

                #region ***Test***

                corner1.position = ((Vector3)wStartPos);
				corner2.position = ((Vector3)wEndPos);

				var corner3Pos = ((Vector3)wStartPos);
                corner3Pos.x = ((Vector3)wEndPos).x;
				corner3.position = corner3Pos;

				var corner4Pos = (Vector3)wStartPos;
				corner4Pos.z = ((Vector3)wEndPos).z;
				corner4.position = corner4Pos;


                #endregion
            }
        }



        public void OnPointerUp(PointerEventData eventData)
        {
           selectionRect.gameObject.SetActive(false);
        }

        private Vector3? getGroundPoint(Vector3 mousePos)
        {
            Ray ray = camera.ScreenPointToRay(mousePos);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if(groundPlane.Raycast(ray, out rayDistance))
            {
                return ray.GetPoint(rayDistance);
            }
            return null;
        }

        private void DeselectAll()
        {
            selectedObjects.ForEach(obj => obj.Deselect());
            selectedObjects.Clear();
        }
    }
}
