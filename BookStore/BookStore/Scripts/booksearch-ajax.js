$(function ()
{
    function onChange()
    {
        $.ajax({
            url: '/bookservice/',
            data:
            {
                action: 'search',
                callerID: $(this).parent().attr('id'),
                search: $(this).val()
            },
            type: 'get',
            dataType: 'json',
            success: function (feed)
            {
                var caller = $('#' + feed.callerID);
                var output = caller.children('.response');
                var template = caller.children('.template');

                output.empty();
                $(feed.books).each(function ()
                {
                    var item = $(template.html());
                    item.find('.title').html(this.Title);
                    item.find('.author').html(this.Author);
                    item.find('.price').html(parseFloat(this.Price).toFixed(2).toString());                    
                    item.find('.id').html(this.ID);
                    if (!this.InStock) item.addClass('stock-shortage');
                    output.append(item);
                });                
            },
            error: function () { console.log('Book search feed is unavailable.'); },        
        });
    }

    $('.book-search').on('input', '.filter-input', onChange);
    // preloading to ensure that there is content presented before the user starts to add filter
    $('.book-search > .filter-input').trigger('input');
});