using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
   public enum PriotrityEnums
    {
        [Display(Name ="Hemen Yap")]
        Do,
        [Display(Name = " Zaman Planla")]
        Schedule,
        [Display(Name = "Daha Sonra Yap")]
        Delegate,
        [Display(Name = "İşi Devret")]
        Delete

    }
}
