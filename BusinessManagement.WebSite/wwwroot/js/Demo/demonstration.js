'use-strict';


$('.demonstration').on('click', function (event) {
  makeWhiteDemonstration($('.demonstration'));
  makeRedDemonstration($(this));
});

$('body').on('click', function () {

  if ($('.demonstration:hover').length <= 0) {
    makeWhiteDemonstration($('.demonstration'));
  }
});
$('.demonstration').on('mouseover', function (event) {
  
  rotate($(this));
});

$('.demonstration').on('mouseout', function (event) {
  
  stoprotate($(this));
});


function makeRedDemonstration(el) {
  el.css("background-color", "red");
}

function makeWhiteDemonstration(el) {
 el.css("background-color", "#fff");
}

function stoprotate(el) {
  el.css({

    'animation': '0s spinnow infinite linear',
    '-webkit-animation': '0s spinnow infinite linear'


  });
}

function rotate(el) {




  el.css({

    'animation': '5s spinnow infinite linear',
  '-webkit-animation': '5s spinnow infinite linear'


    });
  


}