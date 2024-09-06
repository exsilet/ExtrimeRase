using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "HeroesData", menuName = "HeroesData/Hero")]
    public class HeroesData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _iconPlayer;
        [SerializeField] private GameObject _carPrefab;
        [SerializeField] private HeroesTypeID _heroesType;

        public HeroesTypeID HeroesTypeID => _heroesType;
        public string Name => _name;
        public Sprite IconPlayer => _iconPlayer;

        public GameObject CarPrefab => _carPrefab;
    }
}