using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public abstract class CModelBase
    {
        public bool IsDeleted { get; set; }

        // NOTE: The Timestamp attribute specifies that this column will be included in the Where clause of Update and Delete.
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

}
