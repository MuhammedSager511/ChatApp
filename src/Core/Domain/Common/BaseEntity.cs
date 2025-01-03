﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateOnly CreateDate =>DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly ModifiedData { get; set; }
        public bool IsActive { get; set; }


    }
}
