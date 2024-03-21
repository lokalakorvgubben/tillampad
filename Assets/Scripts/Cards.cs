using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsHolder : MonoBehaviour
{
    [System.Serializable]
    public class Card
    {
        public string Name;
        public string Description;
        public float DamageIncrease;
        public int bulletsAdded;
        public float HealthIncrease;
        public float healingFactor;
        public bool leftArm;
        public bool rightArm;
        public bool fire;
        public bool lightning;
        public bool wind;
        public int totalFlares;
        public int lightningJumps;
        public bool cursed;
        public bool bigandcoolcard;
        public float spawnWeight = 1f;
    }

    public List<Card> cards = new List<Card>();

    [Header("Colors")]
    public Color normal;
    public Color cursed;
    public Color blessed;
}
