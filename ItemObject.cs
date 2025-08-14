using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleForAzuraTLOV
{
    internal class ItemObject
    {
        public int xposition, yposition, xleftposition, xrightposition, type;
        public bool contact;
        public ItemObject(int xposition, int yposition,int type, bool contact)
        {
            this.xposition = xposition;
            this.yposition = yposition;
            this.type = type;
            this.contact = contact;
        }
        public bool PlayerCollide(int playerx, int playery)
        {
            if ((this.yposition + 25) > (playery - 35) && (this.yposition - 25) < (playery + 35))
            {
                if (playerx >= (this.xleftposition - 20) && playerx <= (this.xrightposition + 20))
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
        public int DroppedType()
        {
            return type;
        }
    }
}
