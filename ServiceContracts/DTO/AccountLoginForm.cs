using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class AccountLoginForm
    {
        public CustomerLogin Login { get; set; }  = new CustomerLogin();
        public CustomerRegister Register { get; set; } = new CustomerRegister();
    }
}
