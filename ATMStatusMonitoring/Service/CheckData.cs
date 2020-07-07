using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ATMStatusMonitoring.Service
{
    class CheckData
    {
        public int CheckIDATM(string number)
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.ATMs.Load();
                var atm = context.ATMs.FirstOrDefault(item => item.Number == number);
                if (atm != null)
                {
                    return atm.Id;
                }
            }
            return 0;
        }
    }
}
