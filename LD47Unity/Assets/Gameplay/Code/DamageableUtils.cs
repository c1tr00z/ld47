namespace c1tr00z.ld47.Gameplay {
    public static class DamageableUtils {
        public static void Damage(this IDamageable damageable, int damage) {
            damageable.GetLife().Damage(damage);
        }
    }
}