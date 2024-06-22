using INSPECTORV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary.persistance
{// وراثة من دي بي ست dbset
    public class LibraryContext : DbContext
    { // نشتغل  entity frame work   

        //dbcontext مش موجود من الاول نضغط عليه  ومن ثم    " alt و enter "
        public LibraryContext(DbContextOptions<LibraryContext> options) 
            : base(options)
        {
        }
        public DbSet<Inspector> Inspectors { get; set; }//درنالها refrence
        public DbSet<Teacher> Teachers { get; set; }


        
    }

}
