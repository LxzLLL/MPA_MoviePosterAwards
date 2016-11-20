using MPA_MoviePosterAwards.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieManager.GetMovie("1307847");
        }
    }
}
