using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Data;
using src.Model;

namespace src.Repos
{
    class ZodiacRepository
    {
        private readonly DatabaseConnection dbConn;

        public ZodiacRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }




    }
}
