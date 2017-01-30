$(function ()
{
    function updateSum(cartID)
    {
        var cart = $('#' + cartID);
        if (cart.length > 0)
        {
            var sum = 0;
            cart.find('.basket').find('.cart-item').each(function ()
            {
                sum += parseFloat($(this).find('.price-sum').html());
            });
            cart.find('.sum').html(sum.toFixed(2).toString());
        }
    }

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

                        if (book.InStock)
                            item.removeClass('stock-shortage');
                        else
                            item.addClass('stock-shortage');
                    }
                    else if (item.length > 0)
                    { // remove item from cart without actually removing it as Reference
                        item.addClass('remove');
                        item.find('.price-sum').html((0).toFixed(2).toString());
                    }
                }
                updateSum(feed.callerID);
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
        
        // load persistant data
        $.ajax({
            url: '/bookservice/',
            data:
            {
                action: 'get-shopping-cart-items',
                callerID: id
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

                    basket.empty();
                    $(feed.books).each(function ()
                    {
                        var item = $(template.html());

                        // init values
                        item.find('.title').html(this.Title);
                        item.find('.author').html(this.Author);
                        item.find('.id').html(this.ID);
                        item.find('.quantity').val(this.RequestedQuantity);
                        item.find('.price-sum').html((parseFloat(this.RequestedQuantity) * parseFloat(this.Price)).toFixed(2).toString());

                        if (!this.InStock)
                            item.addClass('stock-shortage');

                        basket.append(item);
                    });
                }
                updateSum(feed.callerID);
            },
            error: function () { console.log('Book shopping cart service is unavailable.'); },
        });
    });
});