using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI.ShopSkins;

namespace SaveData
{
    [Serializable]
    public class DataBase
    {
        private CharacterSkins _selectedCharacterSkin;
        
        private List<CharacterSkins> _openCharacterSkins;

        private int _allMoney;

        private DataBase()
        {
            _allMoney = 20000;

            _selectedCharacterSkin = CharacterSkins.Empura;

            _openCharacterSkins = new List<CharacterSkins>() { _selectedCharacterSkin };
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
        public DataBase(int money, CharacterSkins selectedCharacterSkin, List<CharacterSkins> openCharacterSkins)
        {
            Money = money;

            _selectedCharacterSkin = selectedCharacterSkin;

            _openCharacterSkins = new List<CharacterSkins>(openCharacterSkins);
        }
        
        public CharacterSkins SelectedCharacterSkin
        {
            get => _selectedCharacterSkin;
            set
            {
                if (_openCharacterSkins.Contains(value) == false)
                    throw new ArgumentException(nameof(value));

                _selectedCharacterSkin = value;
            }
        }

        public IEnumerable<CharacterSkins> OpenCharacterSkins => _openCharacterSkins;

        public void OpenCharacterSkin(CharacterSkins skin)
        {
            if(_openCharacterSkins.Contains(skin))
                throw new ArgumentException(nameof(skin));

            _openCharacterSkins.Add(skin);
        }
    }
}