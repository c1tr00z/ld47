using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Shotgun : MonoBehaviour {
        [SerializeField] private Bullet _bulletSrc;

        [SerializeField] private Transform _shootingPoint;

        [SerializeField] private int _count;

        [SerializeField] private float _sprayAngle;

        public void Shoot() {
            var currentAngle = transform.rotation.eulerAngles.y;
            var evenCount = _count - _count % 2;
            var deltaAngle = _sprayAngle / _count;
            var isEven = _count % 2 == 0;
            for (var i = 0; i < _count; i++) {
                var bullet = _bulletSrc.Clone();
                bullet.transform.position = _shootingPoint.position;
                var bulletTransform = bullet.transform;
                if (i == 0 && !isEven) {
                    bulletTransform.rotation = Quaternion.LookRotation(_shootingPoint.forward);
                } else {
                    var i2 = i;
                    if (!isEven) {
                        i2++;
                    }
                    var angle = deltaAngle * (i2 / 2) * (i2 % 2 == 0 ? 1 : -1);
                    bulletTransform.rotation = Quaternion.LookRotation(_shootingPoint.forward);
                    var angles = bullet.transform.rotation.eulerAngles;
                    angles.x = angles.z = 0;
                    angles.y += angle;
                    bulletTransform.rotation = Quaternion.Euler(angles); 
                }
            }
        }
    }
}