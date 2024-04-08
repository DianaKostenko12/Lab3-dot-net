using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dot_net.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public string Name { get; set; }
        public decimal InitialCost { get; set; }
        public int DepartmentId { get; set; }
        public int ResponsiblePersonId { get; set; }

        public Asset(int id, int inventoryNumber, string name, decimal initialCost, int departmentId,
            int responsiblePersonId)
        {
            Id = id;
            InventoryNumber = inventoryNumber;
            Name = name;
            InitialCost = initialCost;
            DepartmentId = departmentId;
            ResponsiblePersonId = responsiblePersonId;
        }
    }
}
