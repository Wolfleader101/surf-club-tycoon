using System;
using Attributes;
using UnityEngine;

namespace ScriptableObjects.Managers
{
    [CreateAssetMenu(fileName = "Economy Manager", menuName = "Manager/Economy")]
    public class EconomyManager : SingletonScriptableObject<EconomyManager>
    {
        [SerializeField, FormattedPrice] private float startingMoney = 1000000f;
        public float totalMoney { get; set; }

        private void OnEnable()
        {
            totalMoney = startingMoney;
        }
    }
}