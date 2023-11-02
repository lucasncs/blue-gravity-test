using System;
using Items;
using UnityEngine;

namespace Money
{
    [Serializable]
    public struct Price
    {
        [SerializeField] private CurrencyItem _type;
        [SerializeField] private int _value;

        public CurrencyItem Type => _type;
        public int Value => _value;

        public Price(CurrencyItem type, int value)
        {
            _type = type;
            _value = value;
        }

        public bool IsValid()
        {
            return _type != null && _value != 0;
        }

        public bool IsFree()
        {
            return IsValid() && _value < 0;
        }

        public static Price operator *(Price a, float factor)
        {
            return new Price(a._type, (int)(a._value * factor));
        }
    }
}