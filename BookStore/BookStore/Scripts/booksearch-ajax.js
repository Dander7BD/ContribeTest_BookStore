$(function ()
{
    function onChange()
    {
        $.ajax({
            url: '/bookservice/',
            data: { elementID: $(this).parent().attr('id'), search: $(this).val() },
            type: 'get',
            dataType: 'json',
            success: function (feed)
            {
                var element = $('#' + feed.elementID);
                var output = element.children('.response');
                var template = element.children('.template');

                output.empty();
                $(feed.books).each(function ()
                {
                    console.log('Title: ' + this.Title);
                    
                    var item = $(template.html());
                    item.find('.title').html(this.Title);
                    item.find('.author').html(this.Author);
                    item.find('.price').html(this.Price);                    
                    if (!this.InStock) item.addClass('missing-in-stock');
                    output.append(item);

                    //var staff = staffList.find('.staff' + data.find('id').text());
                    //staff.find('.name').text(data.find('name').text());
                });                

                console.log(feed.elementID + ': Book search feed is succesfully received.');

            },
            error: function () { console.log('Book search feed is unavailable.'); },        
        });
    }

    $('.book-search').on('input', '.filter-input', onChange);
    // preloading to ensure that there is content presented before the user starts to add filter
    $('.book-search > .filter-input').trigger('input');
});