using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Carts
    {
        [JsonInclude]
        public Dictionary<int, CartItem> ItemsDict { get; private set; } = new();

        [JsonIgnore]
        public IReadOnlyCollection<CartItem> Items => ItemsDict.Values;

        public int TotalQuantity => Items.Sum(i => i.Quantity);

        // Tổng tiền trước giảm 
        public decimal TotalMoney => Items.Sum(i => i.Price * i.Quantity);

        // Tổng giảm giá sản phẩm 
        public decimal TotalDiscount => Items.Sum(i => (i.Price - i.FinalPrice) * i.Quantity);

        // Voucher giảm trực tiếp 
        public decimal TotalVoucherDiscount { get; private set; } = 0;

        // Tiền cuối phải trả
        public decimal TotalPayable => Math.Max(0, TotalMoney - TotalDiscount - TotalVoucherDiscount);

        // Hàm áp dụng voucher (demo) 
        public decimal VoucherPercent { get; private set; } = 0;
        private void ResetVoucher()
        {
            TotalVoucherDiscount = 0;
            VoucherPercent = 0;
        }

        public void ApplyVoucherPercent(decimal percent)
        {
            VoucherPercent = percent;

            var afterProductDiscount = TotalMoney - TotalDiscount;
            TotalVoucherDiscount = afterProductDiscount * percent / 100m;
        }

        public void AddOrUpdate(CartItem item)
        {
            if (item == null || item.VariantId <= 0) return;

            item.Quantity = Math.Max(item.Quantity, 1);

            if (ItemsDict.ContainsKey(item.VariantId))
                ItemsDict[item.VariantId].Quantity += item.Quantity;
            else
                ItemsDict[item.VariantId] = item;
            // reset voucher
            ResetVoucher();
        }

        // INCREASE 
        public void Increase(int variantId, int step = 1)
        {
            if (!IsValidVariant(variantId)) return;

            ItemsDict[variantId].Quantity += Math.Max(step, 1);
            // reset voucher
            ResetVoucher();
        }

        // DECREASE 
        public void Decrease(int variantId, int step = 1)
        {
            if (!IsValidVariant(variantId)) return;

            var item = ItemsDict[variantId];
            item.Quantity -= Math.Max(step, 1);

            if (item.Quantity <= 0)
            {
                ItemsDict.Remove(variantId); // xóa luôn khỏi cart 
            }
            // reset voucher
            ResetVoucher();
        }

        // SET QUANTITY (INPUT TAY) 
        public void SetQuantity(int variantId, int quantity)
        {
            if (!IsValidVariant(variantId)) return;

            if (quantity <= 0)
                ItemsDict.Remove(variantId);
            else
                ItemsDict[variantId].Quantity = quantity;

            // reset voucher
            ResetVoucher();
        }

        // REMOVE 
        public void Remove(int variantId)
        {
            if (variantId <= 0) return;

            ItemsDict.Remove(variantId);
            // reset voucher
            ResetVoucher();
        }

        // CLEAR 
        public void Clear()
        {
            ItemsDict.Clear();
            ResetVoucher();
        }
        //========
        // PRIVATE HELPERS 
        private bool IsValidVariant(int variantId)
            => variantId > 0 && ItemsDict.ContainsKey(variantId);

        // Update thuộc tính CartItem (ví dụ chỉ đổi màu) 
        public void ChangeColor(int variantId, string newColor)
        {
            if (!IsValidVariant(variantId)) return;

            var item = ItemsDict[variantId];
            var variant = item.AvailableColors.FirstOrDefault(c => c.Color == newColor);
            if (variant != default)
            {
                item.Color = variant.Color;
                item.ImageUrl = variant.ImageUrl;

            }
        }
    }

}