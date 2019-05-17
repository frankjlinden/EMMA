using EDennis.AspNetCore.Base.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CT.DDS.EMMA.Models
{

    public class SyncConfig : BaseEntity, IHasIntegerId,IEFCoreTemporalModel
    {
        public int Id { get; set; }

        [Required]
        public string ConfigName { get; set; }

        [MaxLength(255)]
        public string ConnectionString { get; set; }

        [MaxLength(150)]
        public string ViewName { get; set; }
       
        public int DataRateDays { get; set; }
        public DateTime DataRateOrigin { get; set; }
        public string DateKey { get; set; }



    }


}
