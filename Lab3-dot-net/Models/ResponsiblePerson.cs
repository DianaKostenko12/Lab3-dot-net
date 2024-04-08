using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dot_net.Models
{
    public class ResponsiblePerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public ResponsiblePerson(int id, string name, string surname, string phone)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Phone = phone;
        }
    }
}
