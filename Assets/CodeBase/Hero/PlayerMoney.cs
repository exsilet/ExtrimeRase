using System;
using SaveData;

namespace Hero
{
    public class PlayerMoney
    {
        public event Action<int> CoinsChanged;

        private IPersistentData _persistentData;

        public PlayerMoney(IPersistentData persistentData)
            => _persistentData = persistentData;

        public void AddCoins(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));

            _persistentData.DataBase.Money += coins;

            CoinsChanged?.Invoke(_persistentData.DataBase.Money);
        }

        public int GetCurrentCoins() => _persistentData.DataBase.Money;

        public bool IsEnough(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));

            return _persistentData.DataBase.Money >= coins;
        }

        public void Spend(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));

            _persistentData.DataBase.Money -= coins;

            CoinsChanged?.Invoke(_persistentData.DataBase.Money);
        }
    }
}