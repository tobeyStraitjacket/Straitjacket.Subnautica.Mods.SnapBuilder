﻿using System.Collections.Generic;
using UnityEngine;

namespace Straitjacket.Subnautica.Mods.SnapBuilder
{
    internal class ColliderCache : MonoBehaviour
    {
        private static ColliderCache main;
        public static ColliderCache Main => main == null
            ? new GameObject("ColliderCache").AddComponent<ColliderCache>()
            : main;

        private Dictionary<Collider, ColliderRecord> records = new Dictionary<Collider, ColliderRecord>();

        /// <summary>
        /// The active <see cref="ColliderRecord"/>
        /// </summary>
        public ColliderRecord Record { get; private set; }

        /// <summary>
        /// Returns the initialises the <see cref="Collider"/> for a given <see cref="Collider"/>
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public ColliderRecord GetRecord(Collider collider) => Record = records.TryGetValue(collider, out ColliderRecord record)
            ? record
            : records[collider] = new ColliderRecord(collider);

        public void RevertAll()
        {
            Record = null;
            foreach (var record in records.Values)
            {
                record.Revert();
            }
        }

        private void Awake()
        {
            if (main != null && main != this)
            {
                Destroy(this);
            }
            else
            {
                main = this;
                transform.SetParent(AimTransform.Main.transform, false);
            }
        }
    }
}
