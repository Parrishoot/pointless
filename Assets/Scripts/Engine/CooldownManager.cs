using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{

    private List<Cooldown> cooldowns = new List<Cooldown>();

    private class Cooldown
    {
        public int enumVal;
        public float time;

        public Cooldown(int enumVal, float time)
        {
            this.enumVal = enumVal;
            this.time = time;
        }
    }

    public void SetCooldown(int newEnumVal, float time)
    {
        cooldowns.Add(new Cooldown(newEnumVal, time));
    }

    public float GetCooldown(int newEnumVal)
    {
        foreach (Cooldown cooldown in cooldowns)
        {
            if (cooldown.enumVal.Equals(newEnumVal))
            {
                return cooldown.time;
            }
        }

        return 0f;
    }

    public bool IsOnCooldown(int newEnumVal)
    {
        return !GetCooldown(newEnumVal).Equals(0f);
    }

    void Update()
    {

        List<Cooldown> newCooldowns = new List<Cooldown>();

        foreach (Cooldown cooldown in cooldowns)
        {
            cooldown.time -= Time.deltaTime;
            if (cooldown.time >= 0)
            {
                newCooldowns.Add(cooldown);
            }
        }

        cooldowns = newCooldowns;
    }
}