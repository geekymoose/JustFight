using UnityEngine;

namespace WeaponSystem
{
    public class ShotPowerEffectDamage : ShotPowerEffect
    {
        public override void Apply(float power, ShotController controller)
        {
            controller.DamageEfficiencyInPercent = power;
        }
    }
}
