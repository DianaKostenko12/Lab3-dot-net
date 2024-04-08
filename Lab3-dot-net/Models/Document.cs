using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dot_net.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public Document(int id, string documentName)
        {
            Id = id;
            DocumentName = documentName;
        }
    }
}
