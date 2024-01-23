using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALL
{
    public class MockUmovieContext : UmovieContext
    {
        public MockUmovieContext(DbContextOptions options) : base(options)
        {
            InitializeMockData();
        }

        private void InitializeMockData()
        {
            // Get real data from UmovieContext
            var realData = Movies.ToList(); // Assuming Movies is a DbSet in your UmovieContext

            // Add the real data to the in-memory database
            Movies.AddRange(realData);

            // Save changes to persist the data in the in-memory database
            SaveChanges();
        }       
    }
}
