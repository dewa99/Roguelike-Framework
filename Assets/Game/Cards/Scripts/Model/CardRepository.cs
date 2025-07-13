using System.Collections.Generic;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public class CardRepository : ICardRepository
    {
        public List<ICardPresenter> HandPile { get; set; }
        public List<ICardPresenter> DrawPile { get; set; }
        public List<ICardPresenter> DiscardPile { get; set; }
        public CardRepository()
        {
            Repository.Repository.CardRepository = this;
        }

        public void Add(ICardPresenter item)
        {
            
        }

        public void Add(ICardPresenter item, CardPileType pileType)
        {
            switch (pileType)
            {
                case CardPileType.Draw:
                    DrawPile.Add(item);
                    break;
                case CardPileType.Hand:
                    HandPile.Add(item);
                    break;
                case CardPileType.Discard:
                    DiscardPile.Add(item);
                    break;
                default:
                    break;
            }
        }

        public void Remove(ICardPresenter item, CardPileType pileType)
        {
            switch (pileType)
            {
                case CardPileType.Draw:
                    DrawPile.Remove(item);
                    break;
                case CardPileType.Hand:
                    HandPile.Remove(item);
                    break;
                case CardPileType.Discard:
                    DiscardPile.Remove(item);
                    break;
                default:
                    break;
            }
        }

        public bool Remove(ICardPresenter item )
        {
            throw new System.NotImplementedException();
        }

        public ICardPresenter Get()
        {
            throw new System.NotImplementedException();
        }

        public List<ICardPresenter> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveAll()
        {
            throw new System.NotImplementedException();
        }
    }

    public enum CardPileType
    {
        Draw,
        Hand,
        Discard
    }
}
