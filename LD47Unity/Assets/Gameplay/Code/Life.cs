using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.ld47.Gameplay {
    public class Life :  MonoBehaviour {

        #region Events

        public static event Action<Life> damaged;
        
        public static event Action<Life> died;

        public static event Action<Life> refreshed;

        #endregion

        #region Private Fields

        private List<Component> _components = new List<Component>();

        #endregion

        #region Serialized Fields

        [SerializeField] private int _defaultLives;

        [SerializeField] private UnityEvent _onDie;
        
        [SerializeField] private UnityEvent _onDamaged;

        #endregion

        #region Accessors

        public int maxLives => _defaultLives;
        
        public int damage { get; private set; }

        public int hp => maxLives - damage;

        public float lifeValue => hp * 1f / maxLives;

        public bool isAlive => hp > 0;

        #endregion

        #region Unity Events

        private void Awake() {
            _components = GetComponents<Component>().ToList();
        }

        #endregion

        #region Class Implementation

        public void Damage(int damage) {
            this.damage += damage;
            if (_onDamaged != null) {
                _onDamaged?.Invoke();
            }
            damaged?.Invoke(this);
            if (this.damage >= maxLives) {
                if (_onDie != null) {
                    _onDie?.Invoke();
                }
                died?.Invoke(this);
            }
        }

        public T GetLifeComponent<T>() {
            return _components.OfType<T>().FirstOrDefault();
        }

        public void SetDefaultLives(int newValue) {
            _defaultLives = newValue;
            refreshed?.Invoke(this);
        }

        #endregion
    }
}