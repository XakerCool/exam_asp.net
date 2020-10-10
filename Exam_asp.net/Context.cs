using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Exam_asp.net.Models;

namespace Exam_asp.net
{
    public class Context:DbContext
    {
        public Context():base("name=DefaultConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<_Question> Questions { get; set; }
        public DbSet<UserResult> UsersResults { get; set; }
    }
}