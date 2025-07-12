namespace RoguelikeCardSystem.Game.Resources.Model
{
    public enum ResourceType
    {
        Crystal = 0,
        Gold = 1,
        Diamond = 2
    }
    public class ResourcesModel
    {
        public int Crystal { get; private set; }
        public void UpdateResource(ResourceType type, int amount)
        {
            Crystal = amount;
        }

    }


}