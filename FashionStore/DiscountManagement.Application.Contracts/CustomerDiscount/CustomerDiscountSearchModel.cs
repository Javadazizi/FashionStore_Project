﻿namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long ProductId { get; set; }
    }
}
