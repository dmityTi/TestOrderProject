using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.DTOs.OrderAggregate
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int InvoiceNumber { get; set; }
        public BillingAddressItem BillingAddress { get; set; }
        public IEnumerable<ArticleItem> Articles { get; set; }
        public IEnumerable<PaymentItem> Payments { get; set; }
    }
}