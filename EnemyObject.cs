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
        public bool PlayerCollide(int playerxleft, int playerxright, int playery)
        {
            if (playery <= this.yposition && playery >= this.yposition-50) 
            {
                if (playerxleft >= this.xleftposition && playerxright <= this.xrightposition)
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
        public bool ProjectileCollide( int projx, int projy)
        {
            
            if (this.yposition >= projy && this.yposition - 25 <= projy)
            {
                if (this.xleftposition >= projx && this.xrightposition <= projx)
                {
                    this.takedamage = true;
                }
            }
            else
            {
                this.takedamage = false;
            }
            return this.takedamage;
        }
        public int TakeDamage(int playerdamagevalue)
        {
            this.healthpoints = this.healthpoints - playerdamagevalue;

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
