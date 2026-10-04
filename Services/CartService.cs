using FakeWebStore.Models;

namespace FakeWebStore.Services
{
    public class CartService
    {
        private readonly List<CartItem> _cartItems = new();

        public event Action? OnCartChanged;

        public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

        public int CartCount =>
            _cartItems.Sum(item => item.Quantity);

        public decimal CartTotal =>
            _cartItems.Sum(item => item.TotalPrice);


        // Add product to cart
        public void AddToCart(Product product)
        {
            if (product == null)
                return;

            var existingItem = _cartItems
                .FirstOrDefault(item => item.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _cartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }

            NotifyCartChanged();
        }


        // Remove product completely
        public void RemoveFromCart(int productId)
        {
            var item = _cartItems
                .FirstOrDefault(item => item.Product.Id == productId);

            if (item != null)
            {
                _cartItems.Remove(item);
                NotifyCartChanged();
            }
        }


        // Increase quantity
        public void IncreaseQuantity(int productId)
        {
            var item = _cartItems
                .FirstOrDefault(item => item.Product.Id == productId);

            if (item != null)
            {
                item.Quantity++;
                NotifyCartChanged();
            }
        }


        // Decrease quantity
        public void DecreaseQuantity(int productId)
        {
            var item = _cartItems
                .FirstOrDefault(item => item.Product.Id == productId);

            if (item == null)
                return;

            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                _cartItems.Remove(item);
            }

            NotifyCartChanged();
        }


        // Clear entire cart
        public void ClearCart()
        {
            _cartItems.Clear();
            NotifyCartChanged();
        }


        // Get quantity for a specific product
        public int GetItemQuantity(int productId)
        {
            return _cartItems
                .FirstOrDefault(item => item.Product.Id == productId)
                ?.Quantity ?? 0;
        }


        // Notify components that cart changed
        private void NotifyCartChanged()
        {
            OnCartChanged?.Invoke();
        }
    }
}