using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ELibrary.Models;

    public class ELibraryContext : DbContext
    {
        public ELibraryContext (DbContextOptions<ELibraryContext> options)
            : base(options)
        {
        }

        public DbSet<ELibrary.Models.Book> Book { get; set; }
    }
