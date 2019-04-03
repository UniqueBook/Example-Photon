using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateMysql.Model
{
    public class User
    {
        public virtual string Name { get; set; }
        public virtual string Account { get; set; }
        public virtual string Pwd { get; set; }
        public virtual int Id { get; set; }


    }
}
