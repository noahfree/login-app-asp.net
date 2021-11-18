using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exploration3.Models
{
    public class UserModel
    {

        public string ID;
        public int tries;
        public int score;
        public TimeSpan time;
        [StringLength(20, MinimumLength = 4)]
        [Required]
        public string Username { get; set; }

        [StringLength(20, MinimumLength = 6)]
        [Required]
        public string Password { get; set; }

    }
}
