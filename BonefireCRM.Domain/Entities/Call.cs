using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonefireCRM.Domain.Entities
{
    public class Call : Activity
    {
        public DateTime CallTime { get; set; }
        public string? Notes { get; set; }
    }
}
