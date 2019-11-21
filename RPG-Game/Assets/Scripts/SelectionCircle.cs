using UnityEngine;

namespace Assets.Scripts
{
    public class SelectionCircle : MonoBehaviour, ISelectionIndicationWithShape
    {
        public GameObject SelectionShape => gameObject;



        public void Deselect()
        {
            SelectionShape.SetActive(false);
        }

        public void Init()
        {
            gameObject.SetActive(false);
        }

        public void Select()
        {
            SelectionShape.SetActive(true);
        }
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
