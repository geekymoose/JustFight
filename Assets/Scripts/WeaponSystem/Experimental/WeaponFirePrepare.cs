namespace WeaponSystem
{
    public class WeaponFirePrepare : WeaponFire
    {
        public void Apply(WeaponController controller)
        {
            controller.DoPrepareFire();
        }
    }
}
