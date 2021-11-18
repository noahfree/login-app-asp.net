using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exploration3.Models
{
    public class TestModel
    {
        public int[] numbers;

        [Required]
        public int input1 { get; set; }

        [Required]
        public int input2 { get; set; }

        [Required]
        public int input3 { get; set; }

        [Required]
        public int input4 { get; set; }

        [Required]
        public int input5 { get; set; }
    }
}
