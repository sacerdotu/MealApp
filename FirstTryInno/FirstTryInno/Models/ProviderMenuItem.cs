using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTryInno.Models
{
    public class ProviderMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public DateTime Date { get; set; }
    }
}
