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
        public decimal TotalMoney => Items.Sum(i => i.Total);

        public void AddOrUpdate(CartItem item)
        {
            if (item == null || item.VariantId <= 0) return;

            item.Quantity = Math.Max(item.Quantity, 1);

            if (ItemsDict.ContainsKey(item.VariantId))
                ItemsDict[item.VariantId].Quantity += item.Quantity;
            else
                ItemsDict[item.VariantId] = item;
        }

        // INCREASE
        public void Increase(int variantId, int step = 1)
        {
            if (!IsValidVariant(variantId)) return;

            ItemsDict[variantId].Quantity += Math.Max(step, 1);
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
        }

        // SET QUANTITY (INPUT TAY)
        public void SetQuantity(int variantId, int quantity)
        {
            if (!IsValidVariant(variantId)) return;

            //ItemsDict[variantId].Quantity = quantity <= 0 ? 1 : quantity;
            if (quantity <= 0)
                ItemsDict.Remove(variantId);
            else
                ItemsDict[variantId].Quantity = quantity;
        }

        // REMOVE
        public void Remove(int variantId)
        {
            if (variantId <= 0) return;
            ItemsDict.Remove(variantId);
        }

        // CLEAR
        public void Clear()
        {
            ItemsDict.Clear();
        }

        // PRIVATE HELPERS
        private bool IsValidVariant(int variantId)
            => variantId > 0 && ItemsDict.ContainsKey(variantId);

        // Update thuộc tính CartItem (ví dụ chỉ đổi màu)
        //public void ChangeColor(int variantId, string newColor)
        //{
        //    if (!IsValidVariant(variantId)) return;
        //    ItemsDict[variantId].Color = newColor;
        //}
    }
}
