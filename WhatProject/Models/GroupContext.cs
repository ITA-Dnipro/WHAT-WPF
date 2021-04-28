using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WhatProject.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    class GroupContext : DbContext
    {
        public GroupContext()
            : base("mysqlCon")
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
    }
}
