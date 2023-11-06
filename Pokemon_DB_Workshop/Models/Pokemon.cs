using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pokemon
    {
        public required string Name { get; set; }
        public int MaxHp { get; set; }
        public int MinHp { get; set; }
    }
}
