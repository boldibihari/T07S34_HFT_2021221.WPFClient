﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalChampionship.Data.Models
{
    public class TokenViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
