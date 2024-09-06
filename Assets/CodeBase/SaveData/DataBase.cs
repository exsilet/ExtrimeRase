﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI.ShopSkins;

namespace SaveData
{
    [Serializable]
    public class DataBase
    {
        private CarSkins _selectedCarSkin;
        private SlotCarData _slotCarData;

        private List<CarSkins> _openCharacterSkins;
        private List<SlotCarData> _slotCarsData;
        private int[] _carUpgradeLevels;

        private int _allMoney;

        public DataBase()
        {
            _allMoney = 200000;

            _selectedCarSkin = CarSkins.EmpuraLvl0;

            _openCharacterSkins = new List<CarSkins>() { _selectedCarSkin };

            _slotCarsData = new List<SlotCarData>() { new SlotCarData(_selectedCarSkin, 0) };
        }

        public int Money
        {
            get => _allMoney;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _allMoney = value;
            }
        }

        [JsonConstructor]
        public DataBase(int money, CarSkins selectedCarSkin, List<CarSkins> openCharacterSkins, int skinLvl)
        {
            Money = money;

            _selectedCarSkin = selectedCarSkin;

            _openCharacterSkins = new List<CarSkins>(openCharacterSkins);

            _slotCarsData = new List<SlotCarData> { new(selectedCarSkin, skinLvl) };
        }

        public CarSkins SelectedCarSkin
        {
            get => _selectedCarSkin;
            set
            {
                if (_openCharacterSkins.Contains(value) == false)
                    throw new ArgumentException(nameof(value));

                _selectedCarSkin = value;
            }
        }

        public void AddNewCar(CarSkins skins, int index)
        {
            foreach (var item in _slotCarsData)
            {
                if (item.CarSkins == skins)
                {
                    item.CarSkins = skins;
                    item.LvlCarUpgrade = index;
                    return;
                }
            }

            _slotCarsData.Add(new SlotCarData(skins, index));
        }

        public IEnumerable<CarSkins> OpenCharacterSkins => _openCharacterSkins;

        public void OpenCharacterSkin(CarSkins skin)
        {
            if (_openCharacterSkins.Contains(skin))
                throw new ArgumentException(nameof(skin));

            _openCharacterSkins.Add(skin);
        }
    }
}