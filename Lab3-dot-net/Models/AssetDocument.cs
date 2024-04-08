using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dot_net.Models
{
    public class AssetDocument
    {
        public int AssetId { get; set; }
        public int DocumentId { get; set; }
        public DateTime Date { get; set; }

        public AssetDocument(int assetId, int documentId, DateTime date)
        {
            AssetId = assetId;
            DocumentId = documentId;
            Date = date;
        }
    }
}
