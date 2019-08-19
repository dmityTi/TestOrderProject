using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus? Status { get; set; }
        public int? InvoiceNumber { get; set; }
        public long BillingAddressId { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public BillingAddress BillingAddress { get; set; }
    }
}