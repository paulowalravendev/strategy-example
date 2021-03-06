using StrategyPatternFirstLock.Business.Enums;
using StrategyPatternFirstLock.Business.Strategies.SalesTax;

namespace StrategyPatternFirstLock.Business.Models;

    public class Order
    {
        public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();

        public IList<Payment> SelectedPayments { get; } = new List<Payment>();

        public IList<Payment> FinalizedPayments { get; } = new List<Payment>();

        public decimal AmountDue => TotalPrice - FinalizedPayments.Sum(payment => payment.Amount);

        public decimal TotalPrice => LineItems.Sum(item => item.Key.Price * item.Value);

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.WaitingForPayment;

        public ShippingDetails ShippingDetails { get; set; }

        public ISalesTaxStrategy? SalesTaxStrategy { get; set; }

        public decimal GetTax()
        {
            return SalesTaxStrategy?.GetTaxFor(this) ?? 0m;
        }
    }