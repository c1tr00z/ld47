using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Shotgun : MonoBehaviour {
        [SerializeField] private Bullet _bulletSrc;

        [SerializeField] private Transform _shootingPoint;

        [SerializeField] private int _count;

        [SerializeField] private float _sprayAngle;
        
        [SerializeField] private float _coolDown;

        public int level = 1;

        private float _lastShotTime;

        [SerializeField]
        private List<Bullet> _bullets = new List<Bullet>();

        [SerializeField] private AudioSource _shootSource;

        [SerializeField] private ParticleSystem _shotParticles;
        
        [SerializeField] private Animation _shotAnimation;

        public void Shoot() {

            if (Time.time - _lastShotTime < _coolDown) {
                return;
            }

            _lastShotTime = Time.time;
            
            var currentAngle = transform.rotation.eulerAngles.y;
            var evenCount = _count - _count % 2;
            var deltaAngle = _sprayAngle / _count;
            var isEven = _count % 2 == 0;

            var launchable = _bullets.Where(b => b.isLaunchable).ToList();
            launchable.Limit(_count);

            while (launchable.Count < _count) {
                launchable.Add(MakeBullet());
            }
            
            _bullets.AddRange(launchable.Where(b => !_bullets.Contains(b)));
            
            for (var i = 0; i < launchable.Count; i++) {
                var bullet = launchable[i];
                bullet.damage = 2 * level;
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
                bullet.Launch(20);
            }
            _shotParticles.Play();
            _shootSource.Play();
            _shotAnimation.Play();
        }

        private Bullet MakeBullet() {
            return _bulletSrc.Clone();
        }
    }
}