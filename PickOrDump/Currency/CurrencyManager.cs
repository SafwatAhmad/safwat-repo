using System;
using UnityEngine;
using System.Collections.Generic;

namespace Safwat.ToolKit.Currency
{
    enum CurrencyType
    {
        gold,
        ruby
    }

    internal class CurrencyManager : MonoBehaviour
    {
        internal static CurrencyManager Instance { get; set; }

        private Dictionary<CurrencyType, Currency> currenciesDictionary = new();

        [SerializeField] private Currency[] currencies;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                foreach (var c in currencies)
                    if (currenciesDictionary.TryAdd(c.Type, c) is false)
                        Debug.LogError("Currency " + c.Type + " already exists!");
            }
            else
                Destroy(Instance.gameObject);
        }

        internal bool CanDeduct(CurrencyType name, int amount) => GetCurrencyAmount(name) - amount >= 0;

        internal int GetCurrencyAmount(CurrencyType type) => currenciesDictionary[type].Amount;

        internal void AddCurrencyAmount(CurrencyType type, int amount) => currenciesDictionary[type].Amount += amount;

        internal void DeductCurrencyAmount(CurrencyType type, int amount) => currenciesDictionary[type].Amount -= amount;
    }

    [Serializable]
    internal class Currency
    {
        [SerializeField] private string name;
        [SerializeField] private CurrencyType type;
        [SerializeField] private int startAmount;

        internal CurrencyType Type => type;

        internal int Amount
        {
            get => PlayerPrefs.GetInt("AmountOf_" + type, startAmount);
            set => PlayerPrefs.SetInt("AmountOf_" + type, value);
        }
    }
}