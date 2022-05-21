using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
 public enum StatusEnums
    {
        [Display(Name ="Beklemede")]
        Wait,
        [Display(Name ="Devam Ediyor")]
        Continuing,
        [Display(Name ="Tamamlandı")]
        Completed



    }
}
