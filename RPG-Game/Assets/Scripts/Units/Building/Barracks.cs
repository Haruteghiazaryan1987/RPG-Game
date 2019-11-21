namespace Assets.Scripts
{
    public class Barracks : Building
    {
        public Barracks(BuildingType type) : base(type){
            buildingType = BuildingType.Baracks;
        }
    }
}