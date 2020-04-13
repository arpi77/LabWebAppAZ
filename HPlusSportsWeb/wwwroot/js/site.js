// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// This is sample code only and should not be used in production.
// It's sole purpose is to mimic a shopping cart for demonstration purposes.


//adds an item to the cart with a default quantity and 
//a size if found.
function addToCart(id, name) {
    var items = getCartItems();

    if (!items) {
        items = [];
    }
    var prodSize = $("input[name='size']").val();
    items.push({
        "id": id,
        "name": name,
        "quantity": 1,
        "size": prodSize
    });
    localStorage.setItem("cartItems", JSON.stringify(items));
}

//gets all the items in the cart
function getCartItems() {
    var items = localStorage.getItem("cartItems");
    return JSON.parse(items);

}

//clears the cart after an order is submitted
function clearCart() {
    localStorage.removeItem("cartItems");
}
