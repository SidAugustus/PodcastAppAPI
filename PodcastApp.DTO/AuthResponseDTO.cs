using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.DTO
{
    public record AuthResponseDTO
    (
        [SmartRequired]
        string AccessToken,

        [SmartRequired]
        string RefreshToken,

        [SmartRequired]
        int UserId,

        [SmartRequired]
        int Role
    );
}

