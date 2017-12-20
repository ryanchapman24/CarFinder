using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarFinder.Models
{
    public class DriveTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DriveTypes()
        {
            this.Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string DriveType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }
    }
}