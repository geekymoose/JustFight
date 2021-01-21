namespace WeaponSystem
{
    public class WeaponFireInstant : WeaponFire
    {
        public void Apply(WeaponController controller)
        {
            if(controller.CanFire())
            {
                controller.DoFire();
            }
        }
    }
}
