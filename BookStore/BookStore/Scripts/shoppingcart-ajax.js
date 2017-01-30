$(function ()
{
    function updateCartItem(itemID, quantity, callerID)
    {
        $.ajax({
            url: '/bookservice/',
            data:
            {
                action: 'update-cart-item',
                callerID: callerID,
                bookID: itemID,
                quantity: quantity.toString()
            },
            type: 'get',
            dataType: 'json',
            success: function (feed)
            {
                if (feed.books.length > 0)
                {
                    var caller = $('#' + feed.callerID);
                    var basket = caller.children('.basket');
                    var template = caller.children('.template');

                    var book = feed.books[0];
                    var item = basket.find('.id:contains("' + feed.books[0].ID + '")').parent('.cart-item');

                    if (book.RequestedQuantity > 0)
                    {
                        if (item.length == 0)
                        { // if item entry is not in basket -> append new
                            item = $(template.html());

                            // init values
                            item.find('.title').html(book.Title);
                            item.find('.author').html(book.Author);
                            item.find('.id').html(book.ID);

                            basket.append(item);
                        }

                        // update the details
                        item.find('.quantity').val(book.RequestedQuantity);
                        item.find('.price-sum').html((parseFloat(book.RequestedQuantity) * parseFloat(book.Price)).toFixed(2).toString());

                        if (feed.InStock)
                            item.removeClass('stock-shortage');
                        else
                            item.addClass('stock-shortage');
                    }
                    else if (item.length > 0)
                    { // remove item from cart
                        item.remove();
                    }
                }
            },
            error: function () { console.log('Book shopping cart service is unavailable.'); },
        });
    }

    function addBook(event)
    {
        var itemID = $(this).find('.id').html();

        // Fetch current (if exists) quantity incremented by one
        var quantity = 1;
        var item = $('#' + event.data).find('.id:contains("' + itemID + '")');
        if (item.length > 0)
            quantity += parseInt(item.parent('.book-item').find('.quantity').val(), 10);

        updateCartItem(itemID, quantity.toString(), event.data);
    }

    function updateBook(event)
    {
        var item = $(this).parent('.cart-item');
        updateCartItem(
            item.find('.id').html(),
            item.find('.quantity').val(),
            event.data);
    }

    $('.shopping-cart').each(function ()
    {
        var id = $(this).attr('id');
        $(this).on('change', '.quantity', id, updateBook);
        $(this).parent("form").on('click', '.book-item', id, addBook);
    });
});