using System.Collections.Generic;
using System.Linq;
using SO;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : MonoBehaviour
    {
        private const string StaticDataHeroesPath = "StaticData/SOHeroes";
        
        private Dictionary<HeroesTypeID, HeroesData> _car;

        public void Load()
        {
            _car = Resources
                .LoadAll<HeroesData>(StaticDataHeroesPath)
                .ToDictionary(x => x.HeroesTypeID, x => x);
        }

        public HeroesData ForCars(HeroesTypeID typeID) =>
            _car.TryGetValue(typeID, out HeroesData staticData) 
                ? staticData 
                : null;
    }
}