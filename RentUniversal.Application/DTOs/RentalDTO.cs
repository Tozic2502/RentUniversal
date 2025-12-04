using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Application.DTOs;

    public class RentalDTO
    {
        public ItemDTO? Item { get; set; }
        public string Id { get; set; } = "";
        public string UserId { get; set; } = "";
        public string ItemId { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartCondition { get; set; } = "";
        public string? ReturnCondition { get; set; }
        public double Price { get; set; }
    }

