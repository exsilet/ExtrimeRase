using System;
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

        private int _allMoney;
        private int _indexScene;
        private int _level;
        private int _scoreGameTrack;
        private int _skinLvl;

        public DataBase()
        {
            _allMoney = 200000;
            _indexScene = 1;
            _level = 1;

            _selectedCarSkin = CarSkins.EmpuraLvl0;

            _openCharacterSkins = new List<CarSkins>() { _selectedCarSkin };

            _slotCarsData = new List<SlotCarData>() { new(_selectedCarSkin, 0) };
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

        public int IndexScene
        {
            get => _indexScene;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _indexScene = value;
            }
        }
        
        public int LevelGame
        {
            get => _level;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _level = value;
            }
        }
        
        public int ScoreGameTrack
        {
            get => _scoreGameTrack;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _scoreGameTrack = value;
            }
        }

        [JsonConstructor]
        public DataBase(int money, CarSkins selectedCarSkin, List<CarSkins> openCharacterSkins, int indexScene, int levelGame, List<SlotCarData> openUpdateCharacterSkins)
        {
            Money = money;
            IndexScene = indexScene;
            LevelGame = levelGame;

            _selectedCarSkin = selectedCarSkin;

            _openCharacterSkins = new List<CarSkins>(openCharacterSkins);

            //_slotCarsData = new List<SlotCarData> { new(selectedCarSkin, SkinLevel) };
            _slotCarsData = new List<SlotCarData>(openUpdateCharacterSkins);
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

        public void AddNewCar(CarSkins skins, int lvlCar)
        {
            foreach (SlotCarData item in _slotCarsData)
            {
                if (item.CarSkins == skins)
                {
                    item.CarSkins = skins;
                    item.LvlCarUpgrade = lvlCar;
                    return;
                }
            }

            _slotCarsData.Add(new SlotCarData(skins, lvlCar));
        }

        public IEnumerable<SlotCarData> OpenUpdateCharacterSkins => _slotCarsData;
        public IEnumerable<CarSkins> OpenCharacterSkins => _openCharacterSkins;

        public void OpenCharacterSkin(CarSkins skin)
        {
            if (_openCharacterSkins.Contains(skin))
                throw new ArgumentException(nameof(skin));

            _openCharacterSkins.Add(skin);
        }
    }
}