import { createContext, useContext, useState } from "react";

// Create a context for the cart
export const CartContext = createContext();

// Provider component to manage cart state and provide it to the component tree
export function CartProvider({ children }) {
    // State to hold the items in the cart
    const [cartItems, setCartItems] = useState([]);

    // Function to add an item to the cart
    const addToCart = (item) => {
        setCartItems((prev) => [...prev, item]);
    };

    // Function to remove an item from the cart by its id
    const removeFromCart = (id) => {
        setCartItems((prev) => prev.filter((item) => item.id !== id));
    };

    // Function to clear all items from the cart
    const clearCart = () => {
        setCartItems([]);
    };

    // Provide the cart state and actions to the component tree
    return (
        <CartContext.Provider value={{ cartItems, addToCart, removeFromCart, clearCart }}>
            {children}
        </CartContext.Provider>
    );
}

// Custom hook to use the CartContext
export function useCart() {
    return useContext(CartContext);
}