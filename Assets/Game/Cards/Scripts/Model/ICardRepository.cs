using System.Collections.Generic;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public interface ICardRepository : IRepository<ICardPresenter>
    {
        void Add(ICardPresenter item, CardPileType pileType);
        void Remove(ICardPresenter item, CardPileType pileType);
        List<ICardPresenter> Get(CardPileType pileType);    
    }
}
