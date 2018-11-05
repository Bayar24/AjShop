/* JS Document */

jQuery(document).ready(function($)
{
	"use strict";

	/* 

	1. Vars and Inits

	*/
	initThumbnail();
	initQuantity();

	/*

	4. Init Thumbnail

	*/

	function initThumbnail()
	{
		if($('.single_product_thumbnails ul li').length)
		{
			var thumbs = $('.single_product_thumbnails ul li');
			var singleImage = $('.single_product_image_background');

			thumbs.each(function()
			{
				var item = $(this);
				item.on('click', function()
				{
					thumbs.removeClass('active');
					item.addClass('active');
					var img = item.find('img').data('image');
					singleImage.css('background-image', 'url(' + img + ')');
				});
			});
		}	
	}

	/* 

	5. Init Quantity

	*/

	function initQuantity()
	{
		if($('.plus').length && $('.minus').length)
		{
			var plus = $('.plus');
			var minus = $('.minus');
			var value = $('#quantity_value');
            var max_value = $('#max_value');
            var add_cart = $('#add_cart_button');

			plus.on('click', function()
			{
				var x = parseInt(value.text());
				if( x < parseInt(max_value.text()) ){
                    value.text(x + 1);
                    add_cart.attr('data-quantity', x + 1);
                }
			});

			minus.on('click', function()
			{
				var x = parseInt(value.text());
				if(x > 1)
				{
					value.text(x - 1);
                    add_cart.attr('data-quantity', x - 1);
				}
			});
		}
	}
});