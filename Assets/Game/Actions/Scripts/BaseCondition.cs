using System;
using UnityEngine;

namespace RogueLikeCardSystem
{
    [System.Serializable]
    public abstract class GameCondition
    {
        public AmountCompareEnum amountCompareEnum;

        public virtual bool Check()
        {
            return true;
        }

        public bool CheckValue(System.Object value1, System.Object value2, AmountCompareEnum amountCompareEnum)
        {
            if (amountCompareEnum != AmountCompareEnum.Equal &&
                amountCompareEnum != AmountCompareEnum.NotEqual)
            {
                try
                {
                    float v1 = Convert.ToSingle(value1);
                    float v2 = Convert.ToSingle(value2);

                    switch (amountCompareEnum)
                    {
                        case AmountCompareEnum.LessThan:
                            return v1 < v2;

                        case AmountCompareEnum.GreaterThan:
                            return v1 > v2;

                        case AmountCompareEnum.LessThanEqual:
                            return v1 <= v2;

                        case AmountCompareEnum.GreaterThanEqual:
                            return v1 >= v2;

                        case AmountCompareEnum.Enough:
                            return (v1 - v2) >= 0;
                    }
                }
                catch (InvalidCastException)
                {
                    return false;
                }
            }

            switch (amountCompareEnum)
            {
                case AmountCompareEnum.Equal:
                    return value1.Equals(value2);

                case AmountCompareEnum.NotEqual:
                    return !value1.Equals(value2);

                default:
                    return false;
            }
        }
        public enum AmountCompareEnum
        {
            Equal,
            NotEqual,
            LessThan,
            LessThanEqual,
            GreaterThan,
            GreaterThanEqual,
            Enough
        }
    }
}



