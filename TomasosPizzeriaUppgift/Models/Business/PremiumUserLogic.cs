using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeriaUppgift.Models.Business
{
    public class PremiumUserLogic
    {
        public decimal Totalprice { get; set; }

        public int BonusPoints { get; set; }

        public void BonusUserPrice(int price)
        {
            Totalprice = price * 0.20m;
        }

        public int GetTotalPrice()
        {
            return Convert.ToInt32(Totalprice);
        }
        public void AddBonusPoints()
        {
            BonusPoints += 10;
        }
        public int GetBonusPoints()
        {
            return BonusPoints;
        }
    }
}
