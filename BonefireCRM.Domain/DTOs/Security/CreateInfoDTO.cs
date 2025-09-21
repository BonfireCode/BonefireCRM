using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonefireCRM.Domain.DTOs.Security
{
    public class CreateInfoDTO
    {
        public string NewEmail { get; init; } = string.Empty;

        public string NewPassword { get; init; } = string.Empty;

        public string OldPassword { get; init; } = string.Empty;
    }
}
