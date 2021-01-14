
$(document).ready(function () {
    thirdPartAnimations();    
    $(window).on('scroll', function () {
        thirdPartAnimations();       
    });


    function thirdPartAnimations() {
 
        var elementPositionOne = $(".third-part-container-part-one").offset().top;

        var brouserPosition = parseInt($(document).scrollTop() + $(window).height());

        if (elementPositionOne <= brouserPosition + 100) {

            if (!$(".third-part-container-part-one").hasClass('in-left')) {
                $(".third-part-container-part-one").addClass('in-left');
            }
        }


        var elementPositionTwo = $(".third-part-container-part-two").offset().top;

        var brouserPosition = parseInt($(document).scrollTop() + $(window).height());

        if (elementPositionTwo <= brouserPosition + 100) {

            if (!$(".third-part-container-part-two").hasClass('in-right')) {
                $(".third-part-container-part-two").addClass('in-right');
            }
        }


        var elementPositionThree = $(".third-part-container-part-three").offset().top;

        var brouserPosition = parseInt($(document).scrollTop() + $(window).height());

        if (elementPositionThree <= brouserPosition + 100) {

            if (!$(".third-part-container-part-three").hasClass('in-left')) {
                $(".third-part-container-part-three").addClass('in-left');
            }
        }
    }
});


    (function($) {

        $.fn.scrolling = function (speed) {
            var $item = $(this);
   
            if ($item.length == 0) return;
                       
            var $parentBlock = $(this).closest('.fullwidth-block');
            var positionTop = $item.data('positionTop');
            var newPositionTop = positionTop + ($(window).scrollTop()) * speed;
            $item.css({ top: newPositionTop });
            
        }
        

	$.fn.arrowRotate = function(speed) {
		var $item = $(this);

        if($item.length == 0 ) return ;

            var $parentBlock = $(this).closest('.fullwidth-block');
            var angel = ($(window).scrollTop() - $parentBlock.offset().top) * speed * 360 / $parentBlock.outerHeight();
		    $item.css({
            transform: 'rotate('+angel+'deg)',
            MozTransform: 'rotate('+angel+'deg)',
            WebkitTransform: 'rotate('+angel+'deg)',
            msTransform: 'rotate('+angel+'deg)'
        })
    }

        $(function () {

            var topContainer = $("#home-part-two").position().top;
            console.log(topContainer);

            //$(".images-of-part-two").css({ top: topContainer });
            //$(".images-of-part-two").data('positionTop', topContainer);

        if ($('.gear-1').length && $('.gear-2').length && $('.gear-3').length && $('.gear-4').length && $('.gear-5').length && $('.gear-6').length && $('.gear-7').length) {
        

            $('.gear-2').data('positionTop', $('.gear-2').position().top);
            $('.gear-3').data('positionTop', $('.gear-3').position().top);
            $('.gear-1').data('positionTop', $('.gear-1').position().top);
            $('.gear-5').data('positionTop', $('.gear-5').position().top);
            $('.gear-4').data('positionTop', $('.gear-4').position().top);
            $('.gear-6').data('positionTop', $('.gear-6').position().top);
            $('.gear-7').data('positionTop', $('.gear-7').position().top);

            $(window).resize(function () {
                ChangeRotateWhenScrolling();
            });

            $(document).ready(function () {
                ChangeRotateWhenScrolling();
            });

                $(window).scroll(function () {
                    ChangeRotateWhenScrolling();
                });
            }                                   


            function ChangeRotateWhenScrolling() {
                if ($(window).width() > 764) {
                    $('.gear-4').scrolling(0.35);
                    $('.gear-5').scrolling(0.40);
                    $('.gear-1').scrolling(0.25);
                    $('.gear-3').scrolling(0.30);
                    $('.gear-2').scrolling(0.5);
                    $('.gear-6').scrolling(0.55);
                    $('.gear-7').scrolling(0.3);


                    $('.gear-4').arrowRotate(-0.8);
                    $('.gear-5').arrowRotate(-0.6);
                    $('.gear-1').arrowRotate(0.4);
                    $('.gear-3').arrowRotate(0.6);
                    $('.gear-2').arrowRotate(-1);
                    $('.gear-6').arrowRotate(0.3);
                    $('.gear-7').arrowRotate(0.4);
                }
            };
    });

})(jQuery);

