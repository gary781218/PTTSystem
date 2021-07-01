using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    class AccountCreateValid
    {
        public bool account { get; set; } = false;
        public bool pwd { get; set; } = false;
        public bool pwdDoubleCheck { get; set; } = false;
        public bool familyNameCheck { get; set; } = false;
        public bool lastNameCheck { get; set; } = false;
        public bool birthday { get; set; } = false;
        public bool mail { get; set; } = false;
    }
}
