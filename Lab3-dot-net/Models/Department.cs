using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dot_net.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public Department(int id, string departmentName)
        {
            Id = id;
            DepartmentName = departmentName;
        }
    }
}
