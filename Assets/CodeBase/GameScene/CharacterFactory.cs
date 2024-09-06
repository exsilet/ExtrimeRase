using System;
using System.Collections.Generic;
using System.Linq;
using Hero;
using UI.ShopSkins;
using UI.UpgradeSkins;
using UnityEngine;

namespace GameScene
{
    [CreateAssetMenu(fileName = "CharacterFactory", menuName = "Gameplay/CharacterFactory")]
    public class CharacterFactory : ScriptableObject
    {
        [SerializeField] private List<CharacterSkin> _characterSkins;
        
        private CharacterSkin _characterSkin;
        
        public Player Get(CarSkins carSkins)
        {
            var instance = GetPrefabs(carSkins);
            return instance;
        }

        private Player GetPrefabs(CarSkins carSkins)
        {
            
            switch (carSkins)
            {
                case CarSkins.BuggyLvl0:
                    _characterSkin = GetSkin(CarSkins.BuggyLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BuggyLvl1:
                    _characterSkin = GetSkin(CarSkins.BuggyLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BuggyLvl2:
                    _characterSkin = GetSkin(CarSkins.BuggyLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BuggyLvl3:
                    _characterSkin = GetSkin(CarSkins.BuggyLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BuggyLvl4:
                    _characterSkin = GetSkin(CarSkins.BuggyLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeetleLvl0:
                    _characterSkin = GetSkin(CarSkins.BeetleLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeetleLvl1:
                    _characterSkin = GetSkin(CarSkins.BeetleLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeetleLvl2:
                    _characterSkin = GetSkin(CarSkins.BeetleLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeetleLvl3:
                    _characterSkin = GetSkin(CarSkins.BeetleLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeetleLvl4:
                    _characterSkin = GetSkin(CarSkins.BeetleLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.ForcedLvl0:
                    _characterSkin = GetSkin(CarSkins.ForcedLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.ForcedLvl1:
                    _characterSkin = GetSkin(CarSkins.ForcedLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.ForcedLvl2:
                    _characterSkin = GetSkin(CarSkins.ForcedLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.ForcedLvl3:
                    _characterSkin = GetSkin(CarSkins.ForcedLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.ForcedLvl4:
                    _characterSkin = GetSkin(CarSkins.ForcedLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeronaLvl0:
                    _characterSkin = GetSkin(CarSkins.BeronaLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeronaLvl1:
                    _characterSkin = GetSkin(CarSkins.BeronaLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeronaLvl2:
                    _characterSkin = GetSkin(CarSkins.BeronaLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeronaLvl3:
                    _characterSkin = GetSkin(CarSkins.BeronaLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BeronaLvl4:
                    _characterSkin = GetSkin(CarSkins.BeronaLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CamaraLvl0:
                    _characterSkin = GetSkin(CarSkins.CamaraLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CamaraLvl1:
                    _characterSkin = GetSkin(CarSkins.CamaraLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CamaraLvl2:
                    _characterSkin = GetSkin(CarSkins.CamaraLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CamaraLvl3:
                    _characterSkin = GetSkin(CarSkins.CamaraLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CamaraLvl4:
                    _characterSkin = GetSkin(CarSkins.CamaraLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.EmpuraLvl0:
                    _characterSkin = GetSkin(CarSkins.EmpuraLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.EmpuraLvl1:
                    _characterSkin = GetSkin(CarSkins.EmpuraLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.EmpuraLvl2:
                    _characterSkin = GetSkin(CarSkins.EmpuraLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.EmpuraLvl3:
                    _characterSkin = GetSkin(CarSkins.EmpuraLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.EmpuraLvl4:
                    _characterSkin = GetSkin(CarSkins.EmpuraLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.SecretLvl0:
                    _characterSkin = GetSkin(CarSkins.SecretLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.SecretLvl1:
                    _characterSkin = GetSkin(CarSkins.SecretLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.SecretLvl2:
                    _characterSkin = GetSkin(CarSkins.SecretLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.SecretLvl3:
                    _characterSkin = GetSkin(CarSkins.SecretLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.SecretLvl4:
                    _characterSkin = GetSkin(CarSkins.SecretLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.PudraLvl0:
                    _characterSkin = GetSkin(CarSkins.PudraLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.PudraLvl1:
                    _characterSkin = GetSkin(CarSkins.PudraLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.PudraLvl2:
                    _characterSkin = GetSkin(CarSkins.PudraLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.PudraLvl3:
                    _characterSkin = GetSkin(CarSkins.PudraLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.PudraLvl4:
                    _characterSkin = GetSkin(CarSkins.PudraLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.NurlatLvl0:
                    _characterSkin = GetSkin(CarSkins.NurlatLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.NurlatLvl1:
                    _characterSkin = GetSkin(CarSkins.NurlatLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.NurlatLvl2:
                    _characterSkin = GetSkin(CarSkins.NurlatLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.NurlatLvl3:
                    _characterSkin = GetSkin(CarSkins.NurlatLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.NurlatLvl4:
                    _characterSkin = GetSkin(CarSkins.NurlatLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CuramLvl0:
                    _characterSkin = GetSkin(CarSkins.CuramLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CuramLvl1:
                    _characterSkin = GetSkin(CarSkins.CuramLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CuramLvl2:
                    _characterSkin = GetSkin(CarSkins.CuramLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CuramLvl3:
                    _characterSkin = GetSkin(CarSkins.CuramLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.CuramLvl4:
                    _characterSkin = GetSkin(CarSkins.CuramLvl4);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BasDesLvl0:
                    _characterSkin = GetSkin(CarSkins.BasDesLvl0);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BasDesLvl1:
                    _characterSkin = GetSkin(CarSkins.BasDesLvl1);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BasDesLvl2:
                    _characterSkin = GetSkin(CarSkins.BasDesLvl2);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BasDesLvl3:
                    _characterSkin = GetSkin(CarSkins.BasDesLvl3);
                    return _characterSkin.GetComponent<Player>();
                case CarSkins.BasDesLvl4:
                    _characterSkin = GetSkin(CarSkins.BasDesLvl4);
                    return _characterSkin.GetComponent<Player>();
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(carSkins));
            }
        }

        private CharacterSkin GetSkin(CarSkins carSkins)
        {
            return _characterSkins.FirstOrDefault(skin => skin.CarSkinItem.SkinType == carSkins);
        }
    }
}