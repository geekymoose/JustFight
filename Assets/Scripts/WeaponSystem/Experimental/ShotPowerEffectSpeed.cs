using UnityEngine;

namespace WeaponSystem
{
    public class ShotPowerEffectSpeed : ShotPowerEffect
    {
        public override void Apply(float power, ShotController controller)
        {
            controller.MovementEfficiencyInPercent = power;
        }
    }
}
