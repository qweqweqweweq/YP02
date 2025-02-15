using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Mail { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
