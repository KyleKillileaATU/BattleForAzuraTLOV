using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleForAzuraTLOV
{
    internal class EnemyObject
    {
        public int xposition, yposition, xleftposition, xrightposition, healthpoints, damagevar;
        public bool contact,takedamage;
        public EnemyObject(int xposition, int yposition, int healthpoints, int damagevar, bool contact)
        {
            this.xposition = xposition;
            this.yposition = yposition;
            this.healthpoints = healthpoints;
            this.damagevar = damagevar;
            this.contact = contact;
        }
        public bool PlayerCollide(int playerx, int playery)
        {
            if ((this.yposition+25) > (playery-35) && (this.yposition - 25) < (playery+35))
            {
                if (playerx >= (this.xleftposition-20) && playerx <= (this.xrightposition+20))
                {
                    this.contact = true;
                }
            }
            else
            {
                this.contact = false;
            }
            return this.contact;
        }
        public int EnemyDealDamage()
        {
            return damagevar;
        }
        public bool ProjectileCollide( int projx, int projy)
        {
            // -300 < / -325 >
            if (this.healthpoints > 0)
            {
                if (this.yposition > projy && (this.yposition - 25) < projy)
                {
                    if (projx >= this.xleftposition && projx <= this.xrightposition)
                    {
                        this.takedamage = true;
                    }
                }
                else
                {
                    this.takedamage = false;
                }
            }
            else if (this.healthpoints <= 0) 
            {
                this.takedamage = false;
            }
            return this.takedamage;
        }
        public int TakeDamage(int playerdamagevalue)
        {
            this.healthpoints  += -(playerdamagevalue);

            if (this.healthpoints >= 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }
    }
}
