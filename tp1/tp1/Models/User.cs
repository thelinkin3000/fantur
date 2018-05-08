﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tp1.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Post> Posts { get; set; }
    }
}