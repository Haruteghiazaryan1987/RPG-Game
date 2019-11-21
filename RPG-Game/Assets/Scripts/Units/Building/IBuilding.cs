using UnityEditor.SceneManagement;

namespace Assets.Scripts
{
    public interface IBuilding
    {
        BuildingType GetType();
        string GetName();
    }
}