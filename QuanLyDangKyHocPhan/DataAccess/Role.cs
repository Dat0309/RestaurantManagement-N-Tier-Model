﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Path { get; set; }
        public string Notes { get; set; }
    }
}
