// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.SaleOrders
{
    public class CreateSaleOrderViewModel
    {
        [Required]
        public int? StoreId { get; set; }

        [Required]
        public DateTime? IssueDate { get; set; }

        [MaxLength(255)]
        public string AdditionalInformation { get; set; }

        public SaleOrder MapTo(SaleOrder saleOrder)
        {
            saleOrder.StoreId = StoreId.Value;
            saleOrder.IssueDate = IssueDate.Value.Date;
            saleOrder.AdditionalInformation = AdditionalInformation;

            return saleOrder;
        }
    }
}
