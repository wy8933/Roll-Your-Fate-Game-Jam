using System.Collections.Generic;
using InventorySystem;
using NUnit.Framework;
using Template;
using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
    public class GameManager : SingletonBehavior<GameManager>
    {
        public SettingSO setting;
        public ItemSO oxygenContainerSO;
        public List<AudioClip> BasementMusic = new List<AudioClip>();
        public List<AudioClip> expeditionMusics = new List<AudioClip>();
        public List<PlayableAsset> SectorTransitionTimeline = new List<PlayableAsset>();
        public Transform respawnPoint;
        public PlayableDirector director;

        protected override void Awake()
        {
            base.Awake();
        }

        public void SetOff(int SectorID)
        {
            director.Play(SectorTransitionTimeline[SectorID]);
            AudioManager.Instance.PlayMusic(expeditionMusics[Random.Range(0, expeditionMusics.Count)]);
            Player.Instance.SetOxygen(setting.GameSetting.initialOxygen + setting.GameSetting.oxygenPerContainer * Inventory.Instance.ItemCount(oxygenContainerSO));
            Player.Instance.isConsumingOxygen = true;
        }

        public void BackHome()
        {
            AudioManager.Instance.PlayMusic(BasementMusic[Random.Range(0, BasementMusic.Count)]);
            Player.Instance.isConsumingOxygen = false;
        }

        public void RunOutOxygen()
        {
            Player.Instance.transform.SetPositionAndRotation(respawnPoint.position, respawnPoint.rotation);
        }
    }
}