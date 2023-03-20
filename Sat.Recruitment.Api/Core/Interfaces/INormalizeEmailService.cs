using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core.Interfaces
{
    public interface INormalizeEmailService
    {
        string NormalizeEmail(string email);
    }
}
