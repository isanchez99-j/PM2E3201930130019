using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Proyecto3.Models
{
    public class Recibo
    {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public byte[] foto_recibo { get; set; }
        public double monto { get; set; }
        public string fecha { get; set; }
        public string descripcion { get; set; }
    }
}
