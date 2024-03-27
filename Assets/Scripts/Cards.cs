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
        public float DamageXIncrease = 1;
        public bool bulletsMultiply = false;
        public int bulletsAdded = 0;
        public float HealthXIncrease = 1;
        public float healingFactor = 1;
        public float ManaXIncrease;
        public float ManaRegen;
        public bool leftArm = false;
        public bool rightArm = false;
        public bool fire = false;
        public bool lightning = false;
        public bool wind = false;
        public int totalFlares = 0;
        public int lightningJumps = 0;
        public bool cursed = false;
        public bool bigandcoolcard = false;
        public float spawnWeight = 1f;
    }

    public List<Card> cards = new List<Card>();

    [Header("Colors")]
    public Color normal;
    public Color cursed;
    public Color blessed;

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            Time.timeScale = 0f;
        }
    }
}
