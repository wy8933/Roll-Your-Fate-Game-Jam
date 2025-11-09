using UnityEngine;
namespace Minigame
{
    public class RepairAntennaMinigame : MinigameController
    {
        public RepairAntenna repairAntenna;

        protected override void Update()
        {
            base.Update();

            if (repairAntenna != null) 
            {
                if (repairAntenna.IsCompelete()) 
                {
                    GameClear();
                }
            }
        }

        public override void OnRightClick()
        {
            gameObject.SetActive(false);
        }
    }
}