using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Dto
{
    public class Player
    {
        public string Address { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int WinsOverLosses { get { return Wins - Losses; } }

    }
}
