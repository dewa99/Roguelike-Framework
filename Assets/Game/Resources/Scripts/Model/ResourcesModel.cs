using System.Collections.Generic;
using UniRx;

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
        public ReactiveDictionary<ResourceType, int> Resources ;

        public ResourcesModel(Dictionary<ResourceType, int> resources)
        {
            Resources = new(resources);
        }
        public void UpdateResource(ResourceType type, int amount)
        {
            Resources[type] = amount;
        }
        
        public int GetResource(ResourceType type)
        {
            return Resources.TryGetValue(type, out var value) ? value : 0;
        }

    }


}