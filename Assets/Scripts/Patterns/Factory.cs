using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Factory
{
    public abstract class Ability
    {
        public abstract void Process();
    }

    public class ShootAbility : Ability
    {
        private PlayerShoot ps;

        public override void Process()
        {
            //Do shooting
            ps.Shoot();
        }
    }

    public class ShieldAbility : Ability
    {
        private PlayerShoot ps;
        public override void Process()
        {
            //Do shield
            ps.Shield();
        }
    }

    public class AbilityFactory
    {
        public Ability GetAbility(string abilityType)
        {
            switch(abilityType)
            {
                case "shoot":
                    return new ShootAbility();
                case "shield":
                    return new ShieldAbility();
                default:
                    return null;
            }
        }
    }

}
