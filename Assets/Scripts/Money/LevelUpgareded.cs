using UI;
using UnityEngine;

namespace Money
{
    public class LevelUpgareded : TriggerAction
    {
        protected override void ExecuteTriggerActionStay()
        {
            
        }

        protected override void ExecuteTriggerActionEnter()
        {
            UIPageController.Instance.UpgarededUIEnter();
            UIPageController.Instance.IsEnoughMoneyF();
        }

        protected override void ExecuteTriggerActionExit()
        {
            UIPageController.Instance.UpgarededUIExit();
        }
    }
}