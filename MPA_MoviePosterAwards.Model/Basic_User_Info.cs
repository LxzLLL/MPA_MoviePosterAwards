using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Model
{
    public class Basic_User_Info
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public string Avatar { get; set; }
        public string Cover { get; set; }
        public bool Limit { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Change_Time { get; set; }
    }
}
